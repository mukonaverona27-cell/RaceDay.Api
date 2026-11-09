using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;

namespace RaceDay.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IApiService _api;

        public AuthController(IApiService api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToRole(User.FindFirst(ClaimTypes.Role)?.Value);

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Please enter your email and password.";
                return View();
            }

            var (success, auth, error) = await TryPost<AuthResponse>("auth/login", new { email, password });

            if (!success || auth == null || string.IsNullOrEmpty(auth.Token))
            {
                ViewBag.Error = success ? "Invalid email or password." : error;
                return View();
            }

            await SignInAsync(auth, email);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRole(auth.Role);
        }

        [HttpGet]
        public IActionResult Register() =>
            User.Identity?.IsAuthenticated == true ? RedirectToRole(User.FindFirst(ClaimTypes.Role)?.Value) : View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string fullName, string email, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "All fields are required.";
                return View();
            }

            role = role == "Organiser" ? "Organiser" : "Participant";

            var (success, _, error) = await TryPost<object>("auth/register", new { fullName, email, password, role });

            if (!success)
            {
                ViewBag.Error = error;
                return View();
            }

            // Registration worked - send them to log in
            ViewBag.Message = "Account created! Please log in.";
            return View("Login");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        // ---------- helpers ----------

        private async Task<(bool success, T? data, string? error)> TryPost<T>(string endpoint, object body)
        {
            try
            {
                return await _api.PostAsync<T>(endpoint, body);
            }
            catch (HttpRequestException)
            {
                return (false, default, "Cannot reach the RaceDay API. Make sure it is running.");
            }
            catch (TaskCanceledException)
            {
                return (false, default, "The RaceDay API took too long to respond.");
            }
        }

        private async Task SignInAsync(AuthResponse auth, string email)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, auth.UserId),
                new(ClaimTypes.Name, string.IsNullOrEmpty(auth.FullName) ? email : auth.FullName),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, auth.Role),
                new("access_token", auth.Token)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = true });

            HttpContext.Session.SetString("Token", auth.Token);
        }

        private IActionResult RedirectToRole(string? role) =>
            string.Equals(role, "Organiser", StringComparison.OrdinalIgnoreCase)
                ? RedirectToAction("Dashboard", "Organiser")
                : RedirectToAction("Dashboard", "Participant");
    }
}
