using LifeNotes.Entities;
using System.Threading.Tasks;

namespace LifeNotes.Services
{
    public interface IRegistrationService
    {
        Task SignOutUserAsync(Users userToCreate, string plainPassword);
    }
}