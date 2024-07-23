#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Goods.System.Social.Network.Microservice.Notifications/Goods.System.Social.Network.Microservice.Notifications.csproj", "Goods.System.Social.Network.Microservice.Notifications/"]
COPY ["DomainService/DomainServices.csproj", "DomainService/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["DomainModel/DomainModel.csproj", "DomainModel/"]
RUN dotnet restore "Goods.System.Social.Network.Microservice.Notifications/Goods.System.Social.Network.Microservice.Notifications.csproj"
COPY . .
WORKDIR "/src/Goods.System.Social.Network.Microservice.Notifications"
RUN dotnet build "Goods.System.Social.Network.Microservice.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Goods.System.Social.Network.Microservice.Notifications.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Goods.System.Social.Network.Microservice.Notifications.dll"]