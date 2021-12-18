using CoreSB.Universal;
using FluentValidation;

namespace CoreSB.Domain.Currency.Validation
{
    public class CurrenciesValidation
        : AbstractValidator<ICurrencyAPI>
    {
        public CurrenciesValidation()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.IsoCode).NotNull().NotEmpty();
            RuleFor(c => c.IsMain).NotNull().NotEmpty();
        }
    }

    public class CurrencyUpdatevalidation
        : AbstractValidator<ICurrencyUpdateAPI>
    {
        public CurrencyUpdatevalidation()
        {
            RuleFor(c => c.IsoCode).NotNull().NotEmpty();
        }
    }

    public class AddCurrencyRateValidation
       : AbstractValidator<ICurrencyRateAdd>
    {
        public AddCurrencyRateValidation()
        {
            RuleFor(c => c.FromCurrency).NotNull().NotEmpty();
            RuleFor(c => c.ToCurrency).NotNull().NotEmpty();
            RuleFor(c => c.Date).NotNull().NotEmpty();
            RuleFor(c => c.Value).NotNull().NotEmpty();
        }
    }

    public class CurrencyRateDALValidation
        : AbstractValidator<CurrencyRatesDAL>
    {
        public CurrencyRateDALValidation()
        {
            RuleFor(c => c.CurrencyFromId).NotNull().NotEmpty();
            RuleFor(c => c.CurrencyToId).NotNull().NotEmpty();
            RuleFor(c => c.Date).NotNull().NotEmpty();
            RuleFor(c => c.Rate).NotNull().NotEmpty().NotEqual(0);
        }
    }

    public class ValidatorCustom : IValidatorCustom
    {
        CurrenciesValidation cv = new CurrenciesValidation();
        CurrencyUpdatevalidation cvUpdate = new CurrencyUpdatevalidation();
        AddCurrencyRateValidation rateAdd = new AddCurrencyRateValidation();

        CurrencyRateDALValidation rateAddDAL = new CurrencyRateDALValidation();

        public bool isValid<T>(T item)
        {
            bool isValid = false;
            switch (item)
            {
                case CurrencyUpdateAPI b:
                    isValid = cvUpdate.Validate(b).IsValid;
                    break;
                case CurrencyAPI a:
                    isValid = cv.Validate(a).IsValid;
                    break;
                case CurrencyRateAdd c:
                    isValid = rateAdd.Validate(c).IsValid;
                    break;
                case CurrencyRatesDAL c:
                    isValid = rateAddDAL.Validate(c).IsValid;
                    break;
            }

            return isValid;
        }
    }
}
