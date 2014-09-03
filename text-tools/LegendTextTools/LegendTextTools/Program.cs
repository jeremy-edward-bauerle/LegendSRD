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
				@"             1D100                                      1D100
Prize        roll     Bulk Cargo                        roll    Rare Cargo

Raft (4      01-75    None                            -       -
tons)

             76-00    1D4 Common                      -       -

Rowboat      01-00    None                              01-99   None
(0.5 tons)

                                                        00      1 Treasure

Fisherman    01-50    1 Provisions, 1 Supplies, 1D4     01-95
(10 tons)             Common

             51-00    1 Provisions, 1 Supplies, 2D4     96-00   1 Rare Supplies
                      Common

Small        01-50    1D6 Provisions, 1D6 Supplies,     01-90   1D6x100 Silver
Trader (40            1 Ammo, 1D10 Common
tons)

             51-75    1D6 Provisions, 1D6 Supplies,     91-99   1 Rare Supplies,
                      1 Ammo, 2D10 Common                       2D6x100 silver

             76-90    1D6 Provisions, 1D6 Supplies,     00      50% chance of One
                      1 Ammo, 1D10 Common, 1                    Treasure, 4D6x1000
                      Rich                                      silver

             91-00    1D6 Provisions, 1D6 Supplies,
                      1 Ammo, 1D10 Common, 1d3
                      Rich

Sloop (50    01-50    1D6 Provisions, 1D6 Supplies,     01-50   None
tons)                 1D4 Ammo, 2D10 Common

             51-75    1D6 Provisions, 1D6 Supplies,     51-75   1D6x200 silver
                      1D4 Ammo, 3D10 Common

             76-90    1D6 Provisions, 1D6 Supplies,     76-90   1 Rare Supplies,
                      1D4 Ammo, 2x2D10                          2D6x200 silver
                      Common, 1D6 Rich

             91-00    1D6 Provisions, 1D6 Supplies,     91-00   1D4 Rare Supplies,
                      1D4 Ammo, 2x2D10                          4D6x200 silver
                      Common, 1D10 Rich

Schooner     01-50    2D6 Provisions, 2D6 Supplies,     01-50   None
(100 tons)            1D6 Ammo, 2x2D10
                      Common

             51-75    2D6 Provisions, 2D6 Supplies,     51-75   1D6x500 silver
                      1D6 Ammo, 3x2D10
                      Common

             76-90    2D6 Provisions, 2D6 Supplies,     76-90   1D4 Rare Supplies,
                      1D6 Ammo, 2x2D10                          4D6x500 silver
                      Common, 2x1D6 Rich

             91-00    2D6 Provisions, 2D6 Supplies,     91-00   1D4 Rare Supplies, 1
                      1D6 Ammo, 3x2D10                          Treasure, 4D6x500 silver
                      Common, 2x1D10 Rich

Snow (120    01-50    2D6 Provisions, 2D6 Supplies,     01-50   None
tons)                 1D6 Ammo, 3x2D10
                      Common

             51-75    2D6 Provisions, 2D6 Supplies,     51-75   1D6x500 silver
                      1D6 Ammo, 4xD10 Common

             76-90    2D6 Provisions, 2D6 Supplies,     76-90   1D4 Rare Supplies,
                      1D6 Ammo, 4x2D10                          4D6x500 silver
                      Common, 2x1D6 Rich

             91-00    2D6 Provisions, 2D6 Supplies,     91-00   1D4 Rare Supplies, 1D4
                      1D6 Ammo, 4x2D10                          Treasure, 4D6x500 silver
                      Common, 2x1D10 Rich

