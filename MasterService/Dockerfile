FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
EXPOSE 80

COPY ["systemywp/MasterService/MasterService.csproj", "systemywp/MasterService/"]
COPY ["systemywp/Domain/Domain.csproj", "systemywp/Domain/"]
RUN dotnet restore "systemywp/MasterService/MasterService.csproj"
COPY . .
WORKDIR "/src/systemywp/MasterService/"
RUN dotnet build "MasterService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasterService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SystemyWP.API.dll"]