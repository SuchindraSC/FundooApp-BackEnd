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


        public List<NotesModel> getNotes(int UserId)
        {
            try
            {
                var checkNote = this.userContext.Notes.Any(x => x.UserId == UserId);
                if (checkNote)
                {
                    var note = this.userContext.Notes.Where(x => x.UserId == UserId && x.Is_Trash == false && x.Is_Archieve == false).ToList();
                    return note;
                }
                else
                {
                    return null;
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

        public List<NotesModel> getTrashNotes(int UserId)
        {
            try
            {
                var checkTrash = this.userContext.Notes.Where(y => y.UserId == UserId && y.Is_Trash == true && y.Is_Archieve == false).ToList();
                if (checkTrash != null)
                {
                    return checkTrash;
                }
                else
                {
                    return null;
                }  
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UnTrashNotes(int NotesId)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == NotesId);
                if (note)
                {
                    var notes = this.userContext.Notes.Where(e => e.NotesId == NotesId).SingleOrDefault();
                    if (notes.Is_Trash == true)
                    {
                        notes.Is_Trash = false;
                        await this.userContext.SaveChangesAsync();
                        return "Notes Removed From Trash Successfully";
                    }
                    else
                    {
                        return "Note Not Present in Trash";
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

        public List<NotesModel> GetArchievedNotes(int UserId)
        {
            try
            {
                var checkArchieve = this.userContext.Notes.Where(y => y.UserId == UserId && y.Is_Trash == false && y.Is_Archieve == true).ToList();
                if (checkArchieve != null)
                {
                    return checkArchieve;
                }
                else
                {
                    return null;
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
                        await this.userContext.SaveChangesAsync();
                        return "Notes UnArchieved Successfully";
                    }
                    else
                    {
                        return "Notes Not In Archieve";
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

        public async Task<string> PinNotes(int NotesId)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == NotesId);
                if (note)
                {
                    var notes = this.userContext.Notes.Where(e => e.NotesId == NotesId).SingleOrDefault();
                    notes.Is_Trash = false;
                    notes.Is_Archieve = false;
                    notes.Is_Pin = true;
                    await this.userContext.SaveChangesAsync();
                    return "Notes Pinned Successfully";
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

        public async Task<string> UnPinNotes(int NotesId)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == NotesId);
                if (note)
                {
                    var checkPin = this.userContext.Notes.Where(x => x.NotesId == NotesId).SingleOrDefault();
                    if (checkPin.Is_Pin == true)
                    {
                        checkPin.Is_Pin = false;
                        await this.userContext.SaveChangesAsync();
                        return "Notes UnPinned Successfully";
                    }
                    else
                    {
                        return "Notes not Pinned";
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

        public async Task<string> AddRemainder(int NotesId, string time)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == NotesId);
                if (note)
                {
                    var addremainder = this.userContext.Notes.Where(x => x.NotesId == NotesId).SingleOrDefault();
                    addremainder.Reminder = time;
                    await this.userContext.SaveChangesAsync();
                    return "Remainder Added Successfully";
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

        public async Task<string> RemoveRemainder(int NoteId)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == NoteId);
                if (note)
                {
                    var checkremainder = this.userContext.Notes.Any(x => x.NotesId == NoteId && x.Reminder != null);
                    if (checkremainder)
                    {
                        var removeremainder = this.userContext.Notes.Where(x => x.NotesId == NoteId).SingleOrDefault();
                        removeremainder.Reminder = null;
                        await this.userContext.SaveChangesAsync();
                        return "Reminder Removed Successfully";
                    }
                    else
                    {
                        return $"Remainder For NoteId {NoteId} Not Available";
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

        public async Task<string> UpdateColorToNote(int NoteId, string color)
        {
            try
            {
                var note = this.userContext.Notes.Any(e => e.NotesId == NoteId);
                if (note)
                {
                    var addcolor = this.userContext.Notes.Where(x => x.NotesId == NoteId).SingleOrDefault();
                    if (addcolor != null)
                    {
                        addcolor.Color = color;
                        await this.userContext.SaveChangesAsync();
                        return "Color Added Successfully";
                    }
                    else
                    {
                        addcolor.Color = "White";
                        return "Default Color";
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
    }
}
