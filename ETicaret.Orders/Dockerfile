#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ETicaret.Orders/ETicaret.Orders.csproj", "ETicaret.Orders/"]
COPY ["ETicaret.Domain/Ordering.Domain.csproj", "ETicaret.Domain/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["Ordering.Application/Ordering.Application.csproj", "Ordering.Application/"]
COPY ["Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Ordering.Infrastructure/"]
RUN dotnet restore "ETicaret.Orders/ETicaret.Orders.csproj"
COPY . .
WORKDIR "/src/ETicaret.Orders"
RUN dotnet build "ETicaret.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ETicaret.Orders.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ETicaret.Orders.dll"]