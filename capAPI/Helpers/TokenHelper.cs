using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace capAPI.Helpers
{
    public static class TokenHelper
    {
        public static string GenersteJWTToken(String Userid,String Roleid)
        {
            //INLIZATION HANDLER
            var JWTTokenHandler=new JwtSecurityTokenHandler();
            //Setup token key
            //1.long secrete 
            //2.convert secret to byte
            string secret = "skahdsuiapfgaeyzisgfyeisgfseryszsdjjbhdbhvytwhdsvkdghvz35734753bg4h34h5vlv654";
            var tokenByteskey=Encoding.UTF8.GetBytes(secret);
            //setup token Descriptier(Claims,Expiry,sigiture)
            var descriptore = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddHours(4),
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
            new Claim("userid",Userid),
            new  Claim("Roleid",Roleid)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenByteskey), SecurityAlgorithms.HmacSha256Signature)
            };
            //encoding data in to json format
            var tokenJason = JWTTokenHandler.CreateToken(descriptore);
            //encoding jason result as token string
            var token = JWTTokenHandler.WriteToken(tokenJason);
            return token;   
        }
        public static string IsValidToken(string token)
        {try
            {



                var JWTTokenHandler = new JwtSecurityTokenHandler();
                //Setup token key
                //1.long secrete 
                //2.convert secret to byte
                string secret = "skahdsuiapfgaeyzisgfyeisgfseryszsdjjbhdbhvytwhdsvkdghvz35734753bg4h34h5vlv654";
                var tokenByteskey = Encoding.UTF8.GetBytes(secret);
                var tokenValidatorparms = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenByteskey),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero

                };
                var tokenBase = JWTTokenHandler.ValidateToken(token, tokenValidatorparms, out SecurityToken validatedtoken);
                return "valid";
            }
            catch (Exception ex)
            {
                return $"invalid{ex.Message}";
            }
        }
    }
}
