using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes1.Controllers
{
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorManager manager;

        private readonly ICollaboratorRepository repository;

        public CollaboratorController(ICollaboratorManager manager, ICollaboratorRepository repository)
        {
            this.manager = manager;
            this.repository = repository;
        }

        [HttpPost]
        [Route("api/addcollaborator")]
        public async Task<IActionResult> addCollaborator([FromBody] CollaboratorModel collab)
        {
            try
            {
                string resultMessage = await this.manager.AddCollaborator(collab);
                if (resultMessage.Equals("Collaborator Added Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else if(resultMessage.Equals("Receiver Email Already Exists"))
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
        [Route("api/removecollaborator")]
        public async Task<IActionResult> removeCollaborator(int CollaboratorId)
        {
            try
            {
                string resultMessage = await this.manager.RemoveCollaborator(CollaboratorId);
                if (resultMessage.Equals("Collaborator Removed Successfully"))
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
        [Route("api/getcollaboratornotes")]
        public IActionResult getCollaboratorNotes(int NotesId)
        {
            try
            {
                List<CollaboratorModel> data = this.manager.getCollaboratorNotes(NotesId);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = $"Get Collaborator Notes Successfull", Data = data });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Get Notes Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
