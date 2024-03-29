﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Blog.Models;

using Microsoft.IdentityModel.Tokens;

namespace Blog.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "mateusnadal"),
                new Claim(ClaimTypes.Role,"admin" ),
                new Claim("", "")
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}