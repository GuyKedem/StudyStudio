using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using StudyStudio.Infrastructure.Commands;
using StudyStudio.Infrastructure.Queries;
using StudyStudio.Web.Controllers;
using Xunit;
using Moq;
using StudyStudio.Web.Models.Exercise;

namespace StudyStudio.Web.Tests.Controllers
{
    public class ExerciseControllerTests
    {
        [Fact]
        public void CanBrowseExercises()
        {
            // Arrange
            var queryServiceMock = new Mock<IExerciseQueryService>();
            var commandServiceMock = new Mock<IExerciseCommandService>();
            var sut = new ExerciseController(commandServiceMock.Object, queryServiceMock.Object);
            var expectedExcercises = new[] { new ExerciseDetails{Body = "Body1"}, new ExerciseDetails{Body = "Body2"}};
            queryServiceMock.Setup(q => q.BrowseExercises()).Returns(expectedExcercises);

            // Act
            var result = sut.Browse();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var searchResults = ((BrowseModel) viewResult.ViewData.Model).SearchResults;
            Assert.Equal<IEnumerable<ExerciseDetails>>(expectedExcercises, searchResults);
        }
    }
}
