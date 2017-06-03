using System;
using System.Diagnostics;
using System.IO;

namespace diploma_project
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var files = Directory.GetFiles (AppDomain.CurrentDomain.BaseDirectory);
			foreach (var f in files) {
				if (f.EndsWith(".txt", StringComparison.CurrentCulture)) {
					File.Delete (f);
				}
			}
			
			var pn = 1;
			var vc = pn * 2;

			using (var fs = File.AppendText ("temp.txt")) {
				fs.WriteLine ("До " + pn + " степени.");
				fs.WriteLine (vc + " переменных.");
				fs.WriteLine ();
			}
			var sw = new Stopwatch();
			sw.Start();
			YoungGrid.Generate (pn, vc, true);
			sw.Stop();
			Console.WriteLine($"Ready! {sw.Elapsed} elapsed.");
			//ErrorBeep();
			Console.ReadLine();
		}

		public static void SuccessBeep()
		{
			for (int i = 0; i< 10; i++)
			{
				for (int j = 0; j< 3; j++)
				{
					Console.Beep(1000, 250);
				}
				System.Threading.Thread.Sleep(500);
			}
		}

		public static void ErrorBeep()
		{
			for (int j = 0; j < 3; j++)
			{
				Console.Beep(500, 500);
			}
			System.Threading.Thread.Sleep(100);
			for (int j = 0; j < 3; j++)
			{
				Console.Beep(500, 250);
			}
			System.Threading.Thread.Sleep(100);
			for (int j = 0; j < 3; j++)
			{
				Console.Beep(500, 500);
			}
			System.Threading.Thread.Sleep(500);
		}
	}
}