using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoloSozluk.Common.Enums;

namespace YoloSozluk.Api.WebApi.Controllers
{
    public class VoteController : BaseController
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }


        //[HttpPost]
        //[Route("Entry/{entryId}")]
        //public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.Up)
        //{
        //    var res = await _mediator.Send(new CreateEntryVoteCommand { );
        //    return Ok(res);
        //}
    }
}
