using LinqToDB.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LinqToDB.Tutorial.Tests
{
	[TestFixture]
	public class TestBase
	{
		private static readonly string[] MaleSurnames;
		private static readonly string[] MaleNames;
		private static readonly string[] FemaleSurnames;
		private static readonly string[] FemaleNames;
		private static readonly Random Random = new Random((int)(DateTime.Now.Ticks/1000));

		/// <summary>
		/// Осуществляет настройку конфигурации по умолчанию
		/// </summary>
		static TestBase()
		{
			var path1 = System.IO.Path.GetFullPath(@"..\..\..\..\DB\central.sqlite");
			var path2 = System.IO.Path.GetFullPath(@"..\..\..\..\DB\database.sqlite");

			// Зададим конфигурацию
			DataConnection.AddOrSetConfiguration("*",   $"Data Source={path1};", ProviderName.SQLiteClassic);
			DataConnection.AddOrSetConfiguration("low", $"Data Source={path2};", ProviderName.SQLiteClassic);

			// Зададим конфигурацию по умолчанию
			DataConnection.DefaultConfiguration = "*";

			DataConnection.TurnTraceSwitchOn(TraceLevel.Info);
			DataConnection.WriteTraceLine = (s1, s2) => Console.WriteLine($"{s1}: {s2}");

			var assembly = typeof(TestBase).Assembly;
			var names = assembly.GetManifestResourceNames();

			MaleSurnames = ReadStrings(assembly, GetFileName(names, "MaleSurname.txt")).ToArray();
			MaleNames = ReadStrings(assembly, GetFileName(names, "MaleName.txt")).ToArray();

			FemaleSurnames = ReadStrings(assembly, GetFileName(names, "FemaleSurname.txt")).ToArray();
			FemaleNames = ReadStrings(assembly, GetFileName(names, "FemaleName.txt")).ToArray();
		}

		private static string GetFileName(string[] names, string name)
		{
			var loc = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower();
			return names.FirstOrDefault(_ => _.EndsWith($"{loc}.{name}"))
				?? names.First(_ => _.EndsWith($"en.{name}"));
		}

		private static IEnumerable<string> ReadStrings(Assembly assembly, string resourceName)
		{
			using (var stream = assembly.GetManifestResourceStream(resourceName))
			using (var reader = new StreamReader(stream))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					yield return line;
				}
			}
		}

		protected static string GenerateName()
		{
			var m = Random.Next(1, 100);

			var surnames = m <= 50 ? MaleSurnames : FemaleSurnames;
			var names = m <= 50 ? MaleNames : FemaleNames;

			var surname = surnames[Random.Next(0, surnames.Length - 1)];
			var name = names[Random.Next(0, surnames.Length - 1)];

			return $"{name} {surname}";
		}

		protected static string GeneratePhone()
		{
			var p1 = Random.Next(100, 999);
			var p2 = Random.Next(0, 999); 
			var p3 = Random.Next(0, 99);
			var p4 = Random.Next(0, 99);

			return $"{Properties.Resources.PhonePrefix} {p1} {p2:000} {p3:00} {p4:00}";
		}
	}
}
