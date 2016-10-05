﻿using System;
using System.Linq;

namespace animestr
{
    public class AnimeDataView
    {
        public AnimeData data = null;
        public IAnimeSource source = null;
        public Display display = null;

        public bool loadedMAL = false;

        public bool[] itemsDisplayed = new bool[]{ false, false, false, false, false };
        //p, o, d, i

        public AnimeDataView(Display display, AnimeData data)
        {
            this.display = display;
            this.source = display.source;
            this.data = data;
        }

        public void Show()
        {
            ShowData();
        }

        public void ReadCommand()
        {
            Console.ForegroundColor = Console.BackgroundColor;
            DoCommand(Console.ReadKey());
        }

        public void DoCommand(ConsoleKeyInfo k)
        {
            if (k.Key == ConsoleKey.I && itemsDisplayed[3])
            {
                if (!loadedMAL)
                {
                    this.display.ShowSplash("Loading information...");   
                    loadedMAL = true;
                    data.info.LoadFromMAL();
                    this.display.splashDone = true;
                }
                this.ShowData();
                ReadCommand();
            }
            else
            {
                InvalidCommand();
            }
        }

        private void InvalidCommand(string msg = "Invalid command!")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg + " Try again.");
            Console.ResetColor();
            ReadCommand();
        }

        public void ShowData()
        {                  
            //data.info.LoadFromMAL();
            display.splashDone = true;
            //Console.ReadKey();
            Utils.ClearConsole();
            int lines = 0;
            if (data.info.title != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Utils.PrintSeperator(data.entry.title, '-', '|');
                Console.ResetColor();
                lines++;
            }
            if (data.info.score != null && data.info.rank != null && data.info.popularity != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Score: ");
                Console.Write(data.info.score);
                for (int i = 0; i < ((Console.WindowWidth - 2) / 3) - 7 - data.info.score.Length; i++)
                {
                    Console.Write(' ');
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("| Rank: ");
                Console.Write(data.info.rank);
                for (int i = 0; i < ((Console.WindowWidth - 2) / 3) - 8 - data.info.rank.Length; i++)
                {
                    Console.Write(' ');
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("| Popularity: ");
                Console.Write(data.info.popularity);
                for (int i = 0; i < ((Console.WindowWidth - 2) / 3) - 14 - data.info.rank.Length; i++)
                {
                    Console.Write(' ');
                }
                Console.Write(Environment.NewLine);
                lines++;
            }
            if (data.entry.url != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Url: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(data.entry.url);
                Console.ResetColor();
                lines++;
            }
            if (data.entry.pictureUrl != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("View picture with 'p'");
                Console.ResetColor();
                itemsDisplayed[0] = true;
                lines++;
            }
            if (data.info.MALUrl != null || data.info.MALPage != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("View myanimelist entry with 'o'");
                Console.ResetColor();
                itemsDisplayed[1] = true;
                lines++;
            }
            if (data.info.alts != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                string s = "Alternate titles: " + string.Join(", ", data.info.alts.Where(x => !Utils.ContainsUnicode(x) || Config.displayUnicodeTitles));
                Console.WriteLine(s);
                int slength = new System.Globalization.StringInfo(s).LengthInTextElements;
                lines += (int)Math.Ceiling(slength / (double)(Console.WindowWidth - 1));
                //TODO:NB: Still cannot fix long JP characters like ー counting issues
                //TODO: make settings to turn off JP characters.
            }
            if (data.info.genres != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                string s = "Genres: " + string.Join(", ", data.info.genres);
                Console.WriteLine(s);
                lines += (int)Math.Ceiling(s.Length / (double)(Console.WindowWidth - 1));
            }
            if (data.info.description != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Description: ");
                string extract = (data.info.description.Split('\n')[0].Length > Console.WindowWidth - 4) ? data.info.description.Split('\n')[0].Substring(0, Console.WindowWidth - 4) : data.info.description.Split('\n')[0];
                extract = (extract.EndsWith(".") ? extract + ".." : extract + "...");
                Console.WriteLine(extract);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("View full description with 'd'");
                lines += 3;
                itemsDisplayed[2] = true;
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Load information from MAL with 'i'");
            lines += 1;
            itemsDisplayed[3] = true;


            Console.ResetColor();
            lines += 2;
            for (int i = 0; i < Console.WindowHeight - lines; i++)
            {
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Go back with 'b' or view episodes with (ENTER)");
            itemsDisplayed[4] = true;

            this.ReadCommand();
        }
    }
}

