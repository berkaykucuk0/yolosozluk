using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoloSozluk.Common.Enums;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.WebApi.Controllers
{
    public class VoteController : BaseController
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.Up)
        {
            var res = await _mediator.Send(new EntryVoteCreateCommand(entryId,voteType,UserId));
            return Ok(res);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.Up)
        {
            var res = await _mediator.Send(new EntryCommentVoteCreateCommand(entryCommentId, voteType, UserId));
            return Ok(res);
        }

        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            var res = await _mediator.Send(new EntryVoteDeleteCommand(entryId,UserId));
            return Ok(res);
        }


        [HttpPost]
        [Route("DeleteEntryCommentVote/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
        {
            var res = await _mediator.Send(new EntryCommentVoteDeleteCommand(entryCommentId, UserId));
            return Ok(res);
        }
    }
}
