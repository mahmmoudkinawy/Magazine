namespace Magazine.Controllers;

[Authorize(Roles = Constants.RoleAdmin)]
public class BaseController : Controller
{
}
