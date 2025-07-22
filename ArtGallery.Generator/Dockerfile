FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG GIFSKI_VERSION=1.34.0
ENV GIFSKI_VERSION=${GIFSKI_VERSION}

WORKDIR /gifski
RUN apt-get update && apt-get install -y --no-install-recommends curl xz-utils && \
    curl -L -o gifski.tar.xz https://github.com/ImageOptim/gifski/releases/download/${GIFSKI_VERSION}/gifski-${GIFSKI_VERSION}.tar.xz && \
    tar --xz -xf gifski.tar.xz linux/gifski && \
    rm gifski.tar.xz

WORKDIR /src

COPY ["ArtGallery.Generator/ArtGallery.Generator.csproj", "ArtGallery.Generator/"]
RUN dotnet restore "ArtGallery.Generator/ArtGallery.Generator.csproj"
COPY . .
WORKDIR "/src/ArtGallery.Generator"
RUN dotnet build "ArtGallery.Generator.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "ArtGallery.Generator.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
COPY --from=build /gifski/linux/gifski /usr/local/bin/gifski
WORKDIR /app
RUN apt-get update && \
    apt-get install -y --no-install-recommends ffmpeg
COPY --from=build /app/publish .
USER $APP_UID
CMD ["./ArtGallery.Generator"]
