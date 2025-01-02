using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChronoPiller.Authorization.Core.DTOs;
using ChronoPiller.Authorization.Core.Entities;
using ChronoPiller.Authorization.Core.Interface;
using ChronoPiller.Authorization.Core.Models.Filters;
using ChronoPiller.Authorization.Infrastructure.Database;
using ChronoPiller.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ChronoPiller.Authorization.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateUser(User? user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    private IQueryable<User?> ParseFilter(IQueryable<User?> collection, UserFilter filter)
    {
        if (filter.Email is not null)
            collection = collection.Where(u => u.Email == filter.Email);
        if (filter.Name is not null)
            collection = collection.Where(u => u.Name == filter.Name);
        if (filter.Surname is not null)
            collection = collection.Where(u => u.Surname == filter.Surname);
        if (filter.UserName is not null)
            collection = collection.Where(u => u.UserName == filter.UserName);

        return collection;
    }
    
    public Task<User?> GetUserByEmail(string email) =>
        _context.Users
            .FirstOrDefaultAsync(user => user.Email == email);

    public async Task DeleteUser(int id)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(us => us.Id == id);
        if (user is null)
        {
            throw new NotFoundException();
        }
        _context.Users.Remove(user);
    }

    public Task<User?> GetUserByName(string name)
    {
        return _context.Users
            .FirstOrDefaultAsync(user => user.Name == name);
    }

    public async Task DeleteUser(string email)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

        if (user is null)
            throw new NotFoundException();

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(UserUpdateModel model)
    {
        User? user = 
            await _context.Users.FirstOrDefaultAsync(us => us.Id == model.Id);
        if (user is null)
            return;
        
        user.Update(model);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetUserByFilter(UserFilter filter) => 
        await ParseFilter(_context.Users, filter).ToListAsync();

    public async Task<User> GetById(int id) => 
        await _context.Users.FirstOrDefaultAsync(us => us.Id == id);
}