﻿namespace TestTask.Shared.ViewModels
{
    public class UserSession
    {
        public string Token { get; set; } = default!;
        public DateTime Expiration { get; set; } =  default!;

    }
}
