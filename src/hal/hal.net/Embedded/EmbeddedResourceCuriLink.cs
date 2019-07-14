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
    public partial class EmbeddedResource
    {
        private const string HREF_TEMPLATE = "/{rel}";
        public EmbeddedResource WithCuriLink(string name, string href)
        {
            if (href.IndexOf(HREF_TEMPLATE) == -1)
            {
                href += HREF_TEMPLATE;
            }
            LinksObjects.Add(new LinkObject(LinkRelations.Curries)
            {
                Links = new List<Link> { Link.New(href, true, name) }
            });
            return this;
        }
        public EmbeddedResource WithCuriLink(params (string, string)[] curies)
        {
            var links = new List<Link>();
            foreach (var curi in curies)
            {
                links.Add(Link.New(curi.Item2, true, curi.Item1));
            }

            LinksObjects.Add(new LinkObject(LinkRelations.Curries)
            {
                Links = links
            });

            return this;
        }
    }
}