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
    public partial class HAL
    {
        public HAL WithEditLink(Link link)
        {
            linkObjects.Add(new LinkObject(LinkRelations.Edit) { Links = new List<Link> { link } });
            return this;
        }
        public HAL WithEditLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "")
        {
            linkObjects.Add(new LinkObject(LinkRelations.Edit)
            {
                Links = new List<Link>
            {
                new Link(href ,httpVerb, templated , name , type , deprecation)
            }
            });
            return this;
        }
    }
}