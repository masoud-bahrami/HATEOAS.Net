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
        public HALEmbedded WithState(IState state)
        {
            ownObjctState = state.GetState();
            WithLinks(state.LinkObjects);
            return new HALEmbedded(this);
        }
    }

    public class HALEmbedded
    {
        private HAL _hal;
        public HALEmbedded(HAL hal)
        {
            _hal = hal;
        }

        public HALEmbedded WithEmbededState(IEmbededState embededState)
        {
            var embeddeds = new List<Embedded>();
            var embedded = new Embedded(embededState.ResourceName);
            foreach (var embededStateState in embededState.States)
            {
                embedded.WithResource(EmbeddedResource.New(embededStateState.GetState())
                    .WithLinkObjects(embededStateState.LinkObjects));
            }
            embeddeds.Add(embedded);
            _hal.WithEmbeddeds(embeddeds);
            return this;
        }

        public string Build()
        {
            return _hal.Build();
        }
    }
}