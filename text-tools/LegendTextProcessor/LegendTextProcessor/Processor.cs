using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using HtmlAgilityPack;

namespace LegendTextProcessor
{
	public class Processor
	{
		public Processor ()
		{

		}

		public void Process (string pathToFile)
		{
			// Step 1.  Create and Load the Html file
			HtmlDocument doc = new HtmlDocument();
			doc.Load (pathToFile);

			// Step 2.  Remove the all <br> tags AND empty <span> tags
			this.RemoveUselessTags (doc);

			// Step 3.  Extract all remaining <span> tags
			List<HtmlNode> spans = this.ExtractDesiredTags (doc);

			// Step 4.  Create a list of all font-family & size styles
			List<string> styles = this.ExtractStyles (spans);

			// Step 5.  Generate 'styles.txt' file, or use the existing file
			if (!File.Exists ("styles.txt")) 
			{
				Console.WriteLine ("Couldn't find styles.txt file.");
				Console.WriteLine ("Generating file...");

				this.WriteStylesToFile (styles);

				Console.WriteLine ("");
				Console.WriteLine ("Complete styles.txt and run program again.");
				Console.WriteLine ("");

				return;
			}

			// Step 6.  Create a dictionary of style/html tag pairs from the 'styles.txt' file
			Dictionary<string, string> stylesDict = this.ReadStylesFromFile ();

			if (stylesDict == null)
				return;

			// Step 7.  Convert to markdown
			this.WriteToMarkdown (spans, stylesDict);
		}

		#region Process helper functions

		/// <summary>
		/// Removes the useless Html Nodes.
		/// 
		/// </summary>
		/// <param name="doc">An HtmlDocument.</param>
		private void RemoveUselessTags (HtmlDocument doc) 
		{
			// Currently removing all <br> and empty <span> tags
			List<HtmlNode> nodesToRemove = doc.DocumentNode
				.SelectNodes ("//br | //span[not(text())]")
				.ToList ();

			foreach (var node in nodesToRemove)
				node.Remove ();
		}

		/// <summary>
		/// Extracts the desired Html nodes. (So far, only non-empty spans seem necessary.)
		/// 
		/// </summary>
		/// <returns>List of Html nodes</returns>
		/// <param name="doc">An HtmlDocument</param>
		private List<HtmlNode> ExtractDesiredTags(HtmlDocument doc)
		{
			List<HtmlNode> nodes = doc.DocumentNode
				.SelectNodes ("//span")
				.ToList ();

			return nodes;
		}

		/// <summary>
		/// Extracts a list of the styles.
		/// 
		/// </summary>
		/// <returns>List of styles.</returns>
		/// <param name="nodes">Html nodes</param>
		private List<string> ExtractStyles (List<HtmlNode> nodes)
		{
			List<string> styles = new List<string> ();

			foreach (var node in nodes)
				styles.Add (node.Attributes ["style"].Value);

			return styles.Distinct ().ToList ();
		}

		/// <summary>
		/// Generates a text file contained a list styles.
		/// 
		/// </summary>
		/// <param name="styles">A list of style strings.</param>
		private void WriteStylesToFile (List<string> styles)
		{
			try 
			{
				StreamWriter sw = new StreamWriter ("styles.txt");

				sw.WriteLine("# Use a text editor to find instances of each style in");
				sw.WriteLine("# the html file, and decided what html tag to associate");
				sw.WriteLine("# with that text.");
				sw.WriteLine("# ");
				sw.WriteLine("# For each style listed below, add an html tag following each '='.");
				sw.WriteLine("# Allowed tags: h1, h2, h3, h4, h5, p, i, b, x.");
				sw.WriteLine("# ");
				sw.WriteLine("# If you don't care about the text for a given style, ");
				sw.WriteLine("# then use 'x' as the html tag.");
				sw.WriteLine("# ");

				int maxLength = 0;
				foreach (var style in styles)
					if (style.Length > maxLength)
						maxLength = style.Length;

				foreach (var style in styles)
				{
					sw.WriteLine (style.PadRight (maxLength + 1) + "= ");
				}

				sw.Close ();
			}
			catch (Exception e) 
			{
				Console.WriteLine ("styles.txt Write Exception: " + e.Message);
			}
		}

		/// <summary>
		/// Reads the style/html tag pairs from 'styles.txt'.
		/// 
		/// </summary>
		/// <returns>A dictionary of style/html tag pairs.</returns>
		private Dictionary<string, string> ReadStylesFromFile ()
		{
			var dict = new Dictionary<string, string> ();

			try 
			{
				StreamReader sr = new StreamReader ("styles.txt");

				string line = sr.ReadLine ();

				while (line != null) {

					if (String.IsNullOrWhiteSpace (line) || line.IndexOf ('#') == 0) 
					{
						line = sr.ReadLine ();
						continue;
					}

					string[] strs = line.Split('=');

					if (strs.Count () != 2) 
					{
						Console.WriteLine ("Error:  Malformed 'styles.txt' file.");
						Console.WriteLine ("");
						Console.WriteLine ("Check that each style is followed by an '=' and an html tag.");
						Console.WriteLine ("");
						return null;
					}

					dict.Add (strs [0].Trim (), strs [1].Trim ());

					line = sr.ReadLine ();
				}

				sr.Close ();
			}
			catch (Exception e) 
			{
				Console.WriteLine ("styles.txt Read Exception: " + e.Message);
			}

			return dict;
		}

		/// <summary>
		/// Writes remaining html to markdown format.
		/// 
		/// </summary>
		/// <param name="nodes">Remaining Html nodes.</param>
		/// <param name="stylesDict">Dictionary of style/html tag pairs.</param>
		private void WriteToMarkdown (List<HtmlNode> nodes, Dictionary<string, string> stylesDict)
		{
			string markdown = "";

			foreach (var span in nodes) {
				string style = span.Attributes ["style"].Value;
				string trimmed = span.InnerHtml.Trim ();
				string text = "";

				if      (stylesDict [style] == "p")     text = trimmed + "\n";
				else if (stylesDict [style] == "h1")    text = "\n# " + trimmed + "\n\n";
				else if (stylesDict [style] == "h2")    text = "\n## " + trimmed + "\n\n";
				else if (stylesDict [style] == "h3")    text = "\n### " + trimmed + "\n\n";
				else if (stylesDict [style] == "h4")    text = "\n#### " + trimmed + "\n\n";
				else if (stylesDict [style] == "h5")    text = "\n##### " + trimmed + "\n\n";
				else if (stylesDict [style] == "i")     text = "_" + trimmed + "_";
				else if (stylesDict [style] == "b")     text = "__" + trimmed + "__";
				else                                    continue;

				markdown += text;
			}


			System.IO.File.WriteAllText ("output.md", markdown);
		}

		#endregion
	}
}

