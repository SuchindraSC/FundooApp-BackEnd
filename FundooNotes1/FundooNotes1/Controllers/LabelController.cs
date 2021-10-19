// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
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

    /// <summary>
    /// Class LabelController
    /// </summary>
    [Authorize]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// ILabelManager manager
        /// </summary>
        private readonly ILabelManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="manager">ILabelManager manager</param>
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the labels.
        /// </summary>
        /// <param name="label">LabelModel label</param>
        /// <returns>Returns IActionResult Status Code After Adding Label</returns>
        [HttpPost]
        [Route("api/addlabels")]
        public async Task<IActionResult> AddLabels([FromBody] LabelModel label)
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

        /// <summary>
        /// Deletes the labels.
        /// </summary>
        /// <param name="LabelId">integer LabelId</param>
        /// <returns>Returns IActionResult Status Code After Deleting Labels</returns>
        [HttpDelete]
        [Route("api/deletelabels")]
        public async Task<IActionResult> DeleteLabels(int LabelId)
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

        /// <summary>
        /// Updates the labels.
        /// </summary>
        /// <param name="label">LabelModel label</param>
        /// <returns>Returns IActionResult Status Code After Updating Labels</returns>
        [HttpPut]
        [Route("api/updatelabels")]
        public async Task<IActionResult> UpdateLabels([FromBody] LabelModel label)
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

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>Returns IActionResult Status Code After Getting Labels</returns>
        [HttpGet]
        [Route("api/getlabels")]
        public IActionResult GetLabels(int UserId)
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

        /// <summary>
        /// Adds the label to notes.
        /// </summary>
        /// <param name="labelModel">LabelModel labelModel</param>
        /// <returns>Returns IActionResult Status Code After Adding Labels To Notes</returns>
        [HttpPut]
        [Route("api/addlabeltonotes")]
        public async Task<IActionResult> AddLabelToNotes([FromBody] LabelModel labelModel)
        {
            try
            {
                string resultMessage = await this.manager.addLabelToNotes(labelModel);
                if (resultMessage.Equals("Added Label To Note"))
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
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">integer labelId</param>
        /// <returns>Returns IActionResult Status Code After Removing Labels From Notes</returns>
        [HttpDelete]
        [Route("api/removelabel")]
        public async Task<IActionResult> RemoveLabel(int labelId)
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

        /// <summary>
        /// Gets the label by note identifier.
        /// </summary>
        /// <param name="notesId">integer notesId</param>
        /// <returns>Returns IActionResult Status Code After Getting Labels By Notes</returns>
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

        /// <summary>
        /// Gets the notes by label.
        /// </summary>
        /// <param name="LabelId">integer LabelId</param>
        /// <returns>Returns IActionResult Status Code After Getting Notes By Label</returns>
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
