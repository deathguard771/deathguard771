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
		public void Add(Permutation p)
		{
			if (this.ContainsKey (p))
			{
				this[p]++;
			}
			else
			{
				base.Add (p, 1);
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
				using (var fs = File.CreateText (path))
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
			Print(output, path, string.Join("\n", txt));
		}
	}
}


