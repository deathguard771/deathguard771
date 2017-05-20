using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace diploma_project
{
	class MainClass
	{
		public static PermutationDictionary Generate(int variablesCount, int polynomialNumber, Output output)
		{
			var y = YJMElement.Generate (variablesCount + 1);
			var e = new ElementarySymmetricPolynomial (variablesCount, polynomialNumber);
			var res = e.Substitution (y);
			res.Print (output, variablesCount + "___" + polynomialNumber + ".txt");
			return res;
		}
		public static void Main (string[] args)
		{
			var pn = 4;
			var files = Directory.GetFiles (AppDomain.CurrentDomain.BaseDirectory);
			foreach (var f in files)
			{
				if (f.EndsWith (".txt"))
				{
					File.Delete (f);
				}
			}
			var ls = new List<PermutationDictionary> ();
			for (int i = 0; i < 10; i++)
			{
				if (i >= pn)
				{
					ls.Add (Generate (i, pn, Output.None));
				}
			}
		}
	}
}