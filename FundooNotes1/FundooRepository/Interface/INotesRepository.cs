// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INoteRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Suchindra Chitnis"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using global::FundooModel;

    /// <summary>
    /// interface INotesRepository
    /// </summary>
    public interface INotesRepository
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">NotesModel notesModel</param>
        /// <returns>returns string after adding notes</returns>
        public Task<string> AddNotes(NotesModel notesModel);

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after passing UserId</returns>
        public List<NotesModel> GetNotes(int UserId);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notes">NotesModel notes</param>
        /// <returns>returns string after updating notes</returns>
        public Task<string> UpdateNotes(NotesModel notes);

        /// <summary>
        /// Trash the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after adding notes to trash</returns>
        public Task<string> TrashNotes(int NotesId);

        /// <summary>
        /// Gets the trashed notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting notes</returns>
        public List<NotesModel> GetTrashNotes(int UserId);

        /// <summary>
        /// Untrash the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after returning the notes from trash</returns>
        public Task<string> UnTrashNotes(int NotesId);

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after deleting notes</returns>
        public Task<string> DeleteNotes(int NotesId);

        /// <summary>
        /// Empties the trash.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns string after empty trash</returns>
        public Task<string> EmptyTrash(int UserId);

        /// <summary>
        /// Archieves the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after notes sent to archieve</returns>
        public Task<string> ArchieveNotes(int NotesId);

        /// <summary>
        /// Unarchieve the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after returning notes from archieve</returns>
        public Task<string> UnArchieveNotes(int NotesId);

        /// <summary>
        /// Gets the archieved notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting notes from archieve</returns>
        public List<NotesModel> GetArchievedNotes(int UserId);

        /// <summary>
        /// Pins the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after pinning notes</returns>
        public Task<string> PinNotes(int NotesId);

        /// <summary>
        /// Unpin the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after unpinning notes</returns>
        public Task<string> UnPinNotes(int NotesId);

        /// <summary>
        /// Adds the remainder.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <param name="time">string time</param>
        /// <returns>returns string after adding reminder</returns>
        public Task<string> AddReminder(int NotesId, string time);

        /// <summary>
        /// Gets the reminder notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting reminder notes</returns>
        public List<NotesModel> GetReminderNotes(int UserId);

        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="NoteId">integer NoteId</param>
        /// <returns>returns string after removing reminder</returns>
        public Task<string> RemoveReminder(int NoteId);

        /// <summary>
        /// Updates the color to note.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <param name="color">string color</param>
        /// <returns>returns string after adding colors to note</returns>
        public Task<string> UpdateColorToNote(int NotesId, string color);

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="NoteId">integer NoteId</param>
        /// <param name="image">IFormFile image</param>
        /// <returns>returns string after adding image</returns>
        public Task<string> AddImage(int NoteId, IFormFile image);

        /// <summary>
        /// Removes the image.
        /// </summary>
        /// <param name="NoteId">int NoteId</param>
        /// <returns>returns string after removing image</returns>
        Task<string> RemoveImage(int NoteId);

    }
}
