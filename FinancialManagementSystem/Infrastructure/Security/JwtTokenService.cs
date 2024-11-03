using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Security
{
    public class JwtTokenService
    {
        private const string SecretKey = "gmostafa358@gmail.com"; // Change this to a secure key
        private readonly SymmetricSecurityKey _key;

        public JwtTokenService()
        {            
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        }

        public string GenerateToken4(string userName, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] { new Claim(ClaimTypes.NameIdentifier, userName) },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateToken(string userName, string role)
        {
            // Create the token header
            var header = new
            {
                alg = "HS256",
                typ = "JWT"
            };
            var headerJson = JsonSerializer.Serialize(header);
            var headerBase64 = Base64UrlEncode(Encoding.UTF8.GetBytes(headerJson));

            // Create the token payload
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Role, role)
            };
            var payload = new
            {
                sub = userName,
                role = role,
                exp = GetCurrentUnixTimeSeconds() + 3600 // Token expires in 1 hour
            };
            var payloadJson = JsonSerializer.Serialize(payload);
            var payloadBase64 = Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));

            // Create the signature
            var signature = CreateSignature(headerBase64, payloadBase64);
            var token = $"{headerBase64}.{payloadBase64}.{signature}";         

            return token; 
        }

        private string CreateSignature(string header, string payload)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey)))
            {
                var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes($"{header}.{payload}"));
                return Base64UrlEncode(signatureBytes);
            }
        }

        private string Base64UrlEncode(byte[] input)
        {
            var base64 = Convert.ToBase64String(input);
            return base64.Replace("+", "-").Replace("/", "_").Replace("=", "");
        }

        private string Base64UrlDecode(string input)
        {
            var base64 = input.Replace("-", "+").Replace("_", "/");
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }

        public Payload GetPayload(string token)
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
            {
                return null; 
            }

            var headerBase64 = parts[0];
            var payloadBase64 = parts[1];
            var signature = parts[2];
            
            var recreatedSignature = CreateSignature(headerBase64, payloadBase64);
           
            if (recreatedSignature != signature)
            {
                return null; 
            }
                    
            var payloadJson = Base64UrlDecode(payloadBase64);
            var payload = JsonSerializer.Deserialize<Payload>(payloadJson);
           
            if (payload == null)
            {
                return null;
            }

            return payload; 
        }

        public bool ValidateToken(string token)
        {
            var payload = GetPayload(token);
          
            if (payload.exp < GetCurrentUnixTimeSeconds())
            {
                return false; 
            }

            return true; 
        }

        private long GetCurrentUnixTimeSeconds()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }

        public class Payload
        {
            public string sub { get; set; }
            public string role { get; set; }
            public long exp { get; set; }           
        }
    }
}
