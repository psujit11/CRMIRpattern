using Ir.Persistance;
using ir.infrastructure;
using ir.infrastructure.DTOs.CustomerDtos;
using ir.infrastructure.Repo.Infrastructure;
using Microsoft.AspNetCore.Builder;
using ir.infrastructure.DTOs.LeadDtos;
using ir.infrastructure.DTOs.OpportunityDtos;
using ir.domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using ir.infrastructure.Repo.Services;
using ir.infrastructure.DTOs.User;
using Microsoft.AspNetCore.Authorization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddInfrastructureService();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddRoles<IdentityRole>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddScoped<IUserAccount, AccountService>();
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();



app.MapPost("/register", async (IUserAccount userAccount, UserDto userdto) =>
{
    return await userAccount.CreateAccount(userdto);


});

app.MapPost("/login", async (IUserAccount userAccount, LoginDto logindto) =>
{
    return await userAccount.LoginAccount(logindto);

});

app.MapPost("/addadmin",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
async (IUserAccount userAccount, UserDto userdto) =>
    {
        return await userAccount.AddNewAdmin(userdto);
    }

    );

app.MapGet("/customers",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin , User")]
async (ICustomerService customerService) =>
{
    return await customerService.GetAllAsync();
});

app.MapGet("/customers/{id}",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin , User")]
async (ICustomerService customerService, int id) =>
{
    return await customerService.GetByIdAsync(id);
});

app.MapPost("/customers",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin , User")]
async (ICustomerService customerService, CustomerCreateDto createDto) =>
{
    return await customerService.AddAsync(createDto);
});

app.MapPut("/customers/{id}",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin , User")]
async (ICustomerService customerService, int id, CustomerCreateDto updateDto) =>
{
    return await customerService.UpdateAsync(id, updateDto);
});

app.MapDelete("/customers/{id}",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
async (ICustomerService customerService, int id) =>
{
    await customerService.DeleteAsync(id);
    return Results.Ok($"{id} is deleted");
});

app.MapGet("/customers/{id}/leads",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")] 
async (ICustomerService customerService, int id) =>
{
    return await customerService.GetLeadsofCustomersByIdAsync(id);
});






app.MapGet("/leads", async (ILeadService leadService) =>
{
    return await leadService.GetAllAsync();
});

app.MapGet("/leads/{id}",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin , User")]
async (ILeadService leadService, int id) =>
{
    return await leadService.GetByIdAsync(id);
});
app.MapGet("/leads/{id}/with-opportunities", 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin , User")] 
async (ILeadService leadService, int id) =>
{
    return await leadService.GetLeadWithOpportunitiesByIdAsync(id);
});
app.MapPost("/leads", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
async (ILeadService leadService, LeadCreateDto createDto) =>
{
    return await leadService.AddAsync(createDto);
});

app.MapPut("/leads/{id}", 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")] 
async (ILeadService leadService, int id, LeadCreateDto updateDto) =>
{
    return await leadService.UpdateAsync(id, updateDto);
});

app.MapDelete("/leads/{id}",
     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
async (ILeadService leadService, int id) =>
{
    await leadService.DeleteAsync(id);
    return Results.Ok();
});

app.MapPut("/leads/{id}/status",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")] 
async (ILeadService leadService, int id, UpdateLeadStatusDto updateDto) =>
{
    updateDto.LeadId = id; 
    await leadService.UpdateLeadStatus(updateDto);
    return Results.Ok();
});

// Opportunity API Endpoints
app.MapGet("/opportunities",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
async (IOpportunityService opportunityService) =>
{
    return await opportunityService.GetAllAsync();
});

app.MapGet("/opportunities/{id}", 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
async (IOpportunityService opportunityService, int id) =>
{
    return await opportunityService.GetByIdAsync(id);
});

app.MapPost("/opportunities",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")] 
async (IOpportunityService opportunityService, OpportunityCreateDto createDto) =>
{
    return await opportunityService.AddAsync(createDto);
});

app.MapPut("/opportunities/{id}",

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")] async (IOpportunityService opportunityService, int id, OpportunityCreateDto updateDto) =>
{
    return await opportunityService.UpdateAsync(id, updateDto);
});

app.MapDelete("/opportunities/{id}",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")] 
async (IOpportunityService opportunityService, int id) =>
{
    await opportunityService.DeleteAsync(id);
    return Results.Ok();
});
app.Run();


