using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult getNotes(int NotesId)
        {
            try
            {
                string resultMessage = this.manager.getNotes(NotesId);
                if (resultMessage.Equals("Details of Note Are Given in Data"))
                {
                    ConnectionMultiplexer connectionmultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionmultiplexer.GetDatabase();
                    string title = database.StringGet("Title");
                    string description = database.StringGet("Description");
                    int notesId = Convert.ToInt32(database.StringGet("notesId"));

                    NotesModel data = new NotesModel
                    {
                        Title = title,
                        Description = description,
                        NotesId = notesId,
                    };
                    return this.Ok(new { Status = true, Message = resultMessage, Data = data});
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
        [Route("api/updatenotes")]
        public async Task<IActionResult> UpdateNotes([FromBody]NotesModel notes)
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
    }
}
