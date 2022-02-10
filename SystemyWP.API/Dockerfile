#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
#WORKDIR /app
#
#COPY *.csproj ./
#RUN dotnet restore
#
#COPY . ./
#RUN dotnet publish -c Release -o out
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /app
#COPY --from=build-env /app/out .
#
#ENTRYPOINT [ "dotnet", "SystemyWP.API.dll" ]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SystemyWP.API.csproj", "./"]
RUN dotnet restore "SystemyWP.API.csproj"
COPY . .
RUN dotnet publish "SystemyWP.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SystemyWP.API.dll"]
