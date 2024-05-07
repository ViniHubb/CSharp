using WebApplicationBase.Models;

namespace WebApplicationBase.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> SearchAllUsers();
        Task<UserModel> SearchById(int id);
        Task<UserModel> ToAdd(UserModel user);
        Task<UserModel> Update(UserModel user, int id);
        Task<bool> Delete(int id);
    }
}
