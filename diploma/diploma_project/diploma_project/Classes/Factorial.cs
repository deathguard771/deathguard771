using System;

namespace diploma_project
{
	public static class Factorial
	{
		public static int Get(int num)
		{
			var res = 1;
			for (int i = 2; i <= num; i++)
			{
				res *= i;
			}
			return res;
		}
	}
}

