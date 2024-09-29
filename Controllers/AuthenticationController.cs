using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GiftsStore.Data;
using GiftsStore.Models;
using System.Linq;

namespace GiftsStore
{
    //Controller
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthServies _authServies;
        public AuthenticationController(IAuthServies authServies)
        {
            _authServies = authServies;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<bool>> Register([FromBody] UserRegister userModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return await _authServies.Register(userModel);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<bool>> Login([FromBody] string userModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return await _authServies.Login(userModel);
        }
        [HttpPost]
        [Route("Otp")]
        public async Task<ActionResult<AuthModel>> CheckOtp([FromBody]UserLogin userLogin)
        {
            if (!ModelState.IsValid) { return new AuthModel { Message = "phone or code erorr" }; }
            return await _authServies.CheckOtp(userLogin);
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult<AuthModel>> RefreshToken([FromBody] string refToken)
        {
            return await _authServies.RefreshToken(refToken);
        }
        [HttpPost]
        [Route("RevokeToken")]
        public async Task<bool> RevokeToken([FromBody] string token)
        {
            return await _authServies.RevokeToken(token);
        }
        [HttpGet]
        [Route("CheckToken")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public bool CheckToken()
        {
            return true;
        }       
    }

    //Models
    public class AuthModel
    {
        public string? Message { get; set; }
        public bool IsAuthanticated { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public IList<string>? Roles { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireson { get; set; }
    }
    public class UserRegister
    {
        [Required]
        [Phone]
        public string? UserName { get; set; }
        [Required]
        public string? FullName { get; set; }
    }
    public class UserLogin
    {
        [Required]
        [Phone]
        public string? UserName { get; set; }
        [Required]
        public int Code { get; set; }
    }
    public class RefreshToken
    {
        [Key]
        public string? Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public string? Token { get; set; }
        [Required]
        public DateTime? Expirson { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [AllowNull]
        public DateTime? RevokedON { get; set; }
    }
    public class JWTValues
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public double DurationInDays { get; set; }
    }

    //Interface
    public interface IAuthServies
    {
        Task<ActionResult<bool>> Register(UserRegister userModel);
        Task<ActionResult<bool>> Login(string phone);
        Task<ActionResult<AuthModel>> CheckOtp(UserLogin userModel);
        Task<ActionResult<AuthModel>> RefreshToken(string token);
        Task<bool> RevokeToken(string token);
    }

    //AuthServies
    public class AuthServies : IAuthServies
    {
        private readonly UserManager<Person> _userManager;
        private readonly IOptions<JWTValues> _jwt;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;
        public AuthServies(UserManager<Person> userManager, IOptions<JWTValues> jwt, IEmailSender emailSender, ApplicationDbContext db)
        {
            _userManager = userManager;
            _jwt = jwt;
            _emailSender = emailSender;
            _db = db;
        }
        private async Task<JwtSecurityToken> CreateJwtSecurityToken(Person identityUser)
        {
            var userClaims = await _userManager.GetClaimsAsync(identityUser);
            var roles = await _userManager.GetRolesAsync(identityUser);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,identityUser.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("uid",identityUser.Id)
            }.Union(userClaims).Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Value.Key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Value.Issuer,
                audience: _jwt.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.Value.DurationInDays).ToLocalTime(),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        private RefreshToken GeneraterRefreshToken(string userId)
        {
            var randomNumber = new byte[32];
            using RNGCryptoServiceProvider genertor = new RNGCryptoServiceProvider();
            genertor.GetBytes(randomNumber);
            return new RefreshToken
            {
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                Expirson = DateTime.UtcNow.AddDays(1),
                Id = Guid.NewGuid().ToString(),
                Token = Convert.ToBase64String(randomNumber),
            };
        }
        public async Task<ActionResult<bool>> Register(UserRegister userModel)
        {
            if (userModel.UserName == null) { return false; }
            var u = await _userManager.FindByEmailAsync(userModel.UserName!);
            if (u is not null) { return false; }
            var user = new Person
            {
                UserName = userModel.UserName!,
                EmailConfirmed = true,
                FullName = userModel.UserName!,
                PhoneNumber = userModel.UserName,
                CreatePinCode = DateTime.Now,
                PinCode = RandomNumberGenerator.GetInt32(100000, 999999)
            };
            var res = await _userManager.CreateAsync(user);
            if (!res.Succeeded) { return false; }
            var newUser = await _userManager.FindByNameAsync(user.PhoneNumber);
            var refreshToken = GeneraterRefreshToken(newUser!.Id);
            object value = await _db.RefreshTokens.AddAsync(refreshToken);
            await _userManager.AddToRoleAsync(user, "User");
            var token = await CreateJwtSecurityToken(user);
            await _userManager.UpdateAsync(user);
            return true;
        }
        public async Task<ActionResult<bool>> Login(string userLogin)
        {
            if (userLogin == null) { return false; }
            var user = await _userManager.FindByNameAsync(userLogin);
            if (user == null) { return false; }
            var token = await CreateJwtSecurityToken(user);
            List<RefreshToken> oldRefreshToken = await _db.RefreshTokens.Where(r => r.UserId == user.Id && r.RevokedON == null && DateTime.UtcNow >= r.Expirson).ToListAsync();
            foreach (var item in oldRefreshToken)
            {
                await RevokeToken(item.Token!);
            }
            var refreshToken = GeneraterRefreshToken(user.Id);
            _db.RefreshTokens!.Add(refreshToken);
            user.PinCode = RandomNumberGenerator.GetInt32(100000, 999999);
            user.CreatePinCode = DateTime.Now;
            await _userManager.UpdateAsync(user);
            _db.SaveChanges();            
            return true;
        }
        public async Task<ActionResult<AuthModel>> RefreshToken(string token)
        {
            var authModel = new AuthModel();
            var tok = await _db.RefreshTokens.SingleOrDefaultAsync(r => r.Token == token);
            if (tok == null)
            {
                authModel.Message = "InValid token";
                return authModel;
            }
            if (!(tok.RevokedON == null && DateTime.UtcNow <= tok.Expirson))
            {
                authModel.Message = "Inactive token";
                return authModel;
            }
            tok.RevokedON = DateTime.UtcNow;
            var newRefreshToken = GeneraterRefreshToken(tok.UserId!);
            await _db.RefreshTokens!.AddAsync(newRefreshToken);
            _db.RefreshTokens.Update(tok);
            await _db.SaveChangesAsync();
            var user = await _userManager.FindByIdAsync(tok.UserId!);
            var jwtToken = await CreateJwtSecurityToken(user!);
            authModel.Message = "Every thing is ok";
            authModel.IsAuthanticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Email = user!.Email;
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Roles = roles.ToList();
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpireson = newRefreshToken.Expirson;
            return authModel;
        }
        public async Task<bool> RevokeToken(string token)
        {
            var toke = await _db.RefreshTokens.SingleOrDefaultAsync(t => t.Token == token);
            if (toke == null) return false;
            if (!(toke.RevokedON == null && DateTime.UtcNow <= toke.Expirson)) return false;
            toke.RevokedON = DateTime.UtcNow;
            _db.RefreshTokens.Update(toke);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<ActionResult<AuthModel>> CheckOtp(UserLogin userLogin)
        {
            if (userLogin == null) { return new AuthModel { Message = "user is null" }; }
            var user = await _userManager.FindByNameAsync(userLogin.UserName!);
            if (user == null) { return new AuthModel { Message = "phone not fond" }; }
            if(userLogin.Code != user.PinCode ) { return new AuthModel { Message = "pincode incorrect" }; }
            if(user.CreatePinCode.AddMinutes(5) < DateTime.Now) { return new AuthModel { Message = "time out" }; }
            var token = await CreateJwtSecurityToken(user);
            List<RefreshToken> oldRefreshToken = await _db.RefreshTokens.Where(r => r.UserId == user.Id && r.RevokedON == null && DateTime.UtcNow >= r.Expirson).ToListAsync();
            foreach (var item in oldRefreshToken)
            {
                await RevokeToken(item.Token!);
            }
            var refreshToken = GeneraterRefreshToken(user.Id);
            _db.RefreshTokens!.Add(refreshToken);
            _db.SaveChanges();
            var rutToken = new AuthModel
            {
                Message = "Every thing is ok",
                IsAuthanticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Roles = await _userManager.GetRolesAsync(user),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpireson = refreshToken.Expirson,
                Phone = userLogin.UserName,
            };
            return rutToken;
        }
    }

    //Seed
    public class Seed
    {
        public static void Setting(WebApplicationBuilder builder)
        {
            builder.Services.Configure<JWTValues>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt => {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
                };
            });
            builder.Services.AddIdentity<Person, IdentityRole>(opt =>
            {
                //SingIn
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedPhoneNumber = false;
                opt.SignIn.RequireConfirmedAccount = false;
                //Password
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();
            builder.Services.AddScoped<IAuthServies, AuthServies>();
        }
        public static async Task AddRoll(IServiceProvider provider, List<string> roles)
        {
            var scopFactory = provider.GetRequiredService<IServiceScopeFactory>();
            var role = scopFactory.CreateScope();
            var ro = role.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (string roleName in roles)
            {
                if (!await ro.RoleExistsAsync(roleName))
                {
                    IdentityRole rol = new IdentityRole { Name = roleName, NormalizedName = roleName };
                    await ro.CreateAsync(rol);
                }
            }
        }
        public static async Task AddAdmin(IServiceProvider provider, string email)
        {
            var scopFactory = provider.GetRequiredService<IServiceScopeFactory>();
            var user = scopFactory.CreateScope();
            var us = user.ServiceProvider.GetRequiredService<UserManager<Person>>();
            if (await us.FindByNameAsync(email) == null)
            {
                Person rol = new Person
                {
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true,
                    FullName = "SuperAdmin",
                    PhoneNumber = "+963956108642"
                };
                await us.CreateAsync(rol, "Qweasd12#");
            }

        }
        public static async Task AddRegion(IServiceProvider provider)
        {
            var scopFactory = provider.GetRequiredService<IServiceScopeFactory>();
            var R = scopFactory.CreateScope();
            var RR = R.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var RRR = await RR.Regions.ToListAsync();
            List<Region> regions = new List<Region>
            {
                new() { Name = "دمشق", Enabled = true },
                new() { Name = "حلب", Enabled = true },
                new() { Name = "حماه", Enabled = true },
            };
            foreach (Region region in regions) {
                if (RRR.Where(a=>a.Name == region.Name).Count()==0) {
                   await RR.Regions.AddAsync(region);
                }                
            }
            await RR.SaveChangesAsync();
        }
    }
}


