﻿using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INoteManager
    {
        Task<string> addNotes(NotesModel notesModel);
        string getNotes(int NotesId);
        Task<string> UpdateNotes(NotesModel notes);
        Task<string> TrashNotes(int NotesId);
        Task<string> DeleteNotes(int NotesId);
    }
}
