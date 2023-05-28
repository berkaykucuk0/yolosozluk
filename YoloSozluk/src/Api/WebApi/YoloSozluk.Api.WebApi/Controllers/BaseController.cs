using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace YoloSozluk.Api.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        public Guid UserId => new Guid(); //new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);   
    }
}
