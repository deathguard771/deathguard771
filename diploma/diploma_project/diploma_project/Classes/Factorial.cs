using System.Collections.Generic;

namespace diploma_project
{
	/// <summary>
	/// Класс для вычисления факториалов
	/// </summary>
	public static class Factorial
	{
		/// <summary>
		/// Вычисленные факториалы
		/// </summary>
		private static List<int> factorials = new List<int> { 1, 1 };
		/// <summary>
		/// Вычислить факториал числа
		/// </summary>
		/// <param name="num">Число</param>
		public static int Get(int num)
		{
			if (num >= factorials.Count)
			{
				for (int i = factorials.Count; i <= num; i++)
				{
					factorials.Add(factorials[i - 1] * i);
				}
			}
			return factorials[num];
		}
	}
}

