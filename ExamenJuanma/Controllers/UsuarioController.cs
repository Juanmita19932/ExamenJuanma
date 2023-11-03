using ExamenJuanma.Models;
using ExamenJuanManuelMuñiz.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly HttpClient _httpClient;
    public List<Usuario> listaUsuario = new List<Usuario>();
    public UsuarioController(ILogger<UsuarioController> logger, DemoContext context)
    {
        _logger = logger;
        _httpClient = new HttpClient();
    }

    [HttpGet]
    public ActionResult Login()
    {
        _logger.LogInformation("Entro en el Login()");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Usuario model)
    {
        var dbContext = HttpContext.RequestServices.GetService<DemoContext>();
        var usuario = dbContext.Usuarios.FirstOrDefault(u => u.Email == model.Email);


        if (usuario != null && usuario.Password == model.Password)
        {

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Email),

        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


            return RedirectToAction("Index", "Home");
        }

        ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";

        return View(model);

    }

    [HttpPost]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("MiCookie");
        Response.Headers["Cache-Control"] = "no-cache, no-store";
        Response.Headers["Pragma"] = "no-cache";
        Response.Headers["Expires"] = "-1";

        return RedirectToAction("Login", "Usuario");
    }

    [HttpGet]
    [Authorize]
    public IActionResult Menu()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Registro()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Registro(Usuario model)
    {
        if (ModelState.IsValid)
        {
            var dbContext = HttpContext.RequestServices.GetService<DemoContext>();


            var existingUser = dbContext.Usuarios.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
                return View(model);
            }

            dbContext.Usuarios.Add(model);
            dbContext.SaveChanges();

            return RedirectToAction("Login");
        }

        return View(model);
    }
}

