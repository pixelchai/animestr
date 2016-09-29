﻿using System;
using System.Collections.Generic;

namespace animestr
{
	public class ConsoleList
	{
		public List<string> items = new List<string>();
		public string title = "list";

		public ConsoleList ()
		{
		}

		public ConsoleList(string title, params string[] args)
		{
			this.title = title;
			this.items.AddRange(args);
		}

		/// <summary>
		/// Prints the list. NB: pageNo is 1-based. It starts with a 1 not a 0!
		/// </summary>
		/// <param name="offtop">Offtop.</param>
		/// <param name="offbottom">Offbottom.</param>
		/// <param name="pageNo">Page no.</param>
		public void PrintList(int offtop, int offbottom, int pageNo)
		{	
			offbottom += 1; //padding

			int pageSize = (Console.WindowHeight - offtop - offbottom);//size = amount of elements 
			for (int i = pageSize * (pageNo - 1); i < ((pageSize * (pageNo - 1))+pageSize); i++) 
			{
				if (i % 2 == 0) 
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
				}
				if (i < items.Count) {
					Console.WriteLine ((i < 9 ? " " : "") + (i + 1) + "| " + (items[i].Length>=Console.WindowWidth-8-i.ToString().Length ? items[i].Substring(0,Console.WindowWidth-8-i.ToString().Length)+"...": items[i]));
				} else {
					Console.WriteLine ();//padding
				}
				Console.ResetColor ();
			}
		}

		/// <summary>
		/// Prints the list. NB: pageNo is 1-based. It starts with a 1 not a 0!
		/// </summary>
		public void PrintList(int pageNo)
		{
			PrintList (0, 0, pageNo);
		}

		/// <summary>
		/// Prints the list. NB: pageNo is 1-based. It starts with a 1 not a 0!
		/// </summary>
		public void PrintList(string text, int pageNo)
		{
			Console.WriteLine(this.title + " - page " + pageNo+"/"+items.Count/(Console.WindowHeight - 3));
			PrintList (1, 2, pageNo);//bottom 2 because line br:
			Console.WriteLine (text);
			Utils.PrintBreak ('-');
		}

		/// <summary>
		/// Prints the list. NB: pageNo is 1-based. It starts with a 1 not a 0! This constructor is the same as (text, pageNo) except you can also specify rel sizes. A relOffBottom of -1 would, for example, limit the lists's height by 1 row.
		/// </summary>
		public void PrintList(string text, int pageNo, int relOffTop, int relOffBottom)
		{
			Console.WriteLine(this.title + " - page " + pageNo+"/"+items.Count/(Console.WindowHeight - 3));
			PrintList (1+relOffTop, 2+relOffBottom, pageNo);//bottom 2 because line br:
			Console.WriteLine (text);
			Utils.PrintBreak ('-');
		}
	}
}
