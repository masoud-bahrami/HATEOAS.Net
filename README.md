**HATEOAS.Net**

------

![Conda](https://img.shields.io/conda/pn/conda-forge/python.svg?color=red&style=plastic)

Online documents: https://masoud-bahrami.github.io/HATEOAS.Net/

------

**HATEOAS.Net contains**

|                 |                                                                           |
| --------------- | ------------------------------------------------------------------------- |
| HATEOAS.Net.HAL | https://github.com/masoud-bahrami/HATEOAS.Net/tree/master/src/hal         |

------



 **[Hypermedia](https://en.wikipedia.org/wiki/Hypermedia)** is the core component of REST APIs. Whiteout them it's just a service which uses HTTP verbs.

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
 **HATEOAS.Net** contains some implementations of HATEOAS in C#  based on different specifications.

------
**Roadmap**

|                                     |                     |
| ----------------------------------- | --------------------|
| Adding Action Enum Query Parameters | In Progress         |
| Adding Action Body Parameters       | In Progress         |
| Adding Action Body Parameters       | In Progress         |
| HAL Parser                          | In Progress         |
| RDFs                                | Planned             |
