using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
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

        public string getNotes(int NotesId)
        {
            try
            {
                return this.repository.getNotes(NotesId);
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
    }
}
