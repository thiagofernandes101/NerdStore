﻿using NerdStore.Catalog.Application.AutoMapper;
using NerdStore.WebApp.Components;
using NerdStore.WebApp.Extensions.Configurations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddApplicationDependencies();
builder.Services.AddInfrastructureDependencies();
builder.Services.AddDomainDependencies();

builder.Services.AddAutoMapper(typeof(DomainEntityToDtoMappingProfile), typeof(ViewModelToDomainEntityMappingProfile));
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
