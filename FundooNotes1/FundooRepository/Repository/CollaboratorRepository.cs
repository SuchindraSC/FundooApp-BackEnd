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
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly UserContext userContext;

        public CollaboratorRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<string> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                var Owner = (from user in this.userContext.Users
                             join notes in this.userContext.Notes
                             on user.UserId equals notes.UserId
                             where notes.NotesId == collaborator.NotesId && user.Emailid == collaborator.SenderEmailid
                             select new { userId = user.UserId }).SingleOrDefault();

                if (Owner != null)
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
                return "Adding Collaborator Failed";
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }


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

        public List<CollaboratorModel> getCollaboratorNotes(int NotesId)
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
