FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ArtGallery.Frontend/ArtGallery.Frontend.csproj", "ArtGallery.Frontend/ArtGallery.Frontend.csproj"]
RUN dotnet restore "ArtGallery.Frontend/ArtGallery.Frontend.csproj"
COPY . .
WORKDIR "/src/ArtGallery.Frontend"
RUN dotnet build "ArtGallery.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "ArtGallery.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
EXPOSE 8080
WORKDIR /app
COPY --from=build /app/publish .
USER $APP_UID
CMD ["./ArtGallery.Frontend"]
