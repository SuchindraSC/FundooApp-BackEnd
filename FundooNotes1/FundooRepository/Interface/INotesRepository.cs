using FundooModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface INotesRepository
    {
        Task<string> addNotes(NotesModel notes);
        List<NotesModel> getNotes(int UserId);
        Task<string> UpdateNotes(NotesModel notes);
        Task<string> TrashNotes(int NotesId);
        List<NotesModel> getTrashNotes(int UserId);
        Task<string> UnTrashNotes(int NotesId);
        Task<string> DeleteNotes(int NotesId);
        Task<string> ArchieveNotes(int NotesId);
        List<NotesModel> GetArchievedNotes(int UserId);
        Task<string> UnArchieveNotes(int NotesId);
        Task<string> PinNotes(int NotesId);
        Task<string> UnPinNotes(int NotesId);
        Task<string> AddRemainder(int NotesId, string time);
        List<NotesModel> GetReminderNotes(int UserId);
        Task<string> RemoveRemainder(int NoteId);
        Task<string> UpdateColorToNote(int NotesId, string color);
        Task<string> AddImage(int NoteId, IFormFile image);
        Task<string> RemoveImage(int NoteId);
       
    }
}
