using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Customer.Command.RegisterCustomer
{
    public  class RegisterCustomerValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerValidator() {

            RuleFor(x => x.Dto.FullName).
                NotEmpty().
                WithMessage("Full name is required.");

            RuleFor(x => x.Dto.Email).
               NotEmpty().
               WithMessage("Email is required.").
               EmailAddress().
               WithMessage("A valid email address is required.");
              

        }



    }
}
