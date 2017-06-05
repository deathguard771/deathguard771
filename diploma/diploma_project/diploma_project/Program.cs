using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace diploma_project
{
	class MainClass
	{
		/// <summary>
		/// Генерация
		/// </summary>
		/// <param name="degree">Степень</param>
		/// <param name="variablesCount">Количество переменных</param>
		public static void Generate(int degree, int variablesCount, bool newVariant = true)
		{
			Stopwatch sw = new Stopwatch(), sw2 = new Stopwatch(), sw3 = new Stopwatch();
			sw.Start();
			var sigmas = new List<List<PermutationDictionary>>();
			//first variant
			for (int i = 1; i <= degree; i++)
			{
				var y = YJMElement.Generate(variablesCount + 1);
				var e = new ElementarySymmetricPolynomial(variablesCount, i);
				var pd = e.Substitution(y);
				pd.Split.Add(i);
				sigmas.Add(new List<PermutationDictionary> { pd });
			}

			var maxOrder = sigmas.Max(kvp => kvp.Max(kvp2 => kvp2.GetMaxOrder()));

			foreach (var item in sigmas)
			{
				foreach (var item2 in item)
				{
					item2.SetOrder(maxOrder);
				}
			}
			sw.Stop();
#if sharp6
			Console.WriteLine($"First stage ready! {sw.Elapsed} elapsed.");
#else
			Console.WriteLine("First stage ready! " + sw.Elapsed + " elapsed.");
#endif
			sw.Restart();
			sigmas[0][0].SimplyPrint(Output.File, "temp.txt", " = {" + string.Join(", ", sigmas[0][0].Split) + "}");
#if sharp6
			Console.WriteLine("{" + string.Join(", ", sigmas[0][0].Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
			Console.WriteLine("{" + string.Join(", ", sigmas[0][0].Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
			for (int i = 2; i <= degree; i++)
			{
				var ls = NumberSplits.GenerateSplits(i);
				foreach (var split in ls)
				{
					if (split.Count > 1)
					{
						var res = new PermutationDictionary();
						//split.Reverse ();
						int i1 = 0, i2 = 0, i3 = -1, prevCnt = 0;
						var c1 = 0;
						sw2.Start();
						if (newVariant)
						{
							foreach (var sigma in sigmas)
							{
								var c2 = 0;
								foreach (var perm in sigma)
								{
									if (perm.Split.Count <= split.Count)
									{
										var cnt = 0;
										int j = 0;
										for (; j < perm.Split.Count; j++)
										{
											if (split[j] != perm.Split[j])
											{
												break;
											}
											cnt++;
										}
										if (cnt > prevCnt)
										{
											prevCnt = cnt;
											i1 = c1;
											i2 = c2;
											i3 = j;
										}
									}
									c2++;
								}
								c1++;
							}
						}
						if (i3 >= 0)
						{
							res = sigmas[i1][i2];
						}
						if (!newVariant)
						{
							i3 = 0;
						}
						sw2.Stop();
						sw3.Start();
						for (int j = i3; j < split.Count; j++) //foreach (var elem in split)
						{
							res = res * sigmas[split[j] - 1][0];
						}
						res.Split.AddRange(split);
						sigmas[i - 1].Add(res);
						res.SimplyPrint(Output.File, "temp.txt", " = {" + string.Join(", ", res.Split) + "}");
						//var ccc = 0;
						//var rrr = res.Where(kvp => kvp.Key.NotTrivialCycles.Count == 3 && kvp.Key.NotTrivialCycles.Count(cycle => cycle.Length == 2) == 3).Select(kvp =>
						//{
						//	ccc += kvp.Value;
						//	return kvp.Value == 1 ? kvp.Key.Text : kvp.Value + "*" + kvp.Key.Text;
						//});
						//res.Print (Output.File, "(" + string.Join(", ", res.Split) + ").txt", string.Join("\r\n", rrr) + "\r\n" + "All" + ccc/*.Text.Replace(" + ", "\n")*/);
#if sharp6
						Console.WriteLine("{" + string.Join(", ", res.Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
						Console.WriteLine("{" + string.Join(", ", res.Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
						sw3.Stop();
					}
					else
					{
						sigmas[i - 1][0].SimplyPrint(Output.File, "temp.txt", " = {" + string.Join(", ", sigmas[i - 1][0].Split) + "}");
#if sharp6
						Console.WriteLine("{" + string.Join(", ", sigmas[i - 1][0].Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
						Console.WriteLine("{" + string.Join(", ", sigmas[i - 1][0].Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
					}
				}
			}
			sw.Stop();
#if sharp6
			Console.WriteLine($"Finding stage: {sw2.Elapsed} elapsed.");
			Console.WriteLine($"Multiplying stage: {sw3.Elapsed} elapsed.");
			Console.WriteLine($"Second stage ready! {sw.Elapsed} elapsed.");
#else
			Console.WriteLine($"Finding stage: " + sw2.Elapsed + " elapsed.");
			Console.WriteLine($"Multiplying stage: " + sw3.Elapsed + " elapsed.");
			Console.WriteLine($"Second stage ready! " + sw.Elapsed + " elapsed.");
#endif

			sw.Restart();
			/*foreach (var item in sigmas)
			{
				foreach (var item2 in item)
				{
					item2.Print2 (Output.File, "temp.txt", " = {" + string.Join (", ", item2.Split) + "}");
					//item2.Print (Output.File, "(" + string.Join (", ", item2.Split) + ").txt", item2.Text.Replace(" + ", "\r\n"));
				}
			}*/
			sw.Stop();
#if sharp6
			Console.WriteLine($"Third stage ready! {sw.Elapsed} elapsed.");
#else
			Console.WriteLine("Third stage ready! " + sw.Elapsed + " elapsed.");
#endif
		}
		
		public static void Main (string[] args)
		{
			var files = Directory.GetFiles (AppDomain.CurrentDomain.BaseDirectory);
			foreach (var f in files) {
				if (f.EndsWith(".txt", StringComparison.CurrentCulture)) {
					File.Delete (f);
				}
			}
			
			var pn = 6;
			var vc = pn * 2;

			using (var fs = File.AppendText ("temp.txt")) {
				fs.WriteLine ("До " + pn + " степени.");
				fs.WriteLine (vc + " переменных.");
				fs.WriteLine ();
			}
			var sw = new Stopwatch();
			sw.Start();
			Generate (pn, vc, true);
			sw.Stop();
#if sharp6
			Console.WriteLine($"Ready! {sw.Elapsed} elapsed.");
#else
            Console.WriteLine("Ready! " + sw.Elapsed + " elapsed.");
#endif
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