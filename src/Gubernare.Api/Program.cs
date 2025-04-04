using Gubernare.Api;
using Gubernare.Api.Extensions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<DescriptionSchemaFilter>();
});
builder.AddConfiguration();
builder.AddDatabase();

builder.AddAccountContext();
builder.AddClientContext();


builder.AddJwtAuthentication();

builder.AddMediator();


var app = builder.Build();

// Middleware pipeline configuration


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAccountApiV1();
app.MapClientApiV1();


app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    // Disable default fonts to avoid download unnecessary fonts
    options.DefaultFonts = false;
});
app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();



app.Run();
