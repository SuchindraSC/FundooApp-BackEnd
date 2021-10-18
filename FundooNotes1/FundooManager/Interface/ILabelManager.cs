using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        public Task<string> addLabels(LabelModel labelModel);
        public Task<string> deleteLabels(int UserId, string labelName);
        public Task<string> updateLabels(LabelModel labelModel);
        public List<string> getLabels(int UserId);
        public Task<string> addLabelToNotes(LabelModel labelModel);
        public Task<string> removeLabel(int labelId, int notesId);
        public List<LabelModel> getLabelsByNote(int notesId);
        public List<NotesModel> getNotesbyLabel(int userId, string labelName);
    }
}
