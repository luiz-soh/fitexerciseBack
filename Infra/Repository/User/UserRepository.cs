﻿using Domain.Base.Paginated;
using Domain.Configuration;
using Domain.DTOs.Authentication;
using Domain.DTOs.User;
using Domain.Entities.User;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Repository.User
{
    public class UserRepository : IUserRepository
    {

        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<Secrets> _secrets;

        public UserRepository(IOptions<Secrets> secrets)
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
            _secrets = secrets;
        }

        public async Task<UserDto> SignIn(SignInDto input)
        {
            using var contexto = new ContextBase(_optionsBuilder, _secrets);
            var user = await contexto.FitUser.Where(x => x.Username == input.Username &&
            x.Password == input.Password).FirstOrDefaultAsync();

            if (user != null)
                return new UserDto(user);
            else
                return new UserDto();
        }

        public async Task SignUp(SignUpDto input)
        {

            var user = new FitUser(input);
            using var contexto = new ContextBase(_optionsBuilder, _secrets);
            await contexto.FitUser.AddAsync(user);
            await contexto.SaveChangesAsync();
        }

        public async Task UpdateRefreshToken(int userId, string refreshToken)
        {
            using var contexto = new ContextBase(_optionsBuilder, _secrets);
            var user = await contexto.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.LastLogin = DateTime.UtcNow;
                contexto.FitUser.Update(user);
                await contexto.SaveChangesAsync();
            }
        }

        public async Task<UserDto> GetRefreshToken(string refreshToken, int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var user = await context.FitUser.Where(x => x.RefreshToken == refreshToken && x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
                return new UserDto(user);
            else
                return new UserDto();
        }

        public async Task<bool> UserAlreadyExists(string username, string email)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            return await context.FitUser.Where(u => u.Username == username || u.UserEmail == email).AnyAsync();
        }

        public async Task<bool> DeleteUser(int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
            {
                context.FitUser.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task AddUserEmail(string email, int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
            {
                user.LastLogin = DateTime.SpecifyKind(user.LastLogin, DateTimeKind.Utc);
                user.UserEmail = email;
                context.FitUser.Update(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<UserDto> GetUserData(int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
                return new UserDto(user);
            else
                return new UserDto();
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var user = await context.FitUser.Where(x => x.UserEmail != null &&
             x.UserEmail.ToLower() == email.ToLower()).FirstOrDefaultAsync();

            if (user != null)
                return new UserDto(user);
            else
                return new UserDto();
        }

        public async Task<bool> AddPasswordRecoveryCode(int id, string code)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);

            var user = await context.FitUser.Where(x => x.UserId == id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.LastLogin = DateTime.SpecifyKind(user.LastLogin, DateTimeKind.Utc);
                user.RecoverCode = code;
                context.Update(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeUserPassword(int userId, string password)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);

            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
            {
                user.LastLogin = DateTime.SpecifyKind(user.LastLogin, DateTimeKind.Utc);
                user.Password = password;
                context.Update(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> GetUserIdByRecoverCode(string code)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var user = await context.FitUser.Where(x => x.RecoverCode == code).FirstOrDefaultAsync();

            if (user != null)
            {
                user.LastLogin = DateTime.SpecifyKind(user.LastLogin, DateTimeKind.Utc);
                user.RecoverCode = null;
                context.Update(user);
                await context.SaveChangesAsync();
                return user.UserId;
            }
            else
                return 0;
        }

        public async Task<PaginatedDto<UserDto>> GetUsersByGymId(int gymId, int? perPage, int? page, string orderBy, string order, string? search)
        {
            perPage ??= 10;
            page ??= 0;
            var skip = perPage * page;

            using var context = new ContextBase(_optionsBuilder, _secrets);

            var query = context.FitUser
                .Where(x => x.GymId == gymId || (gymId == 1 && x.GymId == null));

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.ToLower()) ||
                (x.UserEmail != null && x.UserEmail.ToLower().Contains(search.ToLower())));
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                if (order?.ToLower() == "desc")
                {
                    query = orderBy.ToLower() switch
                    {
                        "name" => query.OrderByDescending(x => x.Username),
                        "id" => query.OrderByDescending(x => x.UserId),
                        "email" => query.OrderByDescending(x => x.UserEmail),
                        _ => query.OrderByDescending(x => x.UserId)
                    };
                }
                else
                {
                    query = orderBy.ToLower() switch
                    {
                        "name" => query.OrderBy(x => x.Username),
                        "id" => query.OrderBy(x => x.UserId),
                        "email" => query.OrderBy(x => x.UserEmail),
                        _ => query.OrderBy(x => x.UserId)
                    };
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.UserId);
            }

            var usersDtoQuery = query.Select(u => new UserDto(u));

            var total = await query.CountAsync();
            var listUsers = await usersDtoQuery.Skip(skip ?? 0).Take(perPage ?? 10).ToListAsync();

            return new PaginatedDto<UserDto>(total, listUsers);
        }

        public async Task<bool> UserBelongsToGym(int userId, int gymId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);

            return await context.FitUser.AnyAsync(x => x.UserId == userId && (x.GymId == gymId || (gymId == 1 && x.GymId == null)));
        }

        public async Task UpdateNextChangeByUserId(int userId, DateOnly nextChange)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);

            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                user.LastLogin = DateTime.SpecifyKind(user.LastLogin, DateTimeKind.Utc);
                user.NextChange = nextChange;
                context.FitUser.Update(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
