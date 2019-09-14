using graphqlpractise.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpractise.Repository
{
    public class SeedDatabase
    {
        public readonly BlogDbContext _context;
        public SeedDatabase(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPerson(string name, string surname)
        {
            var person = new Models.Person()
            {
                Name = name,
                Surname = surname
            };
            _context.Persons.Add(person);
            _context.Posts.Add(new Models.Post()
            {
                Author = person,
                Title = $"Book abount {name} {surname}"
            });

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Person>> All()
        {
            var persons = await _context.Persons.Include(x => x.Posts).ToListAsync();
            var posts = await _context.Posts.ToListAsync();
            return persons;
        }

        public List<Person> InitPersons()
        {
            var persons = new List<Person>()
            {
                new Person()
                {
                    Name = "Brittany",
                    Surname = "Sweatt"
                },
                new Person()
                {
                    Name = "Rachelle",
                    Surname = "Cupples"
                },
                new Person()
                {
                    Name = "Brad",
                    Surname = "Stines"
                }
            };
            _context.AddRange(persons);
            _context.SaveChanges();
            return persons;
        }
    }
}
