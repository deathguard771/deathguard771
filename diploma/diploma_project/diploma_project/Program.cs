using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace diploma_project
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/*var a = new Cycle (new [] { 2, 3 });
			var b = new Cycle (new[] { 1, 4 });
			var perm1 = new Permutation (4, a);
			var perm2 = new Permutation (4, b);
			perm1.Print ();
			perm2.Print ();
			var perm3 = perm1 * perm2;
			perm3.Print ();*/
			//var ls = YJMElement.Generate ();
			//YJMElement.Print (ls);
			/*var sw = new Stopwatch();
			sw.Start ();
			var f = new YJMElement (20);
			sw.Stop ();
			Console.WriteLine ("t(20) = " + sw.Elapsed);
			sw.Restart ();
			var s = new YJMElement (30);
			sw.Stop ();
			Console.WriteLine ("t(30) = " + sw.Elapsed);
			sw.Restart ();
			var t = f * s;
			sw.Stop ();
			Console.WriteLine ("t(pr) = " + sw.Elapsed);*/

			var y = YJMElement.Generate (5);
			var zzz = "zzz";
			var e = new ElementarySymmetricPolynomial (4, 3);
			var res = e.Substitution (y);
			//var res = new PermutationDictionary();
			//res.Add (new Permutation (5, new [] { 1, 3, 4, 2, 5 }));
			//res.Add (new Permutation (5, new [] { 1, 3, 4, 2, 5 }));
			//res.Add (new Permutation (5, new [] { 1, 2, 4, 3, 5 }));
			res.Print ();
		}
	}
}
