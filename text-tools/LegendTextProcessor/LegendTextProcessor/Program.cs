using System;

namespace LegendTextProcessor
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Starting");
			Console.WriteLine ("");


			Processor p = new Processor ();

			p.Process (@"C:\Users\Jeremy\Documents\GitHub\LegendSRD\pdf-output\core\pdf2txt.html");


			Console.WriteLine ("");
			Console.WriteLine ("Finished");

			Console.ReadKey ();
		}
	}
}
