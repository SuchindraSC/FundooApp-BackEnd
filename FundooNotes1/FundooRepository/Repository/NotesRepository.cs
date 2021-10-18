using FundooModel;
using FundooRepository.Context;
using FundooRepository.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FundooRepository.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly UserContext userContext;
        public readonly IConfiguration Configuration;

        public NotesRepository(UserContext userContext, IConfiguration Configuration)
        {
            this.userContext = userContext;
            this.Configuration = Configuration;
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
                    notes.Is_Archieve = false;
                    notes.Is_Pin = false;
                    notes.Is_Trash = true;
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
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteNotes(int NotesId)
        {
            try
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
                    notes.Is_Pin = false;
                    notes.Is_Archieve = true;
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

        public List<NotesModel> GetReminderNotes(int UserId)
        {
            try
            {
                var reminderExists = this.userContext.Notes.Where(x => x.UserId == UserId && x.Is_Trash == false && x.Reminder != null).ToList();
                if(reminderExists != null)
                {
                    return reminderExists;
                }
                return null;
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
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateColorToNote(int NoteId, string color)
        {
            try
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
                    return null;
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> AddImage(int NoteId, IFormFile image)
        {
            try
            {
                var addImage = this.userContext.Notes.Where(x => x.NotesId == NoteId).SingleOrDefault();
                Account account = new Account(
                        this.Configuration["CloudinaryAccount:CloudName"],
                        this.Configuration["CloudinaryAccount:APIKey"],
                        this.Configuration["CloudinaryAccount:APISecret"]);
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadFile = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream())
                };
                var uploadResult = cloudinary.Upload(uploadFile);
                var uploadedImage = uploadResult.Url.ToString();

                if (addImage != null)
                {
                    addImage.Image = uploadedImage;
                    await this.userContext.SaveChangesAsync();
                    return "Image Added Successfully";
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

        public async Task<string> RemoveImage(int NoteId)
        {
            try
            {
                var imageExist = this.userContext.Notes.Where(x => x.NotesId == NoteId).SingleOrDefault();
                if (imageExist != null)
                {
                    imageExist.Image = null;
                    await this.userContext.SaveChangesAsync();
                    return "Image Removed Successfully";
                }
                return null;
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
