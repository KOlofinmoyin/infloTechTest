using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    /// <summary>
    /// Gets the user with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The user with the specified ID, or null if not found.</returns>
    public IEnumerable<User> GetUserById(int id) => _dataAccess.GetAll<User>().Where(u => u.Id == id);

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">The user to add.</param>
    public void CreateUser(User user)
    {
        user.Id = _dataAccess.GetAll<User>().Max(u => u.Id) + 1;
        _dataAccess.Create(user);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The user with updated information.</param>
    public void UpdateUser(User user)
    {
        var existingUser = _dataAccess.GetAll<User>().FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            existingUser.Forename = user.Forename;
            existingUser.Surname = user.Surname;
            existingUser.Email = user.Email;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.IsActive = user.IsActive;

            _dataAccess.Update(existingUser);
        }
    }
}
