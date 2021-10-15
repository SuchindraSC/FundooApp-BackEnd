using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        Task<string> addNotes(NotesModel notesModel);
        List<NotesModel> getNotes(int UserId);
        Task<string> UpdateNotes(NotesModel notes);
        Task<string> TrashNotes(int NotesId);
        List<NotesModel> getTrashNotes(int UserId);
        Task<string> UnTrashNotes(int NotesId);
        Task<string> DeleteNotes(int NotesId);
        Task<string> ArchieveNotes(int NotesId);
        Task<string> UnArchieveNotes(int NotesId);
        List<NotesModel> GetArchievedNotes(int UserId);
        Task<string> PinNotes(int NotesId);
        Task<string> UnPinNotes(int NotesId);
    }
}
