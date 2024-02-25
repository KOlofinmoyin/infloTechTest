
using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

public class HomeController : Controller
{
    private readonly IUserService _userService;
    public HomeController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult Index()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            DateOfBirth = p.DateOfBirth,
            IsActive = p.IsActive
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }
}

//using UserManagement.Services.Domain.Interfaces;
//using UserManagement.Web.Models.Users;

//namespace UserManagement.WebMS.Controllers;

//public class HomeController : Controller
//{
//    [HttpGet]
//    public ViewResult Index() => View();
//}
