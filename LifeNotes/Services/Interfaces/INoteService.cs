using LifeNotes.Entities;
using System.Threading.Tasks;

namespace LifeNotes.Services
{
    public interface INoteService
    {
        Task CreateNoteAsync(Notes noteToCreate);
        Task<Notes> GetNoteByIdsAsync(long userId, int dateId);
        Task<bool> SaveAsync();
    }
}