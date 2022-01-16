#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 4200
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ControleFinanceiro.WebApi/ControleFinanceiro.WebApi.csproj", "ControleFinanceiro.WebApi/"]
COPY ["ControleFinanceiro.Domain/ControleFinanceiro.Domain.csproj", "ControleFinanceiro.Domain/"]
COPY ["ControleFinanceiro.DomainBase.Exceptions/ControleFinanceiro.DomainBase.Exceptions.csproj", "ControleFinanceiro.DomainBase.Exceptions/"]
COPY ["ControleFinanceiro.Infra.Repository/ControleFinanceiro.Infra.Repository.csproj", "ControleFinanceiro.Infra.Repository/"]
COPY ["ControleFinanceiro.Application/ControleFinanceiro.Application.csproj", "ControleFinanceiro.Application/"]
RUN dotnet restore "ControleFinanceiro.WebApi/ControleFinanceiro.WebApi.csproj"
COPY . .
WORKDIR "/src/ControleFinanceiro.WebApi"
RUN dotnet build "ControleFinanceiro.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControleFinanceiro.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControleFinanceiro.WebApi.dll"]
