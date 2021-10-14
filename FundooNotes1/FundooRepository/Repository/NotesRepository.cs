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
                if (notes.Title == null || notes.Description == null || notes.Reminder == null)
                {
                    this.userContext.Notes.Add(notes);
                    await this.userContext.SaveChangesAsync();
                    return "Note Added Successfully";
                }
                else
                {
                    return "Adding Notes Failed";
                }
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


        public async Task<string> TrashNotes(int NotesId)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == NotesId);
                if (note)
                {
                    var notes = this.userContext.Notes.Where(e => e.NotesId == NotesId).SingleOrDefault();
                    notes.Is_Trash = true;
                    notes.Is_Archieve = false;
                    notes.Is_Pin = false;
                    await this.userContext.SaveChangesAsync();
                    return "Notes Sent To Trash Successfully";
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
