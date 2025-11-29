#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
ARG TARGETARCH
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG TARGETARCH
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Expense.Tracking.Api/Expense.Tracking.Api.csproj", "Expense.Tracking.Api/"]
RUN dotnet restore "./Expense.Tracking.Api/Expense.Tracking.Api.csproj" -a $TARGETARCH
COPY . .
WORKDIR "/src/Expense.Tracking.Api"
RUN dotnet build "./Expense.Tracking.Api.csproj" -a $TARGETARCH -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
RUN dotnet publish "./Expense.Tracking.Api.csproj" --no-self-contained -a $TARGETARCH -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Expense.Tracking.Api.dll"]