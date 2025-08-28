# ArtGallery
A simple frontend showing off art along with artist information, grouped by year or by artist.

For an active deployment, see https://art.foxite.me

## ArtGallery.Frontend
The Razor app which serves the frontend. It reads a json file and references images stored on an external server (such as nginx).

## ArtGallery.Generator
Scans a directory and produces a json file for Frontend to consume. It can also produce thumbnails for each image.
