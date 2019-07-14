/*
  HATEOAS.Net solution contains implementations of 
    Hypermedia as the engine of application state (HATEOAS)
    based on different specifications.

 HATEOAS.Net.HAL is an implementation of HAL's Specification, and it also contains some
 extra features such as Link httpVerb(GET, POST ...) and also action parameters.

 Masoud Bahrami
 http://refactor.ir
 https://twitter.com/masodbahrami
 */

using System.Collections.Generic;

namespace HATEOAS.Net.HAL
{
    public interface IState
    {
        [Ignore]
        List<LinkObject> LinkObjects { get; }

        object GetState();
    }
    public abstract class State : IState
    {
        public State()
        {
            LinkObjects = new List<LinkObject>();
        }
        [Ignore]
        public List<LinkObject> LinkObjects { get; private set; }

        protected void AddLink(string relation, Link link)
        {
            LinkObjects.Add(new LinkObject(relation)
            {
                Links = new List<Link> { link }
            });
        }
        protected void AddFirstLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "")
        {
            AddLink(LinkRelations.First, new Link(href, httpVerb, templated, name, type, deprecation));
        }
        protected void AddLastLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "")
        {
            AddLink(LinkRelations.Last, new Link(href, httpVerb, templated, name, type, deprecation));
        }
        protected void AddPreviousLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "")
        {
            AddLink(LinkRelations.Previous, new Link(href, httpVerb, templated, name, type, deprecation));
        }
        protected void AddNextLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "")
        {
            AddLink(LinkRelations.Next, new Link(href, httpVerb, templated, name, type, deprecation));
        }
        protected void AddSelfLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "")
        {
            AddLink(LinkRelations.Self, new Link(href, httpVerb, templated, name, type, deprecation));
        }
        protected void AddEditLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "")
        {
            AddLink(LinkRelations.Edit, new Link(href, httpVerb, templated, name, type, deprecation));
        }
        protected void AddCuriLink(string href, bool? templated = null, string name = "", string type = "", string deprecation = "")
        {
            AddLink(LinkRelations.Curries, Link.New(href, templated, name, type, deprecation));
        }

        public abstract object GetState();
    }
}