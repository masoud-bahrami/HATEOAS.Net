using HATEOAS.Net.HAL.Exceptions;
using System.Linq;
using Xunit;
 
namespace HATEOAS.Net.HAL.Tests
{
    public class EmbeddedResourceTests
    {
        private readonly Person masoud = new Person("Masoud", "Bahrami", 35);
        [Fact]
        public void Should_Raise_Error_If_ObjectState_IsNull()
        {
            //Assert
            Assert.Throws<ResourceStateNullExeption>(() => new EmbeddedResource(null));
            Assert.Throws<ResourceStateNullExeption>(() => EmbeddedResource.New(null));
        }
        [Fact]
        public void Should_Successfully_Set_ObjetcState()
        {
            //Arrange && Act
            var resource = new EmbeddedResource(masoud);

            //Assert
            Assert.True(masoud.Equals(resource.Object));

            //Arrange && Act
            resource = EmbeddedResource.New(masoud);

            //Assert
            Assert.True(masoud.Equals(resource.Object));
        }

        [Fact]
        public void Should_Successfully_Insert_Curi_Embedded_Link()
        {
            //Arrange && Act
            var resource = EmbeddedResource.New(masoud)
                .WithCuriLink("base", "user/masoud");

            //Assert
            Assert.Equal("user/masoud/{rel}", resource.LinksObjects.Single().Links.Single().HRef);
            Assert.True(resource.LinksObjects.Single().Links.Single().Templated);
            Assert.Equal(LinkRelations.Curries, resource.LinksObjects.Single().Relation);
        }
        [Fact]
        public void Should_Successfully_Insert_Edit_Embedded_Link()
        {
            //Arrange && Act
            var resource = EmbeddedResource.New(masoud)
                .WithEditLink("user/masoud", HttpVerbs.GET, true);

            //Assert
            Assert.Equal("user/masoud", resource.LinksObjects.Single().Links.Single().HRef);
            Assert.True(resource.LinksObjects.Single().Links.Single().Templated);
            Assert.Equal(LinkRelations.Edit, resource.LinksObjects.Single().Relation);
        }

        [Fact]
        public void Should_Successfully_Insert_First_Embedded_Link()
        {
            //Arrange && Act
            var resource = EmbeddedResource.New(masoud)
                .WithFirstLink("user/masoud", HttpVerbs.GET);

            //Assert
            Assert.Equal("user/masoud", resource.LinksObjects.Single().Links.Single().HRef);
            Assert.Equal(LinkRelations.First, resource.LinksObjects.Single().Relation);
        }
        [Fact]
        public void Should_Successfully_Insert_Last_Embedded_Link()
        {
            //Arrange && Act
            var resource = EmbeddedResource.New(masoud)
                .WithLastLink("user/masoud", HttpVerbs.GET);

            //Assert
            Assert.Equal("user/masoud", resource.LinksObjects.Single().Links.Single().HRef);
            Assert.Equal(LinkRelations.Last, resource.LinksObjects.Single().Relation);
        }
        [Fact]
        public void Should_Successfully_Insert_Next_Embedded_Link()
        {
            //Arrange && Act
            var resource = EmbeddedResource.New(masoud)
                .WithNextLink("user/masoud", HttpVerbs.GET);

            //Assert
            Assert.Equal("user/masoud", resource.LinksObjects.Single().Links.Single().HRef);
            Assert.Equal(LinkRelations.Next, resource.LinksObjects.Single().Relation);
        }
        [Fact]
        public void Should_Successfully_Insert_Previous_Embedded_Link()
        {
            //Arrange && Act
            var resource = EmbeddedResource.New(masoud)
                .WithPreviousLink("user/masoud", HttpVerbs.GET);

            //Assert
            Assert.Equal("user/masoud", resource.LinksObjects.Single().Links.Single().HRef);
            Assert.Equal(LinkRelations.Previous, resource.LinksObjects.Single().Relation);
        }
        [Fact]
        public void Should_Successfully_Insert_Self_Embedded_Link()
        {
            //Arrange && Act
            var resource = EmbeddedResource.New(masoud)
                .WithSelfLink("user/masoud", HttpVerbs.GET);

            //Assert
            Assert.Equal("user/masoud", resource.LinksObjects.Single().Links.Single().HRef);
            Assert.Equal(LinkRelations.Self, resource.LinksObjects.Single().Relation);
        }
        [Fact]
        public void Should_Successfully_Insert_Start_Embedded_Link()
        {
            //Arrange && Act
            var resource = EmbeddedResource.New(masoud)
                .WithStartLink("user/masoud", HttpVerbs.GET);

            //Assert
            Assert.Equal("user/masoud", resource.LinksObjects.Single().Links.Single().HRef);
            Assert.Equal(LinkRelations.Start, resource.LinksObjects.Single().Relation);
        }
        [Fact]
        public void Should_Successfully_Insert_Link_Embedded_Link()
        {
            //Arrange && Act
            var resource = EmbeddedResource.New(masoud)
                .WithLinkObject(LinkRelations.First, "user/masoud", HttpVerbs.GET);

            //Assert
            Assert.Equal("user/masoud", resource.LinksObjects.Single().Links.Single().HRef);
            Assert.Equal(LinkRelations.First, resource.LinksObjects.Single().Relation);
        }
    }
}