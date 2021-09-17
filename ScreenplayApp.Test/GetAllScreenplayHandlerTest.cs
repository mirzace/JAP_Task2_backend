using AutoMapper;
using MediatR;
using Moq;
using NUnit.Framework;
using ScreenplayApp.API.Controllers;
using ScreenplayApp.Application.Handlers.QueryHandlers;
using ScreenplayApp.Application.Queries;
using ScreenplayApp.Application.Responses;
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
    public class GetAllScreenplayHandlerTest
    {
        public IMock<IScreenplayRepository> _screenplayRepo { get; set; }
        public IMock<GetAllScreenplayQuery> _commandMock { get; set; }
        public IMock<GetAllScreenplayHandler> _handlerMock { get; set; }
        public IMock<IMapper> _mapperMock { get; set; }
        public IMapper _mapper { get; set; }

        [SetUp]
        public void SetUp()
        {
            _screenplayRepo = new Mock<IScreenplayRepository>();
            _commandMock = new Mock<GetAllScreenplayQuery>();
            _mapperMock = new Mock<IMapper>();
            _handlerMock = new Mock<GetAllScreenplayHandler>(
                _screenplayRepo.Object
                );
        }

        [Test]
        public async Task GetAllScreenplayHandler_InputInvalidSearch_ReturnEmptyArray()
        {
            /*
            // Arrange
            _commandMock.Object.Search = "InvalidSearchWhichNobodyExpected";
            _commandMock.Object.Category = "Movie";

            // Act
            var result = await _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken());

            // Assert
            Assert.IsEmpty(result);
            Assert.AreEqual(result.Count, 0);
            */
        }

        [Test]
        public async Task GetAllScreenplayHandler_InputValidSearch_ReturnArrayOfScreenplayResponse()
        {
            /*
            // Arrange
            _commandMock.Object.Search = "Steve Jobs";
            _commandMock.Object.Category = "Movie";

            // Act
            var result = await _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken());

            // Assert
            Assert.IsNotEmpty(result);
            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<ScreenplayResponse>>(result);
            */
        }

        [Test]
        public async Task GetAllScreenplayHandler_InputExpectedRate_ReturnArrayOfScreenplayResponse()
        {
            /*
            // Arrange
            _commandMock.Object.AtLeastStars = 2;
            _commandMock.Object.Category = "Movie";

            // Act
            var result = await _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken());

            // Assert
            Assert.IsNotEmpty(result);
            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<ScreenplayResponse>>(result);
            
            foreach (var item in result)
            {
                Assert.That(item.AverageRate > 2);
            }
            */
        }

        [Test]
        public async Task GetAllScreenplayHandler_InputExpectedDate_ReturnArrayOfScreenplayResponseOlderThanYear()
        {
            /*
            // Arrange
            _commandMock.Object.OlderThanYears = 1;
            _commandMock.Object.Category = "Movie";

            // Act
            var result = await _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken());

            // Assert
            Assert.IsNotEmpty(result);
            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<ScreenplayResponse>>(result);

            foreach (var item in result)
            {
                Assert.That(item.ReleaseDate >= DateTime.Now.AddYears(_commandMock.Object.OlderThanYears * (-1)));
            }
            */
        }

        [Test]
        public async Task GetAllScreenplayHandler_InputInvalidCategory_ReturnEmptyArray()
        {
            /*
            // Arange
            _commandMock.Object.Category = "Movie";
            _commandMock.Object.Search = "";

            // Act
            var result = await _handlerMock.Object.Handle(_commandMock.Object, new CancellationToken());

            // Assert
            Assert.IsEmpty(result);
            Assert.AreEqual(result.Count(), 0);
            */
        }
    }
}
