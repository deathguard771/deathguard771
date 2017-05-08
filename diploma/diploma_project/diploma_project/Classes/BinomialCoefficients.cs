using System;

namespace diploma_project
{
	/// <summary>
	/// Класс для вычисления биномиальных коэффициентов
	/// </summary>
	public static class BinomialCoefficients
	{
		/// <summary>
		/// Вычисляет биномиальный коэффициент
		/// </summary></returns>
		/// <param name="n">N.</param>
		/// <param name="k">K.</param>
		public static int GetBinomialCoefficient(int n, int k)
		{
			double res = 1;
			for (int i = 1; i <= k; ++i)
			{
				res = res * (n - k + i) / i;
			}
			return (int)(res + 0.01);
		}
	}
}

