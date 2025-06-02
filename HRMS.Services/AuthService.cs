using HRMS.Core.Entities;
using HRMS.Core.Interfaces;
using HRMS.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly HRMSDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(HRMSDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<string> RegisterAsync(string name, string email, string password, string role)
    {
        if (_context.Users.Any(u => u.Email == email)) return "User already exists.";

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User { FullName = name, Email = email, PasswordHash = passwordHash, Role = role };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return "Registered successfully.";
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return "Invalid credentials.";

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
