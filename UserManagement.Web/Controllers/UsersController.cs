using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List()
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

    [HttpGet("active")]
    public ViewResult ListActiveUsers()
    {
        var activeItems = _userService.GetAll().Where(p => p.IsActive).Select(p => new UserListItemViewModel
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
            Items = activeItems.ToList()
        };

        return View("List", model); 
    }

    [HttpGet("non-active")]
    public ViewResult ListNonActiveUsers()
    {
        var nonActiveItems = _userService.GetAll().Where(p => !p.IsActive).Select(p => new UserListItemViewModel
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
            Items = nonActiveItems.ToList()
        };

        return View("List", model);
    }

    // GET: users/details/5
    [HttpGet("details")]
    public ActionResult Details(int id)
    {
        var user = _userService.GetUserById(id).FirstOrDefault();

        var userViewModel = new UserListItemViewModel
        {
            Forename = user!.Forename,
            Surname = user!.Surname,
            Email = user!.Email,
            DateOfBirth = user!.DateOfBirth,
            IsActive = user!.IsActive
        };

        if (user == null)
        {
            return NotFound();
        }
        return View(userViewModel);
    }
}
