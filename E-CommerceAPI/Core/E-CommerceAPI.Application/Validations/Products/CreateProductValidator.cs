using E_CommerceAPI.Application.ViewModels.Products;
using FluentValidation;


namespace E_CommerceAPI.Application.Validations.Products
{
    public class CreateProductValidator :  AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
                RuleFor(p => p.Name).NotEmpty()
                .NotNull()
                .WithMessage("Please do not leave the product name blank.")
                .MaximumLength(150).MinimumLength(2)
                .WithMessage("Please enter the product name between 2 and 150 characters");


            RuleFor(p => p.Stock).NotEmpty()
                .NotNull().WithMessage("Please do not leave the product's stock information blank.")
                .Must(p  => p >= 0).WithMessage("Stock information is not negative");

            RuleFor(p => p.Price).NotEmpty()
               .NotNull().WithMessage("Please do not leave the product's price information blank.")
               .Must(p => p >= 0).WithMessage("Price information is not negative");

        }
    }
}
