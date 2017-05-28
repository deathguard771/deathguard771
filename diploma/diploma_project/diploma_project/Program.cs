using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace diploma_project
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var files = Directory.GetFiles (AppDomain.CurrentDomain.BaseDirectory);
			foreach (var f in files) {
				if (f.EndsWith (".txt")) {
					File.Delete (f);
				}
			}
			
			var pn = 2;
			var vc = pn * 2;

			using (var fs = File.AppendText ("temp.txt")) {
				fs.WriteLine ("До " + pn + " степени.");
				fs.WriteLine (vc + " переменных.");
				fs.WriteLine ();
			}

			YoungGrid.Generate (pn, vc);
			var b = 0;
			/*var length = 10;

			var ls = new List<PermutationDictionary> ();
			for (int i = 0; i < length; i++)
			{
				if (i >= pn)
				{
					ls.Add (Generate (i, pn, Output.File));
				}
			}*/
		}
	}
}