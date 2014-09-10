using System;
using System.IO;
using System.Collections.Generic;

namespace LegendTextTools
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string text = 
				@"1D100     Result
01-10     Extremely friendly and willing to impart important news or information to Adventurers at no cost.
11-30     As for previous but seeks payment of some kind (money, food, drink or protection).
31-50     Friendly but with little to share save local gossip.
51-60     Friendly but reticent. Influence rolls needed to gain information and payment of some kind wanted.
61-80     Neutral. Encounter is neither friendly nor hostile.
81-85     Unfriendly. Encounter is angry for some reason and Influence rolls needed to turn the situation into a Neutral encounter.
86-90     As previous but Influence rolls are at -20%.
91-95     Hostile. Influence rolls at -20% are needed to avoid a confrontation of some kind.
96-00     Very Hostile. Encounter launches an unprovoked attack.";

			List<string> lines = MarkdownConverter.MakeTable (text);
			//List<string> lines = MarkdownConverter.MakeTable (text, hasMultipleRowsPerCell: true);
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
