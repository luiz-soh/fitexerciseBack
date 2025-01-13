using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Domain.Configuration;
using FitExerciseBack.Setup;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configurando LoggerFactory e criando uma inst�ncia de ILogger
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});
var logger = loggerFactory.CreateLogger<Program>();

// Add services to the container.

builder.Services.AddControllers();

var accessKey = string.Empty;
var secretKey = string.Empty;

if (builder.Environment.IsProduction())
{
    logger.LogInformation("Ambiente de Producao detectado.");
    accessKey = builder.Configuration.GetSection("AwsAccessKeyId").Value!;
    secretKey = builder.Configuration.GetSection("AwsSecretAccessKey").Value!;
}
else
{
    var awsOptions = builder.Configuration.GetAWSOptions();
    builder.Services.AddDefaultAWSOptions(awsOptions);

    var storeChain = new CredentialProfileStoreChain();
    if (storeChain.TryGetAWSCredentials(awsOptions.Profile, out var awsCredentials))
    {
        logger.LogInformation("TryGetAWSCredentials entrou.");
        accessKey = awsCredentials.GetCredentials().AccessKey;
        secretKey = awsCredentials.GetCredentials().SecretKey;
    }
}

builder.Configuration.AddAmazonSecretsManager("us-east-1", "fitexercise", accessKey, secretKey);

builder.Services.Configure<Secrets>(builder.Configuration);

builder.Services.AddAuthenticationJWT(builder);
builder.Services.AddAWSService<IAmazonS3>();

// builder.Services.AddCors(options =>
//      {
//          options.AddPolicy("AllowAll",
//              builder =>
//              {
//                  builder
//                  .AllowAnyOrigin()
//                  .AllowAnyMethod()
//                  .AllowAnyHeader();
//              });
//      });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfig();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.RegisterService();

var app = builder.Build();

// app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();
//// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
