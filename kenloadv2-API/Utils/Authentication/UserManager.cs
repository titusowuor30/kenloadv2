/*using KenloadV2API.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using KenloadV2API.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace KenloadV2API.Utils.Authentication
{
    public class UserManager : IUserManager
    {
        private readonly Kenloadv2Data db;
        public UserManager(Kenloadv2Data _db)
        {
            this.db = _db;
        }
        public Users GetCurrentUser(HttpContext httpContext)
        {
            int currentUserId = this.GetCurrentUserId(httpContext);

            if (currentUserId == -1)
                return null;

            return this.db.Users.Find(currentUserId);
        }

        public int GetCurrentUserId(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return -1;

            Claim claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            //Claim claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "id");

            if (claim == null)
                return -1;

            int currentUserId;

            if (!int.TryParse(claim.Value, out currentUserId))
                return -1;

            return currentUserId;
        }

        public async Task SignIn(HttpContext httpContext, Users user, bool isPersistence = false)
        {
            ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), JwtBearerDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistence }
            );
        }

        private IEnumerable<Claim> GetUserClaims(Users user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Email, user.email)
            };
            //claims.AddRange(this.GetUserRoleClaims(user));
            return claims;
        }

        public async Task SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
        }

        public ValidateResult Validate(string userName, string password)
        {
            return this.Validate(userName, password, null);
        }

        public ValidateResult Validate(string userName, string password, string secret)
        {
            Users user = this.db.Users.FirstOrDefault(u => u.username == userName);
            if(user == null)
            {
                return new ValidateResult(success: false, error: ValidateResultError.CredentialNotFound);
            }
            if (!string.IsNullOrEmpty(secret))
            {
                byte[] salt = Convert.FromBase64String(user.description);
                string hash = PasswordHasher.ComputeHash(user.userpass, salt);

                if (user.userpass != hash)
                    return new ValidateResult(success: false, error: ValidateResultError.SecretNotValid);
            }

            return new ValidateResult(success: true, user: user);
        }

        public Task<Users> FindUserByUsernameAsync(string username)
        {
            //val res = await db.Users.FirstOrDefault(u => u.username == username);
            return null;
        }

        public SignUpResult SignUp(Users user)
        {
            return this.SignUp(user, null);
        }

        public SignUpResult SignUp(Users user, string secret)
        {
            Users _user = db.Users.FirstOrDefault(u => u.email == user.email);
            if (_user != null)
            {
                return new SignUpResult(success: false, error: SignUpResultError.UserEmailExist);
            }

            if (string.IsNullOrEmpty(secret))
            {
                byte[] salt = PasswordHasher.GenerateRandomSalt();
                string hash = PasswordHasher.ComputeHash(user.userpass, salt);

                user.userpass = hash;
                user.description = Convert.ToBase64String(salt);
            }


            this.db.Users.Add(user);
            this.db.SaveChanges();

            return new SignUpResult(success: true, user: user);
        }

        public async Task<Users> FindUserByEmailAsync(string email)
        {
            return await db.Users.FindAsync(1);
        }
    }
}
*/