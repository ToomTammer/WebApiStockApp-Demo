````bash
## Step
## 1 Other commnad

donet restore

dotnet build

dotnet watch

## 1 create proj.

dotnet new webapi -o api

## 2 ORM, Entity Framwork

Microsoft.EntityFrameworkCore.SqlServer

Microsoft.EntityFrameworkCore.Tools

Microsoft.EntityFrameworkCore.Design

## 3 Create ApplicationDBContext.cs
For help for query data at DB

## 4 Migration DBB

dotnet ef migrations add init

dotnet ef database update

## 5 Identity
Microsoft.Extensions.Identity.Core
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Authentication.JwtBearer

