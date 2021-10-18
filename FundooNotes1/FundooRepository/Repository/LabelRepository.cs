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

        public async Task<string> deleteLabels(int UserId, string labelName)
        {
            try
            {
                var labelexists = this.userContext.Labels.Where(x => x.LabelName == labelName && x.UserId == UserId).SingleOrDefault();
                if (labelexists != null)
                {
                    this.userContext.Labels.Remove(labelexists);
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
                string message = string.Empty;
                var exist = this.userContext.Labels.Where(x => x.LabelId == labelModel.LabelId).Select(x => x.LabelName).SingleOrDefault();
                var existOldLabel = this.userContext.Labels.Where(x => x.LabelName == exist && x.UserId == labelModel.UserId).ToList();
                var labelExists = this.userContext.Labels.Where(x => x.LabelName == labelModel.LabelName && x.UserId == labelModel.UserId && x.NotesId == null).SingleOrDefault();
                if (existOldLabel.Count > 0)
                {
                    message = "Updated Label";
                    if (labelExists != null)
                    {
                        this.userContext.Labels.Remove(labelExists);
                        await this.userContext.SaveChangesAsync();
                        return "Label Updated Successfully";
                    }

                    existOldLabel.ForEach(x => x.LabelName = labelModel.LabelName);
                    this.userContext.Labels.UpdateRange(existOldLabel);
                    await this.userContext.SaveChangesAsync();
                }
                return message;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public List<string> getLabels(int UserId)
        {
            try
            {
                var exist = this.userContext.Labels.Where(x => x.UserId == UserId).Select(x => x.LabelName).Distinct().ToList();
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

        public async Task<string> removeLabel(int labelId, int notesId)
        {
            try
            {
                var existsLabel = this.userContext.Labels.Where(x => x.LabelId == labelId && x.NotesId == notesId).SingleOrDefault();
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
                var exists = this.userContext.Labels.Where(x => x.NotesId == notesId).ToList();
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

        public List<NotesModel> getNotesbyLabel(int userId, string labelName)
        {
            try
            {
                var exists = (from notes in this.userContext.Notes
                              join label in this.userContext.Labels
                              on notes.NotesId equals label.NotesId
                              where userId == label.UserId && label.LabelName == labelName
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
