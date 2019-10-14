using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LinqToDB.Tutorial.Tests
{
	public class TestBase
	{
		/// <summary>
		/// Осуществляет настройку конфигурации по умолчанию
		/// </summary>
		static TestBase()
		{
			var path = System.IO.Path.GetFullPath(@"..\..\..\..\DB\database.sqlite");

			// Зададим конфигурацию
			DataConnection.AddOrSetConfiguration("*", $"Data Source={path};", ProviderName.SQLiteClassic);

			// Зададим конфигурацию по умолчанию
			DataConnection.DefaultConfiguration = "*";

			DataConnection.TurnTraceSwitchOn(TraceLevel.Info);
			DataConnection.WriteTraceLine = (s1, s2) => Console.WriteLine($"{s1}: {s2}");
		}
	}
}
