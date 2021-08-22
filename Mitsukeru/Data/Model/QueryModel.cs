using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Mitsukeru.GraphQl;

namespace Mitsukeru.Data.Model
{
    public class QueryModel
    {
        [Required] [Parameter] public string UserName { set; get; }
        [Required] [Parameter] public MediaType MediaType { set; get; }

        [Parameter] public int ResultsPerPage { set; get; } = 100;
        [Parameter] public int MinimumRecommendationVotes { set; get; }
        [Parameter] public SortingMode SortBy { set; get; } = SortingMode.RelevanceDesc;
        [Parameter] public string ExcludedTags { set; get; } = string.Empty;
        [Parameter] public string FilterTags { set; get; } = string.Empty;

        [Parameter] public bool LibraryIncludePlanned { set; get; }
        [Parameter] public bool LibraryIncludeCurrent { set; get; }
        [Parameter] public bool LibraryIncludeCompleted { set; get; }
        [Parameter] public bool LibraryIncludePaused { set; get; }
        [Parameter] public bool LibraryIncludeDropped { set; get; }

        public string[] GetFilterTags()
        {
            return FilterTags.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }
        
        public string[] GetExcludedTags()
        {
            return ExcludedTags.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }
        
        public MediaListStatus[] GetStatuses()
        {
            var list = new List<MediaListStatus>();
            if(LibraryIncludePlanned)
                list.Add(MediaListStatus.Planning);
            if(LibraryIncludeCurrent)
                list.Add(MediaListStatus.Current);
            if(LibraryIncludeCompleted)
                list.Add(MediaListStatus.Completed);
            if(LibraryIncludePaused)
                list.Add(MediaListStatus.Paused);
            if(LibraryIncludeDropped)
                list.Add(MediaListStatus.Dropped);
            return list.ToArray();
        }
    }
}