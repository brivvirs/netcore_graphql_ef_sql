using AutoMapper;
using GraphQL.Types;
using graphqlpractise.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpractise.GraphQL
{
    public class PersonQuery : ObjectGraphType
    {
        /// <summary>
        /// Define possible queries
        /// </summary>
        /// <param name="dbContext"></param>
        public PersonQuery(BlogDbContext dbContext, IMapper mapper)
        {
            Field<ListGraphType<PersonType>>("allPersons",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id", Description = "id of the person" },
                    new QueryArgument<StringGraphType> { Name = "name", Description = "name of the person" }
                    ),
                resolve: context =>
                {
                    var Id = (int?)context.GetArgument(typeof(int), "id");
                    var Name = (string)context.GetArgument(typeof(string), "name");
                    if (Id.HasValue)
                    {
                        return dbContext.Persons.Include(x => x.Posts).Where(x => x.Id == Id.Value).ToList();
                    }
                    else if (!string.IsNullOrEmpty(Name))
                    {
                        return dbContext.Persons.Include(x => x.Posts).Where(x => x.Name == Name).ToList();
                    }
                    else
                    {
                        return dbContext.Persons.Include(x => x.Posts).ToList();
                    }
                });
        }

    }
}
