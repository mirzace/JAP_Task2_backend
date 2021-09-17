using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using ScreenplayApp.Application.Commands;
using ScreenplayApp.Application.Handlers.CommandHandlers;
using ScreenplayApp.Application.Responses;
using ScreenplayApp.Core.Entities;
using ScreenplayApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenplayApp.Test
{
    [TestFixture]
    public class CreateBookingHandlerTest
    {
        public IMock<IBookingRepository> _bookingRepoMock { get; set; }
        public IMock<ITicketRepository> _ticketRepoMock { get; set; }
        public UserManager<AppUser> _userManager { get; set; }
        public IMock<CreateBookingCommand> _commandMock { get; set; }
        public IMock<CreateBookingHandler> _handlerMock { get; set; }

        [SetUp]
        public void Setup()
        {
            _bookingRepoMock = new Mock<IBookingRepository>();
            _ticketRepoMock = new Mock<ITicketRepository>();
            _commandMock = new Mock<CreateBookingCommand>();
            _handlerMock = new Mock<CreateBookingHandler>(
                _bookingRepoMock.Object, _ticketRepoMock.Object, _userManager
                );
        }

        [TestCase(-3, 1)]
        [TestCase(-1, 2)]
        [TestCase(-99, 3)]
        public async Task CreateBookingHandler_InputInvalidDate_ReturnApiException(int year, int screenplayId)
        {
            // Arange
            _commandMock.Object.Date = DateTime.Now.AddYears(year);
            _commandMock.Object.ScreenplayId = screenplayId;
            _commandMock.Object.AppUserId = 1;
            _commandMock.Object.Location = "Location A";
            _commandMock.Object.NumberOfTickets = 100;

            // Assert
            ApplicationException ex = Assert.ThrowsAsync<ApplicationException>(
                async () => await _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken())
                );

            Assert.That(ex.Message, Is.EqualTo("Can not book this ticket"));
        }

        [TestCase(1, 39)]
        [TestCase(1, 22)]
        [TestCase(1, 43)]
        public async Task CreateBookingHandler_InputValidData_ReturnBookingResponse(int day, int screenplayId)
        {
            // This test will fail because of the DateTime data in the database

            // Arange
            _commandMock.Object.NumberOfTickets = 1;
            _commandMock.Object.ScreenplayId = screenplayId;
            _commandMock.Object.AppUserId = 1;
            _commandMock.Object.Date = DateTime.Now.AddDays(day);
            _commandMock.Object.Location = "Location A";

            // Act
            var result = await _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken());

            // Assert
            Assert.IsInstanceOf<BookingResponse>(result);
            Assert.IsNotNull(result);
        }


        [TestCase(50, 39)]
        [TestCase(100, 22)]
        [TestCase(-4, 43)]
        public async Task CreateBookingHandler_InputInvalidNumberOfTickets_ReturnApiException(int ticketNumber, int screenplayId)
        {
            // Arange
            _commandMock.Object.NumberOfTickets = ticketNumber;
            _commandMock.Object.ScreenplayId = screenplayId;
            _commandMock.Object.AppUserId = 1;
            _commandMock.Object.Date = DateTime.Now.AddDays(1);
            _commandMock.Object.Location = "Location A";
            _commandMock.Object.NumberOfTickets = 100;

            // Assert 
            ApplicationException err = Assert.ThrowsAsync<ApplicationException>(
                () => _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken())
                );

            Assert.That(err.Message, Is.EqualTo("Not enough tickets"));
        }
    }
}
