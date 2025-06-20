﻿using System.Security.Claims;

namespace BuyItPlatform.UserRatingApi.Services.IServices
{
    public interface IJwtTokenHandler
    {
        ICollection<Claim> ExtractTokenData(string token);
    }
}
