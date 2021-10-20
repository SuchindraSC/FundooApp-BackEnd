// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Suchindra Chitnis"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using global::FundooManager.Interface;
    using global::FundooModel;
    using global::FundooRepository.Interface;

    /// <summary>
    /// class LabelManager
    /// </summary>
    public class LabelManager : ILabelManager
    {
        /// <summary>
        /// ILabelRepository repository
        /// </summary>
        private readonly ILabelRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelManager"/> class.
        /// </summary>
        /// <param name="repository">ILabelRepository repository</param>
        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the labels.
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns string after successfully adding label without notesId</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> AddLabels(LabelModel label)
        {
            try
            {
                return this.repository.AddLabels(label);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the labels.
        /// </summary>
        /// <param name="LabelId">integer LabelId</param>
        /// <returns>returns a string after deleting from home</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> DeleteLabels(int LabelId)
        {
            try
            {
                return this.repository.DeleteLabels(LabelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edits the labels.
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns a string after editing label</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> UpdateLabels(LabelModel labelModel)
        {
            try
            {
                return this.repository.UpdateLabels(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns a list for getting labels based on userID</returns>
        /// <exception cref="System.Exception"></exception>
        public List<LabelModel> GetLabels(int UserId)
        {
            try
            {
                return this.repository.GetLabels(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the label to notes.
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns a string after adding label from notes</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> AddLabelToNotes(LabelModel labelModel)
        {
            try
            {
                return this.repository.AddLabelToNotes(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">integer labelId</param>
        /// <returns>returns a string after deleting a label from note</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> RemoveLabel(int labelId)
        {
            try
            {
                return this.repository.RemoveLabel(labelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the labels by note.
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns>returns a list of label by notes</returns>
        /// <exception cref="System.Exception"></exception>
        public List<LabelModel> GetLabelsByNote(int notesId)
        {
            try
            {
                return this.repository.GetLabelsByNote(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the notesby label.
        /// </summary>
        /// <param name="LabelId">integer LabelId</param>
        /// <returns>returns a list of notes based on label</returns>
        /// <exception cref="System.Exception"></exception>
        public List<NotesModel> GetNotesbyLabel(int LabelId)
        {
            try
            {
                return this.repository.GetNotesbyLabel(LabelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
