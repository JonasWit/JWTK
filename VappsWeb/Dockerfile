#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY VappsWeb.csproj .
#RUN dotnet restore VappsWeb.csproj
#COPY . .
#RUN dotnet build VappsWeb.csproj -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish VappsWeb.csproj -c Release -o /app/publish
#
#FROM nginx:alpine AS final
#WORKDIR /usr/share/nginx/html
#COPY --from=publish /app/publish/wwwroot .
#COPY nginx.conf /etc/nginx/nginx.conf


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

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["systemywp/VappsWeb/VappsWeb.csproj", "systemywp/VappsWeb/"]
#COPY ["systemywp/Domain/Domain.csproj", "systemywp/Domain/"]
#RUN dotnet restore "systemywp/VappsWeb/VappsWeb.csproj"
#COPY . .
#WORKDIR "/src/systemywp/VappsWeb/"
#RUN dotnet build "VappsWeb.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "VappsWeb.csproj" -c Release -o /app/publish
#
#FROM nginx:alpine AS final
#WORKDIR /usr/share/nginx/html
#COPY --from=publish /app/publish/wwwroot .
#COPY nginx.conf /etc/nginx/nginx.conf


#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["systemywp/VappsWeb/VappsWeb.csproj", "systemywp/VappsWeb/"]
#COPY ["systemywp/Domain/Domain.csproj", "systemywp/Domain/"]
#RUN dotnet restore "systemywp/VappsWeb/VappsWeb.csproj"
#COPY . .
#WORKDIR "/src/systemywp/VappsWeb/"
#RUN dotnet build "VappsWeb.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "VappsWeb.csproj" -c Release -o /app/publish
#
#FROM nginx:alpine AS final
#WORKDIR /usr/share/nginx/html
#COPY --from=publish /app/publish/wwwroot .
#COPY nginx.conf /etc/nginx/nginx.conf


#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["BlazorApp1/Server/BlazorApp1.Server.csproj", "BlazorApp1/Server/"]
#COPY ["BlazorApp1/Client/BlazorApp1.Client.csproj", "BlazorApp1/Client/"]
#COPY ["BlazorApp1/Shared/BlazorApp1.Shared.csproj", "BlazorApp1/Shared/"]
#RUN dotnet restore "BlazorApp1/Server/BlazorApp1.Server.csproj"
#COPY . .
#WORKDIR "/src/BlazorApp1/Server"
#RUN dotnet build "BlazorApp1.Server.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "BlazorApp1.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "BlazorApp1.Server.dll"]