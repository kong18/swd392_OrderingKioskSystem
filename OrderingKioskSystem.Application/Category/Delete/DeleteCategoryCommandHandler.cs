using MediatR;
using OrderingKioskSystem.Application.Category.Create;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<string> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryExist = await _categoryRepository.FindAsync(x => x.ID == request.ID, cancellationToken);

            if (categoryExist is null || categoryExist.NgayXoa.HasValue)
            {
                return "Category is not found or deleted";
            }

            categoryExist.NgayXoa = DateTime.Now;
            _categoryRepository.Update(categoryExist);

            return await _categoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Delete Success!" : "Delete Fail!";
        }
    }
}
