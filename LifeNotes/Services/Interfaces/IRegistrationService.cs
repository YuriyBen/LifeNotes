using LifeNotes.Entities;
using System.Threading.Tasks;

namespace LifeNotes.Services
{
    public interface IRegistrationService
    {
        /// <summary>
        /// Register and add a new user to the database
        /// </summary>
        /// <param name="userToCreate">User to sign out </param>
        /// <param name="plainPassword">The plain text password</param>
        Task SignOutUserAsync(Users userToCreate, string plainPassword);
    }
}