using FluentValidation.Models;

namespace FluentValidation.FluentValidatior;

public class AddressValidator : AbstractValidator<Address>
{
    public string NotEmptyMessage { get; set; } = "{PropertyName} alanı boş olamaz.";
    public AddressValidator()
    {
        RuleFor(a => a.Content).NotEmpty().WithMessage(NotEmptyMessage);
        RuleFor(a => a.PostCode).NotEmpty().WithMessage(NotEmptyMessage);
        RuleFor(a => a.Province).NotEmpty().WithMessage(NotEmptyMessage);
        RuleFor(a => a.PostCode).MaximumLength(5).WithMessage("{PropertyName} alanı en fazla {MaxLength} karakter olmalıdır.");

    }
}