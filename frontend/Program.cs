var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(lo => lo.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1);
});

builder.WebHost.UseUrls("http://0.0.0.0:3000");

// Adiciona serviços ao container
builder.Services.AddRazorPages();

var app = builder.Build();

// Configura o HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAuthorization();
app.MapGet("/", () => Results.Redirect("index.html"));
app.MapRazorPages();

app.Run();
