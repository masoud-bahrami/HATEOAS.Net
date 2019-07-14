using HATEOAS.Net.HAL.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HATEOAS.Net.HAL.Tests
{
    public class HALTests
    {
        private const string LINK_TITLE = "_links";
        private const string EMBEDDED_TITLE = "_embedded";
        private const string HREF = "href";

        private readonly Person masoud = new Person("Masoud", "Bahrami", 35);
        [Fact]
        public void Should_Raise_Error_If_ObjectState_IsNull()
        {
            //Assert
            Assert.Throws<HalObjectStateIsNullExeption>(() => HAL.Builder().Build());
            Assert.Throws<HalObjectStateIsNullExeption>(() => HAL.Builder().WithLink(new LinkObject("orders")
            {
                Links = new List<Link>() { new Link("/Order") }
            }).Build());
        }

        [Fact]
        public void Should_Be_Ok_Event_Links_And_Embedded_Are_Empty()
        {
            //Arrange
            var currentDateTime = DateTime.Now;

            //Arrange && Act
            var sut = HAL.Builder()
                .WithState(new { EventId = 1, OccuredAt = currentDateTime })
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["OccuredAt"].Value<DateTime>() == currentDateTime);
            Assert.True(JObject.Parse(sut)["EventId"].Value<int>() == 1);
        }
        [Fact]
        public void Should_Be_Ok_If_StateObject_Is_Anonymous()
        {
            //Arrange
            var masoud = "Masoud";
            var bahrami = "Bahrami";

            //Arrange && Act
            var sut = HAL.Builder()
                .WithState(new { Name = masoud, Familty = "Bahrami", Age = 25 })
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == masoud);
            Assert.True(JObject.Parse(sut)["Familty"].Value<string>() == bahrami);
            Assert.True(JObject.Parse(sut)["Age"].Value<int>() == 25);
        }
        [Fact]
        public void Should_Be_Ok_If_StateObject_Is_A_Collection_Of_Key_Value()
        {
            //Arrange && Act
            var sut = HAL.Builder()
                .WithState("Name", "Masoud")
                .WithState("Family", "Bahrami")
                .WithState("Age", "1")
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == "Masoud");
            Assert.True(JObject.Parse(sut)["Family"].Value<string>() == "Bahrami");
            Assert.True(JObject.Parse(sut)["Age"].Value<int>() == 1);
        }
        [Fact]
        public void Should_Be_Ok_If_StateObject_Is_A_Dictionery_Of_Key_Value()
        {
            //Arrange && Act
            var sut = HAL.Builder()
                .WithState(new Dictionary<string, object>
                {
                    { "Name" , "Masoud" },
                    { "Family" , "Bahrami"}
                })
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == "Masoud");
            Assert.True(JObject.Parse(sut)["Family"].Value<string>() == "Bahrami");
        }
        [Fact]
        public void Should_Be_Ok_If_StateObject_Be_IState()
        {
            //Arrange && Act
            var masoud = new Person("Masoud", "Bahrami", 35);
            var sut = HAL.Builder()
                .WithState(masoud)
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)[nameof(Person.FirstName)].Value<string>() == masoud.FirstName);
            Assert.True(JObject.Parse(sut)[nameof(Person.LastName)].Value<string>() == masoud.LastName);
            Assert.True(JObject.Parse(sut)[nameof(Person.Age)].Value<int>() == masoud.Age);
            Assert.True(JObject.Parse(sut)[LINK_TITLE].Count() == masoud.LinkObjects.Count);
        }
        [Fact]
        public void Both_KeyValue_State_And_ObjectState_Should_Be_In_Result()
        {
            //Arrange
            var currentDateTime = DateTime.Now;

            //Arrange && Act
            var sut = HAL.Builder()
                .WithState(new Dictionary<string, object>
                {
                    { "Name" , "Masoud" },
                    { "Family" , "Bahrami"}
                })
                .WithState("Age", 35)
                .WithState(new { EventId = 1, OccuredAt = currentDateTime })
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == "Masoud");
            Assert.True(JObject.Parse(sut)["Family"].Value<string>() == "Bahrami");
            Assert.True(JObject.Parse(sut)["OccuredAt"].Value<DateTime>() == currentDateTime);
            Assert.True(JObject.Parse(sut)["EventId"].Value<int>() == 1);
            Assert.True(JObject.Parse(sut)["Age"].Value<int>() == 35);
        }
        [Fact]
        public void Should_Successfully_Insrted_First_Link_Using_WithFirstLink_Method()
        {
            //Arrange
            var currentDateTime = DateTime.Now;

            //Arrange && Act
            var sut = HAL.Builder()
                .WithState("Name", "Masoud")
                .WithFirstLink("/user/{name}", HttpVerbs.GET, true)
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == "Masoud");

            //Arrange
            var link = JObject.Parse(sut)[LINK_TITLE][LinkRelations.First].ToObject(typeof(Link));
            //Assert
            Assert.True((link as Link).HRef == "/user/{name}");
        }
        [Fact]
        public void Should_Successfully_Insrted_Last_Link_Using_WithFirstLink_Method()
        {
            //Arrange
            var currentDateTime = DateTime.Now;

            //Arrange && Act
            var sut = HAL.Builder()
                .WithState("Name", "Masoud")
                .WithLastLink("/user/{name}", HttpVerbs.GET, true)
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == "Masoud");

            //Arrange
            var link = JObject.Parse(sut)[LINK_TITLE][LinkRelations.Last].ToObject(typeof(Link));
            //Assert
            Assert.True((link as Link).HRef == "/user/{name}");
        }
        [Fact]
        public void Should_Successfully_Insrted_Next_Link_Using_WithFirstLink_Method()
        {
            //Arrange
            var currentDateTime = DateTime.Now;

            //Arrange && Act
            var sut = HAL.Builder()
                .WithState("Name", "Masoud")
                .WithNextLink("/user/{name}", HttpVerbs.GET, true)
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == "Masoud");

            //Arrange
            var link = JObject.Parse(sut)[LINK_TITLE][LinkRelations.Next].ToObject(typeof(Link));
            //Assert
            Assert.True((link as Link).HRef == "/user/{name}");
        }
        [Fact]
        public void Should_Successfully_Insrted_Previous_Link_Using_WithFirstLink_Method()
        {
            //Arrange
            var currentDateTime = DateTime.Now;

            //Arrange && Act
            var sut = HAL.Builder()
                .WithState("Name", "Masoud")
                .WithPreviousLink("/user/{name}", HttpVerbs.GET, true)
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == "Masoud");

            //Arrange
            var link = JObject.Parse(sut)[LINK_TITLE][LinkRelations.Previous].ToObject(typeof(Link));
            //Assert
            Assert.True((link as Link).HRef == "/user/{name}");
        }
        [Fact]
        public void Should_Successfully_Insrted_Self_Link_Using_WithFirstLink_Method()
        {
            //Arrange
            var currentDateTime = DateTime.Now;

            //Arrange && Act
            var sut = HAL.Builder()
                .WithState("Name", "Masoud")
                .WithSelfLink("/user/{name}", HttpVerbs.GET)
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == "Masoud");

            //Arrange
            var link = JObject.Parse(sut)[LINK_TITLE][LinkRelations.Self].ToObject(typeof(Link));
            //Assert
            Assert.True((link as Link).HRef == "/user/{name}");
        }
        [Fact]
        public void Should_Successfully_Insrted_Edit_Link_Using_WithFirstLink_Method()
        {
            //Arrange
            var currentDateTime = DateTime.Now;

            //Arrange && Act
            var sut = HAL.Builder()
                .WithState("Name", "Masoud")
                .WithEditLink("/user/{name}", HttpVerbs.GET, true)
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == "Masoud");

            //Arrange
            var link = JObject.Parse(sut)[LINK_TITLE][LinkRelations.Edit].ToObject(typeof(Link));
            //Assert
            Assert.True((link as Link).HRef == "/user/{name}");
        }
        [Fact]
        public void Should_Successfully_Insrted_Curi_Link_Using_WithFirstLink_Method()
        {
            //Arrange
            var currentDateTime = DateTime.Now;

            //Arrange && Act
            var sut = HAL.Builder()
                .WithState("Name", "Masoud")
                .WithCuriLink("userMgmt", "/user/")
                .Build();

            //Assert
            Assert.True(JObject.Parse(sut)["Name"].Value<string>() == "Masoud");

            //Arrange
            var link = JObject.Parse(sut)[LINK_TITLE][LinkRelations.Curries].ToObject(typeof(Link));
            //Assert
            Assert.True((link as Link).HRef == "/user/{rel}");
            Assert.True((link as Link).Templated);
        }
        [Fact]
        public void Test1()
        {
            var result = HAL.Builder()
                .WithState(new { Name = "Masoud", Familty = "Bahrami", Age = 25 })
                .WithSelfLink("/person/20", HttpVerbs.GET, false)
                .WithFirstLink("/person/first", HttpVerbs.GET, false)
                .WithLastLink("/person/last", HttpVerbs.GET, false)
                .WithLink(new LinkObject("test"){Links = new List<Link>
                {
                    Link.New("asd").WithQueryParameter(ScalarQueryParameter.NewBoolean("has" , 2))
                        .WithQueryParameter(ScalarQueryParameter.NewNumber("age" , 1))
                }
                })
                .WithEmbedded(new Embedded("ed:orders")
                                    .WithResource(EmbeddedResource.New(masoud)
                                    .WithSelfLink("/orders/123", HttpVerbs.GET)
                                    .WithLinkObject("basket", "/orders/123", HttpVerbs.GET))
                             )
                .WithEmbedded(new Embedded("ed:saba")
                    .WithResource(EmbeddedResource.New(masoud)
                        .WithSelfLink("/orders/123", HttpVerbs.GET)
                        .WithLinkObject("basket", "/orders/123", HttpVerbs.GET))
                )

                .Build();
        }
        [Fact]
        public void Test2()
        {
            var propertyInfos = typeof(Person).GetProperties();
            foreach (var info in propertyInfos)
            {

            }
            
            EmbeddedCollection embeddedCollection = new EmbeddedCollection("Ordered")
                {
                    masoud
                };

            var result = HAL.Builder()
                .WithState(masoud)
                .WithEmbededState(embeddedCollection)
                .Build();
        }
    }
}