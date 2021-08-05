using System;
using GraphQL;

namespace Mitsukeru.Data
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