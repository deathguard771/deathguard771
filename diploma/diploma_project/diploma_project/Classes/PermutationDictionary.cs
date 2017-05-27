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
			
		public List<int> Split = new List<int> ();

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
		public void Print2(Output output = Output.Console, string path = "", string addText = "")
		{
			var dictCounts = new Dictionary<Permutation, int> ();
			//var flag = false;
			//res = false;
			foreach (var kvp in this)
			{
				var flagInc = false;
				foreach (var kvp2 in dictCounts)
				{
					if (Permutation.Compare(kvp2.Key, kvp.Key))
					{
						dictCounts [kvp2.Key] += kvp.Value;
						flagInc = true;
						break;
					}
				}
				if (!flagInc)
				{
					dictCounts.Add (kvp.Key, kvp.Value);
				}
			}
			//Permutation res2 = null;
			var txt = dictCounts.Select(kvp => 
			{
				var cnt = Permutation.GetPermutationsCount(kvp.Key);
				if (kvp.Value % cnt > 0)
				{
					//flag = true;
					//res2 = kvp.Key;
					Console.WriteLine("Error. Order = " + kvp.Key.Order + "; Perm = " + kvp.Key.Text + "; Count = " + kvp.Value + "; PermCount = " + cnt + "; Reminder = " + (kvp.Value % cnt));
				}
				return ((kvp.Value / cnt) != 1 ? (kvp.Value / cnt).ToString() : "") + (kvp.Key.NotTrivialCycles.Count > 0 ? ("(" + kvp.Key.Text + ")") : "");
			});
			Print(output, path, string.Join(" + ", txt) + addText + "\n");
			//res = flag;
			//return res2;
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
		/// <summary>
		/// Clears the empty entries.
		/// </summary>
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
		/// <summary>
		/// Sets the order.
		/// </summary>
		/// <param name="newOrder">New order.</param>
		public void SetOrder(int newOrder)
		{
			foreach (var kvp in this)
			{
				kvp.Key.SetOrder (newOrder);
			}
		}
		/// <summary>
		/// Sets the order.
		/// </summary>
		public void SetOrder()
		{
			var newOrder = GetMaxOrder();
			foreach (var kvp in this)
			{
				kvp.Key.SetOrder (newOrder);
			}
		}
		/// <summary>
		/// Gets the max order.
		/// </summary>
		/// <returns>The max order.</returns>
		public int GetMaxOrder()
		{
			return this.Keys.Max (t => t.Order);
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
					var resKey = kvp1.Key * kvp2.Key;
					var resVal = kvp1.Value * kvp2.Value;
					res.Add( resKey, resVal);
				}
			}
			return res;
		}
	}
}


