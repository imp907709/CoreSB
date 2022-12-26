
Configs:
-------------------------------------------------------

startup.cs
custom default MVC Area location folder in API/Areas in startup.cs rerouted through RazorViewEngineOptions Autofac container registration added AutoMapper service added, one coniguration two types of initialization - static and instance API AutoFact to Automapper registration added AutofacServiceProvider returned from ConfigureServices SignalR use and hub routing added Authentication registration: Authentication EF db context Identity core for user managment Added authentication with cookies Added conditional registration for SQL, SQLlite and InMemmory databases Condition is enum class from appsettings.json

ConfigureAutofacDbContexts
multiple SQL DBs in one project possible with Dummy Repository clones per DB scope
registration of connection string to context to Repository to service with IRepository

Program.cs
Added http instead of https routing for Fiddler test to: .UseUrls("http:localhost:5002")


 Decomposition:
 -------------------------------------------------------
 > folder
 files

>API
    WebApi, 
    Controllers,
    MVC Views

>Infrastructure
    >EF
        >Domain
        DomainContextEF
        DomainRepositoryEF
        DomainServiceEF        
    IRepositoryEF
    RepositoryEF
    
    >IO
        >Logging
        >Servialization
        >Settings
    
    >IoC

    >DomainMapping
    >DomainValidation

>Universal:
    IRepository;
    IService;
    IEntities;

    Entities;
    Service;
        
        >DomainSpesific:
            DAL,API, IAPI
        >Infrastructure
            ILogger, IMapper, IValidator

Layers relation directions
API->Infrastructure->Universal


Target Decomposition:
-------------------------------------------------------
> folder
files

API:
	WebAPI, MVC API, MVC Views

INF (Infrastructure):
	Logger:ILogger,Mapper:IMapper,Validattor:IValidation
	>IO
		Serialization, Settings, Logging
	>IoC
	>SignalR
	
	> EF
		RepositoryEF: IRepository
		
	+> Bus, Broker
	
	+> Mongo 
		RepositoryMongo: IRepository
	
DMN (domain specific):
		IDomainService : IService
			{IRepository}
		DomainService: IDomainService
			{IRepository}
			
UNV (universal):
	IEntity
	IRepository
	IService
	
	//no external ref
	Entity: IEntity
	Service: IService {IRepository}
	
	~UNV INF (Universal for infrastructure)
	ILogger
	+ ISerialization
	
	~UNV Domain (Universal or domain)
	IMapper
	IValidation
	
	IServiceBus
	
Layers relation directions
API->DMN
API->INF
    DMN->UNV
    INF->UNV

TODO: [
	
] 

BACKLOG/MILESTONES: [

] 

