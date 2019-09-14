using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpractise.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Post> Posts { get; private set; }

        public Person()
        {
            Posts = new List<Post>();
        }

    }
}
