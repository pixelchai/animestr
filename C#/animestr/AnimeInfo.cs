﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace animestr
{
    public class AnimeInfo
    {
        public string title = null;
        public string description = null;
        public string alts = null;
        public List<string> genres = new List<string>();

        //MAL
        public string score = null;
        public string rank = null;
        public string popularity = null;

        public bool usedMAL { get; private set; }

        public AnimeInfo() { }
        public AnimeInfo(string title)
        {
            this.title = title;
            this.LoadFromMAL();
        }

        public void LoadFromMAL()
        {
            if (this.title != null)
            {
                using (WebClient wc = new WebClient())
                {
                    string page = wc.DownloadString(GetMALUrl(this.title));
                }
            }
        }

        public static string GetMALUrl(string query)
        {
            using (WebClient wc = new WebClient())
            {
                string searchPage = wc.DownloadString(@"http://myanimelist.net/anime.php?q=" + Uri.EscapeDataString(query));

                string linkSection = new MALParser(searchPage).GetLinkSection();

                int hIndex = linkSection.IndexOf("href=\"") + 6;
                return linkSection.Substring(hIndex, linkSection.IndexOf("\"", hIndex) - hIndex);
            }
        }
    }
}
