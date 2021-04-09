﻿using SystemyWP.API.Forms.LegalApp;
using FluentValidation;

namespace SystemyWP.API.Forms.Validation.LegalApp
{
    public class UpdateClientFormValidation: AbstractValidator<UpdateClientForm>
    {
        public UpdateClientFormValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}