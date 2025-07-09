using ArtGallery.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//builder.Services.AddSingleton<IArtRepository, TestArtRepository>();
builder.Services.AddSingleton<IArtRepository, FilesystemArtRepository>();
builder.Services.Configure<FilesystemArtRepository.Options>(builder.Configuration.GetSection("FilesystemArtRepository"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
