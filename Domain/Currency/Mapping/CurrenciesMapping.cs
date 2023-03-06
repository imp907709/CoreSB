using AutoMapper;
using CoreSB.Domain.Currency.Models;
using CoreSB.Domain.Currency.Mongo;
using CoreSB.Universal;

namespace CoreSB.Domain.Currency.Mapping
{
    public class CurrenciesMapping : Profile
    {
        public CurrenciesMapping()
        {
            CreateMap<CurrencyRatesDAL, CrossCurrenciesAPI>()
                .ForMember(d => d.From, m => m.MapFrom(src => src.CurrencyFrom.Name))
                .ForMember(d => d.To, m => m.MapFrom(src => src.CurrencyTo.Name))
                .ReverseMap();

            CreateMap<ICurrencyBL, CurrencyDAL>().ReverseMap();
            // .ForMember(d => d.IsoCode, m => m.MapFrom(src => src.IsoCode))
            // .ForMember(d => d.Name, m => m.MapFrom(src => src.Name))
            // .ReverseMap().ForAllOtherMembers(o => o.Ignore());

            CreateMap<CurrencyUpdateBL, CurrencyDAL>()
                .ForMember(d => d.IsMain, m => m.MapFrom(src => src.IsMain))
                .ForMember(d => d.Name, m => m.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<ICurrencyRateAddAPI, CurrencyRatesDAL>()
                .ForMember(d => d.Date, m => m.MapFrom(src => src.Date))
                .ForMember(d => d.Rate, m => m.MapFrom(src => src.Value))
                .ReverseMap();

            CreateMap<CurrencyRatesDAL, ICurrencyRateAddAPI>()
                .ForMember(d => d.Date, m => m.MapFrom(src => src.Date))
                .ForMember(d => d.Value, m => m.MapFrom(src => src.Rate))
                .ForMember(d => d.FromCurrency, m => m.MapFrom(src => src.CurrencyFrom.IsoCode))
                .ForMember(d => d.ToCurrency, m => m.MapFrom(src => src.CurrencyTo.IsoCode))
                .ReverseMap();

            CreateMap<EntityIntIdDAL, MongoDAL>()
                .ForMember(d=>d.Id, s=>s.Ignore());

            CreateMap<CurrencyDAL, CurrencyMongoDAL>()
                .IncludeBase<EntityIntIdDAL, MongoDAL>();

            CreateMap<CurrencyRatesDAL, CurrencyRatesMongoDAL>()
                .IncludeBase<EntityIntIdDAL, MongoDAL>()
                .ForMember(d => d.CurrencyFrom,
                    opt =>
                    {
                        opt.PreCondition(s => s.CurrencyFrom is not null);
                        opt.MapFrom(s=>s.CurrencyFrom);
                    }
                ).ForMember(d => d.CurrencyTo,
                    opt =>
                    {
                        opt.PreCondition(s => s.CurrencyTo is not null);
                        opt.MapFrom(s=>s.CurrencyTo);
                    }
                );

        }
    }
}
