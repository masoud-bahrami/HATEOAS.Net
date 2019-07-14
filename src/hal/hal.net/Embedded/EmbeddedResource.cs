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
using System.Collections.Generic;

namespace HATEOAS.Net.HAL
{
    public partial class EmbeddedResource
    {
        public static EmbeddedResource New(object obj)
        {
            return new EmbeddedResource(obj);
        }

        public EmbeddedResource(object obj)
        {
            Object = obj ?? throw new ResourceStateNullExeption();
            LinksObjects = new List<LinkObject>();
        }
        public EmbeddedResource WithObject(object obj)
        {
            Object = obj;
            return this;
        }
        public EmbeddedResource WithLinkObject(LinkObject linkObject)
        {
            LinksObjects.Add(linkObject);
            return this;
        }
        public EmbeddedResource WithLinkObjects(ICollection<LinkObject> linkObjects)
        {
            foreach (var linkObject in linkObjects)
            {
                WithLinkObject(linkObject);
            }
            return this;
        }

        public ICollection<LinkObject> LinksObjects { get; set; }
        public object Object { get; set; }
        public Embedded Embedded { get; set; }
    }
}