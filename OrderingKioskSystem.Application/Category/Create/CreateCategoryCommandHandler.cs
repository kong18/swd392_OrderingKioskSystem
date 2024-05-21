using MediatR;
using OrderingKioskSystem.Application.Product.Create;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            bool categoryExist = await _categoryRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (categoryExist)
            {
                return "Category's Name can't be dupplicated";
            }

            var category = new CategoryEntity
            {
                Name = request.Name,
                Url = request.Url,
            };

            _categoryRepository.Add(category);

            return await _categoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Success!" : "Create Fail!";
        }
    }
}
