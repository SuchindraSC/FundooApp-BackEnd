// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorRepository.cs" company="Bridgelabz">
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
    /// class CollaboratorRepository
    /// </summary>
    public class CollaboratorRepository : ICollaboratorRepository
    {
        /// <summary>
        /// UserContext userContext
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorRepository"/> class.
        /// </summary>
        /// <param name="userContext">UserContext userContext</param>
        public CollaboratorRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">CollaboratorModel collaborator</param>
        /// <returns>returns the string after adding collaborator</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<string> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                var receiverEmailExist = this.userContext.Collaborators.Where(x => x.NotesId == collaborator.NotesId && x.ReceiverEmailid == collaborator.ReceiverEmailid).SingleOrDefault();
                if (receiverEmailExist == null)
                {
                    this.userContext.Add(collaborator);
                    await this.userContext.SaveChangesAsync();
                    return "Collaborator Added Successfully";
                }
                return "Receiver Email Already Exists";
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="CollaboratorId">integer CollaboratorId</param>
        /// <returns>returns the string after removing the collaborator</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<string> RemoveCollaborator(int CollaboratorId)
        {
            try
            {
                var receiverEmailExist = this.userContext.Collaborators.Where(x => x.CollaboratorId == CollaboratorId).SingleOrDefault();
                if (receiverEmailExist != null)
                {
                    this.userContext.Collaborators.Remove(receiverEmailExist);
                    await this.userContext.SaveChangesAsync();
                    return "Collaborator Removed Successfully";
                }
                return $"Collaborator Id {CollaboratorId} doesn't Exists";
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
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
                var checkNote = this.userContext.Collaborators.Any(x => x.NotesId == NotesId);
                if (checkNote)
                {
                    var collaborator = this.userContext.Collaborators.Where(x => x.NotesId == NotesId && x.ReceiverEmailid != null).ToList();
                    return collaborator;
                }
                else
                {
                    return null;
                }

            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
