#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
#WORKDIR /app
#
#COPY ./publish /publish
#WORKDIR /publish
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
#ENTRYPOINT [ "dotnet", "SystemyWP.API.Gastronomy.dll" ]

#FROM mcr.microsoft.com/dotnet/core/sdk:6.0 AS build
#WORKDIR /app 
##
## copy csproj and restore as distinct layers
#COPY *.sln .
#COPY SystemyWP.API/*.csproj ./SystemyWP.API/
#COPY SystemyWP.Lib.Shared.Abstractions/*.csproj ./SystemyWP.Lib.Shared.Abstractions/
#COPY SystemyWP.Lib.Shared.DTOs/*.csproj ./SystemyWP.Lib.Shared.DTOs/
##
#RUN dotnet restore 
##
## copy everything else and build app
#COPY SystemyWP.API/. ./SystemyWP.API/
#COPY SystemyWP.Lib.Shared.Abstractions/. ./SystemyWP.Lib.Shared.Abstractions/
#COPY SystemyWP.Lib.Shared.DTOs/. ./SystemyWP.Lib.Shared.DTOs/ 
##
#WORKDIR /app/Experiment
#RUN dotnet publish -c Release -o out 
##
#FROM mcr.microsoft.com/dotnet/core/aspnet:6.0 AS runtime
#WORKDIR /app 
##
#COPY --from=build /app/SystemyWP.API/out ./
#ENTRYPOINT [ "dotnet", "SystemyWP.API.dll" ]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SystemyWP.API.csproj", "./"]
RUN dotnet restore "SystemyWP.API.Gastronomy.csproj"
COPY . .
RUN dotnet publish "SystemyWP.API..Gastronomy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SystemyWP.API.Gastronomy.dll"]