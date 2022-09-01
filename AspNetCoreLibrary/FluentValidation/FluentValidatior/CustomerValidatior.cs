using FluentValidation.Models;

namespace FluentValidation.FluentValidatior;

public class CustomerValidatior : AbstractValidator<Customer>
{
    public string NotEmptyMessage { get; set; } = "{PropertyName} alanı boş olamaz.";
    public CustomerValidatior()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage(NotEmptyMessage);
        RuleFor(c => c.Email).NotEmpty().WithMessage(NotEmptyMessage);
        RuleFor(c => c.Email).EmailAddress().WithMessage("Email alanı doğru formatta olmalıdır.");
        RuleFor(c => c.Age).NotEmpty().WithMessage(NotEmptyMessage);
        RuleFor(c => c.Age).InclusiveBetween(18, 60).WithMessage("Age alanı 18 ile 60 arasında olmalıdır.");

        RuleFor(c => c.BirthDay).NotEmpty().WithMessage(NotEmptyMessage);
        RuleFor(c => c.BirthDay).Must(b =>
        {
            return DateTime.Now.AddYears(-18) >= b;
        }).WithMessage("Yaşınız 18 yaşından büyük olmalıdır.");

        RuleForEach(c => c.Addresses).SetValidator(new AddressValidator());
    }
}