DONE:[

    chat: [
        <- done 09.06.2019 1h45m -> signalR and auth user and messages binded
    ] ~1h45m in 1d
    
    worker: [
        <- done 10.06.2019 01: 24 1h31m -> signalR work queued,started,finished moque
        <- done 10.06.2019 22: 52 1h12m -> signalR work queue and front edited
    ] ~2h 40m in 1d

    orders: [
        <- done 13.06.2019 5h -> Ef core Orders model, migration and seed Many-to-many
        <- done 14.06.2019 1h20m -> cqrs add and multiple context Autofac resolve
        <- done 14.06.2019 40m -> order created
        <- done 14.06.2019 30m -> props changed
        <- done 13.06.2019 5h06m -> Ef core Orders model, migration and seed Many-to-many 
        <- done 14.06.2019 1h20m -> cqrs add and multiple context Autofac resolve
        <- done 14.06.2019 40m -> order created
        <- done 15.06.2019 40m -> order accounter interfaces
        <- done 15.06.2019 1h10m -> order new API, BLL interfaces
        <- done 15.06.2019 1h5m -> order deliverer, clenup
        <- done 15.06.2019 1h10m -> order mapping
        <- done 15.06.2019 1h-> mapping changed

      
    ] ~11h in 3d

    crmvcsb: [

        <- done 02.06.2019 01:53 2h -> PersonAddsPost	
        <- done 02.06.2019 14:40-14:50 10m -> get posts by person
        <- done 02.06.2019 12:14-14:40 2h30m -> get posts by blog
        <- done 02.06.2019 12:14-14:50 2h30m -> get blogs by person
        <- done 02.06.2019 15:13-15:53 40m -> person removes post
        <- done 02.06.2019 15:53-16:03 10m -> person updates post

        <- done 04.06.2019 5h -> react boardGame checker

        <- done 04.09.2019 23:53 05.09.2019 2:40 2h50m -> SignalR chat checker
        <- done 05.06.2019 2h30m -> Login and authenticate template
        <- done 06.06.2019 7h22m -> Identity on MVC views with Identity DB migrations
        <- done 07.06.2019 5h3m -> authorization token and cookie redirect on mvc startup setup        
        <- done 08.06.2019 2h15m -> core mvc with auth and defailt ui mvc 
            rounig with API/areas for view and controller
            {            
                => gen mvc 
                    dotnet new mvc -o {folder} -au individual
                => add areas 
                    options.AreaViewLocationFormats.Add("API/Areas/{2}/Views/{1}/{0}.cshtml");
                => remove compatibility 
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                => move MVC v,c folders
                => include 
                    @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
                    from _viewimport on every layout
                => leave basic routing in startup.cs [
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                    
                        routes.MapRoute(
                            name: "default",
                            template: "{controller=Home}/{action=Index}/{id?}");
                ]

                => dont include in startup.cs
                it conflict with scafoldede razor pages sigin

                    services.AddAuthentication
                        o => CookieAuthenticationDefaults

            }
        <- done 08.06.2019 4h -> move auth
        <- done 09.06.2019 1h45m -> signalR and auth user and messages binded
   
        
        <- done 12.06.2019 3h30m -> react timer sliders 
        <- done 12.06.2019 2h -> react todo        

    ] ~37h 15m in 7d


	newOrder :
	[

		<- 15.07.2019 1h -> new order model
        <- 16.07.2019 1h -> new order migration
        <- 16.07.2019 2h -> new order migration, address initialize, clearup
        
        <- 17.07.2019 45m -> new order migration cleanup
		<- 16.07.2019 1h -> new order migration
		<- 16.07.2019 1h -> new order migration

		<- 17.07.2019 45m -> new order migration cleanup

		<- 22.07.2019 1h -> new order currenciesDAL model

        <- 24.07.2019 1h -> new order currencies manager
        
        <- 25.07.2019 1h 30m -> currencies manager API resp

		<- 26.09.2019 1h 30m -> inmemmory, SQL, SQL, SQLlite conditional contexts

		<- 19.11.2019 3h 30m -> Autofac multiple Irepositories registration

		<- 29.03.2020 3h -> Ilogger, Iserializer, variables classes with variable.json
    ]
    ~8h 15m in 7days

    master:[
        <- done 15.06.2019 12:35-16:02 3h30m -> git merge crmvcsb -> master, order -> master, cleanup master,crmvcsb,order
        
        <- done 01.07.2019 4h -> KATAs heapsort 
        <- done 01.07.2019 1h -> test server project multiple build tasks, and integrate run
        <- done 1h -> integration tests        
        
        <- done 02.07.2019 1h -> integration tests
        <- done 02.07.2019 2h -> quicksort        
        <- done 02.07.2019 1h10m -> heapsort
        <- done 02.07.2019 1h45m -> linked nodes reverce, polindrome check

        <- done 04.07.2019 2h30m -> merge sort
        
        <- done 05.07.2019 1h -> insertion sort 
        <- done 05.07.2019 1h -> sorting tests

        <- done 07.07.2019 2h insert sort rep ->
        <- done 07.07.2019 2h heapsort above heapify ->

    ]~ 23h in 6 days
    	
	TMPL
	[
		<- 10.04.2020  4h -> TMPL branch create and cleanup, warmup
		<- 11.04.2020  3h45m -> TMPL namespaces refactor
		<- 12.04.2020  50m -> TMPL Blogging namespaces refactor
		<- 01.05.2020 2h30m -> 
			registering multiple domainContexts for domainRepositories; 
			several domainServices to one domainManager; 
			and reinnitializing domain DBs (newOrer and Currencies) from controller Index;
        <- done 10.04.2020 - 02.05.2020 p32h f15h in 4d -> refactor SB template to fire up state		
		[
			<- done p 10.04.2020 21:37 p8h->  create new TMPL from newOrder GIT branch with all infrastructure stuff
				double repositories, loggers, mappers
			<- done p 10.04.2020 21:37 p24h -> recreate All main project branches			
				p8h Blogging (manyToMany tags and CQRS)
				p8h NewOrder (volume recount)
				p8h CrossCurrencies (curency to curency throught curency)
			
			<- done 11.04.2020 4h-> refactor AllInOneModels			
			<- done 12.04.2020 4h -> refactor NewOrderModelsOneFile			
			-> CostControlModels
				G:\disk\Files\git\Core\crmvcsb\crmvcsb\Domain\TestModels\Models\CostControl\CostControlModels.cs
			
			<- 10.04.2020  4h -> TMPL branch create and cleanup, warmup
			<- 11.04.2020  3h45m -> TMPL namespaces refactor
			<- 12.04.2020  50m -> TMPL Blogging namespaces refactor
			<- 01.05.2020 2h30m -> 
				registering multiple domainContexts for domainRepositories; 
				several domainServices to one domainManager; 
				and reinnitializing domain DBs (newOrer and Currencies) from controller Index;
			<- done 02.05.2020 3h30m -> move blogging to brunch, 
				merge TMPL to master, merge blogging to master
			<- 08.11.2020 4h -> migrate from net core 2.0 to 3.0
		]~19h in 5days
        
        <- 10.12.2020 2h45m -> rearrange Services and Context interfaces
        
        <- 11.12.2020 3h 10m-> rearrange Services and Context interfaces
        <- 11.12.2020 1h 20m-> fluent validation init

        <- 
            15.12.2020 8h 
            16.12.2020 6h -> read write, repositories to service autofac register; read write DBs reinitialization
                            dividing to read and write leead to register as Irepository problem
        <- 
            18.12.2021 3h 20m
        -> Domain CRUD, REST API
       
        <- 10.03.2021 2h migrate from net 3.1 to net 5.0 ->
        
        <- 11.03.2021 30m folder namespaces rearrange ->

        <- 12.03.2021 30m folder namespaces rearrange ->
        <- 12.03.2021 13:00 14:20 2h 20m net core exception handling ->

        < 20.03.2021 3h rabbit send recieve provider >	
        
		< 21.03.2021 1h 30m bidirectional >
		< 21.03.2021 40m bidirectional by class >
		< 21.03.2021 1h pass model to lambda event handler >
				
		< 26.03.2021 1h10m rabit parse response delegate >
		< 26.03.2021 1h 22:00 23:00 p 3h f 1h 
            Rabbit wraper with delegate ref >

        17.03.2021 3h 30m currency dictionary CRUD >        
            -> currency cross rates CRUD
        -> currency cross rates recount from curency changes via rabbit mq
        -> sync from write db to read
       

        22.12.2022 
            ~10h TMPL docker compose 
        23.12.2022 TMPL
            ~5h TMPL mongo repo, service
        25.12.2022
            < done h5 > mongo add, delete, get by filter
            < done h2 > mongo repository to context, insert, update
        26.12.2022
            < done 6h > mongo context, service, crud, models, update, upsert
	]
	~67h 00m in 25days

]~162h 50m in 50d


