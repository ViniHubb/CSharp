using Microsoft.EntityFrameworkCore;
using WebApplicationBase.Data;
using WebApplicationBase.Models;
using WebApplicationBase.Repositories.Interfaces;

namespace WebApplicationBase.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskSystemDBContext _dbContext;
        public UserRepository(TaskSystemDBContext taskSystemDBContext)
        {
            _dbContext = taskSystemDBContext;
        }

        public async Task<UserModel> SearchById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UserModel>> SearchAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> ToAdd(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> Update(UserModel user, int id)
        {
            UserModel userById = await SearchById(id);

            if (userById == null) 
            {
                throw new Exception($"O usuario de ID: {id} nao foi localizado");
            }

            userById.Name = user.Name;
            userById.Email = user.Email;

            _dbContext.Users.Update(userById);
            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> Delete(int id)
        {
            UserModel userById = await SearchById(id);

            if (userById == null)
            {
                throw new Exception($"O usuario de ID: {id} nao foi localizado");
            }

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
