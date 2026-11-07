using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Categories;

namespace API.Application.Features.Catalog.Categories.Commands.Update
{
    public class UpdateCategorySpecification
        : BaseSpecification<Category>
    {
        public UpdateCategorySpecification(UpdateCategoryCommand command)
        {
            Criteria = c => c.PublicId == command.Id;
        }
    }
}
