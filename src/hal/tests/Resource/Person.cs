
namespace HATEOAS.Net.HAL.Tests
{
    public class Person : State
    {
        private Person(){
            
        }
        public Person(string first, string last, int age)
        {
            FirstName = first;
            LastName = last;
            Age = age;
            AddLinks();
        }

        private void AddLinks()
        {
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

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            var person = (other as Person);
            return FirstName == person.FirstName
                   && LastName == person.LastName
                   && Age == person.Age;
        }
    }
}
