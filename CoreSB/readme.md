# CORE mvc ordering test progect

## Pckages to build core:
-------------------------------------------------------
```
    dotnet add package Newtonsoft.Json --version 12.0.2
    dotnet add package Autofac.Extensions.DependencyInjection --version 4.4.0
    dotnet add package AutoMapper --version 8.1.0
    dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 6.1.0
    dotnet add package Microsoft.AspNetCore.SignalR
	dotnet add package FluentValidation.AspNetCore
	dotnet add package RabbitMQ.Client --version 6.2.1

	dotnet add package xunit --version 2.4.1
	dotnet add package xunit.runner.visualstudio --version 2.4.1
	dotnet add package AngleSharp --version 0.12.1
	dotnet add package FluentAssertions --version 5.7.0
	dotnet add package Microsoft.NET.Test.Sdk --version 16.2.0
	dotnet add package Serilog.AspNetCore	

```

## Migrate from net core 2.0 to 3.0
-------------------------------------------------------
```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.0.0
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore --version 3.0.0
dotnet add package Microsoft.EntityFrameworkCore.Abstractions --version 3.0.0
dotnet add package Microsoft.EntityFrameworkCore.Analyzers --version 3.0.0
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 3.0.0
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 3.0.0
```

## Pckages to build js:
-------------------------------------------------------
```
    npm install --save-dev react react-dom
    npm install --save-dev gulp gulp-babela
    npm install --save-dev webpack webpack-dev-server webpack-cli webpack-stream html-webpack-plugin clean-webpack-plugin
    npm install --save-dev @babel/core @babel/cli @babel/plugin-proposal-class-properties @babel/preset-env @babel/preset-react @babel/plugin-transform-arrow-functions @babel/plugin-transform-classes @babel/plugin-proposal-function-bind
```

## Docker commands
-------------------------------------------------------

docker pull mcr.microsoft.com/mssql/server

-------------------------------------------------------

## Decomposition
-------------------------------------------------------



### API (for fiddler test method,url,attribute,body)
-------------------------------------------------------




startup.cs changes
-------------------------------------------------------
custom default MVC Area location folder in API/Areas
in startup.cs rerouted through  RazorViewEngineOptions

Autofac container registration added

AutoMapper service added, 
one coniguration 
two types of initialization - static and instance API

AutoFact to Automapper registration added

AutofacServiceProvider returned from ConfigureServices

Autofac multiple Irepositories registration
https://autofaccn.readthedocs.io/en/latest/faq/select-by-context.html

-------------------------------------------------------
Added http instead of https routing for Fiddler test to:
    .UseUrls("http://localhost:5002")
