using FluentValidation;

namespace OrderingKioskSystem.Application.Common.Validation
{
    public interface IValidatorProvider
    {
        IValidator<T> GetValidator<T>();
    }
}
