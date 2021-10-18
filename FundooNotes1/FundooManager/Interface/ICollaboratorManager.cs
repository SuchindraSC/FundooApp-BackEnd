using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        Task<string> AddCollaborator(CollaboratorModel collaborator);
        Task<string> RemoveCollaborator(int CollaboratorId);
        List<CollaboratorModel> getCollaboratorNotes(int NotesId);
    }
}
