using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{

    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository repository;
        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }

        public Task<string> addLabels(LabelModel label)
        {
            try
            {
                return this.repository.addLabels(label);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> deleteLabels(int LabelId)
        {
            try
            {
                return this.repository.deleteLabels(LabelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> updateLabels(LabelModel labelModel)
        {
            try
            {
                return this.repository.updateLabels(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<LabelModel> getLabels(int UserId)
        {
            try
            {
                return this.repository.getLabels(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> addLabelToNotes(LabelModel labelModel)
        {
            try
            {
                return this.repository.addLabelToNotes(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> removeLabel(int labelId)
        {
            try
            {
                return this.repository.removeLabel(labelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<LabelModel> getLabelsByNote(int notesId)
        {
            try
            {
                return this.repository.getLabelsByNote(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> getNotesbyLabel(int LabelId)
        {
            try
            {
                return this.repository.getNotesbyLabel(LabelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
