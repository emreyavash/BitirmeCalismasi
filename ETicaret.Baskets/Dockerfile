#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ETicaret.Baskets/ETicaret.Baskets.csproj", "ETicaret.Baskets/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
RUN dotnet restore "ETicaret.Baskets/ETicaret.Baskets.csproj"
COPY . .
WORKDIR "/src/ETicaret.Baskets"
RUN dotnet build "ETicaret.Baskets.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ETicaret.Baskets.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ETicaret.Baskets.dll"]