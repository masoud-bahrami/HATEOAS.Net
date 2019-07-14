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
using System;
using System.Collections.Generic;

namespace HATEOAS.Net.HAL
{
    public partial class HAL
    {
        private const string HREF_TEMPLATE = "/{rel}";
        public HAL WithCuriLink(string name, string href)
        {
            if (href.IndexOf(HREF_TEMPLATE) == -1)
            {
                href += HREF_TEMPLATE;
                href = normilizeHRef(href);
            }
            linkObjects.Add(new LinkObject(LinkRelations.Curries)
            {
                Links = new List<Link> { new Link(href,"", true, name) }
            });
            return this;
        }

        private string normilizeHRef(string href)
        {
            return href.Replace("//", "/");
        }

        public HAL WithCuriLink(params (string, string)[] curies)
        {
            var links = new List<Link>();
            foreach (var curi in curies)
            {
                links.Add(Link.New(curi.Item2, true, curi.Item1));
            }

            linkObjects.Add(new LinkObject(LinkRelations.Curries)
            {
                Links = links
            });

            return this;
        }
    }
}