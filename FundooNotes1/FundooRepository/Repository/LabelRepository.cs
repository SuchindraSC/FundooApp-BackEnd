// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Suchindra Chitnis"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::FundooModel;
    using global::FundooRepository.Context;
    using global::FundooRepository.Interface;

    /// <summary>
    /// Class Lebal Repository
    /// </summary>
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// UserContext userContext
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRepository"/> class.
        /// </summary>
        /// <param name="userContext"></param>
        public LabelRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        /// <summary>
        /// Adds label
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns string after successfully adding label without notesId</returns>
        public async Task<string> AddLabels(LabelModel labelModel)
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

        /// <summary>
        /// Delete the Labels
        /// </summary>
        /// <param name="labelId">integer labelId</param>
        /// <returns>returns a string after deleting from home</returns>
        public async Task<string> DeleteLabels(int labelId)
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

        /// <summary>
        /// Edit label name
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns a string after editing label</returns>
        public async Task<string> UpdateLabels(LabelModel labelModel)
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

        /// <summary>
        /// Gets Label Based on userId
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns a list for getting labels based on userID</returns>
        public List<LabelModel> GetLabels(int UserId)
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

        /// <summary>
        /// Add Label To Notes
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns a string after adding label from notes</returns>
        public async Task<string> AddLabelToNotes(LabelModel labelModel)
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
                    await this.AddLabels(labelModel);
                    return "Added Label To Note";
                }

                return "Add Label To Notes failed";
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        /// <summary>
        /// Removes the Label from Note
        /// </summary>
        /// <param name="labelId">integer labelId</param>
        /// <returns></returns>
        public async Task<string> RemoveLabel(int labelId)
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

        /// <summary>
        /// Get Labels By Note
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns></returns>
        public List<LabelModel> GetLabelsByNote(int notesId)
        {
            try
            {
                var Label = this.userContext.Labels.Where(x => x.NotesId == notesId).ToList();
                if (Label.Count > 0)
                {
                    return Label;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Notes by Label
        /// </summary>
        /// <param name="LabelId">integer LabelId</param>
        /// <returns></returns>
        public List<NotesModel> GetNotesbyLabel(int LabelId)
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
