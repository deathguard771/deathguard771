using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diploma_project
{
	/// <summary>
	/// Класс, описывающий перестановку
	/// </summary>
	public class Permutation
	{
		public static int GetPermutationsCount(Permutation p)
		{
			var dict = new Dictionary<int, int> ();
			/*for (int i = 1; i <= p.Order; i++)
			{
				dict.Add (i, 0);
			}*/
			foreach (var cycle in p.cycles)
			{
				if (!dict.ContainsKey (cycle.Length))
				{
					dict.Add (cycle.Length, 0);
				}
				dict [cycle.Length]++;
			}
			var nnn = 1;
			foreach (var kvp in dict)
			{
				nnn *= (int)Math.Pow (kvp.Key, kvp.Value) * Factorial.Get(kvp.Value);
			}
			return Factorial.Get(p.Order) / nnn;
		}
		/// <summary>
		/// Компаратор для сортировки циклов перестановки 
		/// </summary>
		private static Comparison<Cycle> PermutationComparer = new Comparison<Cycle> ((x, y) =>
		{
			if (x.First == y.First)
			{
				return 0;
			}
			else
			{
				return x.First < y.First ? -1 : 1;
			}
		});
		/// <summary>
		/// "Сравнение двух перестановок"
		/// </summary>
		/// <param name="p1">P1.</param>
		/// <param name="p2">P2.</param>
		public static bool Compare(Permutation p1, Permutation p2)
		{
			if (p1.NotTrivialCycles.Count != p2.NotTrivialCycles.Count)
			{
				return false;
			}
			for (int i = 0; i < p1.NotTrivialCycles.Count; i++)
			{
				//if (p1.NotTrivialCycles [i].Length != p2.NotTrivialCycles [i].Length)
				if (p2.NotTrivialCycles.Where(c => c.Length == p1.NotTrivialCycles[i].Length).Count() == 0)
				{
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// Порядок перестановки
		/// </summary>
		private int order;
		/// <summary>
		/// Циклы
		/// </summary>
		public List<Cycle> cycles;
		/// <summary>
		/// Gets the not trivial cycles.
		/// </summary>
		/// <value>The not trivial cycles.</value>
		public List<Cycle> NotTrivialCycles
		{
			get
			{
				return this.cycles.Where (c => c.Elements.Length > 1).ToList ();
			}
		}
		/// <summary>
		/// Порядок перестановки
		/// </summary>
		public int Order
		{
			get { return order; }
		}
		/// <summary>
		/// Текствое представление
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get
			{
				if (NotTrivialCycles.Count > 0)
				{
					return string.Join ("", NotTrivialCycles.Select (cycle => cycle.Text));
				}
				else
				{
					return string.Join ("", cycles.Select (cycle => cycle.Text));
				}
			}
		}
		/// <summary>
		/// Инициализирует экземпляр класса <see cref="diploma_project.Permutation"/>
		/// </summary>
		public Permutation (int o)
		{
			order = o;
			cycles = new List<Cycle> ();
		}
		/// <summary>
		/// Инициализирует экземпляр класса <see cref="diploma_project.Permutation"/>
		/// </summary>
		/// <param name="_cycles">Коллекция циклов</param>
		public Permutation (int o, IEnumerable<Cycle> _cycles)
		{
			order = o;
			InitPermuatation(_cycles);
		}
		/// <summary>
		/// Инициализирует экземпляр класса <see cref="diploma_project.Permutation"/>
		/// </summary>
		/// <param name="_cycles">Циклы</param>
		public Permutation (int o, params Cycle[] _cycles)
		{
			order = o;
			InitPermuatation(_cycles.ToList());
		}
		/// <summary>
		/// Инициализирует экземпляр класса <see cref="diploma_project.Permutation"/>
		/// </summary>
		/// <param name="_cycles">Массивы элементов циклов</param>
		public Permutation(int o, params int[][] cycles)
		{
			order = o;
			var lsCycles = new List<Cycle> ();
			foreach (var cycle in cycles)
			{
				lsCycles.Add (new Cycle (cycle));
			}
			InitPermuatation(lsCycles);
		}
		/// <summary>
		/// Инициализирует перестановку
		/// </summary>
		/// <param name="_cycles">Коллекция циклов</param>
		private void InitPermuatation(IEnumerable<Cycle> _cycles)
		{
			cycles = new List<Cycle> (_cycles);
			Normalize ();
		}
		/// <summary>
		/// Приводит перестановку к стандартному виду
		/// </summary>
		private void Normalize()
		{
			for (int i = 1; i <= order; i++)
			{			
				if (cycles.Count (c => c.Contains (i)) == 0)
				{
					cycles.Add (new Cycle (new [] { i }));
				}
			}
			cycles.Sort (PermutationComparer);
		}
		/// <summary>
		/// Добавляет в перестановку цикл
		/// </summary>
		/// <param name="newCycle">Новый цикл</param>
		private void AddCycle(Cycle newCycle, bool needNormalize = false)
		{
			cycles.Add (newCycle);
			if (needNormalize)
			{
				Normalize ();
			}
		}
		/// <summary>
		/// Добавляет в перестановку цикл
		/// </summary>
		/// <param name="newCycle">Новый цикл</param>
		private void AddCycle(int[] newCycle, bool needNormalize = false)
		{
			cycles.Add (new Cycle(newCycle));
			if (needNormalize)
			{
				Normalize ();
			}
		}
		/// <summary>
		/// Применяет перестановку к заданному числу
		/// </summary>
		/// <param name="number">Число</param>
		public int Apply(int number)
		{
			var res = number;
			foreach (var cycle in cycles)
			{
				if (cycle.Contains (number))
				{
					res = cycle.Apply (number);
					break;
				}
			}
			return res;
		}
		/// <summary>
		/// Устанавливает порядок перестановки
		/// </summary>
		/// <param name="newOrder">Новый порядок</param>
		public void SetOrder(int newOrder)
		{
			if (newOrder > order)
			{
				order = newOrder;
				for (int i = order + 1; i <= newOrder; i++)
				{
					AddCycle (new [] { i });
				}
				Normalize ();
			}
		}
		/// <summary>
		/// Печатает перестановку
		/// </summary>
		public void Print()
		{
			Console.WriteLine (Text);
		}
		/// <summary>
		/// Определяет равен ли объект <see cref="System.Object"/> данному объекту <see cref="diploma_project.Permutation"/>.
		/// </summary>
		/// <param name="p1">Объект, который нужно сравнить</param>
		public override bool Equals(Object p1)
		{
			return p1 is Permutation ? this == p1 as Permutation : false;
		}
		/// <summary>
		/// Возвращает хэш-код объекта <see cref="diploma_project.Permutation"/>
		/// </summary>
		public override int GetHashCode()
		{
			return Text.GetHashCode ();
		}

		/// <param name="p1">Первая перестановка</param>
		/// <param name="p2">Вторая перестановка</param>
		public static Permutation operator *(Permutation p1, Permutation p2)
		{
			if (p1.order != p2.order)
			{
				if (p1.order > p2.order)
				{
					p2.SetOrder (p1.order);
				}
				else if (p1.order < p2.order)
				{
					p1.SetOrder (p2.order);
				}
				//throw new ArgumentException ("Порядок перестановок не совпадает");
			}
			#region parallel
			/*var cls = new List<Cycle> ();
			Parallel.ForEach (p1.cycles, cycle => 
			{
				var ls = new List<int> ();
				foreach (var elem in cycle.Elements)
				{
					if (cls.Count(c => c.Contains (elem)) == 0)
					{
						ls.Add (elem);
						break;
					}
				}
				var i = 0;
				while (i < ls.Count)
				{
					var f = p1.Apply(ls[i]);
					var s = p2.Apply(f);
					if (!ls.Contains(s))
					{
						ls.Add(s);
					}
					i++;
				}
				if (ls.Count > 0)
				{
					cls.Add(new Cycle(ls.ToArray ()));
				}
			});
			var result = new Permutation (p1.order, cls);*/
			#endregion
			var result = new Permutation (p1.order);
			foreach (var cycle in p1.cycles)
			{
				var ls = new List<int> ();
				foreach (var elem in cycle.Elements)
				{
					if (result.cycles.Count(c => c.Contains (elem)) == 0)
					{
						ls.Add (elem);
						break;
					}
				}
				var i = 0;
				while (i < ls.Count)
				{
					var f = p1.Apply(ls[i]);
					var s = p2.Apply(f);
					if (!ls.Contains(s))
					{
						ls.Add(s);
					}
					i++;
				}
				if (ls.Count > 0)
				{
					result.AddCycle (ls.ToArray ());
				}
			}
			result.Normalize ();
			return result;
		}
		/// <param name="p1">Первая перестановка</param>
		/// <param name="p2">Вторая перестановка</param>
		public static bool operator ==(Permutation p1, Permutation p2)
		{
			var result = true;
			if (p1.order == p2.order)
			{
				for (int i = 0; i < p1.cycles.Count; i++)
				{
					if (p1.cycles [i] != p2.cycles [i])
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
		/// <param name="p1">Первая перестановка</param>
		/// <param name="p2">Вторая перестановка</param>
		public static bool operator !=(Permutation p1, Permutation p2)
		{
			return !(p1 == p2);
		}
	}
}

