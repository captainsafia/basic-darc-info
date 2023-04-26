using Azure.Identity;
using BasicDarcInfo.Util;
using Microsoft.DotNet.Maestro.Client.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ClientFactory>();
builder.Services.AddScoped<DarcInfo>();
builder.Services.AddRazorPages();

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(new Uri("https://darc-info2.vault.azure.net/"), new DefaultAzureCredential());
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapGet("/", context =>
{
    context.Response.Redirect("darcinfo");
    return Task.CompletedTask;
});

app.Run();
