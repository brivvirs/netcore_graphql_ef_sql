using AutoMapper;
using GraphQL.Types;
using graphqlpractise.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpractise.GraphQL
{

    public static class GraphQLObjectMapper
    {
        public static void AddGraphQLObjectMapper(this IServiceCollection services)
        {
            // Register DTO to graphQL entity
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, PersonType>();
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
        }
    }

    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType()
        {
            Field(x => x.Id).Description("The person ID field");
            Field(x => x.Name).Description("The person Name field");
            Field(x => x.Surname).Description("The person Surname field");
            // This should be described in the POST object
            // Field(x => x.Posts).Description("The person Posts");
        }
    }

    public class PersonInputType : InputObjectGraphType<Person>
    {
        public PersonInputType()
        {
            Field(x => x.Name).Description("The person Name field");
            Field(x => x.Surname).Description("The person Surname field");
            // This should be described in the POST object
            // Field(x => x.Posts).Description("The person Posts");
        }
    }
}
