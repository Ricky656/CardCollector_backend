FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5284
EXPOSE 7155

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY CardCollector_backend/CardCollector_backend.csproj ./CardCollector_backend/
RUN dotnet restore "CardCollector_backend/CardCollector_backend.csproj"
COPY . .
WORKDIR /src/CardCollector_backend
RUN dotnet publish -c ${BUILD_CONFIGURATION} -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app ./
RUN mkdir Data
ENTRYPOINT ["dotnet", "CardCollector_backend.dll"]