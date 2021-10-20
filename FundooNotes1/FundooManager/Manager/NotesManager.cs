// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Suchindra Chitnis"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using global::FundooManager.Interface;
    using global::FundooModel;
    using global::FundooRepository.Interface;

    /// <summary>
    /// class NotesManager
    /// </summary>
    public class NotesManager : INoteManager
    {
        /// <summary>
        /// INotesRepository repository
        /// </summary>
        private readonly INotesRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesManager"/> class.
        /// </summary>
        /// <param name="repository">INotesRepository repository</param>
        public NotesManager(INotesRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="note">NotesModel note</param>
        /// <returns>returns string after adding notes</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> AddNotes(NotesModel note)
        {
            try
            {
                return this.repository.AddNotes(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting notes</returns>
        /// <exception cref="System.Exception"></exception>
        public List<NotesModel> GetNotes(int UserId)
        {
            try
            {
                return this.repository.GetNotes(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="note">NotesModel note</param>
        /// <returns>returns string after updating notes</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> UpdateNotes(NotesModel note)
        {
            try
            {
                return this.repository.UpdateNotes(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Trash the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after adding notes to trash</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> TrashNotes(int NotesId)
        {
            try
            {
                return this.repository.TrashNotes(NotesId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the trashed notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting notes</returns>
        /// <exception cref="System.Exception"></exception>
        public List<NotesModel> GetTrashNotes(int UserId)
        {
            try
            {
                return this.repository.GetTrashNotes(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Untrash the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns> returns string after returning the notes from trash</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> UnTrashNotes(int NotesId)
        {
            try
            {
                return this.repository.UnTrashNotes(NotesId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after deleting notes</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> DeleteNotes(int NotesId)
        {
            try
            {
                return this.repository.DeleteNotes(NotesId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Archieves the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after notes sent to archieve</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> ArchieveNotes(int NotesId)
        {
            try
            {
                return this.repository.ArchieveNotes(NotesId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the archieved notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting notes from archieve</returns>
        /// <exception cref="System.Exception"></exception>
        public List<NotesModel> GetArchievedNotes(int UserId)
        {
            try
            {
                return this.repository.GetArchievedNotes(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Unarchieve the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after returning notes from archieve</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> UnArchieveNotes(int NotesId)
        {
            try
            {
                return this.repository.UnArchieveNotes(NotesId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Pins the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after pinning notes</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> PinNotes(int NotesId)
        {
            try
            {
                return this.repository.PinNotes(NotesId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Unpin the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after unpinning notes</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> UnPinNotes(int NotesId)
        {
            try
            {
                return this.repository.UnPinNotes(NotesId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Adds the remainder.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <param name="time">string time</param>
        /// <returns>returns string after adding reminder</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> AddReminder(int NotesId, string time)
        {
            try
            {
                return this.repository.AddReminder(NotesId, time);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the reminder notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting reminder notes</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<NotesModel> GetReminderNotes(int UserId)
        {
            try
            {
                return this.repository.GetReminderNotes(UserId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="NoteId">integer NoteId</param>
        /// <returns>returns string after removing reminder</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> RemoveReminder(int NoteId)
        {
            try
            {
                return this.repository.RemoveReminder(NoteId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Updates the color to note.
        /// </summary>
        /// <param name="NoteId">integer NoteId</param>
        /// <param name="color">string color</param>
        /// <returns>returns string after adding color</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> UpdateColorToNote(int NoteId, string color)
        {
            try
            {
                return this.repository.UpdateColorToNote(NoteId, color);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="NoteId">integer NoteId</param>
        /// <param name="image">IFormFile image</param>
        /// <returns>returns string after adding image</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> AddImage(int NoteId, IFormFile image)
        {
            try
            {
                return this.repository.AddImage(NoteId, image);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// Removes the image.
        /// </summary>
        /// <param name="NoteId">int NoteId</param>
        /// <returns>returns string after removing image</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<string> RemoveImage(int NoteId)
        {
            try
            {
                return this.repository.RemoveImage(NoteId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}
