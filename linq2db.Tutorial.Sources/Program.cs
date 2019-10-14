using LinqToDB.Data;
using LinqToDB.Tutorial.Models;
using System;
using System.Linq;

namespace LinqToDB.Tutorial
{
	class Program
	{
		static void Main(string[] args)
		{
			// Зададим конфигурацию
			DataConnection.DefaultSettings = new LinqToDBSettings();

			// Создадим соединения
			using (var db = new DataConnection())
			{
				// Создадим объект для выполнения запроса
				IQueryable<Customer> customersTable = db.GetTable<Customer>();

				// Выполним запрос
				Customer[] customers = customersTable.ToArray();

				// Выведем результаты запроса
				foreach (var c in customers)
					Console.WriteLine($"{c.FullName}: {c.Phone}");
			}

			Console.ReadKey();
		}
	}
}
