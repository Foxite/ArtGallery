// TODO image descriptions
// TODO layout design
// TODO next/previous in FSV
// TODO fix clicking on text in FSV closes it
// TODO explicit dark mode colors?
// TODO artist page

using ArtGallery.Data;
using ArtGallery.Frontend.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSingleton<IArtRepository, FilesystemArtRepository>();
builder.Services.Configure<FilesystemArtRepository.Options>(builder.Configuration.GetSection("FilesystemArtRepository"));
builder.Services.Configure<PageOptions>(builder.Configuration.GetSection("Page"));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
