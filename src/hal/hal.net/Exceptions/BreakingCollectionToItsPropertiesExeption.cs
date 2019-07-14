/*
  HATEOAS.Net solution contains implementations of 
    Hypermedia As The Engine Of Application State (HATEOAS)
    based on different specifications.

 HATEOAS.Net.HAL is an implementation of HAL's Specification, and it also contains some
 extra features such as Link httpVerb(GET, POST ...) and also action parameters.

 Masoud Bahrami
 http://refactor.ir
 https://twitter.com/masodbahrami
 */

namespace HATEOAS.Net.HAL.Exceptions
{
    public class BreakingCollectionToItsPropertiesExeption : System.Exception
    {
        private const string MESSAGE = "HRef could not be empty or whitespace.";
        /// <summary>
        /// Initializes a new instance of the <see cref="NAPException"/> class.
        /// </summary>
        public BreakingCollectionToItsPropertiesExeption() : base(MESSAGE)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NAPException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        public BreakingCollectionToItsPropertiesExeption(string message)
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
        public BreakingCollectionToItsPropertiesExeption(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
        
    }
}