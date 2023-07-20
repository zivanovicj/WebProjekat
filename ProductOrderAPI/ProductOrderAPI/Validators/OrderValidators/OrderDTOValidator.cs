using FluentValidation;
using ProductOrderAPI.DTO.OrderDTO;

namespace ProductOrderAPI.Validators.OrderValidators
{
    public class OrderDTOValidator : AbstractValidator<OrderDTO>
    {
        public OrderDTOValidator()
        {
            RuleFor(x => x.DeliveryAddress).NotEmpty();
            RuleFor(x => x.Comment).NotEmpty();
            RuleFor(x => x.OrderedProducts).NotEmpty();
            RuleForEach(x => x.OrderedProducts).SetValidator(new OrderItemDTOValidator());
        }
    }
}
