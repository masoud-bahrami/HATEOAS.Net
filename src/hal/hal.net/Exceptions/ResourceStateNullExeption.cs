﻿/*
  HATEOAS.Net solution contains implementations of 
    Hypermedia as the engine of application state (HATEOAS)
    based on different specifications.

 HATEOAS.Net.HAL is an implementation of HAL's Specification, and it also contains some
 extra features such as Link httpVerb(GET, POST ...) and also action parameters.

 Masoud Bahrami
 http://refactor.ir
 https://twitter.com/masodbahrami
 */

namespace HATEOAS.Net.HAL.Exceptions
{
    public class ResourceStateNullExeption : System.Exception
    {
        private const string MESSAGE = "EmbeddedResource State should not be null or empty.";
        /// <summary>
        /// Initializes a new instance of the <see cref="NAPException"/> class.
        /// </summary>
        public ResourceStateNullExeption() : base(MESSAGE)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NAPException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        public ResourceStateNullExeption(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NAPException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public ResourceStateNullExeption(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
        
    }
}