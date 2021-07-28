using Microsoft.AspNetCore.Mvc;

namespace MyTestAzureB2C.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
