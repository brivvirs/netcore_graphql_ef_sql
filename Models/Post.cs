using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpractise.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Person Author { get; set; }
    }
}
