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
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext userContext;

        public LabelRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<string> addLabels(LabelModel labelModel)
        {
            try
            {
                var labelExists = this.userContext.Labels.Where(x => x.LabelName == labelModel.LabelName && x.UserId == labelModel.UserId && x.NotesId == null).SingleOrDefault();
                if (labelExists == null)
                {
                    this.userContext.Labels.Add(labelModel);
                    await this.userContext.SaveChangesAsync();
                    return "Label Added Successfully";
                }
                return "Label Already Exists";
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<string> deleteLabels(int UserId, string labelName)
        {
            try
            {
                var labelexists = this.userContext.Labels.Where(x => x.LabelName == labelName && x.UserId == UserId).SingleOrDefault();
                if (labelexists != null)
                {
                    this.userContext.Labels.Remove(labelexists);
                    await this.userContext.SaveChangesAsync();
                    return "Label Deleted Successfully";
                }
                return "No Label Present";
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<string> updateLabels(LabelModel labelModel)
        {
            try
            {
                var labelexists = this.userContext.Labels.Where(x => x.LabelId == labelModel.LabelId && x.UserId == labelModel.UserId).SingleOrDefault();
                if (labelexists != null)
                {
                    labelexists.LabelName = labelModel.LabelName;
                    await this.userContext.SaveChangesAsync();
                    return "Label Updated Successfully";
                }
                return "No Label Present";
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public List<string> getLabels(int UserId)
        {
            try
            {
                var exist = this.userContext.Labels.Where(x => x.UserId == UserId).Select(x => x.LabelName).Distinct().ToList();
                if (exist != null)
                {
                    return exist;
                }
                return null;
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }
    }
}
