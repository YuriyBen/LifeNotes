using LifeNotes.Entities;
using LifeNotes.Models;
using System.Threading.Tasks;

namespace LifeNotes.Services
{
    public interface ILoginService
    {
        Task dsf(LoginDTO userClaims);
        Users GetUserOrDefault(LoginDTO userClaims);
    }
}