using KenloadV2API.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace KenloadV2API.Utils.Authentication
{
    public interface IUserManager
    {
        SignUpResult SignUp(Users user);
        SignUpResult SignUp(Users user, string secret);
        Task SignIn(HttpContext httpContext, Users user, bool isPersistence = false);
        Task SignOut(HttpContext httpContext);
        int GetCurrentUserId(HttpContext httpContext);
        Users GetCurrentUser(HttpContext httpContext);
        Task<Users> FindUserByUsernameAsync(string username);
        Task<Users> FindUserByEmailAsync(string email);
        ValidateResult Validate(string userName, string password);
        ValidateResult Validate(string userName, string password, string secret);
    }

    public enum ValidateResultError
    {
        CredentialExpired,
        CredentialNotFound,
        SecretNotValid
    }

    public class ValidateResult
    {
        public Users User { get; set; }
        public bool Success { get; set; }
        public ValidateResultError? Error { get; set; }

        public ValidateResult(Users user = null, bool success = false, ValidateResultError? error = null)
        {
            this.User = user;
            this.Success = success;
            this.Error = error;
        }
    }


    public enum SignUpResultError
    {
        CredentialNotFound,
        UsernameExist,
        UserEmailExist
    }

    public class SignUpResult
    {
        public Users User { get; set; }
        public bool Success { get; set; }
        public SignUpResultError? Error { get; set; }

        public SignUpResult(Users user = null, bool success = false, SignUpResultError? error = null)
        {
            this.User = user;
            this.Success = success;
            this.Error = error;
        }
    }



}
