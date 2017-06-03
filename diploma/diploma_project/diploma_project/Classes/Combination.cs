using System;
using System.Collections.Generic;

namespace diploma_project
{
	/// <summary>
	/// Класс для полчения неупорядоченных выборок множества
	/// </summary>
	public class Combination
	{
		/// <summary>
		/// Множество
		/// </summary>
		private List<int> setList = new List<int>();
		/// <summary>
		/// Все уже полученные выборки
		/// </summary>
		private List<List<int>> сombinations = new List<List<int>> ();
		/// <summary>
		/// Количество элементов множства
		/// </summary>
		private int setLength;
		/// <summary>
		/// Количество элементов выборки
		/// </summary>
		private int combinatonLength;
		/// <summary>
		/// Выборка
		/// </summary>
		public List<int> CurrentCombination
		{
			get
			{
				var res = new List<int> ();
				for (int i = 0; i < combinatonLength; i++)
				{
					res.Add (setList [i]);
				}
				return res;
			}
		}
		/// <summary>
		/// Все уже полученные выборки
		/// </summary>
		public List<List<int>> Combinations
		{
			get { return сombinations; }
		}
		/// <summary>
		/// Текущий номер выборки
		/// </summary>
		public int CurrentNumber
		{
			get { return сombinations.Count; }
		}
		/// <summary>
		/// Текстовое представление
		/// </summary>
		public string Text
		{
			get { return string.Join (" ", CurrentCombination); }
		}
		/// <summary>
		/// Инициализирует экземпляр класса <see cref="Combination"/>
		/// </summary>
		/// <param name="_setLength">Количество элементов множества</param>
		/// <param name="_combinationLength">Количетсов элементов выборки</param>
		public Combination(int _setLength, int _combinationLength)
		{
			setLength = _setLength;
			combinatonLength = _combinationLength;
			for (int i = 0; i < setLength; i++)
			{
				setList.Add (i + 1);
			}
			сombinations.Add (CurrentCombination);
		}
		/// <summary>
		/// Вычисляет следующую выборку
		/// </summary>
		/// <returns><c>true</c>, если выборка получена, иначе - <c>false</c></returns>
		public bool GetNextCombination ()
		{
			for (int i = combinatonLength - 1; i >= 0; i--)
			{
				if (setList [i] < setLength - combinatonLength + i + 1)
				{
					setList [i]++;
					for (int j = i + 1; j < combinatonLength; j++)
					{
						setList [j] = setList [j - 1] + 1;
					}
					сombinations.Add (CurrentCombination);
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Печатает текущую выборку
		/// </summary>
		public void Print()
		{
			Console.WriteLine (CurrentNumber + ": " + Text);
		}
	}
}

