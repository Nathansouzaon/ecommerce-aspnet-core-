#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/web/App.WebApp.Mvc/App.WebApp.Mvc.csproj", "src/web/App.WebApp.Mvc/"]
RUN dotnet restore "src/web/App.WebApp.Mvc/App.WebApp.Mvc.csproj"
COPY . .
WORKDIR "/src/src/web/App.WebApp.Mvc"
RUN dotnet build "App.WebApp.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.WebApp.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.WebApp.Mvc.dll"]