using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
        private readonly ILogger _logger;
        public EntryController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
        {
            _logger.Information($"Start : Getting item details for {query}", query);
            var res = await _mediator.Send(query);  
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _mediator.Send(new GetEntryDetailQuery(id,UserId));
            return Ok(res);
        }

        [HttpGet]
        [Route("Comments/{id}")]
        public async Task<IActionResult> GetEntryComments(Guid id, int page,int pageSize)
        {
            var res = await _mediator.Send(new GetEntryCommentsQuery(id, UserId,page,pageSize));
            return Ok(res);
        }

        [HttpGet]
        [Route("UserEntries")]
        public async Task<IActionResult> GetUserEntries(Guid userId,string userName, int page, int pageSize)
        {
            if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
               userId = UserId;
            var res = await _mediator.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));
            return Ok(res);
        }

        [HttpGet]
        [Route("GetMainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int page,int pageSize)
        {   
            var res = await _mediator.Send(new GetMainPageEntriesQuery(UserId,page,pageSize));
            return Ok(res);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
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
