using MediatR;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.CreateUser
{
    public class CreateNewUserCommandHandler : IRequestHandler<CreateNewUserCommand, string>
    {
        private readonly IUserRepository _repository;
        public CreateNewUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
            
        }

        public async Task<string> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _repository.FindAsync(x => x.Email == request.Email == null);
            if (isExist is null)
            {
                throw new DuplicationException("Email đã tồn tại");
            }

            _repository.Add(isExist);
            _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return isExist.Email;

        }
    }
}
