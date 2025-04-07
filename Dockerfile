FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

RUN echo "--- Contents of / BEFORE COPY ---"
RUN ls -l /

COPY ../. /app/

WORKDIR /app

RUN echo "--- Contents of /app AFTER COPY ---"
RUN ls -l /app
RUN ls -l /app/CalculatorApi
RUN ls -l /app/BL
RUN ls -l /app/DAL

RUN dotnet restore CalculatorApi/CalculatorApi.csproj

RUN dotnet publish CalculatorApi/CalculatorApi.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "CalculatorApi.dll"]