using LifeNotes.Entities;
using LifeNotes.Models;
using System.Threading.Tasks;

namespace LifeNotes.Services
{
    public interface ILoginService
    {
        /// <summary>
        /// Receive the user from database using user claims
        /// </summary>
        /// <param name="userClaims">An object which consists of email and password</param>
        Users GetUserOrDefault(LoginDTO userClaims);
    }
}