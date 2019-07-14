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

using HATEOAS.Net.HAL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HATEOAS.Net.HAL
{
    public class LinkObject
    {
        public LinkObject(string relation)
        {
            if (string.IsNullOrEmpty(relation))
            {
                throw new LinkRelationNullExeption();
            }
            Relation = relation;
        }
        /// <summary>
        /// Describes at https://tools.ietf.org/html/rfc5988
        /// </summary>
        public string Relation { get; internal set; }
        public ICollection<Link> Links { get; set; }
        public bool HasOnlyOneLink()
        {
            return Links.Any() && Links.Count == 1;
        }
        public bool HasAnyLink()
        {
            return Links.Any();
        }

        public int LinkCount()
        {
            return Links.Count;
        }
    }
    public class LinkObjectBuilder
    {
        private static LinkObject linkObject;
        public LinkObjectBuilder(string relation)
        {
            linkObject = new LinkObject(relation)
            {
                Links = new List<Link>()
            };
        }
        public LinkObjectBuilder WithLink(Link link)
        {
            linkObject.Links.Add(link);
            return this;
        }
        public LinkObjectBuilder WithLinks(ICollection<Link> links)
        {
            foreach (var link in links)
            {
                WithLink(link);
            }

            return this;
        }
        public LinkObjectBuilder WithLinks(params Link[] links)
        {
            WithLinks(links.ToList());
            return this;
        }
        public LinkObject Build()
        {
            if (!linkObject.HasAnyLink())
            {
                throw new LinkObjectLinksCollectionEmptyExeption();
            }
            return linkObject;
        }
    }
}