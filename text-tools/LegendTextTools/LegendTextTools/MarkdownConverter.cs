using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace LegendTextTools
{
	public static class MarkdownConverter
	{

		#region MakeTable

		/// <summary>
		/// Makes the table from various plain-text sources.
		/// </summary>
		/// <returns>The table.</returns>
		/// <param name="text">The string to create a table from, or a file path to the string.</param>
		/// <param name="isFilePath">If set to <c>true</c> then 'text' is file path.</param>
		/// <param name="hasMultipleRowsPerCell">If set to <c>true</c> then the plain-text table has multiple rows per cell.</param>
		public static List<string> MakeTable (string text, 
		                                      bool isFilePath = false, 
		                                      bool hasMultipleRowsPerCell = false)
		{
			if (isFilePath && File.Exists (text) && hasMultipleRowsPerCell) 
			{
				return MakeTable (new List<string> (File.ReadAllLines (text)), 
				                                    hasMultipleRowsPerCell: true);
			}
			else if (isFilePath && File.Exists (text))
			{
				return MakeTable (new List<string> (File.ReadAllLines (text)));
			}
			else if (hasMultipleRowsPerCell)
			{
				return MakeTable (new List<string> (text.Split ('\n')), 
				                  hasMultipleRowsPerCell: true);
			}
			else 
			{
				return MakeTable (new List<string> (text.Split ('\n')));
			}
		}


		public static List<string> MakeTable (List<string> lines, 
		                                      bool hasMultipleRowsPerCell = false)
		{
			// The returned table text
			List<string> table = new List<string> ();

			// plain text must have a blank line between all rows, 
			// including the table header. 
			// The last line must also be blank.
			if (hasMultipleRowsPerCell)
			{
				// Blank lines deliminate the table rows.
				// Find the first blank line - all lines above it are table header lines.
				int headLength = 0;

				foreach (string line in lines)
				{
					if (String.IsNullOrWhiteSpace (line))
					{
						break;
					}
					headLength++;
				}

				// Loop through the head lines and find where each column starts
				// (First column starts at index 0)
				List<int> columnIndices = new List<int> () {0};

				for (int i = 0; i < headLength; i++)
				{
					char[] head = lines [i].ToCharArray ();

					bool foundTwoSpaces = false;
					for (int j = 1; j < head.Length; j++)
					{
						if (!foundTwoSpaces && head [j] == ' ' && head [j - 1] == ' ') 
						{
							foundTwoSpaces = true;
						}

						if (foundTwoSpaces == true && head [j] != ' ') 
						{
							columnIndices.Add (j);
							foundTwoSpaces = false;
						}
					}
				}

				// Remove duplicates from pipeIndices list
				columnIndices = columnIndices.Distinct ().ToList ();
				columnIndices.Sort ();

				// cellData will store the information in the cell, 
				// regardless of how many lines (of plain text) it spans
				List<string> cellData = new List<string> (columnIndices.Count);

				for (int i = 0; i < columnIndices.Count; i++)
				{
					cellData.Add ("");
				}

				// Loop through the whole table
				foreach (string line in lines)
				{
					// Strip some text from each line, 
					// and save it until we reach a blank line.
					for (int i = 0; 
					     i <= columnIndices.Count - 1 && columnIndices[i] < line.Length; 
					     i++)
					{
						int columnLength;

						// The last column (for a line containing all indices)
						if (i == columnIndices.Count - 1)
						{
							columnLength = (line.Length - columnIndices [i]);
						}
						else
						{
							columnLength = (columnIndices [i + 1] - columnIndices [i]);

							// The last column (for a shorter line)
							if (columnIndices [i] + columnLength > line.Length)
							{
								columnLength = (line.Length - columnIndices [i]);
							}
						}


						string data = line.Substring (columnIndices [i], columnLength);

						if (String.IsNullOrWhiteSpace (data))
						{
							continue;
						}
						else
						{
							cellData [i] += data.Trim () + " ";
						}
					}

					// Blank lines deliminate rows
					if (String.IsNullOrWhiteSpace (line))
					{
						// Check if all cellData is whitespace
						int whitespaceCount = 0;
						foreach (string s in cellData)
						{
							if (String.IsNullOrWhiteSpace (s)) 
							{
								whitespaceCount++;
							}
						}
						if (whitespaceCount == cellData.Count)
						{
							continue;
						}

						string columnData = "| ";

						// Add the current cell data to the table
						// then clear the data
						for (int i = 0; i < cellData.Count; i++)
						{
							columnData += cellData [i] + "| ";
							cellData [i] = "";
						}

						table.Add (columnData.Trim ());
					}
				}
			}

			else 
			{
				// Insert pipe chars at these indices
				List<int> pipeIndices = new List<int> () {0};

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
					if (String.IsNullOrWhiteSpace (line))
						continue;

					char[] output = ("| " + line.TrimEnd ().PadRight (padding) + " |").ToCharArray ();

					foreach (int i in pipeIndices) 
						output [i] = '|';

					table.Add (new string (output));
				}
			}

			// Add "|--------------|" under the first line
			char [] underline = table [0].ToCharArray ();

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

