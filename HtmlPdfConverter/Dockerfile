#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HtmlPdfConverter/HtmlPdfConverter.csproj", "HtmlPdfConverter/"]
RUN dotnet restore "HtmlPdfConverter/HtmlPdfConverter.csproj"
COPY . .
WORKDIR "/src/HtmlPdfConverter"
RUN dotnet build "HtmlPdfConverter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HtmlPdfConverter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HtmlPdfConverter.dll"]

RUN apt update && apt install -y chromium

