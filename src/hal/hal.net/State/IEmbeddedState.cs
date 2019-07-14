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

using System.Collections;
using System.Collections.Generic;

namespace HATEOAS.Net.HAL
{
    public interface IEmbededState
    {
        string ResourceName { get;}

        ICollection<IState> States { get; }
    }
    public class EmbeddedCollection : IEmbededState , ICollection<IState>
    {
        public EmbeddedCollection(string resourceName)
        {
            ResourceName = resourceName;
            _states = new List<IState>();
        }
        public EmbeddedCollection(string resourceName, List<IState> states)
        {
            ResourceName = resourceName;
            _states = states;
        }

        public string ResourceName { get; private set; }

        private ICollection<IState> _states;

        public int Count => States.Count;

        public bool IsReadOnly => States.IsReadOnly;

        public ICollection<IState> States => _states;

        public void Add(IState item)
        {
            _states.Add(item);
        }

        public void Clear()
        {
            _states.Clear();
        }

        public bool Contains(IState item)
        {
            return States.Contains(item);
        }

        public void CopyTo(IState[] array, int arrayIndex)
        {
            States.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IState> GetEnumerator()
        {
            return States.GetEnumerator();
        }

        public bool Remove(IState item)
        {
            return _states.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return States.GetEnumerator();
        }
    }
}