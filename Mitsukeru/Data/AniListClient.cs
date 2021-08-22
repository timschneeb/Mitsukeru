using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Mitsukeru.GraphQl;

namespace Mitsukeru.Data
{
    public static class AniListClient
    {
        private static readonly GraphQLHttpClient GraphQlHttpClient;

        static AniListClient()
        {
            var uri = new Uri("https://graphql.anilist.co");
            var graphQlOptions = new GraphQLHttpClientOptions
            {
                EndPoint = uri
            };

            GraphQlHttpClient = new GraphQLHttpClient(graphQlOptions, new NewtonsoftJsonSerializer());
        }

        public static async Task<AniListResponse[]> ExecuteQuery(string username, MediaType mediaType, Action<long, long>? progress)
        {
            var responses = new List<AniListResponse>();
            var page = 1;
            do
            {
                var query = await ExecutePartialQuery(page, username, mediaType);
                responses.Add(query);
                progress?.Invoke(query.Page?.PageInfo?.CurrentPage ?? 1, query.Page?.PageInfo?.LastPage ?? 1);
                page++;
            } while (responses.LastOrDefault() != null && (responses.Last().Page?.PageInfo?.HasNextPage ?? false));

            return responses.ToArray();
        }
        
        public static async Task<AniListResponse> ExecutePartialQuery(int page, string username, MediaType mediaType)
        {
            var builder = new QueryQueryBuilder()
                .WithPage(
                    new PageQueryBuilder()
                        .WithPageInfo(
                            new PageInfoQueryBuilder().WithAllFields())
                        .WithMediaList(
                            new MediaListQueryBuilder()
                                .WithStatus()
                                .WithMedia(
                                    new MediaQueryBuilder()
                                        .WithId()
                                        .WithTitle(
                                            new MediaTitleQueryBuilder()
                                                .WithRomaji()
                                                .WithEnglish())
                                        .WithGenres()
                                        .WithAverageScore()
                                        .WithCoverImage(
                                            new MediaCoverImageQueryBuilder()
                                                .WithLarge())
                                        .WithRecommendations(
                                            new RecommendationConnectionQueryBuilder()
                                                .WithNodes(
                                                    new RecommendationQueryBuilder()
                                                        .WithId()
                                                        .WithRating()
                                                        .WithMediaRecommendation(
                                                            new MediaQueryBuilder()
                                                                .WithId()
                                                                .WithTitle(
                                                                    new MediaTitleQueryBuilder()
                                                                        .WithRomaji()
                                                                        .WithEnglish())
                                                                .WithTags(
                                                                    new MediaTagQueryBuilder().WithName())
                                                                .WithGenres()
                                                                .WithAverageScore()
                                                                .WithDescription(asHtml: true)
                                                                .WithCoverImage(
                                                                    new MediaCoverImageQueryBuilder()
                                                                        .WithLarge()))
                                                ),
                                            page: 1, perPage: 50, sort: new RecommendationSort?[]
                                            {
                                                RecommendationSort.Rating
                                            })),
                            userName: username, type: mediaType),
                    page: page, perPage: 50);
            
            var request = new GraphQLRequest
            {
                Query = builder.Build()
            };
            
            var response = await GraphQlHttpClient.SendQueryAsync<object>(request);
            if (response.Errors is {Length: > 0})
            {
                throw new GraphQlException(response.Errors);
            }
            return AniListResponse.FromJson(response.Data.ToString());
        }
    }
}