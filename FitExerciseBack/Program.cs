using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Domain.Configuration;
using FitExerciseBack.Setup;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);

var storeChain = new CredentialProfileStoreChain();

if (storeChain.TryGetAWSCredentials(awsOptions.Profile, out var awsCredentials))
{
    builder.Configuration.AddAmazonSecretsManager("us-east-1", "fitexercise", awsCredentials);
}

builder.Services.Configure<Secrets>(builder.Configuration);

builder.Services.AddAuthenticationJWT(builder);
builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfig();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.RegisterService();

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
