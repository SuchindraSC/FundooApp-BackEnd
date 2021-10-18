using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository repository;
        public CollaboratorManager(ICollaboratorRepository repository)
        {
            this.repository = repository;
        }

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

        public List<CollaboratorModel> getCollaboratorNotes(int NotesId)
        {
            try
            {
                return this.repository.getCollaboratorNotes(NotesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
