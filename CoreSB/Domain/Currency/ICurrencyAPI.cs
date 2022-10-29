using System;

namespace CoreSB.Domain.Currency
{
    public interface ICurrencyRateAddAPI
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }

    public interface ICrossCurrenciesAPI
    {
        string From { get; set; }
        string To { get; set; }
        decimal Rate { get; set; }
    }

    public interface IGetCurrencyCommand
    {
        string FromCurrency { get; set; }
        string ToCurrency { get; set; }
        string ThroughCurrency { get; set; }
        DateTime Date { get; set; }
    }

    public interface ICurrencyBL
    {
        public string Name { get; set; }
        public string IsoName { get; set; }
        public int IsoCode { get; set; }
        public bool IsMain { get; set; }
    }

    public interface ICurrencyUpdateBL : ICurrencyBL
    {
    }


    public interface ICommandType
    {
    }

    public interface IPayload
    {
    }

    public interface ICommand
    {
        public ICommandType commandType { get; set; }
        public IPayload payload { get; set; }
    }

    public class BasicCommand : ICommand
    {
        public ICommandType commandType { get; set; }
        public IPayload payload { get; set; }
    }

    public class CreateCommand : ICommandType
    {
    }

    public class UpdateCommand : ICommandType
    {
    }

    public class DeleteCommand : ICommandType
    {
    }
}
