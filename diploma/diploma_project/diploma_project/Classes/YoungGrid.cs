﻿using System;
using System.Collections.Generic;
using System.IO;
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
			//first variant
			for (int i = 1; i <= degree; i++)
			{
				var y = YJMElement.Generate (variablesCount + 1);
				var e = new ElementarySymmetricPolynomial (variablesCount, i);
				var pd = e.Substitution (y);
				pd.Split.Add (i);
				sigmas.Add (new List<PermutationDictionary> { pd });
			}
			//sigmas [0] [0].Print ();
			for (int i = 2; i <= degree; i++)
			{
				var ls = NumberSplits.GenerateSplits (i);
				foreach (var split in ls)
				{
					if (split.Count > 1)
					{
						var res = new PermutationDictionary ();
						foreach (var elem in split)
						{
							res = res * sigmas [elem - 1] [0];
						}
						res.Split.AddRange (split);
						//res.ClearEmptyEntries ();
						sigmas [i - 1].Add (res);
					}
				}
			}

			/*//second variant
			var ls = new List<PermutationDictionary> ();
			for (int i = 1; i <= degree; i++)
			{
				var y = YJMElement.Generate (variablesCount + 1);
				var e = new ElementarySymmetricPolynomial (variablesCount, i);
				ls.Add (e.Substitution (y));
				sigmas.Add (new List<PermutationDictionary>());
			}

			for (int i = 1; i <= degree; i++)
			{
				var splits = NumberSplits.GenerateSplits (i);
				foreach (var split in splits)
				{
					var res = new PermutationDictionary ();
					for (int j = 0; j < split.Count; j++)
					{
						for (int k = 0; k < split [j]; k++)
						{
							res = res * ls [j];
						}
					}
					res.ClearEmptyEntries ();
					sigmas [i - 1].Add (res);
				}
			}*/

			//var a = 0;

			foreach (var item in sigmas)
			{
				foreach (var item2 in item)
				{
					item2.Print2 (Output.File, "temp.txt", " = {" + string.Join(", ", item2.Split) + "}");
				}
			}
		}
	}
}
