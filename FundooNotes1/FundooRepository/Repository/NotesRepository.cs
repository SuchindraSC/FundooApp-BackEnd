using FundooModel;
using FundooRepository.Context;
using FundooRepository.Interface;
using StackExchange.Redis;
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
                if (notes.Title != null || notes.Description != null)
                {
                    this.userContext.Notes.Add(notes);
                    await this.userContext.SaveChangesAsync();
                    return "Note Added Successfully";
                }
                else
                {
                    return "Adding Note Failed";
                }
                
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string getNotes(int NotesId)
        {
            try
            {
                var checkNote = this.userContext.Notes.Any(x => x.NotesId == NotesId);
                if (checkNote)
                {
                    var note = this.userContext.Notes.Where(x => x.NotesId == NotesId).SingleOrDefault();
                    ConnectionMultiplexer connectionmultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionmultiplexer.GetDatabase();
                    database.StringSet(key: "Title", note.Title);
                    database.StringSet(key: "Description", note.Description);
                    database.StringSet(key: "notesId", note.NotesId.ToString());
                    return "Details of Note Are Given in Data";
                }
                else
                {
                    return "Invalid NotesId";
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

        public async Task<string> DeleteNotes(int NotesId)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == NotesId);
                if (note)
                {
                    var checkNote = this.userContext.Notes.Where(x => x.NotesId == NotesId).SingleOrDefault();
                    if (checkNote.Is_Trash == true)
                    {
                        this.userContext.Notes.Remove(checkNote);
                        await this.userContext.SaveChangesAsync();
                        return "Note Deleted Successfully";
                    }
                    else
                    {
                        return "Note is not present in Trash";
                    }
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

        public async Task<string> ArchieveNotes(int NotesId)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == NotesId);
                if (note)
                {
                    var notes = this.userContext.Notes.Where(e => e.NotesId == NotesId).SingleOrDefault();
                    notes.Is_Trash = false;
                    notes.Is_Archieve = true;
                    notes.Is_Pin = false;
                    await this.userContext.SaveChangesAsync();
                    return "Notes Archieved Successfully";
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

        public async Task<string> UnArchieveNotes(int NotesId)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == NotesId);
                if (note)
                {
                    var checkArchieve = this.userContext.Notes.Where(x => x.NotesId == NotesId).SingleOrDefault();
                    if (checkArchieve.Is_Archieve == true)
                    {
                        checkArchieve.Is_Archieve = false;
                    }
                    await this.userContext.SaveChangesAsync();
                    return "Notes UnArchieved Successfully";
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
