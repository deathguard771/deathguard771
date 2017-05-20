using System;
using System.Collections.Generic;
using System.Linq;

namespace diploma_project
{
	/// <summary>
	/// Решетка Юнга
	/// </summary>
	public class YoungGrid
	{
		/// <summary>
		/// Генерация
		/// </summary>
		/// <param name="degree">Степень</param>
		/// <param name="variablesCount">Количество переменных</param>
		public static void Generate(int degree, int variablesCount)
		{
			var sigmas = new List<List<PermutationDictionary>> ();
			for (int i = 1; i <= degree; i++)
			{
				var ls = NumberSplits.GenerateSplits (i);
				var y = YJMElement.Generate (variablesCount + 1);
				var e = new ElementarySymmetricPolynomial (variablesCount, i);
				sigmas.Add (new List<PermutationDictionary> { e.Substitution (y) });
			}
			var a = 0;
		}
	}
}