Brigantine   01-50    3D6 Provisions, 3D6 Supplies,     01-50   None
(100 tons)            2D6 Ammo, 3D10 Common

             51-75    3D6 Provisions, 3D6 Supplies,     51-75   1D6x500 silver
                      2D6 Ammo, 2x2D10
                      Common, 1D6 Rich

             76-90    3D6 Provisions, 3D6 Supplies,     76-90   1D4 Rare Supplies,
                      2D6 Ammo, 2xD10 Common,                   4D6x500 silver
                      3x1D6 Rich

             91-00    3D6 Provisions, 3D6 Supplies,     91-00   1D4 Rare Supplies, 1
                      2D6 Ammo, 3x2D10                          Treasure, 4D6x500 silver
                      Common, 2x1D10 Rich

Fluyt (250   01-50    6D6 Provisions, 6D6 Supplies,     01-50   3D6x500 silver, 50%
tons)                 3D6 Ammo, 3x5D10                          chance of 1D6 Rare
                      Common, 1D10 Rich                         Supplies

             51-75    6D6 Provisions, 6D6               51-75   6D6x500 silver, 1D6
                      Supplies, 3D6 Ammo,4x5D10                 Rare Supplies
                      Common, 3x1D10 Rich

             76-90    6D6 Provisions, 6D6 Supplies,     76-90   8D6x500 silver, 1D6
                      3D6 Ammo, 5x5D10                          Rare Supplies, 50%
                      Common, 4xD10 Rich                        chance of 1D4 Treasures

             91-00    6D6 Provisions, 6D6 Supplies,     91-00   10D6x500 silver, 1D6
                      3D6 Ammo, 6x5D10                          Rare Supplies, 1D4
                      Common, 4x3D10 Rich                       Treasures

Barque       01-50    2D6 Provisions, 2D6 Supplies,     01-50   None
(80)                  1D6 Ammo, 2D10 Common,
                      1D4 Rich

             51-75    2D6 Provisions, 2D6 Supplies,     51-75   1D6x200 silver, 50%
                      1D6 Ammo,3D10 Common,                     chance of 1 Rare
                      2D4 Rich                                  Supplies

             76-90    2D6 Provisions, 2D6               76-90   1 Rare Supplies,
                      Supplies, 1D6 Ammo,2x2D10                 2D6x200 silver, 50%
                      Common, 3D4 Rich                          chance of one Treasure

             91-00    2D6 Provisions, 2D6               91-00   1D4 Rare Supplies, 1
                      Supplies, 1D6 Ammo,3x2D10                 Treasure, 4D6x200 silver
                      Common, 4D4 Rich

Indiaman     01-50    6D6 Provisions, 6D6 Supplies,     01-50   4D6x500 silver, 1D6
(300 tons)            3D6 Ammo, 6x5D10                          Rare Supplies
                      Common, 2D10 Rich

             51-75    6D6 Provisions, 6D6 Supplies,     51-75   6D6x500 silver, 1D6
                      3D6 Ammo, 7x5D10                          Rare Supplies, 1 Treasure
                      Common, 3x2D10 Rich

             76-90    6D6 Provisions, 6D6 Supplies,     76-90   8D6x500 silver, 1D6
                      3D6 Ammo, 10x5D10                         Rare Supplies, 1D4
                      Common, 4x3D10 Rich                       Treasures

             91-00    6D6 Provisions, 6D6 Supplies,     91-00   10D6x500 silver, 1D6
                      3D6 Ammo, 10x5D10                         Rare Supplies, 2D4
                      Common, 5x4D10 Rich                       Treasures

Galleon      01-50    6D6 Provisions, 6D6 Supplies,     01-50   10D6x500 silver, 1D6
(250)                 3D6 Ammo, 4x5D10                          Rare Supplies, 1 Treasure
                      Common, 2D10 Rich

             51-75    6D6 Provisions, 6D6 Supplies,     51-75   12D6x500 silver, 1D6
                      3D6 Ammo, 6x5D10                          Rare Supplies, 1D4
                      Common, 2x2D10 Rich                       Treasures

             76-90    6D6 Provisions, 6D6 Supplies,     76-90   16D6x500 silver, 1D6
                      3D6 Ammo, 6x5D10                          Rare Supplies, 2D4
                      Common, 4x4D10 Rich                       Treasures

             91-00    6D6 Provisions, 6D6 Supplies,     91-00   20D6x500 silver, 1D6
                      3D6 Ammo, 6x5D10                          Rare Supplies, 3D4
                      Common, 6x4D10 Rich                       Treasures

