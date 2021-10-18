using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes1.Controllers
{
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager manager;

        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
            
        }

        [HttpPost]
        [Route("api/addlabels")]
        public async Task<IActionResult> addLabels([FromBody] LabelModel label)
        {
            try
            {
                string resultMessage = await this.manager.addLabels(label);
                if (resultMessage.Equals("Label Added Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else if(resultMessage.Equals("Label Already Exists"))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/adeletelabels")]
        public async Task<IActionResult> deleteLabels(int UserId, string labelName)
        {
            try
            {
                string resultMessage = await this.manager.deleteLabels(UserId, labelName);
                if (resultMessage.Equals("Label Deleted Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/updatelabels")]
        public async Task<IActionResult> updateLabels([FromBody] LabelModel label)
        {
            try
            {
                string resultMessage = await this.manager.updateLabels(label);
                if (resultMessage.Equals("Label Updated Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getlabels")]
        public  IActionResult getLabels(int UserId)
        {
            try
            {
                List<string> data = this.manager.getLabels(UserId);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "Get Label Successful", Data = data });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Get Label Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
