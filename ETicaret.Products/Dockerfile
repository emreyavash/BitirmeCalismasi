#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ETicaret.Products/ETicaret.Products.csproj", "ETicaret.Products/"]
RUN dotnet restore "ETicaret.Products/ETicaret.Products.csproj"
COPY . .
WORKDIR "/src/ETicaret.Products"
RUN dotnet build "ETicaret.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ETicaret.Products.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ETicaret.Products.dll"]