using HATEOAS.Net.HAL.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HATEOAS.Net.HAL.Tests
{
    public class LinkTests
    {
        private const string HTTPVERB = "httpVerb";

        [Fact]
        public void Shoud_Raise_Error_If_HRef_IsNullOrWhiteSpace()
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<HRefNullExeption>(() => new Link(null));
            Assert.Throws<HRefNullExeption>(() => new Link(""));
        }
        [Fact]
        public void Shoud_Return_Just_HRef_Link_When_Only_HRef_Is_Specified()
        {
            //Arrange
            //Act
            var sut = new Link("/orders/20");
            var links = sut.GetProperties();
            //Assert
            Assert.Equal(1, links.Count(a => a.Item1 == "href"));
        }
        [Fact]
        public void Shoud_Return_Templated_When_Templated_Is_Specified()
        {
            //Arrange
            //Act
            var sut = Link.New("/orders/{id}", true);
            var links = sut.GetProperties();
            //Assert
            Assert.Equal(3, links.Count);
            Assert.Equal(1, links.Count(a => a.Item1 == "href"));
            Assert.Contains(links, a => a.Item1 == "templated");
        }
        [Fact]
        public void Shoud_Return_Deprecation_When_Templated_Is_Specified()
        {
            //Arrange
            //Act
            var sut = Link.New("/orders/{id}", null, null, null, "deprecation value");
            var links = sut.GetProperties();
            //Assert
            Assert.Equal(3, links.Count);
            Assert.Equal(1, links.Count(a => a.Item1 == "href"));
            Assert.Contains(links, a => a.Item1 == "deprecation");
            Assert.Contains(links, a => a.Item2 == "deprecation value");
        }
        [Fact]
        public void Shoud_Return_Name_When_Templated_Is_Specified()
        {
            //Arrange
            //Act
            var sut = Link.New("/orders/{id}", null, "Name value");

            var links = sut.GetProperties();
            //Assert
            Assert.Equal(3, links.Count);
            Assert.Equal(1, links.Count(a => a.Item1 == "href"));
            Assert.Contains(links, a => a.Item1 == "name");
            Assert.Contains(links, a => a.Item2 == "Name value");
        }
        [Fact]
        public void Shoud_Return_Type_When_Templated_Is_Specified()
        {
            //Arrange
            //Act
            var sut = new Link("/orders/{id}", HttpVerbs.GET, null, null, "Type value");
            var links = sut.GetProperties();
            //Assert
            Assert.Equal(3, links.Count);
            Assert.Equal(1, links.Count(a => a.Item1 == "href"));
            Assert.Contains(links, a => a.Item1 == "type");
            Assert.Contains(links, a => a.Item2 == "Type value");
        }
        [Fact]
        public void Shoud_Return_5_Links_When_All_Property_Are_Specified()
        {
            //Arrange
            //Act
            var sut = Link.New("/orders/{id}", true, "name", "type", "deprication");
            var links = sut.GetProperties();
            //Assert
            Assert.Equal(6, links.Count);
            Assert.Equal(1, links.Count(a => a.Item1 == "href"));
        }
        [Fact]
        public void Shoud_Successfully_Create_GET_Link()
        {
            //Arrange
            //Act
            var sut = Link.NewGET("/orders/{id}");
            var links = sut.GetProperties();

            //Assert
            Assert.Equal(2, links.Count);
            Assert.Equal(HttpVerbs.GET, sut.HttpVerb);
            Assert.Contains(links, a => a.Item1 == HTTPVERB);
        }
        [Fact]
        public void Shoud_Successfully_Create_POST_Link()
        {
            //Arrange
            //Act
            var sut = Link.NewPOST("/orders/{id}");
            var links = sut.GetProperties();

            //Assert
            Assert.Equal(2, links.Count);
            Assert.Equal(HttpVerbs.POST, sut.HttpVerb);
            Assert.Contains(links, a => a.Item1 == HTTPVERB);
        }
        [Fact]
        public void Shoud_Successfully_Create_CONNECT_Link()
        {
            //Arrange
            //Act
            var sut = Link.NewCONNECT("/orders/{id}");
            var links = sut.GetProperties();

            //Assert
            Assert.Equal(2, links.Count);
            Assert.Equal(HttpVerbs.CONNECT, sut.HttpVerb);
            Assert.Contains(links, a => a.Item1 == HTTPVERB);
        }
        [Fact]
        public void Shoud_Successfully_Create_DELETE_Link()
        {
            //Arrange
            //Act
            var sut = Link.NewDELETE("/orders/{id}");
            var links = sut.GetProperties();

            //Assert
            Assert.Equal(2, links.Count);
            Assert.Equal(HttpVerbs.DELETE, sut.HttpVerb);
            Assert.Contains(links, a => a.Item1 == HTTPVERB);
        }
        [Fact]
        public void Shoud_Successfully_Create_HEAD_Link()
        {
            //Arrange
            //Act
            var sut = Link.NewHEAD("/orders/{id}");
            var links = sut.GetProperties();

            //Assert
            Assert.Equal(2, links.Count);
            Assert.Equal(HttpVerbs.HEAD, sut.HttpVerb);
            Assert.Contains(links, a => a.Item1 == HTTPVERB);
        }
        [Fact]
        public void Shoud_Successfully_Create_OPTIONS_Link()
        {
            //Arrange
            //Act
            var sut = Link.NewOPTIONS("/orders/{id}");
            var links = sut.GetProperties();

            //Assert
            Assert.Equal(2, links.Count);
            Assert.Equal(HttpVerbs.OPTIONS, sut.HttpVerb);
            Assert.Contains(links, a => a.Item1 == HTTPVERB);
        }
        [Fact]
        public void Shoud_Successfully_Create_PATCH_Link()
        {
            //Arrange
            //Act
            var sut = Link.NewPATCH("/orders/{id}");
            var links = sut.GetProperties();

            //Assert
            Assert.Equal(2, links.Count);
            Assert.Equal(HttpVerbs.PATCH, sut.HttpVerb);
            Assert.Contains(links, a => a.Item1 == HTTPVERB);
        }
        [Fact]
        public void Shoud_Successfully_Create_PUT_Link()
        {
            //Arrange
            //Act
            var sut = Link.NewPUT("/orders/{id}");
            var links = sut.GetProperties();

            //Assert
            Assert.Equal(2, links.Count);
            Assert.Equal(HttpVerbs.PUT, sut.HttpVerb);
            Assert.Contains(links, a => a.Item1 == HTTPVERB);
        }
        [Fact]
        public void Shoud_Successfully_Create_TRACE_Link()
        {
            //Arrange
            //Act
            var sut = Link.NewTRACE("/orders/{id}");
            var links = sut.GetProperties();

            //Assert
            Assert.Equal(2, links.Count);
            Assert.Equal(HttpVerbs.TRACE, sut.HttpVerb);
            Assert.Contains(links, a => a.Item1 == HTTPVERB);
        }
        [Fact]
        public void Shoud_Successfully_Insert_QueryParameters()
        {
            //Arrange
            //Act
            var sut = Link.NewTRACE("/orders/{id}")
                .WithQueryParameter(ScalarQueryParameter.NewBoolean("isAdmin", 1));

            //Assert
            Assert.Equal(1, sut.QueryParameters.Count);
            Assert.Equal("isAdmin", sut.QueryParameters.Single().Title);
        }
        [Fact]
        public void Shoud_Raise_Error_When_Inserting_QueryParameter_With_The_Same_Position()
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<LinkQueryParameterPositionDuplicatedException>(() =>
                Link.NewTRACE("/orders/{id}")
                    .WithQueryParameter(ScalarQueryParameter.NewBoolean("isAdmin", 1))
                    .WithQueryParameter(ScalarQueryParameter.NewNumber("age", 1)));

            //Arrange
            var parameters = new List<ScalarQueryParameter>
            {
                ScalarQueryParameter.NewBoolean("isAdmin", 1),
                ScalarQueryParameter.NewBoolean("isAdmin", 1)
            };

            //Act
            //Assert
            Assert.Throws<LinkQueryParameterPositionDuplicatedException>(() =>
                Link.NewTRACE("/orders/{id}").WithQueryParameters(parameters)
                );
        }
    }
}