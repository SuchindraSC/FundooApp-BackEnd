using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes1.Controllers
{
    //[Authorize]
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
        [Route("api/deletelabels")]
        public async Task<IActionResult> deleteLabels(int LabelId)
        {
            try
            {
                string resultMessage = await this.manager.deleteLabels(LabelId);
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
                if (resultMessage == "Label Updated")
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
                List<LabelModel> data = this.manager.getLabels(UserId);
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

        [HttpPut]
        [Route("api/addlabeltonotes")]
        public async Task<IActionResult> addLabelToNotes([FromBody] LabelModel labelModel)
        {
            try
            {
                string resultMessage = await this.manager.addLabelToNotes(labelModel);
                if (resultMessage.Equals("Added Label To Note"))
                {
                    return this.Ok(new ResponseModel<string>(){ Status = true, Message = resultMessage});
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
        [Route("api/removelabel")]
        public async Task<IActionResult> removeLabel(int labelId)
        {
            try
            {
                string result = await this.manager.removeLabel(labelId);
                if (result == "Label Removed Successfully")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getlabelbynoteid")]
        public IActionResult GetLabelByNoteId(int notesId)
        {
            try
            {
                var result = this.manager.getLabelsByNote(notesId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<LabelModel>>() { Status = true, Message = "Get Label By Notes", Data = result });
                }

                return this.BadRequest(new ResponseModel<List<string>>() { Status = false, Message = "Get Label By Notes Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getnotesbylabel")]
        public IActionResult GetNotesByLabel(int LabelId)
        {
            try
            {
                var result = this.manager.getNotesbyLabel(LabelId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<NotesModel>>() { Status = true, Message = "Get Notes By Label", Data = result });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Get Notes By Label" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
