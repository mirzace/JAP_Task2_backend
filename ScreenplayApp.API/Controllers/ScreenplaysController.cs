using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScreenplayApp.Application.Commands;
using ScreenplayApp.Application.Queries;
using ScreenplayApp.Application.Responses;
using ScreenplayApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenplayApp.API.Controllers
{
    public class ScreenplaysController : BaseController
    {
        private readonly IMediator _mediator;
        public ScreenplaysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IReadOnlyList<ScreenplayResponse>> GetAll([FromQuery] GetAllScreenplayQuery command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ScreenplayResponse> Get(int id)
        {
            return await _mediator.Send(new GetScreenplayQuery { ScreenplayId = id });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ScreenplayResponse>> CreateScreenplay([FromBody] CreateScreenplayCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
