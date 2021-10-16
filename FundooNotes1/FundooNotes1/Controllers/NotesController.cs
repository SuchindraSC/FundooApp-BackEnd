using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes1.Controllers
{
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteManager manager;

        private readonly INotesRepository repository;

        public NotesController(INoteManager manager, INotesRepository repository)
        {
            this.manager = manager;
            this.repository = repository;
        }

        [HttpPost]
        [Route("api/addnotes")]
        public async Task<IActionResult> addNotes([FromBody] NotesModel note)
        {
            try
            {
                string resultMessage = await this.manager.addNotes(note);
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

        [HttpGet]
        [Route("api/getnotes")]
        public IActionResult getNotes(int UserId)
        {
            try
            {
                List<NotesModel> data = this.manager.getNotes(UserId);
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

        [HttpGet]
        [Route("api/gettrashnotes")]
        public IActionResult getTrashNotes(int UserId)
        {
            try
            {
                List<NotesModel> data = this.manager.getTrashNotes(UserId);
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

        [HttpPut]
        [Route("api/setreminder")]
        public async Task<IActionResult> AddReminder(int NotesId, string time)
        {
            try
            {
                string resultMessage = await this.manager.AddRemainder(NotesId, time);
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

        [HttpPut]
        [Route("api/removereminder")]
        public async Task<IActionResult> RemoveReminder(int NotesId)
        {
            try
            {
                string resultMessage = await this.manager.RemoveRemainder(NotesId);
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
