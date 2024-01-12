using Auth;
using ResponseModel.Common;
using SwaggerConfiguration;
using System.Net.Mime;
using TemplateAPIServices;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(x =>
{
    x.InvalidModelStateResponseFactory = context =>
    {
        var result = new ApiResult(context.ModelState);

        result.ContentTypes.Add(MediaTypeNames.Application.Json);
        result.ContentTypes.Add(MediaTypeNames.Application.Xml);
        return result;
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AuthDependencyInjection(builder.Configuration);
builder.Services.SwaggerDependencyInjection(builder.Configuration);

//services
builder.Services.AddTemplateServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
