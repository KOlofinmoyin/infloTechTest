﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("UserManagement.Data.DataContext");

    protected override void OnModelCreating(ModelBuilder model)
        => model.Entity<User>().HasData(new[]
        {
            new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", DateOfBirth = new DateOnly(1985,12,09), IsActive = true },
            new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", DateOfBirth = new DateOnly(1955,10,28), IsActive = true },
            new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", DateOfBirth = new DateOnly(1965,04,01), IsActive = false },
            new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", DateOfBirth = new DateOnly(1960,02,19), IsActive = true },
            new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", DateOfBirth = new DateOnly(1996,01,12), IsActive = true },
            new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", DateOfBirth = new DateOnly(1987,05,02), IsActive = true },
            new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", DateOfBirth = new DateOnly(1997,07,29), IsActive = false },
            new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", DateOfBirth = new DateOnly(1980,08,10), IsActive = false },
            new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", DateOfBirth = new DateOnly(1989,06,12), IsActive = false },
            new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", DateOfBirth = new DateOnly(1978,03,11), IsActive = true },
            new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", DateOfBirth = new DateOnly(2000,11,19), IsActive = true },
        });

    public DbSet<User>? Users { get; set; }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => base.Set<TEntity>();

    public void Create<TEntity>(TEntity entity) where TEntity : class
    {
        base.Add(entity);
        SaveChanges();
    }

    public new void Update<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
        SaveChanges();
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        SaveChanges();
    }
}
