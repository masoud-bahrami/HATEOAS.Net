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



namespace HATEOAS.Net.HAL
{
    /* 
https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods
 */
    /// <summary>
    /// HTTP defines a set of request methods to indicate the desired action to be performed for a given resource.
    /// Although they can also be nouns, these request methods are sometimes referred as HTTP verbs.
    /// Each of them implements a different semantic, but some common features are shared by a group of them:
    /// e.g. a request method can be safe, idempotent, or cacheable.
    /// </summary>
    public class HttpVerbs
    {
        /// <summary>
        /// The GET method requests a representation of the specified resource.
        /// Requests using GET should only retrieve data.
        /// </summary>
        public static string GET = "GET";
        /// <summary>
        /// The HEAD method asks for a response identical to that of a GET request,
        /// but without the response body.
        /// </summary>
        public static string HEAD = "HEAD";
        /// <summary>
        /// The POST method is used to submit an entity to the specified resource, often causing a change in state or side effects on the server.
        /// </summary>
        public static string POST = "POST";
        /// <summary>
        /// The PUT method replaces all current representations of the target resource with the request payload.
        /// </summary>
        public static string PUT = "PUT";
        /// <summary>
        /// The DELETE method deletes the specified resource.
        /// </summary>
        public static string DELETE = "DELETE";
        /// <summary>
        /// The CONNECT method establishes a tunnel to the server identified by the target resource.
        /// </summary>
        public static string CONNECT = "CONNECT";
        /// <summary>
        /// The OPTIONS method is used to describe the communication options for the target resource.
        /// </summary>
        public static string OPTIONS = "OPTIONS";
        /// <summary>
        /// The TRACE method performs a message loop-back test along the path to the target resource.
        /// </summary>
        public static string TRACE = "TRACE";
        /// <summary>
        /// The PATCH method is used to apply partial modifications to a resource.
        /// </summary>
        public static string PATCH = "PATCH";

    }
}
