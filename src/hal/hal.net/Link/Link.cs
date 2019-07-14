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
using HATEOAS.Net.HAL.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace HATEOAS.Net.HAL
{
    public partial class Link
    {
        private const string HREF = "href";
        private const string HTTP_VERB = "httpVerb";
        private const string TEMPLATED = "templated";
        private const string NAME = "name";
        private const string TYPE = "type";
        private const string DEPRECATION = "deprecation";
        private const string QUERY_PARAMETERS = "queryParamaters";

        public static Link NewGET(string href, bool? templated = null, string name = "", string type = "",
            string deprecation = "")
        {
            return new Link(href, HttpVerbs.GET, templated, name, type, deprecation);
        }
        public static Link NewPOST(string href, bool? templated = null, string name = "", string type = "",
            string deprecation = "")
        {
            return new Link(href, HttpVerbs.POST, templated, name, type, deprecation);
        }
        public static Link NewCONNECT(string href, bool? templated = null, string name = "", string type = "",
            string deprecation = "")
        {
            return new Link(href, HttpVerbs.CONNECT, templated, name, type, deprecation);
        }
        public static Link NewDELETE(string href, bool? templated = null, string name = "", string type = "",
            string deprecation = "")
        {
            return new Link(href, HttpVerbs.DELETE, templated, name, type, deprecation);
        }
        public static Link NewHEAD(string href, bool? templated = null, string name = "", string type = "",
            string deprecation = "")
        {
            return new Link(href, HttpVerbs.HEAD, templated, name, type, deprecation);
        }
        public static Link NewOPTIONS(string href, bool? templated = null, string name = "", string type = "",
            string deprecation = "")
        {
            return new Link(href, HttpVerbs.OPTIONS, templated, name, type, deprecation);
        }
        public static Link NewPATCH(string href, bool? templated = null, string name = "", string type = "",
            string deprecation = "")
        {
            return new Link(href, HttpVerbs.PATCH, templated, name, type, deprecation);
        }
        public static Link NewPUT(string href, bool? templated = null, string name = "", string type = "",
            string deprecation = "")
        {
            return new Link(href, HttpVerbs.PUT, templated, name, type, deprecation);
        }
        public static Link NewTRACE(string href, bool? templated = null, string name = "", string type = "",
            string deprecation = "")
        {
            return new Link(href, HttpVerbs.TRACE, templated, name, type, deprecation);
        }
        public static Link New(string href, bool? templated = null, string name = "", string type = "",
            string deprecation = "")
        {
            return new Link(href, "", templated, name, type, deprecation);
        }
        [JsonConstructor]
        public Link(string href, string httpVerb = "", bool? templated = null, string name = "", string type = "", string deprecation = "")
        {
            if (string.IsNullOrEmpty(href))
            {
                throw new HRefNullExeption();
            }

            HttpVerb = httpVerb;
            HRef = href;
            Templated = templated;
            Type = type;
            Name = name;
            Deprecation = deprecation;
            QueryParameters = new List<ScalarQueryParameter>();
        }
        public List<(string, object)> GetProperties()
        {
            List<(string, object)> res = new List<(string, object)>
            {
                (HREF, HRef)
            };

            if (HttpVerb != null)
            {
                res.Add((HTTP_VERB, HttpVerb));
            }
            if (Templated != null)
            {
                res.Add((TEMPLATED, Templated));
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                res.Add((NAME, Name));
            }
            if (!string.IsNullOrWhiteSpace(Type))
            {
                res.Add((TYPE, Type));
            }

            if (!string.IsNullOrWhiteSpace(Deprecation))
            {
                res.Add((DEPRECATION, Deprecation));
            }

            if (QueryParameters.Any())
            {
                res.Add((QUERY_PARAMETERS, QueryParameters));
            }

            return res;
        }

        public Link WithQueryParameter(ScalarQueryParameter parameter)
        {
            if(QueryParameters.Any(p=>p.Position == parameter.Position))
                throw new LinkQueryParameterPositionDuplicatedException();

            QueryParameters.Add(parameter);
            return this;
        }
        public Link WithQueryParameters(ICollection<ScalarQueryParameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                WithQueryParameter(parameter);
            }
            return this;
        }
        /// <summary>
        ///    The "href" property is REQUIRED.
        ///   Its value is either a URI [RFC3986] or a URI Template [RFC6570].
        ///   If the value is a URI Template then the HRefNullExeption Object SHOULD have a
        ///   "templated" attribute whose value is true.
        /// </summary>
        public string HRef { get; }
        /// <summary>
        /// the "httpVerb" is OPTIONAL.
        /// Its value is string, and is used to specify the HttpVerb of the link(action method).
        /// This feature does not exist in the original HAL specification.
        /// </summary>
        public string HttpVerb { get; private set; }
        /// <summary>
        /// The "templated" property is OPTIONAL.
        /// Its value is boolean and SHOULD be true when the HRefNullExeption Object's "href"
        /// property is a URI Template.
        ///
        /// Its value SHOULD be considered false if it is undefined or any other
        /// value than true.
        /// </summary>
        public bool? Templated { get; private set; }
        /// <summary>
        ///  The "type" property is OPTIONAL.
        /// Its value is a string used as a hint to indicate the media type
        /// expected when dereferencing the target resource.
        /// </summary>
        public string Type { get; private set; }

        internal string QueryParametersTitle
        {
            get { return QUERY_PARAMETERS; }
        }
        internal IEnumerable<Dictionary<string, object>> GetQueryParameters()
        {
            foreach (var parameter in QueryParameters)
            {
                var dictionary = new Dictionary<string, object>
                {
                    { "Title", parameter.Title },
                    { "Position", parameter.Position },
                    { "Type",Enum.GetName(typeof(QueryParameterType) ,  parameter.Type )}
                };
                yield return dictionary;
            }
        }

    /// <summary>
    ///  The "deprecation" property is OPTIONAL.
    /// Its presence indicates that the link is to be deprecated(i.e. removed)
    /// at a future date.Its value is a URL that SHOULD provide
    /// further information about the deprecation.
    /// A client SHOULD provide some notification (for example, by logging a
    /// warning message) whenever it traverses over a link that has this
    /// property.The notification SHOULD include the deprecation property's
    /// value so that a client manitainer can easily find information about
    /// the deprecation.
    /// </summary>
    public string Deprecation { get; private set; }
    /// <summary>
    ///  The "name" property is OPTIONAL.
    /// Its value MAY be used as a secondary key for selecting HRefNullExeption Objects
    /// which share the same relation type.
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// The "queryParameters" is OPTIONAL.
    /// Its value is a collection of "ScalarQueryParameter", and is used to specify the QueryParameters of the link(action method).
    /// This feature does not exist in the original HAL specification.
    /// </summary>
    public ICollection<ScalarQueryParameter> QueryParameters { get; private set; }
}
}