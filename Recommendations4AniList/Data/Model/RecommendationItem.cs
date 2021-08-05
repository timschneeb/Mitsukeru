using System;
using Recommendations4AniList.GraphQl;

namespace Recommendations4AniList.Data.Model
{
    [Serializable]
    public class RecommendationItem
    {
        public MediaItem RecommendationMedia { set; get; }
        public MediaItem[] RecommendationOrigin { set; get; }
    }
}