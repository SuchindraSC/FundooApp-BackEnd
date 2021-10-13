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
        Task<string> UpdateNotes(NotesModel notes);
    }
}
