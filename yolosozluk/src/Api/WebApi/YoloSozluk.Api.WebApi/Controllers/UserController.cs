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
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _mediator.Send(new GetUserDetailQuery(id));
            return Ok(user);
        }

        [HttpGet("{UserName}/{id}")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));
            return Ok(user);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]UserLoginCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost]  
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UserCreateCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }


        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> ConfirmEMail(Guid id)
        {
            var res = await _mediator.Send(new UserConfirmEmailCommand { ConfirmationId = id } );
            return Ok(res);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangPassword([FromBody] UserChangePasswordCommand command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;
            var res = await _mediator.Send(command);
            return Ok(res);
        }

    }
}
