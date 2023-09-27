using Ecom.ClientEntity.Request.Product;
using FluentValidation;

namespace Ecom.API.Validator
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(product => product.ProductName)
                                                .NotEmpty()
                                                .WithMessage("name should not empty")
                                                .NotNull()
                                                .WithMessage("name should not null")
                                                .MinimumLength(3)
                                                .WithMessage("minimum length should be 3")
                                                .MaximumLength(100)
                                                .WithMessage("maximum length should be 100");
            RuleFor(product => product.Brand)
                                                .NotEmpty()
                                                .WithMessage("name should not empty")
                                                .NotNull()
                                                .WithMessage("name should not null")
                                                .MinimumLength(3)
                                                .WithMessage("minimum length should be 3")
                                                .MaximumLength(100)
                                                .WithMessage("maximum length should be 100");
            RuleFor(product => product.Model)
                                                .NotEmpty()
                                                .WithMessage("name should not empty")
                                                .NotNull()
                                                .WithMessage("name should not null")
                                                .MinimumLength(3)
                                                .WithMessage("minimum length should be 3")
                                                .MaximumLength(100)
                                                .WithMessage("maximum length should be 100");
            RuleFor(product => product.Price)
                                                 .NotEmpty()
                                                 .WithMessage("name should not empty")
                                                 .NotNull()
                                                 .WithMessage("name should not null")
                                                 .GreaterThan(0)
                                                 .WithMessage("negative price not allow");

        }
    }
}
