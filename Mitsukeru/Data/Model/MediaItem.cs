using System;
using System.Linq;
using System.Text.Json.Serialization;
using Mitsukeru.GraphQl;

namespace Mitsukeru.Data.Model
{
    [Serializable]
    public class MediaItem
    {
        public long Id { set; get; }
        public string Title { set; get; }
        public string[] Genres { set; get; }
        public string Description { set; get; }
        public long AverageScore { set; get; }
        public string ThumbnailUrl { set; get; }
        public MediaType Type { set; get; }

        public string Url()
        {
            return $"https://anilist.co/{Type.ToString().ToLower()}/{Id.ToString()}";
        }
        
        [JsonConstructor]
        public MediaItem(long id, string title, string[] genres, string description, long averageScore, string thumbnailUrl, MediaType type)
        {
            Id = id;
            Title = title;
            Genres = genres;
            Description = description;
            AverageScore = averageScore;
            ThumbnailUrl = thumbnailUrl;
            Type = type;
        }

        public MediaItem(Media media, MediaType mediaType)
        {
            Id = media.Id;
            Type = mediaType;
            Title = media.Title.English ?? media.Title.Romaji;
            Genres = media.Genres;
            Description = media.Description;
            AverageScore = media.AverageScore ?? -1;
            ThumbnailUrl = media.CoverImage.Large.ToString();
        }
    }
}