﻿using System;
using System.Net;

namespace animestr
{
    /* TODO:
			 * Read first character, establish what is being inputed from that character and ask for the rest of the input (like "MOOO:" above)
			 * Make the searching system
			 * Make a source selection system with indicators to show if the source can be reached, is working, etc...
			 * Make a search result caching system
			 * Show a recommended anime list on startup
			 * Make a cool ascii art splash screen
			*/

    class MainClass
	{
		public static void Main (string[] args)
		{
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            MALParser mal = new MALParser(AnimeInfo.GetMALPage("seitokai yakneidomo"));
            Console.WriteLine(string.Join(", ",mal.GetDescription()));
        }
    }
}
