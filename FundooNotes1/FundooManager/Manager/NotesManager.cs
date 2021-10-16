using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class NotesManager : INoteManager
    {
        private readonly INotesRepository repository;
        public NotesManager(INotesRepository repository)
        {
            this.repository = repository;
        }

        public Task<string> addNotes(NotesModel note)
        {
            try
            {
                return this.repository.addNotes(note);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> getNotes(int UserId)
        {
            try
            {
                return this.repository.getNotes(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public List<NotesModel> getTrashNotes(int UserId)
        {
            try
            {
                return this.repository.getTrashNotes(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public Task<string> AddRemainder(int NotesId, string time)
        {
            try
            {
                return this.repository.AddRemainder(NotesId, time);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public Task<string> RemoveRemainder(int NoteId)
        {
            try
            {
                return this.repository.RemoveRemainder(NoteId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

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
