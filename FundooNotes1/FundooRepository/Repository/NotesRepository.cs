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
    public class NotesRepository : INotesRepository
    {
        private readonly UserContext userContext;

        public NotesRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<string> addNotes(NotesModel notes)
        {
            try
            {
                this.userContext.Notes.Add(notes);
                await this.userContext.SaveChangesAsync();
                return "Note Added Successfully";
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
