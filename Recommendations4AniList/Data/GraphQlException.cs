using System;
using GraphQL;

namespace Recommendations4AniList.Data
{
    public class GraphQlException : Exception
    {
        public GraphQLError[] Errors;
        public GraphQlException(GraphQLError[] errors) : base()
        {
            Errors = errors;
        }
    }
}