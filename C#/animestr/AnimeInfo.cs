﻿using System;
using System.Collections.Generic;
using System.Net;

namespace animestr
{
    public class AnimeInfo
    {
        public string title = null;
        public string description = null;
        public List<string> alts = new List<string>();
        public List<string> genres = new List<string>();

        //MAL
        public string score = null;
        public string rank = null;
        public string popularity = null;

        public string MALPage = null;
        public string MALUrl = null;

        public AnimeInfo() { }
        public AnimeInfo(string title)
        {
            this.title = title;
        }

        public void LoadFromMAL()
        {
            if (this.title != null)
            {
                MALPage = GetMALPage(this.title, out this.MALUrl);
                MALParser mal = new MALParser(MALPage);
                this.title = mal.GetTitle();
                this.description = mal.GetDescription();
                this.alts = mal.GetAlts();
                this.genres = mal.GetGenres();
                this.score = mal.GetScore();
                this.rank = mal.GetRank();
                this.popularity = mal.GetPopularity();
            }
        }

        public static string GetMALPage(string query, out string malUrl)
        {
            using (WebClient wc = new WebClient())
            {
                string searchPage = wc.DownloadString(@"http://myanimelist.net/anime.php?q=" + Uri.EscapeDataString(query));
                malUrl = Parsing.GetBetween(new MALParser(searchPage).GetLinkSection(), "href=\"", "\"");
                return wc.DownloadString(malUrl);
            }
        }
    }
}
