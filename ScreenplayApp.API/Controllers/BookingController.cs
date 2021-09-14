using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScreenplayApp.API.Extensions;
using ScreenplayApp.Application.Commands;
using ScreenplayApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenplayApp.API.Controllers
{
    //[Authorize]
    public class BookingController : BaseController
    {
        private readonly IMediator _mediator;
        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BookingResponse>> CreateBooking([FromBody] CreateBookingCommand command)
        {
            //command.AppUserId = User.GetUserId();
            command.AppUserId = 1;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
