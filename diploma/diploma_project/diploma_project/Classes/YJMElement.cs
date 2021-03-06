﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace diploma_project
{
	/// <summary>
	/// Класс, представляющий YJM элемент
	/// </summary>
	public class YJMElement
	{
		/// <summary>
		/// Порядок входящих в YJM-элемент перестановок
		/// </summary>
		private int order;
		/// <summary>
		/// Список перестановок
		/// </summary>
		private List<Permutation> permutations;
		/// <summary>
		/// Порядок перестановки
		/// </summary>
		public int Order
		{
			get { return order; }
		}
		/// <summary>
		/// Список перестановок
		/// </summary>
		public List<Permutation> Permutations
		{
			get { return permutations; }
		}
		/// <summary>
		/// Текствое представление
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get
			{
				return string.Format("S({0}) = {1}", order, string.Join("+", permutations.Select(p => p.Text)));
			}
		}
		/// <summary>
		/// Инициализирует экземпляр класса <see cref="YJMElement"/>
		/// </summary>
		/// <param name="ord">Порядок</param>
		public YJMElement(int ord)
		{
			order = ord;
			permutations = new List<Permutation>();
			for (int i = 1; i < ord; i++)
			{
				permutations.Add(new Permutation(ord, new int[] { i, ord }));
			}
		}
		/// <summary>
		/// Инициализирует экземпляр класса <see cref="YJMElement"/>
		/// </summary>
		/// <param name="perm">Список перестановок</param>
		public YJMElement(List<Permutation> perm)
		{
			permutations = new List<Permutation>();
			permutations.AddRange(perm);
		}
		/// <summary>
		/// Печатает YJM-элемент
		/// </summary>
		public void Print()
		{
			Console.WriteLine(Text);
		}
		/// <summary>
		/// Устанавливает порядок YJM-элемента
		/// </summary>
		/// <param name="newOrder">Новый порядок</param>
		public void SetOrder(int newOrder)
		{
			if (newOrder > order)
			{
				order = newOrder;
				foreach (var perm in permutations)
				{
					perm.SetOrder(newOrder);
				}
			}
		}
		/// <summary>
		/// Генерирует набор последовательных YJM-элементов
		/// </summary>
		/// <param name="order">Порядок группы</param>
		public static List<YJMElement> Generate(int order)
		{
			var result = new List<YJMElement>();
			for (int i = 2; i <= order; i++)
			{
				result.Add(new YJMElement(i));
			}
			return result;
		}
		/// <summary>
		/// Печатает набор YJM-элементов
		/// </summary>
		/// <param name="elements">Набор YJM-элементов</param>
		public static void Print(List<YJMElement> elements)
		{
			foreach (var elem in elements)
			{
				elem.Print();
			}
		}
		/// <summary>
		/// Определяет равен ли объект <see cref="object"/> данному объекту <see cref="YJMElement"/>.
		/// </summary>
		/// <param name="obj">Объект, который нужно сравнить</param>
		public override bool Equals(object obj)
		{
			return obj is YJMElement && this == obj as YJMElement;
		}
		/// <summary>
		/// Возвращает хэш-код объекта <see cref="YJMElement"/>
		/// </summary>
		public override int GetHashCode()
		{
			return permutations.GetHashCode();
		}

		/// <param name="y1">Первый YJM-элемент</param>
		/// <param name="y2">Второй YJM-элемент</param>
		public static YJMElement operator *(YJMElement y1, YJMElement y2)
		{
			if (y1.order > y2.order)
			{
				y2.SetOrder(y1.order);
			}
			else if (y2.order > y1.order)
			{
				y1.SetOrder(y2.order);
			}
			var result = new List<Permutation>();
			foreach (var perm1 in y1.permutations)
			{
				foreach (var perm2 in y2.permutations)
				{
					result.Add(perm1 * perm2);
				}
			}
			return new YJMElement(result);
		}
		/// <param name="y1">Первый YJM-элемент</param>
		/// <param name="y2">Второй YJM-элемент</param>
		public static bool operator ==(YJMElement y1, YJMElement y2)
		{
			if (y1.order == y2.order)
			{
				for (int i = 0; i < y1.order; i++)
				{
					if (y1.permutations[i] != y2.permutations[i])
					{
						return false;
					}
				}
			}
			else
			{
				return false;
			}
			return true;
		}
		/// <param name="y1">Первый YJM-элемент</param>
		/// <param name="y2">Второй YJM-элемент</param>
		public static bool operator !=(YJMElement y1, YJMElement y2)
		{
			return !(y1 == y2);
		}
	}
}