Sloop of     01-50    2D6 Provisions, 2D6 Supplies,     01-50   1D6x200 silver
War (50               1D6 Ammo,2D10 Common
tons)

             51-75    2D6 Provisions, 2D6 Supplies,     51-75   1 Rare Supplies,
                      1D6 Ammo,3D10 Common                      2D6x200 silver

             76-90    2D6 Provisions, 2D6 Supplies,     76-90   1D4 Rare Supplies,
                      1D6 Ammo,3D10 Common,                     4D6x200 silver
                      1D6 Rich

             91-00    2D6 Provisions, 2D6 Supplies,     91-00   1D4 Rare Supplies,
                      1D6 Ammo,3D10 Common,                     6D6x200 silver, 50%
                      1D10 Rich                                 chance of 1 Treasure

Corvette     01-50    2D6 Provisions, 2D6 Supplies,     01-50   1D6x200 silver
(40 tons)             1D6 Ammo, 1D10 Common

             51-75    2D6 Provisions, 2D6 Supplies,     51-75   1 Rare Supplies,
                      1D6 Ammo, 2D10 Common                     2D6x200 silver

             76-90    2D6 Provisions, 2D6 Supplies,     76-90   1D4 Rare Supplies,
                      1D6 Ammo, 1D10 Common,                    4D6x200 silver
                      1D6 Rich

             91-00    2D6 Provisions, 2D6 Supplies,     91-00   1D4 Rare Supplies,
                      1D10 Common, 1D10 Rich                    6D6x200 silver, 50%
                                                                chance of 1 Treasure

Frigate (80  01-50    2D6 Provisions, 2D6 Supplies,     01-50   1D6x200 silver, 50%
tons)                 2D6 Ammo, 2D10 Common,                    chance of 1 Rare
                      1D4 Rich                                  Supplies

             51-75    2D6 Provisions, 2D6 Supplies,     51-75   1 Rare Supplies,
                      2D6 Ammo,2D10 Common,                     2D6x200 silver, 50%
                      2D6 Rich                                  chance of one Treasure

             76-90    2D6 Provisions, 2D6 Supplies,     76-90   1D4 Rare Supplies, 1
                      2D6 Ammo,4D10 Common,                     Treasure, 4D6x200 silver
                      3D6 Rich

             91-00    2D6 Provisions, 2D6 Supplies,     91-00   1D4 Rare Supplies, 1D4
                      3D10 Common, 2x2D6 Rich                   Treasure, 4D6x200 silver

Warship      01-50    2D6 Provisions, 2D6 Supplies,     01-50   1D6x200 silver, 50%
(100 tons)            4D6 Ammo, 2D10 Common,                    chance of 1 Rare
                      1D4 Rich                                  Supplies

             51-75    2D6 Provisions, 2D6 Supplies,     51-75   1 Rare Supplies,
                      4D6 Ammo,2D10 Common,                     2D6x200 silver, 50%
                      2D6 Rich                                  chance of one Treasure

             76-90    2D6 Provisions, 2D6 Supplies,     76-90   1D4 Rare Supplies, 1
                      4D6 Ammo,4D10 Common,                     Treasure, 4D6x200 silver
                      2x3D6 Rich

             91-00    2D6 Provisions, 2D6 Supplies,     91-00   1D4 Rare Supplies, 1D4
                      4D6 Ammo, 3D10 Common,                    Treasure, 4D6x200 silver
                      3x2D6 Rich
";

			//List<string> lines = MarkdownConverter.MakeTable (text);
			List<string> lines = MarkdownConverter.MakeTable (text, hasMultipleRowsPerCell: true);
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
