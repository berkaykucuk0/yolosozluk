using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : BaseController
    {
       private readonly IMediator _mediator;

       public EntryController(IMediator mediator)
       {
           _mediator = mediator;
       }

       [HttpPost]
       [Route("CreateEntry")]
       public async Task<IActionResult> CreateEntry([FromBody] EntryCreateCommand command)
       {
            if (!command.CreatedUserId.HasValue)
                command.CreatedUserId = UserId;

            var res = await _mediator.Send(command);
            return Ok(res);
       }

        [HttpPost]
        [Route("CreateEntryCommand")]
        public async Task<IActionResult> CreateEntryCommand([FromBody] EntryCommentCreateCommand command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;

            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}
