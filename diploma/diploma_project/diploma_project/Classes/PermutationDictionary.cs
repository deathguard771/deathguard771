using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace diploma_project
{
	/// <summary>
	/// Словарь "перестановка - коэффициент"
	/// </summary>
	public class PermutationDictionary : Dictionary<Permutation, int>
	{
		/// <summary>
		/// Текстовое представление
		/// </summary>
		public string Text
		{
			get
			{
				return string.Join (" + ", this.Select (kvp => 
				{
					if (kvp.Value < 1)
					{
						return "";
					}
					else if (kvp.Value == 1)
					{
						return kvp.Key.Text;
					}
					else
					{
						return kvp.Value + "*" + kvp.Key.Text;
					}
				}));
			}
   		}
		/// <summary>
		/// Добавляет перестановку в словарь
		/// </summary>
		/// <param name="p">Перестановка</param>
		public void Add(Permutation p, int count = 1)
		{
			if (this.ContainsKey (p))
			{
				this[p] += count;
			}
			else
			{
				base.Add (p, count);
			}
		}
		/// <summary>
		/// Добавляет несколько перестановок в словарь
		/// </summary>
		/// <param name="p">Коллекция перестановок</param>
		public void Add(IEnumerable<Permutation> p)
		{
			foreach (var i in p)
			{
				this.Add (i);
			}
		}
		/// <summary>
		/// Печатает словарь перестановок
		/// </summary>
		public void Print(Output output = Output.Console, string path = "", string text = "")
		{
			if (text == "")
			{
				text = Text;
			}
			if (output == Output.Console)
			{
				Console.WriteLine (text);
			}
			else if (output == Output.File)
			{
				using (var fs = File.AppendText (path))
				{
					fs.Write (text);
					fs.WriteLine ();
				}
			}
		}
		/// <summary>
		/// Печатаем только тип перестановки и его количество
		/// </summary>
		public void Print2(Output output = Output.Console, string path = "")
		{
			var dictCounts = new Dictionary<Permutation, int> ();
			foreach (var kvp in this)
			{
				var flagInc = false;
				foreach (var kvp2 in dictCounts)
				{
					if (Permutation.Compare(kvp2.Key, kvp.Key))
					{
						dictCounts[kvp2.Key]++;
						flagInc = true;
						break;
					}
				}
				if (!flagInc)
				{
					dictCounts.Add (kvp.Key, 1);
				}
			}
			var txt = dictCounts.Select(kvp => kvp.Key.Text + " = " + kvp.Value).ToList();
			Print(output, path, string.Join("\n", txt) + "\n\n");
		}
		/// <summary>
		/// Copy the specified PermutationDictionary.
		/// </summary>
		/// <param name="p1">P1.</param>
		public static PermutationDictionary Copy(PermutationDictionary p1)
		{
			var res = new PermutationDictionary ();
			foreach (var kvp in p1)
			{
				res.Add (kvp.Key, kvp.Value);
			}
			return res;
		}

		public void ClearEmptyEntries()
		{
			var keys = new List<Permutation> ();
			foreach (var kvp in this)
			{
				if (kvp.Key.NotTrivialCycles.Count == 0)
				{
					keys.Add (kvp.Key);
				}
			}
			foreach (var key in keys)
			{
				this.Remove (key);
			}
		}

		/// <param name="p1">P1.</param>
		/// <param name="p2">P2.</param>
		public static PermutationDictionary operator * (PermutationDictionary p1, PermutationDictionary p2)
		{
			var res = new PermutationDictionary ();
			if (p1.Count == 0 && p2.Count > 0)
			{
				return Copy (p2);
			}
			else if (p2.Count == 0 && p1.Count > 0)
				{
					return Copy (p1);
				}
				else if (p1.Count == 0 && p2.Count == 0)
					{
						throw new Exception ("Две нулевых штуки.");
					}
			foreach (var kvp1 in p1)
			{
				foreach (var kvp2 in p2)
				{
					res.Add( kvp1.Key * kvp2.Key, kvp1.Value * kvp2.Value);
				}
			}
			return res;
		}
	}
}


