using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using KenloadV2API.Configuration;
using KenloadV2API.Models.DTOs.Requests;
using KenloadV2API.Models.DTOs.Responses;
using KenloadV2API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using KenloadV2API.Services;
using KenloadV2API.Data;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using System.Net;
using System.Net.Sockets;
using MimeKit;

namespace KenloadV2API.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")] // api/authManagement
    [ApiController]

    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
        private IEmailSender _mailSender;
        private readonly JwtConfig _jwtConfig;
        private readonly Kenloadv2Data _context;
        public AutoBackup _backup;

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public AuthManagementController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IConfiguration configuration,
            IEmailSender mailSender,
            Kenloadv2Data context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailSender = mailSender;
            _jwtConfig = optionsMonitor.CurrentValue;
            _context = context;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous, EnableCors("ServerPolicy")]


        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {

            if (ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                                "Email already in use"
                            },
                        Success = false
                    });
                }
                var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);

                    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                    var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                    var regmessage = new Message(new string[] { newUser.Email }, "Kenload v2 Auth - Account created successfully!", "An account with your email was created from the following Host:"+GetLocalIPAddress()+".Please contact kenload admin if it wasn't you!");
                    string url = $"{_configuration["AppUrl"]}/api/AuthManagement/ConfirmEmail?userid={newUser.Id}&token={validEmailToken}";
                    var message =new Message(new string[]{ newUser.Email.ToString() }, "Confirm your email", $"<h1>Kenload v2 Auth</h1>" +
                        $"<p>Please confirm your email by <a href='{url}'>Clicking here or Copy the link bellow  and paste on a browser tab\n</a></p>"+$"\n{url}");
                    _mailSender.SendEmail(regmessage);
                    _mailSender.SendEmail(message);

                    return Ok(new RegistrationResponse()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }

        [HttpPost("ConfirmEmail/{userId}/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest(new RegistrationResponse()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "Invalid payload"
                    },
                });

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return Ok(new RegistrationResponse()
                {
                    Success = true,
                });

            return BadRequest(new RegistrationResponse()
            {
                Success = false,
                Errors = new List<string>() {
                        "Email confirmation Failed!"
                    },
            });
        }

        [HttpPost("ForgotPassword/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest(new RegistrationResponse()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "User with such email not found in our databases!"
                    },
                });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}&token={validToken}";
            var message=new Message(new string[] {email},"Kenload v2 Auth - Reset Password", "Follow the instructions to reset your password\n" +
                "Click on the password reset link bellow to reset your password.\n" +
                new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("\n<a href={0}>click here</a>\nOr copy and paste the link below on a browser\n{0}", $"\n{url}\n") });
            _mailSender.SendEmail(message);

            return Ok("Success! Check your email for a password reset link!");
        }

        [HttpPost]
        [Route("ChangePassword")]
        [EnableCors("ServerPolicy")]
        public async Task<IActionResult> changePassword([FromBody] ChangePassword cp_user)
        {
            var user = await _userManager.FindByEmailAsync(cp_user.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, "Used does not exist!");

            if (String.Compare(cp_user.NewPassword, cp_user.ConfirmPassword) != 0)
                return StatusCode(StatusCodes.Status400BadRequest, "Passwords Don't Match!");
            var result = await _userManager.ChangePasswordAsync(user, cp_user.CurrentPassword, cp_user.NewPassword);

            if (!result.Succeeded)
            {
                var errors = new List<String>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error:" + errors);
            }
            var message = new Message(new string[] { user.Email }, "Kenload v2 Security - Password Changed", "Hi " + user.Email + ",Your password was changed.Host Machine:"+GetLocalIPAddress()+".Please login to Kenload to check the activity or contact system admin if it wasn't you.");
            _mailSender.SendEmail(message);
            return Ok("Password Changed successfully!");
        }
        //admin forgot password
        [HttpPost]
        [Route("AdminUserForgotPassword")]
        [EnableCors("ServerPolicy")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword pass)
        {
            var user = await _userManager.FindByEmailAsync(pass.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, "Used does not exist!");

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, pass.NewPassword);

            if (!passwordChangeResult.Succeeded)
            {
                var errors = new List<String>();

                foreach (var error in passwordChangeResult.Errors)
                {
                    errors.Add(error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error:" + errors);
            }
            var message = new Message(new string[] { user.Email }, "Kenload v2 Security - Password Changed by second party.", "Hi " + user.Email + ",Your password was changed.Host Machine:" + GetLocalIPAddress() + ".Please login to Kenload to check the activity or contact system admin if it wasn't you.");
            _mailSender.SendEmail(message);
            return Ok("User Password Reset successful!");
        }



        [HttpPost]
        [Route("Login")]
        [EnableCors("ServerPolicy")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            //check backup schedule
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                var disableduser = await _context.Users.Where(x => x.email == existingUser.Email && x.deletedstatus==true).FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                                "Invalid login request"
                            },
                        Success = false
                    });
                }
                if (disableduser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                                "User account has been disabled. Please contact admin for more information!"
                            },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (!isCorrect)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                                "Invalid login request"
                            },
                        Success = false
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser);
                var message=new Message(new string[] { user.Email }, "Kenload v2 Security - New user login from:"+GetLocalIPAddress(), "Hey!\nNew login to your account noticed\nNew login to your account at " + DateTime.Now + ".");
               _mailSender.SendEmail(message);

                return Ok(new RegistrationResponse()
                {
                    Success = true,
                    Token = jwtToken
                });
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }

}