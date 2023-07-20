using FluentValidation;
using ProductOrderAPI.DTO.ProductDTO;

namespace ProductOrderAPI.Validators.ProductValidators
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.ProductName).NotNull();
            RuleFor(x => x.Price).NotNull().GreaterThan(0);
            RuleFor(x => x.Amount).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Description).NotNull();
        }
    }
}
