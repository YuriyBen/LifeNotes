using LifeNotes.Entities;
using System.Threading.Tasks;

namespace LifeNotes.Services
{
    public interface INoteService
    {

        /// <summary>
        /// Create and add a new note to the database
        /// </summary>
        /// <param name="noteToCreate">Note to create</param>
        Task CreateNoteAsync(Notes noteToCreate);
        /// <summary>
        /// Get the note of current user from the database
        /// </summary>
        /// <param name="userId">Id of current user</param>
        /// <param name="dateId">The converted date of note</param>
        Task<Notes> GetNoteByIdsAsync(long userId, int dateId);
        /// <summary>
        /// Checks whether the note exists with the defined date 
        /// </summary>
        /// <param name="dateId">The converted date of note</param>
        Task<bool> NoteIsAlreadyExistsAsync(int dateId);
        /// <summary>
        /// Save changes
        /// </summary>
        Task<bool> SaveAsync();
    }
}