using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Order.Command.CheckoutOrder
{
    public class CheckoutOrderValidator :AbstractValidator<CheckoutOrderCommand>
    { 
        public CheckoutOrderValidator() { 
            
            RuleFor(x=>x.dto.Items).
                NotEmpty().
                WithMessage("Cannot checkout an empty order.");
        
        }

    }
}
