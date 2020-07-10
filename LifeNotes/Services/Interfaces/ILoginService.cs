using LifeNotes.Entities;
using LifeNotes.Models;
using System.Threading.Tasks;

namespace LifeNotes.Services
{
    public interface ILoginService
    {
        Users GetUserOrDefault(LoginDTO userClaims);
    }
}