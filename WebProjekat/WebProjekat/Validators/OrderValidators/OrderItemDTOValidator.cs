using FluentValidation;
using WebProjekat.DTO.OrderDTO;

namespace WebProjekat.Validators.OrderValidators
{
    public class OrderItemDTOValidator : AbstractValidator<OrderItemDTO>
    {
        public OrderItemDTOValidator()
        {
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.ProductID).NotEmpty();
        }
    }
}
