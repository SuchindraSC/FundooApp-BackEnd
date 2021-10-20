// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Suchindra Chitnis"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using global::FundooModel;

    /// <summary>
    /// interface ILabelManager
    /// </summary>
    public interface ILabelManager
    {
        /// <summary>
        /// Adds the labels.
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns string after successfully adding label without notesId</returns>
        public Task<string> AddLabels(LabelModel labelModel);

        /// <summary>
        /// Deletes the labels.
        /// </summary>
        /// <param name="LabelId">integer LabelId</param>
        /// <returns>returns a string after deleting from home</returns>
        public Task<string> DeleteLabels(int LabelId);

        /// <summary>
        /// Edits the labels.
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns a string after editing label</returns>
        public Task<string> UpdateLabels(LabelModel labelModel);

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns a list for getting labels based on userID</returns>
        public List<LabelModel> GetLabels(int UserId);

        /// <summary>
        /// Adds the label to notes.
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>returns a string after adding label from notes</returns>
        public Task<string> AddLabelToNotes(LabelModel labelModel);

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">integer labelId</param>
        /// <returns>returns a string after deleting a label from note</returns>
        public Task<string> RemoveLabel(int labelId);

        /// <summary>
        /// Gets the labels by note.
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns>returns a list of label by notes</returns>
        public List<LabelModel> GetLabelsByNote(int notesId);

        /// <summary>
        /// Gets the notesby label.
        /// </summary>
        /// <param name="LabelId">integer LabelId</param>
        /// <returns>returns a list of notes based on label</returns>
        public List<NotesModel> GetNotesbyLabel(int LabelId);
    }
}
