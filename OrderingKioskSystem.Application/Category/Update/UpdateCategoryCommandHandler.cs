using MediatR;
using OrderingKioskSystem.Application.Category.Create;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<string> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryExist = await _categoryRepository.FindAsync(x => x.ID == request.ID, cancellationToken);

            if (categoryExist is null || categoryExist.NgayXoa.HasValue)
            {
                return "Category is not found or deleted";
            }

            categoryExist.Name = request.Name ?? categoryExist.Name;
            categoryExist.Url = request.Url ?? categoryExist.Url;

            categoryExist.NgayCapNhat = DateTime.Now;
            _categoryRepository.Update(categoryExist);

            return await _categoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Fail!";
        }
    }
}
