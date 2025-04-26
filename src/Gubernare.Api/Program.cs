using Guabernare.ServiceDefaults;
using Gubernare.Api.Extensions;

using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.AddDefaultOpenApi();
builder.AddServiceDefaults();

builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<DescriptionSchemaFilter>();
});
builder.AddConfiguration();
builder.AddDatabase();

builder.Services.AddCors(options => //TODO: TIRAR MAIS TARDE!!!
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()    
            .AllowAnyMethod()  
            .AllowAnyHeader();  
    });
});
builder.AddAccountContext();
builder.AddClientContext();
builder.AddLegalProceedingContext();


builder.AddJwtAuthentication();

builder.AddMediator();


var app = builder.Build();




app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAccountApiV1();
app.MapClientApiV1();
app.MapLegalProceedingApiV1();


app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    
    options.DefaultFonts = false;
});
app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();

app.UseCors("AllowAll");

app.Run();