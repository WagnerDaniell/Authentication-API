using ApiAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração de autenticação antes de Build()
var key = Encoding.ASCII.GetBytes(Settings.SecretKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Adiciona os serviços necessários (como controladores)
builder.Services.AddControllers();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
app.UseHttpsRedirection();

app.UseAuthentication();  // Ativa o middleware de autenticação
app.UseAuthorization();   // Ativa o middleware de autorização

app.MapControllers();     // Mapeia os controllers para que as rotas sejam acessíveis

app.Run();
