using Ir.Persistance;
using ir.infrastructure;
using ir.infrastructure.DTOs.CustomerDtos;
using ir.infrastructure.Repo.Infrastructure;
using Microsoft.AspNetCore.Builder;
using ir.infrastructure.DTOs.LeadDtos;
using ir.infrastructure.DTOs.OpportunityDtos;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddInfrastructureService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/customers", async (ICustomerService customerService) =>
{
    return await customerService.GetAllAsync();
});

app.MapGet("/customers/{id}", async (ICustomerService customerService, int id) =>
{
    return await customerService.GetByIdAsync(id);
});

app.MapPost("/customers", async (ICustomerService customerService, CustomerCreateDto createDto) =>
{
    return await customerService.AddAsync(createDto);
});

app.MapPut("/customers/{id}", async (ICustomerService customerService, int id, CustomerCreateDto updateDto) =>
{
    return await customerService.UpdateAsync(id, updateDto);
});

app.MapDelete("/customers/{id}", async (ICustomerService customerService, int id) =>
{
    await customerService.DeleteAsync(id);
    return Results.Ok($"{id} is deleted");
});

app.MapGet("/customers/{id}/leads", async (ICustomerService customerService, int id) =>
{
    return await customerService.GetLeadsofCustomersByIdAsync(id);
});






app.MapGet("/leads", async (ILeadService leadService) =>
{
    return await leadService.GetAllAsync();
});

app.MapGet("/leads/{id}", async (ILeadService leadService, int id) =>
{
    return await leadService.GetByIdAsync(id);
});
app.MapGet("/leads/{id}/with-opportunities", async (ILeadService leadService, int id) =>
{
    return await leadService.GetLeadWithOpportunitiesByIdAsync(id);
});
app.MapPost("/leads", async (ILeadService leadService, LeadCreateDto createDto) =>
{
    return await leadService.AddAsync(createDto);
});

app.MapPut("/leads/{id}", async (ILeadService leadService, int id, LeadCreateDto updateDto) =>
{
    return await leadService.UpdateAsync(id, updateDto);
});

app.MapDelete("/leads/{id}", async (ILeadService leadService, int id) =>
{
    await leadService.DeleteAsync(id);
    return Results.Ok();
});

app.MapPut("/leads/{id}/status", async (ILeadService leadService, int id, UpdateLeadStatusDto updateDto) =>
{
    updateDto.LeadId = id; 
    await leadService.UpdateLeadStatus(updateDto);
    return Results.Ok();
});

// Opportunity API Endpoints
app.MapGet("/opportunities", async (IOpportunityService opportunityService) =>
{
    return await opportunityService.GetAllAsync();
});

app.MapGet("/opportunities/{id}", async (IOpportunityService opportunityService, int id) =>
{
    return await opportunityService.GetByIdAsync(id);
});

app.MapPost("/opportunities", async (IOpportunityService opportunityService, OpportunityCreateDto createDto) =>
{
    return await opportunityService.AddAsync(createDto);
});

app.MapPut("/opportunities/{id}", async (IOpportunityService opportunityService, int id, OpportunityCreateDto updateDto) =>
{
    return await opportunityService.UpdateAsync(id, updateDto);
});

app.MapDelete("/opportunities/{id}", async (IOpportunityService opportunityService, int id) =>
{
    await opportunityService.DeleteAsync(id);
    return Results.Ok();
});
app.Run();


