using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface INotesRepository
    {
        Task<string> addNotes(NotesModel notes);
        string getNotes(int NotesId);
        Task<string> UpdateNotes(NotesModel notes);
        Task<string> TrashNotes(int NotesId);
        Task<string> UnTrashNotes(int NotesId);
        Task<string> DeleteNotes(int NotesId);
        Task<string> ArchieveNotes(int NotesId);
        Task<string> UnArchieveNotes(int NotesId);
        Task<string> PinNotes(int NotesId);
        Task<string> UnPinNotes(int NotesId);
    }
}
