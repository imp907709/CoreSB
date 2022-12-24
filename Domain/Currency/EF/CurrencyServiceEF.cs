using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CoreSB.Infrastructure.IO.Settings;
using CoreSB.Universal;
using CoreSB.Universal.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CoreSB.Domain.Currency.EF
{
    public class CurrencyServiceEF : ServiceEF, ICurrencyServiceEF
    {
        internal IRepositoryEFRead _repositoryRead;
        internal IRepositoryEFWrite _repositoryWrite;
        internal IMapper _mapper;
        internal IValidatorCustom _validator;

        public CurrencyServiceEF(IRepositoryEFRead repositoryRead, IRepositoryEFWrite repositoryWrite, IMapper mapper,
            IValidatorCustom validator, ILoggerCustom logger)
            : base(repositoryRead, repositoryWrite, mapper, validator, logger)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;

            _mapper = mapper;
            _validator = validator;
        }

        public CurrencyServiceEF(IRepositoryEFWrite repositoryWrite, IMapper mapper, IValidatorCustom validator,
            ILoggerCustom logger)
            : base(repositoryWrite, mapper, validator, logger)
        {
            _repositoryWrite = repositoryWrite;

            _mapper = mapper;
            _validator = validator;
        }


        /*Currencies*/
        public async Task<ICurrencyBL> AddCurrency(ICurrencyBL currency)
        {
            var isValid = _validator.isValid(currency);

            var currencyExists = _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == currency.IsoCode)
                .FirstOrDefault();
            if (currencyExists != null)
            {
                base.statusChangeAndLog(new Failure(),
                    MessagesComposite.EntityAllreadyExists(currency.GetType().Name,
                        this._repositoryWrite.GetDatabaseName()));

                //this._status = new Failure();
                //this._status.Message = MessagesComposite.EntityAllreadyExists(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                //_logger.Information(this.actualStatus);
                return null;
            }

            var entityToAdd = _mapper.Map<CurrencyBL, CurrencyDAL>((CurrencyBL)currency);
            await _repositoryWrite.AddAsync<CurrencyDAL>(entityToAdd);
            await _repositoryWrite.SaveAsync();
            if (entityToAdd != null && entityToAdd.Id > 0)
            {
                base.statusChangeAndLog(new Success(),
                    MessagesComposite.EntitySuccessfullyCreated(currency.GetType().Name,
                        this._repositoryWrite.GetDatabaseName()));

                //this._status = new Success();
                //this._status.Message = MessagesComposite.EntitySuccessfullyCreated(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                //_logger.Information(this.actualStatus);
            }

            var entityAdded = _mapper.Map<CurrencyDAL, ICurrencyBL>(entityToAdd);
            return entityAdded;
        }

        public ICurrencyUpdateBL UpdateCurrency(ICurrencyUpdateBL currency)
        {
            var isValid = _validator.isValid(currency);
            var currencyExists = _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == currency.IsoCode)
                .FirstOrDefault();
            if (currencyExists == null)
            {
                base.statusChangeAndLog(new Failure(),
                    MessagesComposite.EntityNotFoundOnUpdate(currency.GetType().Name,
                        this._repositoryWrite.GetDatabaseName()));

                //this._status = new Failure();
                //this._status.Message = MessagesComposite.EntityNotFoundOnUpdate(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                //_logger.Information(this._status.Message);
                return null;
            }

            var currencyUpdate = (CurrencyUpdateBL)currency;
            _mapper.Map<CurrencyUpdateBL, CurrencyDAL>(currencyUpdate, currencyExists);
            _repositoryWrite.Update<CurrencyDAL>(currencyExists);
            _repositoryWrite.Save();

            base.statusChangeAndLog(new Success(),
                MessagesComposite.EntityModified(currency.GetType().Name, this._repositoryWrite.GetDatabaseName()));

            //this._status = new Success();
            //this._status.Message = MessagesComposite.EntityModified(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
            return currencyUpdate;
        }

        public IServiceStatus DeleteCurrency(string currencyIso)
        {
            ICurrencyUpdateBL command = new CurrencyUpdateBL() {IsoName = currencyIso};
            return DeleteCurrency(command);
        }

        public IServiceStatus DeleteCurrency(ICurrencyUpdateBL currency)
        {
            var isValid = _validator.isValid(currency);
            var currencyExists = _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == currency.IsoCode)
                .FirstOrDefault();
            if (currencyExists == null)
            {
                base.statusChangeAndLog(new Failure(),
                    MessagesComposite.EntityNotFoundOnDelete(currency.GetType().Name,
                        this._repositoryWrite.GetDatabaseName()));

                //this._status = new Failure();
                //this.actualStatus = MessagesComposite.EntityNotFoundOnDelete(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                //_logger.Information(this.actualStatus);
                return this._status;
            }

            _repositoryWrite.Delete<CurrencyDAL>(currencyExists);
            _repositoryWrite.Save();

            base.statusChangeAndLog(new Success(),
                MessagesComposite.EntityDeleted(currency.GetType().Name, this._repositoryWrite.GetDatabaseName()));

            //this.actualStatus = MessagesComposite.EntityDeleted(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
            return this._status;
        }


        public async Task<ICurrencyRateAddAPI> AddCurrencyRateQuery(ICurrencyRateAddAPI query)
        {
            ICurrencyRateAddAPI result = null;

            var isValid = _validator.isValid(query);
            if (!isValid)
            {
                base.statusChangeAndLog(new Failure(),
                    MessagesComposite.ModelValidationErrorOnCreate(query.GetType().Name,
                        this._repositoryWrite.GetDatabaseName()));
                return null;
            }

            var currencyFrom = await _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == query.FromCurrency)
                .FirstOrDefaultAsync();
            if (currencyFrom == null)
            {
                base.statusChangeAndLog(new Failure(),
                    MessagesComposite.EntityNotFoundOnCreation(currencyFrom.GetType().Name,
                        this._repositoryWrite.GetDatabaseName()));
                return null;
            }

            var currencyTo = await _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == query.ToCurrency)
                .FirstOrDefaultAsync();
            if (currencyTo == null)
            {
                base.statusChangeAndLog(new Failure(),
                    MessagesComposite.EntityNotFoundOnCreation(currencyTo.GetType().Name,
                        this._repositoryWrite.GetDatabaseName()));
                return null;
            }

            var dtExp = CompareByDateExp(query.Date, ExpressionType.Equal, DateComparisonRange.Day);
            var currencyCrossRate = await _repositoryWrite.QueryByFilter<CurrencyRatesDAL>(s =>
                    s.CurrencyFromId == currencyFrom.Id 
                    && s.CurrencyToId == currencyTo.Id
                    && dtExp.Compile().Invoke(s)
                    )
                .FirstOrDefaultAsync();

            if (currencyCrossRate != null)
            {
                base.statusChangeAndLog(new Failure(),
                    MessagesComposite.EntityAllreadyExists(currencyCrossRate.GetType().Name,
                        this._repositoryWrite.GetDatabaseName()));
                return null;
            }

            var crossRateToAdd = _mapper.Map<ICurrencyRateAddAPI, CurrencyRatesDAL>(query);
            crossRateToAdd.CurrencyFromId = currencyFrom.Id;
            crossRateToAdd.CurrencyToId = currencyTo.Id;

            isValid = _validator.isValid(crossRateToAdd);
            if (!isValid)
            {
                base.statusChangeAndLog(new Failure(),
                    MessagesComposite.ModelValidationErrorOnCreate(crossRateToAdd.GetType().Name,
                        this._repositoryWrite.GetDatabaseName()));
                return null;
            }

            await _repositoryWrite.AddAsync<CurrencyRatesDAL>(crossRateToAdd);
            await _repositoryWrite.SaveAsync();

            result = _mapper.Map<CurrencyRatesDAL, ICurrencyRateAddAPI>(crossRateToAdd);

            return result;
        }

        public async Task<IList<CrossCurrenciesAPI>> ValidateCrossRates(ICrossCurrencyValidateCommand command)
        {
            List<CrossCurrenciesAPI> result = new List<CrossCurrenciesAPI>();
            List<CurrencyRatesDAL> created = new List<CurrencyRatesDAL>();

            var rateFrom = await _repositoryWrite
                .QueryByFilter<CurrencyRatesDAL>(s =>
                s.Date <= command.To
                && s.Date >= command.From
                && s.CurrencyFromId == command.CurrencyFrom
                && s.CurrencyToId == command.CurrencyTo).ToListAsync();

            var rateTo = await _repositoryWrite
                .QueryByFilter<CurrencyRatesDAL>(s =>
                    s.Date <= command.To
                    && s.Date >= command.From
                    && s.CurrencyFromId == command.CurrencyTo 
                    && s.CurrencyToId == command.CurrencyFrom).ToListAsync();

            if (rateFrom.Count != rateTo.Count)
            {
                // select from exceed
                var fromExceeded = rateFrom
                    .Where(s => rateTo.Any(c => c.Date != s.Date))
                    .ToList();

                if (fromExceeded?.Any() == true)
                {
                    var added = await exceededCrossRateAdd(fromExceeded);
                    created.AddRange(added);
                }

                // select to exceed
                var toExceeded = rateTo 
                    .Where(s => rateFrom.Any(c => c.Date != s.Date))
                    .ToList();

                if (toExceeded?.Any() == true)
                {
                    var added = await exceededCrossRateAdd(toExceeded);
                    created.AddRange(added);
                }
            }

            result = _mapper.Map<List<CrossCurrenciesAPI>>(created);
            return result;
        }

        async Task<List<CurrencyRatesDAL>> exceededCrossRateAdd(IList<CurrencyRatesDAL> exceed)
        {
            if (exceed?.Any() == true)
            {
                var toAdd = new List<CurrencyRatesDAL>();
                    
                foreach (var from in exceed)
                {
                    toAdd.Add(new CurrencyRatesDAL() { CurrencyFrom = from.CurrencyTo, CurrencyTo = from.CurrencyFrom, Date = from.Date, Rate = 1 / from.Rate});
                }
                await _repositoryWrite.AddRangeAsync(toAdd);
                await _repositoryWrite.SaveAsync();
                return toAdd;
            }

            return new List<CurrencyRatesDAL>();
        }

        public async Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command)
        {
            IList<CrossCurrenciesAPI> result = new List<CrossCurrenciesAPI>();

            if (command != null && command.FromCurrency != null)
            {
                decimal fromRate;
                decimal toRate;

                var pairFrom = await _repositoryWrite
                    .QueryByFilter<CurrencyRatesDAL>(c =>
                        c.CurrencyFrom.IsoName.ToLower() == command.FromCurrency.ToLower()
                        && c.CurrencyTo.IsoName.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom)
                    .Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                if (pairFrom == null)
                {
                    var pairFromReveresd = await _repositoryWrite
                        .QueryByFilter<CurrencyRatesDAL>(c =>
                            c.CurrencyTo.IsoName.ToLower() == command.FromCurrency.ToLower()
                            && c.CurrencyFrom.IsoName.ToLower() == command.ThroughCurrency.ToLower())
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
                    .QueryByFilter<CurrencyRatesDAL>(c =>
                        c.CurrencyFrom.IsoName.ToLower() == command.ToCurrency.ToLower()
                        && c.CurrencyTo.IsoName.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                if (pairTo == null)
                {
                    var pairToReversed = await _repositoryWrite
                        .QueryByFilter<CurrencyRatesDAL>(c =>
                            c.CurrencyFrom.IsoName.ToLower() == command.ToCurrency.ToLower()
                            && c.CurrencyTo.IsoName.ToLower() == command.ThroughCurrency.ToLower())
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

                result.Add(new CrossCurrenciesAPI()
                {
                    From = command.FromCurrency,
                    To = command.ToCurrency,
                    Throught = command.ThroughCurrency,
                    Rate = rate
                });
            }

            return result.Cast<ICrossCurrenciesAPI>().ToList();
        }


        
        public void Initialize()
        {
            _repositoryWrite.AddRangeAsync(InitialPreloadData.initialCurrencies);
            try { _repositoryWrite.SaveIdentity<CurrencyDAL>(); }
            catch (Exception e)
            {
                throw;
            }

            _repositoryWrite.AddRangeAsync(InitialPreloadData.CrossCurrencies_2019);
            _repositoryWrite.AddRangeAsync(InitialPreloadData.CrossCurrencies_2022);
            try { _repositoryWrite.SaveIdentity<CurrencyRatesDAL>(); }
            catch (Exception e)
            {
                throw;
            }
        }
        
        public void ReInitialize()
        {
            _repositoryRead.ReInitialize();
            _repositoryWrite.ReInitialize();

            this.Initialize();
        }
        
        public void CleanUp()
        {
            _repositoryWrite.ReInitialize();
            _repositoryWrite.DeleteRange(_repositoryRead.GetAll<CurrencyRatesDAL>().ToList());
            _repositoryWrite.DeleteRange(_repositoryRead.GetAll<CurrencyDAL>().ToList());
            try { _repositoryWrite.Save(); }
            catch (Exception e) { throw; }
        }


        public async Task ValidateCrudTest()
        {
            var crs = await _repositoryWrite.GetAll<CurrencyDAL>().Take(10)
                .ToListAsync();
            
            var cur = new CurrencyDAL
            {
                Name = "test genned",
                IsoName = "test name",
                IsoCode = 132,
                IsMain = false
            };
            
            await _repositoryWrite.AddAsync(cur);
            await _repositoryWrite.SaveAsync();

            var res = await _repositoryWrite.GetAll<CurrencyDAL>(s=>s.IsoName == "test name")
                .ToListAsync();
        }
    }
}
