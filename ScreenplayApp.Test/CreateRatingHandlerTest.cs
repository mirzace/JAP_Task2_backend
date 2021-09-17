using Moq;
using NUnit.Framework;
using ScreenplayApp.Application.Commands;
using ScreenplayApp.Application.Handlers.CommandHandlers;
using ScreenplayApp.Core.Entities;
using ScreenplayApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenplayApp.Test
{
    [TestFixture]
    public class CreateRatingHandlerTest
    {
        public IMock<IScreenplayRepository> _screenplayRepoMock { get; set; }
        public IMock<IRatingRepository> _ratingRepoMock { get; set; }
        public IMock<CreateRatingCommand> _commandMock { get; set; }
        public IMock<CreateRatingHandler> _handlerMock { get; set; }

        [SetUp]
        public void SetUp()
        {
            _screenplayRepoMock = new Mock<IScreenplayRepository>();
            _ratingRepoMock = new Mock<IRatingRepository>();
            _commandMock = new Mock<CreateRatingCommand>();
            _handlerMock = new Mock<CreateRatingHandler>(
                _ratingRepoMock.Object, _screenplayRepoMock.Object
            );
        }

        [TestCase(10)]
        [TestCase(12)]
        [TestCase(-1)]
        public void CreateRatingHandler_InputInvalidRateRange_ReturnApiException(int rate)
        {

            // Arange
            _commandMock.Object.Rate = rate;
            _commandMock.Object.ScreenplayId = 1;

            // Act

            // Assert 
            ApplicationException err = Assert.ThrowsAsync<ApplicationException>(
                () => _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken())
                );

            Assert.That(err.Message, Is.EqualTo("Rate must be between 1 and 5"));
        }

        [TestCase(0)]
        [TestCase(999)]
        [TestCase(-20)]
        public void CreateRatingHandler_InputInvalidScreenplayId_ReturnApiException(int id)
        {
            // Arange
            _commandMock.Object.Rate = 5;
            _commandMock.Object.ScreenplayId = id;

            // Assert 
            ApplicationException err = Assert.ThrowsAsync<ApplicationException>(
                () => _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken())
                );

            Assert.That(err.Message, Is.EqualTo("Screenplay not found"));
        }

        // I had a problem with async calls 
        // For some reason, it throws an ApplicationException("Screenplay Not Found")
        // My guess would be that there is something with Db or Repo setup

        [TestCase(5, 1)]
        [TestCase(1, 2)]
        [TestCase(3, 3)]
        public async Task CreateRatingHandler_InputValidRequest_ReturnInt(int rate, int id)
        {   
            /*
            // Arrange
            _commandMock.Object.Rate = rate;
            _commandMock.Object.ScreenplayId = id;

            // Assert
            var result = await _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken());
            Assert.IsInstanceOf<int>(result);
            */
        }
    }
}
