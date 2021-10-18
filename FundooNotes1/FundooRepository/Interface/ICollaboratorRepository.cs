using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ICollaboratorRepository
    {
        Task<string> AddCollaborator(CollaboratorModel collaborator);
        Task<string> RemoveCollaborator(int CollaboratorId);
        List<CollaboratorModel> getCollaboratorNotes(int NotesId);
    }
}
