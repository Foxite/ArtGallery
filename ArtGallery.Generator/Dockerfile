FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ArtGallery.Generator/ArtGallery.Generator.csproj", "ArtGallery.Generator/"]
RUN dotnet restore "ArtGallery.Generator/ArtGallery.Generator.csproj"
COPY . .
WORKDIR "/src/ArtGallery.Generator"
RUN dotnet build "ArtGallery.Generator.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "ArtGallery.Generator.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
RUN apt-get update && \
    apt-get install -y --no-install-recommends ffmpeg
COPY --from=build /app/publish .
USER $APP_UID
CMD ["./ArtGallery.Generator"]
