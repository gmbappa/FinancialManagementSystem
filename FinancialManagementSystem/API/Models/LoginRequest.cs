﻿
namespace API.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class TokenRequest
    {
        public string Token { get; set; }
    }
}