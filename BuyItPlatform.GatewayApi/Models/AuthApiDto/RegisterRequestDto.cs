﻿namespace BuyItPlatform.GatewayApi.Models.AuthApiDto
{
    public class RegisterRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
