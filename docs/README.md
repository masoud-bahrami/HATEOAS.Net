**HATEOAS.Net**

------

![Conda](https://img.shields.io/conda/pn/conda-forge/python.svg?color=red&style=plastic)

Online documents: https://masoud-bahrami.github.io/HATEOAS.Net/

------

**How to use:**

|                 |                                                              |
| --------------- | ------------------------------------------------------------ |
| Nugget package  | https://www.nuget.org/packages/HATEOAS.Net.HAL/1.0.0         |
| Package version | 1.0.0                                                        |
| .NET Version    | .NET Standard 2.0                                            |
| Compatibility   | .NET Code version 2.0 and above, .NET standard 4.6.1 and above |

------



 **[Hypermedia](https://en.wikipedia.org/wiki/Hypermedia)** is the core component of REST API. Whiteout them it's just a service which uses HTTP verbs.

 As Roy Fielding(the creator of REST Architecture) states his 2000 PhD dissertation ( "Architectural Styles and the Design of Network-based Software Architectures") an REST Architecture has some design constrains. 

Roy defined it as:

> *REST's client-server  [separation of concerns](https://en.wikipedia.org/wiki/Separation_of_concerns) simplifies component implementation, reduces the complexity of connector semantics, improves the effectiveness of performance tuning, and increases the scalability of pure server components. Layered system constraints allow intermediaries—[proxies](https://en.wikipedia.org/wiki/Proxy_server), [gateways](https://en.wikipedia.org/wiki/Gateway_(telecommunications)), and [firewalls](https://en.wikipedia.org/wiki/Firewall_(computing))—to be introduced at various points in the communication without changing the interfaces between components, thus allowing them to assist in communication translation or improve performance via large-scale, shared caching. REST enables intermediate processing by constraining messages to be self-descriptive: interaction is stateless between requests, standard methods and media types are used to indicate semantics and exchange information, and responses explicitly indicate [cacheability](https://en.wikipedia.org/wiki/Web_cache).*

--[Wikipedia](https://en.wikipedia.org/wiki/Representational_state_transfer)

Constrains specified by Roy are: 

1. Client-server architecture

2. Statelessness

3. Cacheability

4. Layered system

5. Code on demand (optional)

6. Uniform interface

   Uniform interface have some constrains :

   ​	**Resource identification in requests**

   ​	**Resource manipulation through representations**

   ​	**Self-descriptive messages**

   ​	**Hypermedia as the engine of application state (HATEOAS)**

   

------

**HATEOAS** 

HATEOAS is mean having access to just entry point of any *Resource Endpoint*, as a human or system, you should be able to discover all services provided by the server, and how to communicate with server. A REST client needs little to no prior knowledge about how to interact with an application or server beyond a generic understanding of hypermedia.

REST Architecture lets you rich http responses using hypermedia links to gives the REST client chances to dynamically travers provided services using this links.

------
 **HATEOAS.Net**

 **HATEOAS.Net** contains some implementations of HATEOAS in C#  based on different specifications. Different solutions have been proposed for the use of hypertexts. Hypertext Application Language(HAL) is one of these solutions. 
 HAL is a simple and very popular way to inject hypertexts into http responses based on JSON format. HAL is [drafted](https://tools.ietf.org/html/draft-kelly-json-hal-08) by  [Mike Kelly](mike@stateless.co).

------
 **HATEOAS.Net.HAL**

 **HATEOAS.Net.HAL** is an agile, simple, lightweight and easy to use HAL specification for .NET developers. It also added some optional but very useful features to the HAL specification:
 1. Adding **HTTP verbs** to the link. (The link can be richer by adding HTTP verbs(GET, POST, DELETE and ...))
 2. Adding **Query Parameters** to the link. (The link can be more useful by specifying its parameters)

------
 **How to use HATEOAS.Net.HAL**

 Every http response is contains 3 main parts.
 1. **State** (State is the resource which exposed by the REST API. For example order.)
 2. **Hypertexts** (Hypertexts is represented as links.)
 3. **Embedded State with Hypertexts** (Embedded State is when the response is a collection of states or the response has some related resources wich client is care about it.)


 **HATEOAS.Net.HAL** provided a simple fluent builder to generate a HAP response

 ```c#
                HAL.Builder()
                .WithState(new { Name = "Masoud", Family = "Bahrami", Age = 28 }) // State or Resource
                .WithSelfLink("/person/20", HttpVerbs.GET, false) //Hypertext
                .WithFirstLink("/person/first", HttpVerbs.GET, false)//Hypertext
                .WithLastLink("/person/last", HttpVerbs.GET, false)//Hypertext
                .WithLink(new LinkObject("test"){Links = new List<Link> //Hypertext
                {
                    Link.New("asd").WithQueryParameter(ScalarQueryParameter.NewBoolean("has" , 2))
                        .WithQueryParameter(ScalarQueryParameter.NewNumber("age" , 1))
                }
                })
                .WithEmbedded(new Embedded("ed:orders") //Embedded 
                                    .WithResource(EmbeddedResource.New(masoud)
                                    .WithSelfLink("/orders/123", HttpVerbs.GET)
                                    .WithLinkObject("basket", "/orders/123", HttpVerbs.GET))
                             )
                .WithEmbedded(new Embedded("ed:saba") //Embedded 
                    .WithResource(EmbeddedResource.New(masoud)
                        .WithSelfLink("/orders/123", HttpVerbs.GET)
                        .WithLinkObject("basket", "/orders/123", HttpVerbs.GET))
                )

                .Build();
 ```

  Also for the case of simplicity state object can be inherited from:
  ```c#
  public interface IState
    {
        List<LinkObject> LinkObjects { get; }

        object GetState();
    }
  ```
  and embedded states can be inherited from:
  ```c#
  public interface IEmbededState
    {
        string ResourceName { get;}

        ICollection<IState> States { get; }
    }
  ```
So we can more easily generate the HAL Response:
```c#
                 HAL.Builder()
                .WithState(person)
                .WithEmbededState(embeddedCollection)
                .Build();
```
-------
**How to use Links**

 Every links have some specifications:

 1. HRef. Its value is either a URI [RFC3986] or a URI Template [RFC6570]
 2. HTTP Verb. Its value is string, and is used to specify the HttpVerb of the link(action method).
 3. Is Templated. Its value is boolean and SHOULD be true when the HRef is a URI template (/orders/{id})
 4. Name. Its value MAY be used as a secondary key for selecting HRef Objects
 5. Type. Its value is a string used as a hint to indicate the media type expected when dereferencing the target resource.
 6. Deprecation. Its presence indicates that the link is to be deprecated(i.e. removed)
 7. Query Parameters. Its value is a collection of "ScalarQueryParameter", and is used to specify the QueryParameters of the link(action method).

 HTTP Verbs should be one of this verbs:
 ```c#
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
 ```
 For every http verbs there is a helper method to create a new links:
 ```c#
 Link.NewGET("/orders"); // Create a new GET link
 Link.NewPOST("/orders");// Create a new POST link
 Link.NewCONNECT("/orders");// Create a new CONNECT link
 Link.NewDELETE("/orders");// Create a new DELETE link
 Link.NewHEAD("/orders");// Create a new HEAD link
 Link.NewOPTIONS("/orders");// Create a new OPTIONS link
 Link.NewPATCH("/orders");// Create a new PATCH link
 Link.NewPUT("/orders");// Create a new PUT link
 Link.NewTRACE("/orders");// Create a new TRACE link
 ```
 Adding query parameters to the link. Every query parameters have some fueatures. Its name, position and type. The position is the location of the parameter in the URL, for example the first parameter is name or the secound parameter is age.Type can be one of this:
 ```c#
 enum QueryParameterType
    {
        String,
        Boolean,
        Number,
        DateTime,
        Char,
        Enum,
        Object,
        Collection
    }
 ```

 To define a new query parameter we should use this class:
 ```C#
 ScalarQueryParameter.NewString(string title, short position);
 ScalarQueryParameter.NewBoolean(string title, short position);
 ScalarQueryParameter.NewNumber(string title, short position);
 ScalarQueryParameter.NewDateTime(string title, short position);
 ScalarQueryParameter.NewChar(string title, short position);
 ```

 Adding the parameter to the link:
 ```c#
 Link.NewTRACE("/orders/{id}")
     .WithQueryParameter(ScalarQueryParameter.NewBoolean("isAdmin", 1));
 ```

Link exceptions:

1. *HRef*  is required. When creating a new link, If *HRef* is null or white space  *HRefNullExeption* will be raised. 

   

------

**LinkObject**

Every hypertext in the HAL response that contains a **Link Relations** and a **Link .**  

 **LinkRelations**  is a static class that contains some standard relation types described in [rfc5988](https://tools.ietf.org/html/rfc5988).

```c#
 public static class LinkRelations
    {
        public static readonly string Curries= "curies";

        /// <summary>
        /// o  Relation Name: alternate
        /// o Description: Designates a substitute for the link's context.
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Alternate => "alternate";
        /// <summary>
        ///  o  Relation Name: appendix
        /// o Description: Refers to an appendix.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Appendix => "appendix";
        /// <summary>
        ///  o  Relation Name: bookmark
        /// o Description: Refers to a bookmark or entry point.
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Bookmark => "bookmark";
        /// <summary>
        ///   o  Relation Name: chapter
        /// o Description: Refers to a chapter in a collection of resources.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Chapter => "chapter";
        /// <summary>
        ///  o  Relation Name: contents
        /// o Description: Refers to a table of contents.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Contents => "contents";
        /// <summary>
        ///  o  Relation Name: copyright
        /// o Description: Refers to a copyright statement that applies to the
        /// link's context.
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Copyright => "copyright";
        /// <summary>
        ///  o  Relation Name: current
        /// o Description: Refers to a resource containing the most recent item(s) in a collection of resources.
        /// o  Reference: [RFC5005]
        /// </summary>
        public static string Current => "current";
        /// <summary>
        ///   o  Relation Name: describedby
        /// o Description: Refers to a resource providing information about the link's context.
        /// o Documentation: <http://www.w3.org/TR/powder-dr/#assoc-linking>
        /// </summary>
        public static string Describedby => "describedby";
        /// <summary>
        ///  o  Relation Name: edit
        /// o Description: Refers to a resource that can be used to edit the link's context.
        /// o Reference: [RFC5023]
        /// </summary>
        public static string Edit => "edit";
        /// <summary>
        /// o  Relation Name: edit-media
        /// o Description: Refers to a resource that can be used to edit media associated with the link's context.
        /// o Reference: [RFC5023]
        /// </summary>
        public static string EditMedia => "edit-media";
        /// <summary>
        ///    o  Relation Name: enclosure
        /// o Description: Identifies a related resource that is potentially large and might require special handling.
        /// o Reference: [RFC4287]
        /// </summary>
        public static string Enclosure => "enclosure";
        /// <summary>
        ///   o  Relation Name: first
        /// o Description: An IRI that refers to the furthest preceding resource in a series of resources.
        /// o  Reference: [RFC5988]
        /// o  Notes: this relation type registration did not indicate a reference.  Originally requested by Mark Nottingham in December 2004.
        /// </summary>
        public static string First => "first";
        /// <summary>
        ///    o  Relation Name: glossary
        /// o Description: Refers to a glossary of terms.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Glossary => "glossary";
        /// <summary>
        ///    o  Relation Name: help
        ///   o Description: Refers to a resource offering help(more information, links to other sources information, etc.)
        ///   o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Help => "help";
        /// <summary>
        ///    o  Relation Name: hub
        /// o Description: Refers to a hub that enables registration for notification of updates to the context.
        /// o  Reference: <http://pubsubhubbub.googlecode.com/> <http://pubsubhubbub.googlecode.com/svn/trunk/pubsubhubbub-core-0.3.html>
        /// o Notes: this relation type was requested by Brett Slatkin.
        /// </summary>
        public static string Hub => "hub";
        /// <summary>
        ///  o  Relation Name: index
        /// o Description: Refers to an index.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Index => "index";
        /// <summary>
        /// o  Relation Name: last
        /// o Description: An IRI that refers to the furthest following resource in a series of resources.
        /// o  Reference: [RFC5988]
        /// o  Notes: this relation type registration did not indicate a reference.  Originally requested by Mark Nottingham in December2004.
        /// </summary>
        public static string Last => "last";
        /// <summary>
        /// o  Relation Name: latest-version
        /// o Description: Points to a resource containing the latest(e.g., current) version of the context.
        /// o  Reference: [RFC5829]
        /// </summary>
        public static string LatestVersion => "latest-version";
        /// <summary>
        ///  o  Relation Name: license
        /// o Description: Refers to a license associated with the link's context.
        /// o Reference: [RFC4946]
        /// </summary>
        public static string License => "license";
        /// <summary>
        ///  o  Relation Name: next
        /// o Description: Refers to the next resource in a ordered series of resources.
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Next => "next";
        /// <summary>
        ///  o  Relation Name: next-archive
        /// o Description: Refers to the immediately following archive resource.
        /// o Reference: [RFC5005]
        /// </summary>
        public static string NextArchive => "next-archive";
        /// <summary>
        ///  o  Relation Name: payment
        /// o Description: indicates a resource where payment is accepted.
        /// o Reference: [RFC5988]
        /// o Notes: this relation type registration did not indicate a
        /// reference.Requested by Joshua Kinberg and Robert Sayre.  It is meant as a general way to facilitate acts of payment, and thus
        /// this specification makes no assumptions on the type of payment or
        /// transaction protocol.  Examples may include a Web page where
        /// donations are accepted or where goods and services are available
        /// for purchase.rel= "payment" is not intended to initiate an
        /// automated transaction.In Atom documents, a link element with a
        /// rel = "payment" attribute may exist at the feed/channel level and/or
        /// the entry/item level.  For example, a rel = "payment" link at the
        /// feed/channel level may point to a "tip jar" URI, whereas an entry/
        /// item containing a book review may include a rel= "payment" link
        /// that points to the location where the book may be purchased
        /// through an online retailer.
        /// </summary>
        public static string Payment => "payment";
        /// <summary>
        ///  o  Relation Name: prev
        /// o Description: Refers to the previous resource in an ordered series
        /// of resources.Synonym for "previous".
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Prev => "prev";
        /// <summary>
        /// o Relation Name: predecessor-version
        /// o  Description: Points to a resource containing the predecessor
        /// version in the version history.
        /// o Reference: [RFC5829]
        /// </summary>
        public static string PredecessorVersion => "predecessor-version";
        /// <summary>
        ///    o  Relation Name: previous
        /// o Description: Refers to the previous resource in an ordered series of resources.Synonym for "prev".
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Previous => "previous";
        /// <summary>
        ///  o  Relation Name: prev-archive
        /// o Description: Refers to the immediately preceding archive resource.
        /// o Reference: [RFC5005]
        /// </summary>
        public static string PrevArchive => "prev-archive";
        /// <summary>
        ///    o  Relation Name: related
        /// o Description: Identifies a related resource.
        /// o  Reference: [RFC4287]
        /// </summary>
        public static string Related => "related";
        /// <summary>
        ///    o  Relation Name: replies
        /// o Description: Identifies a resource that is a reply to the context
        /// of the link.
        /// o  Reference: [RFC4685]
        /// </summary>
        public static string Replies => "replies";
        /// <summary>
        ///    o  Relation Name: section
        /// o Description: Refers to a section in a collection of resources.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Section => "section";
        /// <summary>
        /// o  Relation Name: self
        /// o Description: Conveys an identifier for the link's context.
        /// o Reference: [RFC4287]
        /// </summary>
        public static string Self => "self";
        /// <summary>
        /// o  Relation Name: service
        /// o Description: Indicates a URI that can be used to retrieve a service document.
        /// o  Reference: [RFC5023]
        /// o  Notes: When used in an Atom document, this relation type specifies
        /// Atom Publishing Protocol service documents by default.  Requested
        /// by James Snell.
        /// </summary>
        public static string Service => "service";
        /// <summary>
        /// o  Relation Name: start
        /// o Description: Refers to the first resource in a collection of
        /// resources.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Start => "start";
        /// <summary>
        ///    o  Relation Name: stylesheet
        /// o Description: Refers to an external style sheet.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Stylesheet => "stylesheet";
        /// <summary>
        ///    o  Relation Name: subsection
        /// o Description: Refers to a resource serving as a subsection in a
        /// collection of resources.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Subsection => "subsection";
        /// <summary>
        ///   o  Relation Name: successor-version
        /// o Description: Points to a resource containing the successor version
        /// in the version history.
        /// o Reference: [RFC5829]
        /// </summary>
        public static string SuccessorVersion => "successor-version";
        /// <summary>
        ///  o  Relation Name: up
        /// o Description: Refers to a parent document in a hierarchy of
        /// documents.
        /// o  Reference: [RFC5988]
        /// o  Notes: this relation type registration did not indicate a
        /// reference.  Requested by Noah Slater.
        /// </summary>
        public static string Up => "up";
        /// <summary>
        ///  o  Relation Name: version-history
        /// o Description: points to a resource containing the version history
        /// for the context.
        /// o  Reference: [RFC5829]
        /// </summary>
        public static string VersionHistory => "version-history";
        /// <summary>
        /// o  Relation Name: via
        /// o Description: Identifies a resource that is the source of the
        /// information in the link's context.
        /// o Reference: [RFC4287]
        /// </summary>
        public static string Via => "via";
        /// <summary>
        ///   o  Relation Name: working-copy
        /// o Description: Points to a working copy for this resource.
        /// o Reference: [RFC5829]
        /// </summary>
        public static string WorkingCopy => "working-copy";
        /// <summary>
        ///   o  Relation Name: working-copy-of
        /// o Description: Points to the versioned resource from which this
        /// working copy was obtained.
        /// o Reference: [RFC5829]
        /// </summary>
        public static string WorkingCopyOf => "working-copy-of";
    }
```



**LinkObject**  contains a list of link plus a relation type for hypertext. We can create a new **LinkObject**  using its constructor or use **LinkObjectBuilder**.

```C#
//Create a new 'Self' link 
new LinkObjectBuilder(LinkRelations.Self)
                .WithLink(new Link("/order/{id}", HttpVerbs.GET,true))
                .Build();
```

Link Object exceptions:

1. The relation is required, if it null or white space  *LinkRelationNullExeption* will be raised.
2. If link collection is null or empty *LinkObjectLinksCollectionEmptyExeption* will be raised.

------

**State**

State can be an object or anonymous object.

```c#
//using anonymous object as the State 
HAL.Builder()
 .WithState(new { Name = masoud, Family = "Bahrami", Age = 25 })
```

```c#
  			//Using typed object as the State
            var masoud = new Person("Masoud", "Bahrami", 35);
            
			HAL.Builder()
                .WithState(masoud)
```



Also **State** can be represented as a collection of Key/Value:

```c#
				//the State is specified as a Key/Value property
				//Key is the name of the property
				//value is the value of the property
				HAL.Builder()
                .WithState("Name", "Masoud")
```

```c#
 				//the state can be a  Dictionary of key/value.
				//Key is the name of the property
				//value is the value of the property
				HAL.Builder()
                .WithState(new Dictionary<string, object>
                {
                    { "Name" , "Masoud" },
                    { "Family" , "Bahrami"}
                })
```

If both Object State and Key/Value states is specified, all of them is merged:

```c#
				//Using typed object as the State
            	var masoud = new Person("Masoud", "Bahrami", 35);
            
				HAL.Builder()
				.WithState(masoud)
                .WithState("Phone", "123456789")
                .WithState(new Dictionary<string, object>
                {
                    { "PostalCode" , "2132520" },
                    { "IsAdmin" , true}
                })
```

Result is:

```json

{  
   "FirstName":"Masoud",
   "LastName":"Bahrami",
   "Age":35,
   "Phone":"123456789",
   "PostalCode":"2132520",
   "IsAdmin":true
}
```



------

**State Exceptions**

1. If neither State object nor Key/Value State are specified, *HalObjectStateIsNullExeption* will be raised. 



------

**Adding LinkObject (Hypertext) to the HAL response**

**HAL.Builder** has a couple of methods for adding hypertexts:

```c#
//Using WithLink methods we cann add a link object.
var linkObject1 = new LinkObject("orders")
						{
    						Links = new List<Link>() { new Link("/Order") }
						};

HAL.Builder()
    .WithState("Phone", "123456789")
    .WithLink(linkObject1)
    
    
    var linkObject2 = new LinkObject("orders")
						{
    						Links = new List<Link>() { new Link("/Order") }
						};

//Using WithLinks methods we cann add a collection of link objects.
HAL.Builder()
   .WithState("Phone", "123456789")
   .WithLinks(linkObject1 ,linkObject2)
```

Adding a hypertext with *First* relation:

```c#
            HAL.Builder()
                .WithState("Name", "Masoud")
                .WithFirstLink("/user/{name}", HttpVerbs.GET, true)
                .Build();
```

Adding a hypertext with *Last* relation:

```c#
             HAL.Builder()
                .WithState("Name", "Masoud")
                .WithLastLink("/user/{name}", HttpVerbs.GET, true)
                .Build();
```

Adding a hypertext with *Next* relation:

```c#
             HAL.Builder()
                .WithState("Name", "Masoud")
                .WithNextLink("/user/{name}", HttpVerbs.GET, true)
                .Build();
```

Adding a hypertext with *Previous* relation:

```c#
             HAL.Builder()
                .WithState("Name", "Masoud")
                .WithPreviousLink("/user/{name}", HttpVerbs.GET, true)
                .Build();
```

Adding a hypertext with *Self* relation:

```c#
             HAL.Builder()
                .WithState("Name", "Masoud")
                .WithSelfLink("/user/{name}", HttpVerbs.GET, true)
                .Build();
```

Adding a hypertext with *Edit* relation:

```c#
             HAL.Builder()
                .WithState("Name", "Masoud")
                .WithEditLink("/user/{name}", HttpVerbs.GET, true)
                .Build();
```

Adding a hypertext with *Curi* relation:

```c#
             HAL.Builder()
                .WithState("Name", "Masoud")
                .WithCuriLink("/user/{name}", HttpVerbs.GET, true)
                .Build();
```



------

**Adding Embedded Stats and Links**

**Embedded**  has a Resource Name and a list of **EmbeddedResource**.

**EmbeddedResource** has has a couple of *Builder* methods to create a new one:

```C#
           
			//Create a new EmbeddedResource with one Curi link
			Person masoud = new Person("Masoud", "Bahrami", 35);

            EmbeddedResource.New(masoud)
                			.WithCuriLink("base", "user/masoud");
```



```c#
       //Create a new EmbeddedResource with one Edit link
		Person masoud = new Person("Masoud", "Bahrami", 35);

        EmbeddedResource.New(masoud)
            			.WithEditLink("base", "user/masoud");
```
```c#
   //Create a new EmbeddedResource with one First link
	Person masoud = new Person("Masoud", "Bahrami", 35);

    EmbeddedResource.New(masoud)
        			.WithFirstLink("base", "user/masoud");
```
```c#
    //Create a new EmbeddedResource with one First link
	Person masoud = new Person("Masoud", "Bahrami", 35);

	EmbeddedResource.New(masoud)
    				.WithFirstLink("base", "user/masoud");
```
```c#
	//Create a new EmbeddedResource with one Last link
	Person masoud = new Person("Masoud", "Bahrami", 35);

	EmbeddedResource.New(masoud)
					.WithLastLink("base", "user/masoud");
```
```c#
    //Create a new EmbeddedResource with one Next link
    Person masoud = new Person("Masoud", "Bahrami", 35);

    EmbeddedResource.New(masoud)
                    .WithNextLink("base", "user/masoud");
```
```c#
    //Create a new EmbeddedResource with one Previous link
    Person masoud = new Person("Masoud", "Bahrami", 35);

    EmbeddedResource.New(masoud)
                    .WithPreviousLink("base", "user/masoud");
```
```c#
    //Create a new EmbeddedResource with one Self link
    Person masoud = new Person("Masoud", "Bahrami", 35);

    EmbeddedResource.New(masoud)
                    .WithSelfLink("base", "user/masoud");
```
```c#
    //Create a new EmbeddedResource with one Start link
    Person masoud = new Person("Masoud", "Bahrami", 35);

    EmbeddedResource.New(masoud)
                    .WithStartLink("base", "user/masoud");
```
```c#
    //Create a new EmbeddedResource with one custom Link
    Person masoud = new Person("Masoud", "Bahrami", 35);

    EmbeddedResource.New(masoud)
                    .WithLinkObject(LinkRelations.First, "user/masoud", HttpVerbs.GET);
```
**Adding Embedded resources to the HAL response**

```c#
			var orders = new Embedded("ed:orders")
                                    .WithResource(EmbeddedResource.New(masoud)
                                    .WithSelfLink("/orders/123", HttpVerbs.GET);       

			var basket= new Embedded("ed:basket")
            		        .WithResource(EmbeddedResource.New(masoud)
                        	.WithSelfLink("/orders/123", HttpVerbs.GET);
			
                                                  
			HAL.Builder()
               .WithState(new { Name = "Masoud", Family = "Bahrami", Age = 25 })
               .WithSelfLink("/person/20", HttpVerbs.GET, false)
               .WithEmbedded(orders)
               .WithEmbedded(basket)
               .Build();
```



**Embedded Exceptions**

1. if Resource name is null or white space, *EmbeddedResourceNameNullOrEmptyException* will be raised.



------

**Don't Repeat Yourself with State Abstract Class**

**State** is implemented from **IState**.  If  http response model inherited from **State** or implemented  **IState** it has both state and hypertexts links.

```c#
public abstract class State : IState
    {
        public State()
        {
            LinkObjects = new List<LinkObject>();
        }
        [Ignore]
        public List<LinkObject> LinkObjects { get; private set; }

        protected void AddLink(string relation, Link link);
        
        protected void AddFirstLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "");
        
        protected void AddLastLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "");
        
        protected void AddPreviousLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "");
        
        protected void AddNextLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "");
        
        protected void AddSelfLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "");
        
        protected void AddEditLink(string href, string httpVerb, bool? templated = null, string name = "", string type = "", string deprecation = "");
        
        protected void AddCuriLink(string href, bool? templated = null, string name = "", string type = "", string deprecation = "");

        public abstract object GetState();
    }
```

For example if Http Response model is  Person:

```c#
public class Person : State
{
        public Person(string first, string last, int age)
        {
            FirstName = first;
            LastName = last;
            Age = age;
            AddLinks();
        }

        private void AddLinks()
        {
        	//Adding links
            AddSelfLink("/person/20", HttpVerbs.GET,true);
            AddFirstLink("/person/first", HttpVerbs.GET,true);
            AddLastLink("/person/last", HttpVerbs.GET, true);
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public override object GetState()
        {
            return this as Person;
        }
    }
```

So in this way we don't have to repeat all Person related links, every time we need to return Person as a Http Response.([Don't Repeat Yourself](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself))

```c#
				HAL.Builder()
               	   .WithState(IState)
```



Also we can use any object implemented **IState** to create a collection of Embedded resources using **EmbeddedCollection**

```c#
				var embeddedCollection = new EmbeddedCollection("Ordered")
                {
                     new Person("Masoud", "Bahrami", 35)
                };

                HAL.Builder()
                   .WithState(new PagedViewModel(pageCount:100,currentPage:2))
                   .WithEmbededState(embeddedCollection)
                   .Build();
```

