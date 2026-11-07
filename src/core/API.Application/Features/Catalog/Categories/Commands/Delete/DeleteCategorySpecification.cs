using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Categories;

namespace API.Application.Features.Catalog.Categories.Commands.Delete
{
    public class DeleteCategorySpecification
        : BaseSpecification<Category>
    {
        public DeleteCategorySpecification(DeleteCategoryCommand command)
        {
            Criteria = category => category.PublicId == command.Id;
        }
    }
}
