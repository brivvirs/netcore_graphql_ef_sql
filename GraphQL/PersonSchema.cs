using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpractise.GraphQL
{
    public class PersonSchema : Schema
    {
        public PersonSchema(ServiceProvider resolver) : base(resolver)
        {
            var query = resolver.GetService<PersonQuery>();
            Query = query;
            var mutation = resolver.GetService<PersonMutation>();
            Mutation = mutation;
        }
    }
}
