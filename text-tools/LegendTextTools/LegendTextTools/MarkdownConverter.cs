using System;
using System.IO;
using System.Collections.Generic;

namespace LegendTextTools
{
	public static class MarkdownConverter
	{

		#region MakeTable

		public static List<string> MakeTable (string text, bool isFilePath = false)
		{
			if (isFilePath && File.Exists (text)) 
			{
				return MakeTable (new List<string> (File.ReadAllLines (text)));
			}
			else 
			{
				return MakeTable (new List<string> (text.Split ('\n')));
			}
		}


		public static List<string> MakeTable (List<string> lines)
		{
			// Insert pipe chars at these indices
			List<int> pipeIndices = new List<int> () {0};

			// The returned table text
			List<string> table = new List<string> ();

			// Find the pipe indices using the heading line as a guide
			char[] head = lines [0].ToCharArray ();

			bool foundTwoSpaces = false;
			for (int i = 1; i < head.Length; i++)
			{
				if (!foundTwoSpaces && head [i] == ' ' && head [i - 1] == ' ') 
				{
					foundTwoSpaces = true;
				}
					
				if (foundTwoSpaces == true && head [i] != ' ') 
				{
					pipeIndices.Add (i);
					foundTwoSpaces = false;
				}
			}

			// Use the length of the first line for padding
			int padding = lines [0].Length;

			// Add pipes to each line
			foreach (string line in lines)
			{
				if (String.IsNullOrEmpty(line))
					continue;

				char[] output = ("| " + line.TrimEnd ().PadRight (padding) + " |").ToCharArray ();

				foreach (int i in pipeIndices) 
					output [i] = '|';

				table.Add (new string (output));
			}

			// Add "|--------------|" under the first line
			char[] underline = table [0].ToCharArray ();

			for (int i = 0; i < underline.Length; i++) 
				if (underline [i] != '|')
					underline [i] = '-';

			table.Insert (1, new string (underline));


			return table;
		}

		#endregion // MakeTable

		#region AddPreColonBold

		public static List<string> AddPreColonBold (string text, bool isFilePath = false)
		{
			if (isFilePath && File.Exists (text)) 
			{
				return AddPreColonBold (new List<string> (File.ReadAllLines (text)));
			}
			else 
			{
				return AddPreColonBold (new List<string> (text.Split ('\n')));
			}
		}

		public static List<string> AddPreColonBold (List<string> lines)
		{
			List<string> boldedLines = new List<string> ();

			foreach (string line in lines) 
			{
				// We want to keep blank lines
				if (String.IsNullOrEmpty (line)) 
				{
					boldedLines.Add ("");
					continue;
				}
				// Add bold to everything before the colon
				else if (line.Contains (":")) 
				{
					int colonIndex = line.IndexOf (":");

					string preColon = line.Substring (0, colonIndex);

					string postColon = line.Substring (colonIndex);

					boldedLines.Add ("**" + preColon + "**" + postColon);
				}
				// Keep every other line, unmodified
				else
				{
					boldedLines.Add (line.TrimEnd ());
				}
			}

			return boldedLines;
		}

		#endregion // AddPreColonBold

		#region AddBlockquote

		public static List<string> AddBlockquote (string text, bool isFilePath = false)
		{
			if (isFilePath && File.Exists (text)) 
			{
				return AddBlockquote (new List<string> (File.ReadAllLines (text)));
			}
			else 
			{
				return AddBlockquote (new List<string> (text.Split ('\n')));
			}
		}

		public static List<string> AddBlockquote (List<string> lines)
		{
			List<string> blockquotedLines = new List<string> ();

			foreach (string line in lines) 
			{
				if (String.IsNullOrEmpty (line)) 
				{
					blockquotedLines.Add ("");
					continue;
				}
				else
				{
					blockquotedLines.Add ("> " + line);
				}
			}

			return blockquotedLines;
		}

		#endregion // AddBlockquote
	}
}

