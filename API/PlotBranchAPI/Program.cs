using Microsoft.EntityFrameworkCore;
using PlotBranchAPI.Application.Services;
using PlotBranchAPI.Business.Services;
using PlotBranchAPI.Data;
using PlotBranchAPI.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GraphDbContext>(options =>
    options.UseInMemoryDatabase("GraphDb"));

builder.Services.AddScoped<IGraphRepository, GraphRepository>();
builder.Services.AddScoped<INodeRepository, NodeRepository>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IEdgeRepository, EdgeRepository>();

builder.Services.AddScoped<IGraphService, GraphService>();
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IEdgeService, EdgeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
