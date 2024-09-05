## Step
## 1 Other commnad
``bash
donet restore
``bash
dotnet build
``bash
dotnet watch

## 1 create proj.
``bash
dotnet new webapi -o api

## 2 ORM, Entity Framwork
``bash
Microsoft.EntityFrameworkCore.SqlServer
``bash
Microsoft.EntityFrameworkCore.Tools
``bash
Microsoft.EntityFrameworkCore.Design

## 3 Create ApplicationDBContext.cs
For help for query data at DB

## 4 Migration DBB
``bash
dotnet ef migrations add init
``bash
dotnet ef database update

## 5 Identity
Microsoft.Extensions.Identity.Core
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Authentication.JwtBearer

