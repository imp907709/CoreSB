using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreSB.Infrastructure.IO.Settings;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace CoreSB.Domain.Currency.EF
{
    public class CurrencyServiceEF : Service, ICurrencyServiceEF
    {
        IRepositoryEFRead _repositoryRead;
        IRepositoryEFWrite _repositoryWrite;
        IMapper _mapper;
        IValidatorCustom _validator;

        public CurrencyServiceEF(IRepositoryEFRead repositoryRead, IRepositoryEFWrite repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null, ILoggerCustom logger = null)
           : base(repositoryRead, repositoryWrite, mapper, validator, logger)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;

            _mapper = mapper;
            _validator = validator;
        }
        public CurrencyServiceEF(IRepositoryEFWrite repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null, ILoggerCustom logger = null)
           : base(repositoryWrite, mapper, validator, logger)
        {
            _repositoryWrite = repositoryWrite;

            _mapper = mapper;
            _validator = validator;
        }


        /*Currencies*/
        public async Task<ICurrencyAPI> AddCurrency(ICurrencyAPI currency)
        {
            var isValid = _validator.isValid(currency);

            var currencyExists = _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == currency.IsoCode).FirstOrDefault();
            if (currencyExists != null)
            {
                base.statusChangeAndLog(new Failure(), MessagesComposite.EntityAllreadyExists(currency.GetType().Name, this._repositoryWrite.GetDatabaseName()));
                //this._status = new Failure();
                //this._status.Message = MessagesComposite.EntityAllreadyExists(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                //_logger.Information(this.actualStatus);
                return null;
            }

            var entityToAdd = _mapper.Map<ICurrencyAPI, CurrencyDAL>(currency);
            await _repositoryWrite.AddAsync<CurrencyDAL>(entityToAdd);
            await _repositoryWrite.SaveAsync();
            if (entityToAdd != null && entityToAdd.Id > 0)
            {
                base.statusChangeAndLog(new Success(), MessagesComposite.EntitySuccessfullyCreated(currency.GetType().Name, this._repositoryWrite.GetDatabaseName()));
                //this._status = new Success();
                //this._status.Message = MessagesComposite.EntitySuccessfullyCreated(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                //_logger.Information(this.actualStatus);
            }

            var entityAdded = _mapper.Map<CurrencyDAL, ICurrencyAPI>(entityToAdd);
            return entityAdded;
        }
        public ICurrencyUpdateAPI UpdateCurrency(ICurrencyUpdateAPI currency)
        {
            var isValid = _validator.isValid(currency);
            var currencyExists = _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == currency.IsoCode).FirstOrDefault();
            if (currencyExists == null)
            {
                base.statusChangeAndLog(new Failure(), MessagesComposite.EntityNotFoundOnUpdate(currency.GetType().Name, this._repositoryWrite.GetDatabaseName()));
                //this._status = new Failure();
                //this._status.Message = MessagesComposite.EntityNotFoundOnUpdate(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                //_logger.Information(this._status.Message);
                return null;
            }

            var currencyUpdate = (CurrencyUpdateAPI)currency;
            _mapper.Map<CurrencyUpdateAPI, CurrencyDAL>(currencyUpdate, currencyExists);
            _repositoryWrite.Update<CurrencyDAL>(currencyExists);
            _repositoryWrite.Save();

            base.statusChangeAndLog(new Success(), MessagesComposite.EntityModified(currency.GetType().Name, this._repositoryWrite.GetDatabaseName()));
            //this._status = new Success();
            //this._status.Message = MessagesComposite.EntityModified(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
            return currencyUpdate;
        }
        public IServiceStatus DeleteCurrency(string currencyIso)
        {
            ICurrencyUpdateAPI command = new CurrencyUpdateAPI() { IsoCode = currencyIso };
            return DeleteCurrency(command);
        }
        public IServiceStatus DeleteCurrency(ICurrencyUpdateAPI currency)
        {
            var isValid = _validator.isValid(currency);
            var currencyExists = _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == currency.IsoCode).FirstOrDefault();
            if (currencyExists == null)
            {
                base.statusChangeAndLog(new Failure(), MessagesComposite.EntityNotFoundOnDelete(currency.GetType().Name, this._repositoryWrite.GetDatabaseName()));
                //this._status = new Failure();
                //this.actualStatus = MessagesComposite.EntityNotFoundOnDelete(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                //_logger.Information(this.actualStatus);
                return this._status;
            }

            _repositoryWrite.Delete<CurrencyDAL>(currencyExists);
            _repositoryWrite.Save();

            base.statusChangeAndLog(new Success(), MessagesComposite.EntityDeleted(currency.GetType().Name, this._repositoryWrite.GetDatabaseName()));
            //this.actualStatus = MessagesComposite.EntityDeleted(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
            return this._status;
        }


        public async Task<ICurrencyRateAddAPI> AddCurrencyRateQuerry(ICurrencyRateAddAPI query)
        {
            ICurrencyRateAddAPI result = null;

            var isValid = _validator.isValid(query);
            if (!isValid)
            {
                base.statusChangeAndLog(new Failure(), MessagesComposite.ModelValidationErrorOnCreate(query.GetType().Name, this._repositoryWrite.GetDatabaseName()));
                return null;
            }

            var currencyFrom = await _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == query.FromCurrency).FirstOrDefaultAsync();
            if (currencyFrom == null)
            {
                base.statusChangeAndLog(new Failure(), MessagesComposite.EntityNotFoundOnCreation(currencyFrom.GetType().Name, this._repositoryWrite.GetDatabaseName()));
                return null;
            }
            var currencyTo = await _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == query.ToCurrency).FirstOrDefaultAsync();
            if (currencyTo == null)
            {
                base.statusChangeAndLog(new Failure(), MessagesComposite.EntityNotFoundOnCreation(currencyTo.GetType().Name, this._repositoryWrite.GetDatabaseName()));
                return null;
            }
            var currencyCrossRate = await _repositoryWrite.QueryByFilter<CurrencyRatesDAL>(s => s.CurrencyFromId == currencyFrom.Id && s.CurrencyToId == currencyTo.Id
                && s.Date.Year == query.Date.Year && s.Date.Month == query.Date.Month && s.Date.Day == query.Date.Day).FirstOrDefaultAsync();
            if (currencyCrossRate != null)
            {
                base.statusChangeAndLog(new Failure(), MessagesComposite.EntityAllreadyExists(currencyCrossRate.GetType().Name, this._repositoryWrite.GetDatabaseName()));
                return null;
            }

            var crossRateToAdd = _mapper.Map<ICurrencyRateAddAPI, CurrencyRatesDAL>(query);
            crossRateToAdd.CurrencyFromId = currencyFrom.Id;
            crossRateToAdd.CurrencyToId = currencyTo.Id;

            isValid = _validator.isValid(crossRateToAdd);
            if (!isValid)
            {
                base.statusChangeAndLog(new Failure(), MessagesComposite.ModelValidationErrorOnCreate(crossRateToAdd.GetType().Name, this._repositoryWrite.GetDatabaseName()));
                return null;
            }

            await _repositoryWrite.AddAsync<CurrencyRatesDAL>(crossRateToAdd);
            await _repositoryWrite.SaveAsync();

            result = _mapper.Map<CurrencyRatesDAL, ICurrencyRateAddAPI>(crossRateToAdd);

            return result;
        }

        public async Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command)
        {

            IList<CrossCurrenciesAPI> result = new List<CrossCurrenciesAPI>();

            if (command != null && command.FromCurrency != null)
            {

                decimal fromRate;
                decimal toRate;

                var pairFrom = await _repositoryWrite
                    .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == command.FromCurrency.ToLower()
                    && c.CurrencyTo.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom)
                    .Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                if (pairFrom == null)
                {
                    var pairFromReveresd = await _repositoryWrite
                    .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyTo.IsoCode.ToLower() == command.FromCurrency.ToLower()
                    && c.CurrencyFrom.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                    if (pairFromReveresd == null) { throw new System.Exception("No from currency pair found"); }

                    fromRate = 1 / pairFromReveresd.Rate;
                }
                else
                {
                    fromRate = pairFrom.Rate;
                }


                var pairTo = await _repositoryWrite
                    .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == command.ToCurrency.ToLower()
                    && c.CurrencyTo.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                if (pairTo == null)
                {

                    var pairToReversed = await _repositoryWrite
                        .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == command.ToCurrency.ToLower()
                        && c.CurrencyTo.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                        .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                        .FirstOrDefaultAsync();

                    if (pairToReversed == null) { throw new System.Exception("No to currency pair found"); }

                    toRate = 1 / pairToReversed.Rate;
                }
                else
                {
                    toRate = pairTo.Rate;
                }


                var rate = fromRate / toRate;

                result.Add(new CrossCurrenciesAPI() { From = command.FromCurrency, To = command.ToCurrency, Throught = command.ThroughCurrency, Rate = rate });
            }

            return result.Cast<ICrossCurrenciesAPI>().ToList();
        }


        public void ReInitialize()
        {

            _repositoryRead.ReInitialize();
            _repositoryWrite.ReInitialize();

            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 1, Name = "USD", IsoCode = "USD" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 2, Name = "EUR", IsoCode = "EUR" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 3, Name = "GBP", IsoCode = "GBP" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 4, Name = "RUB", IsoCode = "RUB" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 5, Name = "JPY", IsoCode = "JPY" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 6, Name = "AUD", IsoCode = "AUD" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 7, Name = "CAD", IsoCode = "CAD" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 8, Name = "CHF", IsoCode = "CHF" });
            try { _repositoryWrite.SaveIdentity<CurrencyDAL>(); }
            catch (Exception e)
            {
                throw;
            }

            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 4, CurrencyFromId = 1, CurrencyToId = 4, Rate = 63.18M, Date = new DateTime(2019, 07, 23) });
            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 5, CurrencyFromId = 2, CurrencyToId = 4, Rate = 70.64M, Date = new DateTime(2019, 07, 23) });
            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 6, CurrencyFromId = 3, CurrencyToId = 4, Rate = 78.67M, Date = new DateTime(2019, 07, 23) });

            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 7, CurrencyFromId = 2, CurrencyToId = 5, Rate = 85.2M, Date = new DateTime(2019, 07, 23) });
            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 8, CurrencyFromId = 3, CurrencyToId = 5, Rate = 95.2M, Date = new DateTime(2019, 07, 23) });

            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 9, CurrencyFromId = 2, CurrencyToId = 6, Rate = 15M, Date = new DateTime(2019, 07, 23) });
            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 10, CurrencyFromId = 6, CurrencyToId = 3, Rate = 0.25M, Date = new DateTime(2019, 07, 23) });

            try { _repositoryWrite.SaveIdentity<CurrencyRatesDAL>(); }
            catch (Exception e)
            {
                throw;
            }
        }
        public void CleanUp()
        {
            _repositoryWrite.ReInitialize();
            _repositoryWrite.DeleteRange(_repositoryRead.GetAll<CurrencyRatesDAL>().ToList());
            _repositoryWrite.DeleteRange(_repositoryRead.GetAll<CurrencyDAL>().ToList());
            try { _repositoryWrite.Save(); } catch (Exception e) { throw; }
        }

    }
}
