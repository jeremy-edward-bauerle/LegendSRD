using System;
using System.Collections.Generic;
using System.IO;

namespace LegendTextTools
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string text = 
				@"Type           Size           Reach           Damage           AP/HP                  Range
Bite           L              L               1D8+1D8          As for Head            -
Kick           L              VL              1D6+1D8          As for Leg             -
Foreclaw       M              M               1D4+1D8          As for Foreclaw        -";

			List<string> lines = MarkdownConverter.MakeTable (text);
			//List<string> lines = MarkdownConverter.AddPreColonBold (text);
			//List<string> lines = MarkdownConverter.AddBlockquote (text);

			using (StreamWriter writer = new StreamWriter("table.txt")) 
			{
				foreach (string line in lines)
					writer.WriteLine (line);
			}

		}
	}
}
