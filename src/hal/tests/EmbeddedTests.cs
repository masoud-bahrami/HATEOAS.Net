using HATEOAS.Net.HAL.Exceptions;
using System.Linq;
using Xunit;

namespace HATEOAS.Net.HAL.Tests
{
    public class EmbeddedTests
    {
        [Fact]
        public void Should_Raise_Error_If_ObjectState_IsNull()
        {
            //Assert
            Assert.Throws<EmbeddedResourceNameNullOrEmptyException>(() => new Embedded(null));
        }
        [Fact]
        public void Should_Successfully_Set_EmbededName()
        {
            //Arrange && Act
            var embedded = new Embedded("ea:order");

            //Assert
            Assert.Equal("ea:order", embedded.ResourceName);
        }
        [Fact]
        public void Should_Successfully_Set_EmbededResources()
        {
            //Arrange
            var embedded = new Embedded("ea:order");

            //Assert
            Assert.False(embedded.Resources == null);
            Assert.Equal(0, embedded.Resources.Count);

            //Act
            embedded.WithResource(EmbeddedResource.New(new { Name = "Masoud" }).WithSelfLink("order/name", HttpVerbs.GET));

            //Assert
            Assert.Equal(1, embedded.Resources.Count);
            Assert.Equal(LinkRelations.Self, embedded.Resources.Single().LinksObjects.Single().Relation);
            Assert.Equal("order/name", embedded.Resources.Single().LinksObjects.Single().Links.Single().HRef);
        }
    }
}