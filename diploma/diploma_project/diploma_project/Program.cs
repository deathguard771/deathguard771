using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace diploma_project
{
	class MainClass
	{
		#region последовательно
		/// <summary>
		/// Генерация
		/// </summary>
		/// <param name="degree">Степень</param>
		/// <param name="variablesCount">Количество переменных</param>
		public static void Generate(int degree, int variablesCount, bool newVariant = true)
		{
#if TRACE
			Stopwatch sw = new Stopwatch(), sw2 = new Stopwatch(), sw3 = new Stopwatch();
			sw.Start();
#endif
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
#if TRACE
			sw.Stop();
#if sharp6
			Console.WriteLine($"First stage ready! {sw.Elapsed} elapsed.");
#else
			Console.WriteLine("First stage ready! " + sw.Elapsed + " elapsed.");
#endif

			sw.Restart();
#endif
			sigmas[0][0].SimplyPrint(Output.File, "consist\\temp" + string.Join(", ", sigmas[0][0].Split) + ".txt", " = {" + string.Join(", ", sigmas[0][0].Split) + "}");
#if TRACE
#if sharp6
			Console.WriteLine("{" + string.Join(", ", sigmas[0][0].Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
			Console.WriteLine("{" + string.Join(", ", sigmas[0][0].Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
#endif
			var ls = new Dictionary<int, List<List<int>>>();
			for (int i = 2; i<degree + 1; i++)
			{
				ls.Add(i, NumberSplits.GenerateSplits(i));
			}
			for (int i = 2; i <= degree; i++)
			{
				//var ls = NumberSplits.GenerateSplits(i);
				foreach (var split in ls[i])
				{
					var fName = $"consist\\temp " + string.Join(", ", split) + ".txt";
					if (split.Count > 1)
					{
						var res = new PermutationDictionary();
						//split.Reverse ();
						/*int i1 = 0, i2 = 0, i3 = -1, prevCnt = 0;
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
#if TRACE
						sw2.Stop();
#endif
						*/
#if TRACE
						sw3.Start();
#endif
						for (int j = 0; j < split.Count; j++) //foreach (var elem in split)
						{
							res = res * sigmas[split[j] - 1][0];
						}
						res.Split.AddRange(split);
						//sigmas[i - 1].Add(res);
						res.SimplyPrint(Output.File, fName, " = {" + string.Join(", ", res.Split) + "}");
#if TRACE
#if sharp6
						Console.WriteLine("{" + string.Join(", ", res.Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
						Console.WriteLine("{" + string.Join(", ", res.Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
						sw3.Stop();
#endif
					}
					else
					{
						sigmas[i - 1][0].SimplyPrint(Output.File, fName, " = {" + string.Join(", ", sigmas[i - 1][0].Split) + "}");
#if TRACE
#if sharp6
						Console.WriteLine("{" + string.Join(", ", sigmas[i - 1][0].Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
						Console.WriteLine("{" + string.Join(", ", sigmas[i - 1][0].Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
#endif
					}
				}
			}
#if TRACE
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
			sw.Stop();
#if sharp6
			Console.WriteLine($"Third stage ready! {sw.Elapsed} elapsed.");
#else
			Console.WriteLine("Third stage ready! " + sw.Elapsed + " elapsed.");
#endif
#endif
		}
		#endregion

		#region параллельно
		/// <summary>
		/// Генерация
		/// </summary>
		/// <param name="degree">Степень</param>
		/// <param name="variablesCount">Количество переменных</param>
		public static void ParallelGenerate(int degree, int variablesCount, bool newVariant = true)
		{
#if TRACE
			Stopwatch sw = new Stopwatch(), sw2 = new Stopwatch(), sw3 = new Stopwatch();
			sw.Start();
#endif
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
#if TRACE
			sw.Stop();
#if sharp6
			Console.WriteLine($"First stage ready! {sw.Elapsed} elapsed.");
#else
			Console.WriteLine("First stage ready! " + sw.Elapsed + " elapsed.");
#endif
			sw.Restart();
#endif
			sigmas[0][0].SimplyPrint(Output.File, "parall" + "\\temp" + string.Join(", ", sigmas[0][0].Split) + ".txt", " = {" + string.Join(", ", sigmas[0][0].Split) + "}");
#if TRACE
#if sharp6
			Console.WriteLine("{" + string.Join(", ", sigmas[0][0].Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
			Console.WriteLine("{" + string.Join(", ", sigmas[0][0].Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
#endif
			var ls = new Dictionary<int, List<List<int>>>();
			for (int i = 2; i < degree + 1; i++)
			{
				ls.Add(i, NumberSplits.GenerateSplits(i));
			}
			Parallel.ForEach(ls, splits =>
			{
				//var ls = NumberSplits.GenerateSplits(i);
				var i = splits.Key;
				foreach (var split in splits.Value)
				{
					var fName = $"parall\\temp " + string.Join(", ", split) + ".txt";
					if (split.Count > 1)
					{
						var res = new PermutationDictionary();
#if TRACE
						sw3.Start();
#endif
						for (int j = 0; j < split.Count; j++)
						{
							res = res * sigmas[split[j] - 1][0];
						}
						res.Split.AddRange(split);
						res.SimplyPrint(Output.File, fName, " = {" + string.Join(", ", res.Split) + "}");
#if TRACE
#if sharp6
						Console.WriteLine("{" + string.Join(", ", res.Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
						Console.WriteLine("{" + string.Join(", ", res.Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
						sw3.Stop();
#endif
					}
					else
					{
						sigmas[i - 1][0].SimplyPrint(Output.File, fName, " = {" + string.Join(", ", sigmas[i - 1][0].Split) + "}");
#if TRACE
#if sharp6
						Console.WriteLine("{" + string.Join(", ", sigmas[i - 1][0].Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
						Console.WriteLine("{" + string.Join(", ", sigmas[i - 1][0].Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
#endif
					}
				}
			});
#if TRACE
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
			sw.Stop();
#if sharp6
			Console.WriteLine($"Third stage ready! {sw.Elapsed} elapsed.");
#else
			Console.WriteLine("Third stage ready! " + sw.Elapsed + " elapsed.");
#endif
#endif
		}
		#endregion

		#region параллельно2
		/// <summary>
		/// Генерация
		/// </summary>
		/// <param name="degree">Степень</param>
		/// <param name="variablesCount">Количество переменных</param>
		public static void ParallelGenerate2(int degree, int variablesCount, bool newVariant = true)
		{
#if TRACE
			Stopwatch sw = new Stopwatch(), sw2 = new Stopwatch(), sw3 = new Stopwatch();
			sw.Start();
#endif
			var sigmas = new List<List<PermutationDictionary>>();
			//first variant
			for (int i = 1; i <= degree; i++)
			{
				var y = YJMElement.Generate(variablesCount + 1);
				var e = new ElementarySymmetricPolynomial(variablesCount, i);
				var pd = e.Substitution(y);
				pd.Split.Add(i);
				sigmas.Add(new List<PermutationDictionary> { pd });

				GC.Collect();
			}
			var maxOrder = sigmas.Max(kvp => kvp.Max(kvp2 => kvp2.GetMaxOrder()));

			foreach (var item in sigmas)
			{
				foreach (var item2 in item)
				{
					item2.SetOrder(maxOrder);
				}
			}

#if TRACE
			sw.Stop();
#if sharp6
			Console.WriteLine($"First stage ready! {sw.Elapsed} elapsed.");
#else
			Console.WriteLine("First stage ready! " + sw.Elapsed + " elapsed.");
#endif
			sw.Restart();
#endif
			sigmas[0][0].SimplyPrint(Output.File, "parall2" + "\\temp" + string.Join(", ", sigmas[0][0].Split) + ".txt", " = {" + string.Join(", ", sigmas[0][0].Split) + "}");
#if TRACE
#if sharp6
			Console.WriteLine("{" + string.Join(", ", sigmas[0][0].Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
			Console.WriteLine("{" + string.Join(", ", sigmas[0][0].Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
#endif
			GC.Collect();
			var ls = new Dictionary<int, List<List<int>>>();
			for (int i = 2; i<degree + 1; i++)
			{
				ls.Add(i, NumberSplits.GenerateSplits(i));
			}
			GC.Collect();
			for (int i = 5; i<degree + 1; i++)
			{
				//var ls = NumberSplits.GenerateSplits(i);
				Parallel.ForEach(ls[i], split =>
				{
					var fName = $"parall2\\temp " + string.Join(", ", split) + ".txt";
					if (split.Count > 1)
					{
						var res = new PermutationDictionary();
#if TRACE
						sw3.Start();
#endif
						for (int j = 0; j < split.Count; j++)
						{
							res = res * sigmas[split[j] - 1][0];
							GC.Collect();
						}
						res.Split.AddRange(split);
						res.SimplyPrint(Output.File, fName, " = {" + string.Join(", ", res.Split) + "}");
						GC.Collect();
#if TRACE
#if sharp6
						Console.WriteLine("{" + string.Join(", ", res.Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
						Console.WriteLine("{" + string.Join(", ", res.Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
						sw3.Stop();
#endif
					}
					else
					{
						sigmas[i - 1][0].SimplyPrint(Output.File, fName, " = {" + string.Join(", ", sigmas[i - 1][0].Split) + "}");
						GC.Collect();
#if TRACE
#if sharp6
						Console.WriteLine("{" + string.Join(", ", sigmas[i - 1][0].Split) + "} printed! " + $"{sw.Elapsed} elapsed.");
#else
						Console.WriteLine("{" + string.Join(", ", sigmas[i - 1][0].Split) + "} printed! " + sw.Elapsed + " elapsed.");
#endif
#endif
					}
				});
			}
#if TRACE
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
			sw.Stop();
#if sharp6
			Console.WriteLine($"Third stage ready! {sw.Elapsed} elapsed.");
#else
			Console.WriteLine("Third stage ready! " + sw.Elapsed + " elapsed.");
#endif
#endif
		}
#endregion

		public static void Main(string[] args)
		{
			//Console.WriteLine(string.Join(" ", args));
			var pn = 5;
			var vc = pn * 2;
			//var sss = NumberSplits.GenerateSplits2(pn);

			//Console.WriteLine(string.Join(" ", sss.Select(s => string.Join(",", s))));
			//Console.ReadKey();

			var run = Run.ParallelInner;
			if (args.Length > 1)
			{
				if (args[1] == "outer")
				{
					run = Run.ParallelOuter;
				}
				else if (args[1] == "inner")
				{
					run = Run.ParallelInner;
				}
			}

			var sw = new Stopwatch();
			var timer = new Timer();
			timer.Interval = 100;
			timer.Elapsed += delegate
			{
				Console.Clear();
				Console.WriteLine("Elapsed: " + sw.Elapsed);
			};
			sw.Start();
			//timer.Start();
			if (run == Run.Consistent)
			{
				var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\consist");
				foreach (var f in files)
				{
					if (f.EndsWith(".txt", StringComparison.CurrentCulture))
					{
						File.Delete(f);
					}
				}
				Generate(pn, vc, true);
				sw.Stop();
				timer.Stop();
				Console.Clear();
				using (var fs = File.AppendText("consist\\" + pn + ".time"))
				{
					fs.WriteLine(sw.Elapsed);
				}
#if sharp6
				Console.WriteLine($"Consistent ready! {sw.Elapsed} elapsed.");
#else
            	Console.WriteLine("Consistent ready! " + sw.Elapsed + " elapsed.");
#endif
			}
			else if (run == Run.ParallelOuter)
			{
				var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\parall");
				foreach (var f in files)
				{
					if (f.EndsWith(".txt", StringComparison.CurrentCulture))
					{
						File.Delete(f);
					}
				}
				ParallelGenerate(pn, vc, true);
				sw.Stop();
				//timer.Stop();
				Console.Clear();
				using (var fs = File.AppendText("parall\\" + pn + ".time"))
				{
					fs.WriteLine(sw.Elapsed);
				}
#if sharp6
				Console.WriteLine($"Parallel outer ready! {sw.Elapsed} elapsed.");
#else
            	Console.WriteLine("Parallel outer ready! " + sw.Elapsed + " elapsed.");
#endif
			}
			else if (run == Run.ParallelInner)
			{
				var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\parall2");
				foreach (var f in files)
				{
					if (f.EndsWith(".txt", StringComparison.CurrentCulture))
					{
						File.Delete(f);
					}
				}
                ParallelGenerate2(pn, vc, true);
				sw.Stop();
				//timer.Stop();
				Console.Clear();
				using (var fs = File.AppendText("parall2\\" + pn + ".time"))
				{
					fs.WriteLine(sw.Elapsed);
				}
#if sharp6
				Console.WriteLine($"Parallel inner ready! {sw.Elapsed} elapsed.");

#else
            	Console.WriteLine("Parallel inner ready! " + sw.Elapsed + " elapsed.");
#endif
			}
			//Console.ReadLine();
		}

		public static void SuccessBeep()
		{
			/*for (int i = 0; i< 10; i++)
			{
				for (int j = 0; j< 3; j++)
				{
					Console.Beep(1000, 250);
				}
				System.Threading.Thread.Sleep(500);
			}*/
		}

		public static void ErrorBeep()
		{
			/*for (int j = 0; j < 3; j++)
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
			System.Threading.Thread.Sleep(500);*/
		}
	}

	enum Run
	{
		ParallelOuter,
		ParallelInner,
		Consistent
	}
}