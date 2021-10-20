// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorManager.cs" company="Bridgelabz">
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
    /// CollaboratorManager
    /// </summary>
    public class CollaboratorManager : ICollaboratorManager
    {
        /// <summary>
        /// ICollaboratorRepository repository
        /// </summary>
        private readonly ICollaboratorRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorManager"/> class.
        /// </summary>
        /// <param name="repository">ICollaboratorRepository repository</param>
        public CollaboratorManager(ICollaboratorRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collab">CollaboratorModel collab</param>
        /// <returns>returns string after adding collaborator</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> AddCollaborator(CollaboratorModel collab)
        {
            try
            {
                return this.repository.AddCollaborator(collab);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="CollaboratorId">integer CollaboratorId</param>
        /// <returns>returns the string after removing the collaborator</returns>
        /// <exception cref="System.Exception"></exception>
        public Task<string> RemoveCollaborator(int CollaboratorId)
        {
            try
            {
                return this.repository.RemoveCollaborator(CollaboratorId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the collaborator notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns the list after passing NotesId</returns>
        /// <exception cref="System.Exception"></exception>
        public List<CollaboratorModel> GetCollaboratorNotes(int NotesId)
        {
            try
            {
                return this.repository.GetCollaboratorNotes(NotesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
