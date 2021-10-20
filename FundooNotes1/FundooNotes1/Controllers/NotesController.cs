// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz">
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
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StackExchange.Redis;
    using global::FundooManager.Interface;
    using global::FundooModel;
    using global::FundooRepository.Interface;

    /// <summary>
    /// class NotesController
    /// </summary>
    [Authorize]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// INoteManager manager
        /// </summary>
        private readonly INoteManager manager;

        /// <summary>
        /// INotesRepository repository
        /// </summary>
        private readonly INotesRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="manager">INoteManager manager</param>
        /// <param name="repository">INotesRepository repository</param>
        public NotesController(INoteManager manager, INotesRepository repository)
        {
            this.manager = manager;
            this.repository = repository;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="note">NotesModel note</param>
        /// <returns>returns string after adding notes</returns>
        [HttpPost]
        [Route("api/addnotes")]
        public async Task<IActionResult> AddNotes([FromBody] NotesModel note)
        {
            try
            {
                string resultMessage = await this.manager.AddNotes(note);
                if (resultMessage.Equals("Note Added Successfully"))
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
        /// Gets the notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting notes</returns>
        [HttpGet]
        [Route("api/getnotes")]
        public IActionResult GetNotes(int UserId)
        {
            try
            {
                List<NotesModel> data = this.manager.GetNotes(UserId);
                if (data != null)
                { 
                    return this.Ok(new { Status = true, Message = $"Notes for UserId {UserId} are", Data = data });
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

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notes">NotesModel notes</param>
        /// <returns>returns string after updating notes</returns>
        [HttpPut]
        [Route("api/updatenotes")]
        public async Task<IActionResult> UpdateNotes([FromBody] NotesModel notes)
        {
            try
            {
                string resultMessage = await this.manager.UpdateNotes(notes);
                if (resultMessage.Equals("Notes Updated Successfully"))
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
        /// Trash the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after adding notes to trash</returns>
        [HttpPut]
        [Route("api/trashnotes")]
        public async Task<IActionResult> TrashNotes(int NotesId)
        {
            try
            {
                string resultMessage = await this.manager.TrashNotes(NotesId);
                if (resultMessage.Equals("Notes Sent To Trash Successfully"))
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
        /// Gets the trashed notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting notes</returns>
        [HttpGet]
        [Route("api/gettrashnotes")]
        public IActionResult GetTrashNotes(int UserId)
        {
            try
            {
                List<NotesModel> data = this.manager.GetTrashNotes(UserId);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = $"Notes for User Id {UserId} in Trash are", Data = data });
                }   
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Get Trash Notes Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Untrash the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after returning the notes from trash</returns>
        [HttpPut]
        [Route("api/untrashnotes")]
        public async Task<IActionResult> UnTrashNotes(int NotesId)
        {
            try
            {
                string resultMessage = await this.manager.UnTrashNotes(NotesId);
                if (resultMessage.Equals("Notes Removed From Trash Successfully"))
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
        /// Deletes the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after deleting notes</returns>
        [HttpDelete]
        [Route("api/deletenotes")]
        public async Task<IActionResult> DeleteNotes(int NotesId)
        {
            try
            {
                string resultMessage = await this.manager.DeleteNotes(NotesId);
                if (resultMessage.Equals("Note Deleted Successfully"))
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
        /// Archieves the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after notes sent to archieve</returns>
        [HttpPut]
        [Route("api/archievenotes")]
        public async Task<IActionResult> ArchieveNotes(int NotesId)
        {
            try
            {
                string resultMessage = await this.manager.ArchieveNotes(NotesId);
                if (resultMessage.Equals("Notes Archieved Successfully"))
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
        /// Gets the archieved notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting notes from archieve</returns>
        [HttpGet]
        [Route("api/getarchievenotes")]
        public IActionResult GetArchieveNotes(int UserId)
        {
            try
            {
                List<NotesModel> data = this.manager.GetArchievedNotes(UserId);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = $"Notes for User Id {UserId} Archieved are", Data = data });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Get Archieved Notes Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Unarchieve the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after returning notes from archieve</returns>
        [HttpPut]
        [Route("api/unarchievenotes")]
        public async Task<IActionResult> UnArchieveNotes(int NotesId)
        {
            try
            {
                string resultMessage = await this.manager.UnArchieveNotes(NotesId);
                if (resultMessage.Equals("Notes UnArchieved Successfully"))
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
        /// Pins the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after pinning notes</returns>
        [HttpPut]
        [Route("api/pinnotes")]
        public async Task<IActionResult> PinNotes(int NotesId)
        {
            try
            {
                string resultMessage = await this.manager.PinNotes(NotesId);
                if (resultMessage.Equals("Notes Pinned Successfully"))
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
        /// Unpin the notes.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <returns>returns string after unpinning notes</returns>
        [HttpPut]
        [Route("api/unpinnotes")]
        public async Task<IActionResult> UnPinNotes(int NotesId)
        {
            try
            {
                string resultMessage = await this.manager.UnPinNotes(NotesId);
                if (resultMessage.Equals("Notes UnPinned Successfully"))
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
        /// Adds the remainder.
        /// </summary>
        /// <param name="NotesId">integer NotesId</param>
        /// <param name="time">string time</param>
        /// <returns>returns string after adding reminder</returns>
        [HttpPut]
        [Route("api/setreminder")]
        public async Task<IActionResult> AddReminder(int NotesId, string time)
        {
            try
            {
                string resultMessage = await this.manager.AddReminder(NotesId, time);
                if (resultMessage.Equals("Remainder Added Successfully"))
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
        /// Gets the reminder notes.
        /// </summary>
        /// <param name="UserId">integer UserId</param>
        /// <returns>returns list after getting reminder notes</returns>
        [HttpGet]
        [Route("api/getremindernotes")]
        public IActionResult getReminderNotes(int UserId)
        {
            try
            {
                List<NotesModel> data = this.manager.GetReminderNotes(UserId);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = $"Notes for UserId {UserId} are", Data = data });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Get Reminder Notes Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="NoteId">integer NoteId</param>
        /// <returns>returns string after removing reminder</returns>
        [HttpPut]
        [Route("api/removereminder")]
        public async Task<IActionResult> RemoveReminder(int NotesId)
        {
            try
            {
                string resultMessage = await this.manager.RemoveReminder(NotesId);
                if (resultMessage.Equals("Reminder Removed Successfully"))
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
        /// Updates the color to note.
        /// </summary>
        /// <param name="NoteId">integer NoteId</param>
        /// <param name="color">string color</param>
        /// <returns>returns string after adding color to notes</returns>
        [HttpPut]
        [Route("api/addcolor")]
        public async Task<IActionResult> UpdateColorToNote(int NoteId, string color)
        {
            try
            {
                string resultMessage = await this.manager.UpdateColorToNote(NoteId, color);
                if (resultMessage.Equals("Color Added Successfully"))
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
        /// Adds the image.
        /// </summary>
        /// <param name="NoteId">integer NoteId</param>
        /// <param name="image">IFormFile image</param>
        /// <returns>returns string after adding image</returns>
        [HttpPut]
        [Route("api/addimage")]
        public async Task<IActionResult> AddImage(int NoteId, IFormFile image)
        {
            try
            {
                string resultMessage = await this.manager.AddImage(NoteId, image);
                if (resultMessage.Equals("Image Added Successfully"))
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
        /// Removes the image.
        /// </summary>
        /// <param name="NoteId">int NoteId</param>
        /// <returns>returns string after removing image</returns>
        [HttpPut]
        [Route("api/removeimage")]
        public async Task<IActionResult> RemoveImage(int NoteId)
        {
            try
            {
                string resultMessage = await this.manager.RemoveImage(NoteId);
                if (resultMessage.Equals("Image Removed Successfully"))
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
    }
}
