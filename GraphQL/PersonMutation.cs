using AutoMapper;
using GraphQL.Types;
using graphqlpractise.Models;
using graphqlpractise.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpractise.GraphQL
{
    public class PersonMutation : ObjectGraphType
    {
        public PersonMutation(BlogDbContext dbContext, IMapper mapper)
        {
            Name = "Person_mutation";
            Field<PersonType>(
                "addPerson",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PersonInputType>> { Name = "person" }
                ),
                resolve: context =>
                {
                    var person = (Person)context.GetArgument(typeof(Person), "person");
                    dbContext.Persons.Add(person);
                    dbContext.SaveChanges();
                    return person;
                });
        }
    }
}
