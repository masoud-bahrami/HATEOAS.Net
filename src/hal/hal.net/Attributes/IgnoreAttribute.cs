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

namespace HATEOAS.Net.HAL
{
    /// <summary>
    /// When generating HLA Response,
    ///  Properties which contain Ignore attribute will be ignored.
    /// </summary>
    public class IgnoreAttribute : Attribute
    {
    }
}