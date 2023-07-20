using FluentValidation;
using ProductOrderAPI.DTO.OrderDTO;

namespace ProductOrderAPI.Validators.OrderValidators
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
