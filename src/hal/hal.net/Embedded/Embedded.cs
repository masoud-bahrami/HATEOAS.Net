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
    public class Embedded
    {
        public Embedded(string resourceName, ICollection<EmbeddedResource> resources = null)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new EmbeddedResourceNameNullOrEmptyException();

            ResourceName = resourceName;
            Resources = resources ?? new List<EmbeddedResource>();
        }
        public Embedded WithResource(EmbeddedResource embeddedResource)
        {
            Resources.Add(embeddedResource);
            return this;
        }
        public string ResourceName { get; set; }
        public ICollection<EmbeddedResource> Resources { get; set; }
    }
}