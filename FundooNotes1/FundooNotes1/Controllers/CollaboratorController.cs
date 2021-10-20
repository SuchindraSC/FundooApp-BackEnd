// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Suchindra Chitnis"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotes1.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using global::FundooManager.Interface;
    using global::FundooModel;
    using global::FundooRepository.Interface;

    /// <summary>
    /// Class Collaborator Controller
    /// </summary>
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// ICollaboratorManager manager
        /// </summary>
        private readonly ICollaboratorManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorController"/> class.
        /// </summary>
        /// <param name="manager">ICollaboratorManager manager</param>
        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collab">CollaboratorModel collab</param>
        /// <returns>Returns IActionResult Status Code After Adding Collaborator</returns>
        [HttpPost]
        [Route("api/addcollaborator")]
        public async Task<IActionResult> AddCollaborator([FromBody] CollaboratorModel collab)
        {
            try
            {
                string resultMessage = await this.manager.AddCollaborator(collab);
                if (resultMessage.Equals("Collaborator Added Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else if (resultMessage.Equals("Receiver Email Already Exists"))
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

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="CollaboratorId">integer CollaboratorId</param>
        /// <returns>Returns IActionResult Status Code After Removing Collaborator</returns>
        [HttpDelete]
        [Route("api/removecollaborator")]
        public async Task<IActionResult> RemoveCollaborator(int CollaboratorId)
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

        /// <summary>
        /// Gets the collaborator notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>Returns IActionResult Status Code After Getting Collaborator Notes</returns>
        [HttpGet]
        [Route("api/getcollaboratornotes")]
        public IActionResult GetCollaboratorNotes(int NotesId)
        {
            try
            {
                List<CollaboratorModel> data = this.manager.GetCollaboratorNotes(NotesId);
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
