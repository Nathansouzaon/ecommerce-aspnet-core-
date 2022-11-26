#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/api gateways/App.Bff.Compras/App.Bff.Compras.csproj", "src/api gateways/App.Bff.Compras/"]
RUN dotnet restore "src/api gateways/App.Bff.Compras/App.Bff.Compras.csproj"
COPY . .
WORKDIR "/src/src/api gateways/App.Bff.Compras"
RUN dotnet build "App.Bff.Compras.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Bff.Compras.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Bff.Compras.dll"]