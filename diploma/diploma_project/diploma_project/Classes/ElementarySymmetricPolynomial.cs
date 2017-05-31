using System;
using System.Collections.Generic;
using System.Linq;

namespace diploma_project
{
	/// <summary>
	/// Класс, описывающий элементарный симметрический многочлен
	/// </summary>
	public class ElementarySymmetricPolynomial : SymmetricPolynomial
	{
		/// <summary>
		/// Количество переменных
		/// </summary>
		private int variablesCount; 
		/// <summary>
		/// Переменные
		/// </summary>
		private List<List<int>> terms = new List<List<int>>();
		/// <summary>
		/// Текстовое представление
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get
			{
				return string.Join (" + ", terms.Select (t => string.Join ("*", t.Select (tt => "X" + tt))));
			}
		}
		/// <summary>
		/// Инициализирует экземпляр класса <see cref="diploma_project.ElementarySymmetricPolynomial"/>
		/// </summary>
		/// <param name="_varCount">Количество переменных</param>
		/// <param name="_num">Номер элементарного симметрического многочлена</param>
		public ElementarySymmetricPolynomial (int _varCount, int _num)
		{
			if (_num > _varCount)
			{
				throw new ArgumentException ("Номер многочлена не может быть больше количества переменных.");
			}
			variablesCount = _varCount;
			GenerateElementarySymmetricPolynomial (_num);
		}
		/// <summary>
		/// Генерирует элементарный симметрический многочлен
		/// </summary>
		private void GenerateElementarySymmetricPolynomial(int num)
		{
			var combination = new Combination (variablesCount, num);
			do
			{
				terms.Add(combination.CurrentCombination);
			}
			while (combination.GetNextCombination ());
		}
		/// <summary>
		/// Печатает элементарный симметрический многочлен
		/// </summary>
		public void Print()
		{
			Console.WriteLine (Text);
		}
		/// <summary>
		/// Подставить аргументы
		/// </summary>
		/// <param name="args">Список аргументов</param>
		public PermutationDictionary Substitution(List<YJMElement> args)
		{
			var res = new PermutationDictionary ();
			foreach (var term in terms)
			{
				var tmp = args[term[term.Count - 1] - 1];
				//for (int i = 1; i < term.Count; i++)
				for (int i = term.Count - 2; i >= 0; i--)
				{
					tmp = tmp * args [term [i] - 1];
				}
				res.Add (tmp.Permutations);
				//res.AddRange (tmp);
			}
			return res;
		}
	}
}

