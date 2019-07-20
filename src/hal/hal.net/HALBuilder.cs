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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HATEOAS.Net.HAL
{
    public enum HALFormat
    {
        HAL_Json,
        Json
    }
    public partial class HAL
    {
        private HAL()
        {
            linkObjects = new List<LinkObject>();
            ownState = new Dictionary<string, object>();
            embeddeds = new List<Embedded>();
        }
        public static HAL Builder()
        {
            return new HAL();
        }
        private ICollection<LinkObject> linkObjects;
        private ICollection<Embedded> embeddeds;
        private object ownObjctState;
        private Dictionary<string, object> ownState;
        private HALFormat format;

        private const string LINK_TITLE = "_links";
        private const string EMBEDDED_TITLE = "_embedded";
        private const string HREF = "href";
        public HAL WithLinks(ICollection<LinkObject> links)
        {
            linkObjects = links;
            return this;
        }
        public HAL WithLinks(params LinkObject[] linksObjects)
        {
            linkObjects = linksObjects;
            return this;
        }
        public HAL WithLink(LinkObject linksObject)
        {
            linkObjects.Add(linksObject);
            return this;
        }

        public HAL WithEmbeddeds(ICollection<Embedded> embeddeds)
        {
            foreach (var embedded in embeddeds)
            {
                WithEmbedded(embedded);
            }
            return this;
        }
        public HAL WithEmbeddeds(params Embedded[] embeddeds)
        {
            WithEmbeddeds(embeddeds);
            return this;
        }
        public HAL WithEmbedded(Embedded embedded)
        {
            embeddeds.Add(embedded);
            return this;
        }

        public HAL WithState(object state)
        {
            ownObjctState = state;
            return this;
        }

        public HAL WithState(Dictionary<string, object> states)
        {
            ownState = states;
            return this;
        }
        public HAL WithState(string propName, object propValue)
        {
            ownState.Add(propName, propValue);
            return this;
        }

        public HAL As(HALFormat format)
        {
            this.format = format;
            return this;
        }
        public string Build()
        {
            Guard();

            JObject jsonObject = new JObject();
            SetObjectState(jsonObject);
            SetKeyValueState(jsonObject);
            jsonObject[LINK_TITLE] = Links(linkObjects);
            jsonObject[EMBEDDED_TITLE] = Embedded();
            var hal = jsonObject.ToString(Formatting.None);
            return hal;
        }

        private void SetKeyValueState(JObject jsonObject)
        {
            if (ownState != null && ownState.Any())
            {
                foreach (var keyValue in ownState)
                {
                    jsonObject[keyValue.Key] = keyValue.Value.ToJToken();
                }
            }
        }

        private void SetObjectState(JObject jsonObject)
        {
            if (ownObjctState != null)
            {
                foreach (var keyValue in ownObjctState.BreakesToItsProperties())
                {
                    jsonObject[keyValue.Key] = keyValue.Value.ToJToken();
                }
            }
        }

        private void Guard()
        {
            if (ownObjctState == null && (ownState == null || !ownState.Any()))
            {
                throw new HalObjectStateIsNullExeption();
            }
        }

        private JToken Embedded()
        {
            JObject linkObjects = new JObject();
            if (embeddeds != null && embeddeds.Any())
            {
                foreach (var embedded in embeddeds)
                {
                    List<JObject> jobjects = new List<JObject>();
                    foreach (var resource in embedded.Resources)
                    {
                        JObject link = new JObject
                        {
                            [LINK_TITLE] = Links(resource.LinksObjects)
                        };

                        foreach (var keyValue in resource.Object.BreakesToItsProperties())
                        {
                            link[keyValue.Key] = keyValue.Value.ToJToken();
                        }
                        jobjects.Add(link);
                    }
                    linkObjects[embedded.ResourceName] = new JArray(jobjects);
                }
            }

            return linkObjects;
        }

        private JObject Links(ICollection<LinkObject> linkObjects)
        {
            JObject links = new JObject();
            if (linkObjects != null && linkObjects.Any())
            {
                foreach (var linkObject in linkObjects)
                {
                    if (linkObject.HasOnlyOneLink())
                    {
                        links[linkObject.Relation] = GetLinkObject(linkObject.Links.Single());
                    }
                    else
                    {
                        JArray linkArray = new JArray();
                        foreach (var linkObjectLink in linkObject.Links)
                        {
                            var jObject = GetLinkObject(linkObject.Links.Single());
                            linkArray.Add(jObject);
                        }
                        links[linkObject.Relation] = linkArray;
                    }
                }
            }

            return links;
        }

        private JObject GetLinkObject(Link link)
        {
            JObject linkValue = new JObject();
            foreach (var valueTuple in link.GetProperties())
            {
                linkValue[valueTuple.Item1] = valueTuple.Item2.ToJToken();
            }

            var list = new List<JObject>();
            foreach (var valueTuple in link.GetQueryParameters())
            {
                JObject param = new JObject();
                foreach (var o in valueTuple)
                {
                    param[o.Key] = o.Value.ToJToken();
                }
                list.Add(param);
            }

            linkValue[link.QueryParametersTitle] = list.ToJToken();
            return linkValue;
        }
    }
}