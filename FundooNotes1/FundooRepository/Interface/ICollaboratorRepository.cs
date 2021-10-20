// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Suchindra Chitnis"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using global::FundooModel;

    /// <summary>
    /// interface ICollaboratorRepository
    /// </summary>
    public interface ICollaboratorRepository
    {
        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">CollaboratorModel collaborator</param>
        /// <returns>returns the string after adding collaborator</returns>
        public Task<string> AddCollaborator(CollaboratorModel collaborator);

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="CollaboratorId">integer CollaboratorId</param>
        /// <returns>returns the string after removing the collaborator</returns>
        public Task<string> RemoveCollaborator(int CollaboratorId);

        /// <summary>
        /// Gets the collaborator notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns the list after passing NotesId</returns>
        public List<CollaboratorModel> GetCollaboratorNotes(int NotesId);
    }
}
