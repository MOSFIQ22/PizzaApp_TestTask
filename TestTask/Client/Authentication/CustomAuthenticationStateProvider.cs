using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace EPOS.BlazorClient.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService sessionStorage;
        private readonly HttpClient http;
        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage, HttpClient http)
        {
            this.sessionStorage = sessionStorage;
            this.http=http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
           var token = await sessionStorage.GetItemAsStringAsync("token");
            var identity = new ClaimsIdentity();
            http.DefaultRequestHeaders.Authorization = null;
            if(!string.IsNullOrEmpty(token) )
            {
                var storedToken = token.Replace("\"", "");
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token.Replace("\"", "")), "jwt");
                http.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer", storedToken);
            }
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }
        
        public  IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            var claims = new List<Claim>();
            if(keyValuePairs != null)
            {
                keyValuePairs.ToList()
                    .ForEach(kvp =>
                    {
                        Console.WriteLine(kvp.Key);
                        Console.WriteLine(kvp.Value.ToString());
                        string va = kvp.Value.ToString() ?? "";
                        if (kvp.Key== "name")
                        {
                              
                            claims.Add(new Claim(ClaimTypes.Name, va));
                            
                        }
                        else if (kvp.Key == "role")
                        {
                            claims.Add(new Claim(ClaimTypes.Role, va));
                        }
                        else
                        {
                            claims.Add(new Claim(kvp.Key, va));
                        }
                        
                    });
            }

            return claims;
        }

        private  byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
