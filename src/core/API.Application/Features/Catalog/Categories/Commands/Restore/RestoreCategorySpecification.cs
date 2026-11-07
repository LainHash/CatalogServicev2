using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Categories;

namespace API.Application.Features.Catalog.Categories.Commands.Restore
{
    public class RestoreCategorySpecification
        : BaseSpecification<Category>
    {
        public RestoreCategorySpecification(RestoreCategoryCommand command)
        {
            Criteria = category => category.PublicId == command.Id;
        }
    }
}
