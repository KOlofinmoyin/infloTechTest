using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;
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

    // GET: users/profile/5
    [HttpGet("profile")]
    public ActionResult Profile(int id)
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


    // GET: users/create
    [HttpGet("create")]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public ActionResult Create(UserListItemViewModel userViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                Forename = userViewModel.Forename ?? string.Empty,
                Surname = userViewModel.Surname ?? string.Empty,
                Email = userViewModel.Email ?? string.Empty,
                DateOfBirth = userViewModel.DateOfBirth,
                IsActive = userViewModel.IsActive,
            };
            _userService.CreateUser(user);

            return RedirectToAction(nameof(List));
        }
        return View(userViewModel);
    }

    // GET: users/edit/2
    [HttpGet("edit")]
    public ActionResult Edit(int id)
    {
        var user = _userService.GetUserById(id).FirstOrDefault();

        var userViewModel = new UserListItemViewModel
        {
            Id = user!.Id,
            Forename = user!.Forename,
            Surname = user!.Surname,
            Email = user!.Email,
            DateOfBirth = user!.DateOfBirth,
            IsActive = user!.IsActive
        };

        if (userViewModel == null)
        {
            return NotFound();
        }
        return View(userViewModel);
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, UserListItemViewModel userViewModel)
    {
        if (id != userViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var user = new User
            {
                Id = userViewModel.Id,
                Forename = userViewModel.Forename ?? string.Empty,
                Surname = userViewModel.Surname ?? string.Empty,
                Email = userViewModel.Email ?? string.Empty,
                DateOfBirth = userViewModel.DateOfBirth,
                IsActive = userViewModel.IsActive,
            };

            _userService.UpdateUser(user);

            return RedirectToAction(nameof(List));
        }
        return View(userViewModel);
    }

}
