// TODO determine if jquery, bootstrap and other libraries are needed
// TODO nsfw warning dialog
// TODO image descriptions
// TODO selectively hide images
// TODO layout design
// TODO next/previous in FSV
// TODO fix clicking on text in FSV closes it
// TODO figure out why files dont update in frontend and static container when updating through sftp container

using ArtGallery.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSingleton<IArtRepository, FilesystemArtRepository>();
builder.Services.Configure<FilesystemArtRepository.Options>(builder.Configuration.GetSection("FilesystemArtRepository"));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
