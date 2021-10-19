using FundooModel;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext userContext;

        public LabelRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<string> addLabels(LabelModel labelModel)
        {
            try
            {
                var labelExists = this.userContext.Labels.Where(x => x.LabelName == labelModel.LabelName && x.UserId == labelModel.UserId && x.NotesId == null).SingleOrDefault();
                if (labelExists == null)
                {
                    this.userContext.Labels.Add(labelModel);
                    await this.userContext.SaveChangesAsync();
                    return "Label Added Successfully";
                }
                return "Label Already Exists";
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<string> deleteLabels(int labelId)
        {
            try
            {
                var label = this.userContext.Labels.Where(x => x.LabelId == labelId).SingleOrDefault();
                var labelexists = this.userContext.Labels.Where(x => x.LabelName == label.LabelName && x.UserId == label.UserId).ToList();
                if (labelexists.Count > 0)
                {
                    this.userContext.Labels.RemoveRange(labelexists);
                    await this.userContext.SaveChangesAsync();
                    return "Label Deleted Successfully";
                }
                return "No Label Present";
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<string> updateLabels(LabelModel labelModel)
        {
            try
            {
                var labelexist = this.userContext.Labels.Where(x => x.LabelId == labelModel.LabelId).SingleOrDefault();
                var existOldLabel = this.userContext.Labels.Where(x => x.LabelName == labelexist.LabelName && x.UserId == labelexist.UserId).ToList();
                if (existOldLabel.Count > 0)
                {
                    //existOldLabel.ForEach(x => x.LabelName = labelModel.LabelName);
                    this.userContext.Labels.UpdateRange(existOldLabel);
                    await this.userContext.SaveChangesAsync();
                    return "Label Updated";
                }
                return "Label Update Failed";
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public List<LabelModel> getLabels(int UserId)
        {
            try
            {
                var exist = this.userContext.Labels.Where(x => x.UserId == UserId).Distinct().ToList();
                if (exist != null)
                {
                    return exist;
                }
                return null;
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<string> addLabelToNotes(LabelModel labelModel)
        {
            try
            {
                var exists = this.userContext.Labels.Where(x => x.LabelName == labelModel.LabelName && x.NotesId == labelModel.NotesId ).SingleOrDefault();
                if (exists == null)
                {
                    this.userContext.Labels.Add(labelModel);
                    await this.userContext.SaveChangesAsync();
                    labelModel.LabelId = 0;
                    labelModel.NotesId = null;
                    await this.addLabels(labelModel);
                    return "Added Label To Note";
                }

                return "Add Label To Notes failed";
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<string> removeLabel(int labelId)
        {
            try
            {
                var existsLabel = this.userContext.Labels.Where(x => x.LabelId == labelId).SingleOrDefault();
                if (existsLabel != null)
                {
                    this.userContext.Labels.Remove(existsLabel);
                    await this.userContext.SaveChangesAsync();
                    return "Label Removed Successfully";
                }
                return null;
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
                var listLabel = this.userContext.Labels.Where(x => x.NotesId == notesId).ToList();
                if (listLabel.Count > 0)
                {
                    return listLabel;
                }
                return null;
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
                var labelexists = this.userContext.Labels.Where(x => x.LabelId == LabelId).SingleOrDefault();
                var exists = (from label in this.userContext.Labels
                              join notes in this.userContext.Notes
                              on label.NotesId equals notes.NotesId
                              where labelexists.UserId == label.UserId && labelexists.LabelName == label.LabelName
                              select notes).ToList();

                if (exists.Count > 0)
                {
                    return exists;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
