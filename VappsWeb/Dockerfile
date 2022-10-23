FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
EXPOSE 80

COPY ["systemywp/VappsWeb/VappsWeb.csproj", "systemywp/VappsWeb/"]
COPY ["systemywp/Domain/Domain.csproj", "systemywp/Domain/"]
RUN dotnet restore "systemywp/VappsWeb/VappsWeb.csproj"
COPY . .
WORKDIR "/src/systemywp/VappsWeb/"
RUN dotnet build "VappsWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VappsWeb.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ["systemywp/VappsWeb/nginx.conf", "/etc/nginx/nginx.conf"]