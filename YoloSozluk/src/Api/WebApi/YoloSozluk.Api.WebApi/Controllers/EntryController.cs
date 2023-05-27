using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoloSozluk.Common.Models.Commands;
using YoloSozluk.Common.Models.Queries;

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

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
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
