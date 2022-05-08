

using System;

namespace CoreSB.Domain.Currency
{
    public interface ICurrencyRateAdd
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

    public interface ICurrencyAPI
    {
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public bool IsMain { get; set; }
    }

    public interface ICurrencyUpdateAPI : ICurrencyAPI
    {
    }


    public interface ICommandType { }
    public interface IPayload { }
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
    public class CreateCommand : ICommandType { }
    public class UpdateCommand : ICommandType { }
    public class DeleteCommand : ICommandType { }


}

