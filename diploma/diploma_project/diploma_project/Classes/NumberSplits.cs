using System.Collections.Generic;
using System.Linq;

namespace diploma_project
{
	public static class NumberSplits
	{
		private static List<int> _prev = new List<int>();
		private static List<List<int>> _compoz = new List<List<int>>();
		/// <summary>
		/// генерирует разбиения числа
		/// </summary>
		/// <param name="n">Число</param>
		public static List<List<int>> GenerateSplits2(int n)
		{
			n += 1;
			Step(n - 1, n - 1);
			var fCompoz = new List<List<int>>();
			foreach (var item in _compoz)
			{
				fCompoz.Add(new List<int>());
				item.Sort((delegate (int x, int y)
				{
					if (x > y)
					{
						return -1;
					}
					else if (x < y)
					{
						return 1;
					}
					else
					{
						return 0;
					}
				}));
				fCompoz.Last().AddRange(item);
				item.Clear();
			}
			_compoz.Clear();
			for (int i = fCompoz.Count - 1; i >= 0; i--)
			{
				for (int j = i - 1; j >= 0; j--)
				{
					if (CompareSplits(fCompoz[i], fCompoz[j]))
					{
						fCompoz.RemoveAt(i);
						break;
					}
				}
			}
			/*fCompoz.Sort(delegate (List<int> x, List<int> y)
			{
				if (x.Count > y.Count)
				{
					return 1;
				}
				else if (x.Count < y.Count)
				{
					return -1;
				}
				else
				{
					for (int i = 0; i < x.Count; i++)
					{
						if (x[i] > y[i])
						{
							return -1;
						}
						else if (x[i] < y[i])
						{
							return 1;
						}
					}
					return 0;
				}
			});*/
			return fCompoz;
		}
		/// <summary>
		/// шаг рекурсии, считающей композиции числа
		/// </summary>
		/// <param name="n">N.</param>
		/// <param name="beg">Beg.</param>
		private static void Step(int n, int beg)
		{
			for (int i = 1; i <= n; i++)
			{
				_prev.Add(i);
				Step(n - i, beg);
			}
			if (_prev.Sum() == beg)
			{
				if (_prev.Count == 0)
				{
					_prev.Add(0);
				}
				_compoz.Add(new List<int>(_prev));
			}
			if (_prev.Count > 0)
			{
				_prev.RemoveAt(_prev.Count - 1);
			}
		}
		/// <summary>
		/// Compares the splits2.
		/// </summary>
		/// <returns><c>true</c>, if splits2 was compared, <c>false</c> otherwise.</returns>
		/// <param name="l1">L1.</param>
		/// <param name="l2">L2.</param>
		private static bool CompareSplits(List<int> l1, List<int> l2)
		{
			if (l1.Count != l2.Count)
			{
				return false;
			}
			for (int i = 0; i < l1.Count; i++)
			{
				if (l1[i] != l2[i])
				{
					return false;
				}
			}
			return true;
		}
		public static List<List<int>> GenerateSplits(int n)
		{
			var ls = new List<List<int>>();
			int[] a = new int[n + 1];
			int x = 0;
			int q = 0;
			//step 1
			for (int i = n; i >= 1; i--)
			{
				a[i] = 1;
			}
			int m = 1;
			a[0] = 0;
			var flag2 = true;
			//step 2
			do
			{
				if (flag2)
				{
					a[m] = n;
					q = m - (n == 1 ? 1 : 0);
					//step 3
				}
				else
				{
					flag2 = true;
				}
				ls.Add(a.ToList().GetRange(1, m));
				if (a[q] == 2)
				{
					//step 4
					a[q] = 1;
					q = q - 1;
					m = m + 1;
					flag2 = false;
					continue;
				}
				else
				{
					//step 5
					if (q == 0)
					{
						return ls;
					}
					else
					{
						x = a[q] - 1;
						a[q] = x;
						n = m - q + 1;
						m = q + 1;
					}
				}
				//step 6
				do
				{
					if (n <= x)
					{
						break;
					}
					else
					{
						a[m] = x;
						m = m + 1;
						n = n - x;
					}
				}
				while (true);
			}
			while (true);
		}

		public static void Trace(string text)
		{
#if TRACE
			System.Console.WriteLine(text);
			System.Console.ReadKey();
#endif
		}
	}
}

