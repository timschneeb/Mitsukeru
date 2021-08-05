using System;
using Mitsukeru.GraphQl;

namespace Mitsukeru.Data.Model
{
    [Serializable]
    public class RecommendationItem
    {
        public MediaItem RecommendationMedia { set; get; }
        public MediaItem[] RecommendationOrigin { set; get; }
    }
}