FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SystemyWP.API.Gastronomy.csproj", "./"]
RUN dotnet restore "SystemyWP.API.Gastronomy.csproj"
COPY . .
RUN dotnet publish "SystemyWP.API.Gastronomy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SystemyWP.API.Gastronomy.dll"]