using HATEOAS.Net.HAL.Exceptions;
using Xunit;

namespace HATEOAS.Net.HAL.Tests
{
    public class LinkObjectTests
    {
        [Fact]
        public void Shoud_Raise_Error_If_Relation_IsNullOrWhiteSpace()
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<LinkRelationNullExeption>(() => new LinkObject(null));
            Assert.Throws<LinkRelationNullExeption>(() => new LinkObject(""));
        }
        [Fact]
        public void Shoud_Return_Error_When_LinkObject_Dose_Not_Any_Link()
        {
            //Arrange
            var sut = new LinkObjectBuilder("/orders/{id}");

            //Assert
            Assert.Throws<LinkObjectLinksCollectionEmptyExeption>(() => sut.Build());
        }
        [Fact]
        public void HasOnlyOneLink_Method_Shoud_Return_True_When_Just_1Link_Defined()
        {
            //Arrange
            //Act
            var sut = new LinkObjectBuilder("/orders/{id}")
                .WithLink(new Link("/order/")).Build();
            //Assert
            Assert.True(sut.HasOnlyOneLink());
        }
        [Fact]
        public void Shoud_Return_2_When_LinkCount_Called()
        {
            //Arrange
            //Act
            var sut = new LinkObjectBuilder("/orders/{id}")
                .WithLink(new Link("/order/"))
                .WithLink(new Link("/order/{id}", HttpVerbs.GET,true))
                .Build();
            //Assert
            Assert.Equal(2, sut.LinkCount());
        }
    }
}