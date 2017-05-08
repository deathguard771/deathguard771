using System;
using System.Collections.Generic;
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
		public void Print()
		{
			Console.WriteLine (Text);
		}
	}
}

