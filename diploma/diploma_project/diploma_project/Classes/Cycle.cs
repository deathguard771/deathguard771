using System;
using System.Collections;
using System.Linq;

namespace diploma_project
{
	/// <summary>
	/// Класс, описывающий цикл перестанови
	/// </summary>
	public class Cycle
	{
		/// <summary>
		/// Сам цикл
		/// </summary>
		private int[] cycle;
		/// <summary>
		/// Длина цикла
		/// </summary>
		public int Length
		{
			get
			{
				return cycle.Length;
			}
		}
		/// <summary>
		/// Элементы цикла
		/// </summary>
		public int[] Elements
		{
			get
			{
				return cycle;
			}
		}
		/// <summary>
		/// Первый элемент цикла
		/// </summary>
		public int First
		{
			get
			{
				return cycle [0];
			}
		}
		/// <summary>
		/// Текстовое представление
		/// </summary>
		public string Text
		{
			get
			{
				return "(" + string.Join (",", cycle) + ")";
			}
		}
		/// <summary>
		/// Инициализирует экземпляр класса <see cref="diploma_project.Cycle"/>
		/// </summary>
		public Cycle ()
		{
			cycle = new int[0];
		}
		/// <summary>
		/// Инициализирует экземпляр класса <see cref="diploma_project.Cycle"/>
		/// </summary>
		/// <param name="newCycle">Массив элементов цикла</param>
		public Cycle (params int[] newCycle)
		{
			InitCycle (newCycle);
		}
		/// <summary>
		/// Конструктор копирования класса <see cref="diploma_project.Cycle"/>
		/// </summary>
		/// <param name="newCycle">Другой цикл</param>
		public Cycle (Cycle newCycle)
		{
			InitCycle (newCycle.Elements);
		}
		/// <summary>
		/// Инициализирует цикл массивом элементов
		/// </summary>
		/// <param name="newCycle">Массив элементов</param>
		private void InitCycle(int[] newCycle)
		{
			cycle = new int[newCycle.Length];
			var maxIndex = -1;
			var maxValue = -1;
			for (int i = 0; i < newCycle.Length; i++)
			{
				if (newCycle [i] > maxValue)
				{
					maxValue = newCycle [i];
					maxIndex = i;
				}
			}
			for (int i = maxIndex, j = 0; j < cycle.Length; i++, j++)
			{
				if (i == newCycle.Length)
				{
					i = 0;
				}
				cycle [j] = newCycle [i];
			}
		}
		/// <summary>
		/// Применяет цикл к элементу
		/// </summary>
		/// <returns>The cycle.</returns>
		/// <param name="number">Элемент</param>
		public int Apply(int number)
		{
			var res = number;
			for (int i = 0; i < cycle.Length; i++)
			{
				if (cycle [i] == number)
				{
					res = cycle [(i + 1) % cycle.Length];
					break;
				}
			}
			return res;
		}
		/// <summary>
		/// Содержит ли цикл заданный элемент
		/// </summary>
		/// <param name="number">Элемент</param>
		public bool Contains(int number)
		{
			return cycle.Contains (number);
		}
		/// <summary>
		/// Печатает цикл
		/// </summary>
		public void Print()
		{
			Console.WriteLine (Text);
		}
		/// <summary>
		/// Определяет равен ли объект <see cref="System.Object"/> данному объекту <see cref="diploma_project.Cycle"/>.
		/// </summary>
		/// <param name="p1">Объект, который нужно сравнить</param>
		public override bool Equals(Object c1)
		{
			return c1 is Cycle ? this == c1 as Cycle : false;
		}
		/// <summary>
		/// Возвращает хэш-код объекта <see cref="diploma_project.Cycle"/>
		/// </summary>
		public override int GetHashCode()
		{
			return Text.GetHashCode ();
		}

		/// <param name="c1">Цикл 1</param>
		/// <param name="c2">Цикл 2</param>
		public static bool operator ==(Cycle c1, Cycle c2)
		{
			var result = true;
			if (c1.Length == c2.Length)
			{
				for (int i = 0; i < c1.Length; i++)
				{
					if (c1.Elements [i] != c2.Elements [i])
					{
						result = false;
						break;
					}
				}
			}
			else
			{
				result =  false;
			}
			return result;
		}
		/// <param name="c1">Цикл 1</param>
		/// <param name="c2">Цикл 2</param>
		public static bool operator !=(Cycle c1, Cycle c2)
		{
			return !(c1 == c2);
		}
	}
}

