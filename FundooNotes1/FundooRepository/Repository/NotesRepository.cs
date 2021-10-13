using FundooModel;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly UserContext userContext;

        public NotesRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<string> addNotes(NotesModel notes)
        {
            try
            {
                this.userContext.Notes.Add(notes);
                await this.userContext.SaveChangesAsync();
                return "Note Added Successfully";
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateNotes(NotesModel notes)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == notes.NotesId);
                if(note)
                {
                    var updatedNotes = this.userContext.Notes.Where(x => x.NotesId == notes.NotesId).SingleOrDefault();
                    updatedNotes.Title = notes.Title;
                    updatedNotes.Description = notes.Description;
                    await this.userContext.SaveChangesAsync();
                    return "Notes Updated Successfully";
                }
                else
                {
                    return "Invalid Note Id";
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DelteNotes(NotesModel notes)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == notes.NotesId);
                if (note)
                {
                    var updatedNotes = this.userContext.Notes.Where(x => x.NotesId == notes.NotesId).SingleOrDefault();
                    updatedNotes = notes;
                    await this.userContext.SaveChangesAsync();
                    return "Notes Updated Successfull";
                }
                else
                {
                    return "Invalid Note Id";
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
