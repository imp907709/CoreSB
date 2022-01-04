# CORE mvc ordering test progect

## Pckages to build core:
-------------------------------------------------------

```
  
dotnet add package Newtonsoft.Json 
dotnet add package Autofac.Extensions.DependencyInjection
dotnet add package AutoMapper 
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection 
dotnet add package Microsoft.AspNetCore.SignalR
dotnet add package FluentValidation.AspNetCore
dotnet add package RabbitMQ.Client
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL 
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore 
dotnet add package Microsoft.EntityFrameworkCore.Abstractions 
dotnet add package Microsoft.EntityFrameworkCore.Analyzers 
dotnet add package Microsoft.EntityFrameworkCore.InMemory 
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore 
dotnet add package Serilog.AspNetCore

dotnet add package xunit 
dotnet add package xunit.runner.visualstudio
dotnet add package AngleSharp 
dotnet add package FluentAssertions
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package Serilog.AspNetCore
dotnet add package Swashbuckle.AspNetCore
dotnet add package Swashbuckle.AspNetCore.SwaggerUI

```

## Docker commands
-------------------------------------------------------

docker pull mcr.microsoft.com/mssql/server

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=QwErTy_1" -e "MSSQL_PID=Express" -p 1433:1433 --name msSQL -d
mcr.microsoft.com/mssql/server:latest

-------------------------------------------------------
