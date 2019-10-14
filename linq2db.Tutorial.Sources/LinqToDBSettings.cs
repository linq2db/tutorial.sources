using LinqToDB.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToDB.Tutorial
{
	class LinqToDBSettings : ILinqToDBSettings
	{
		public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

		public string DefaultConfiguration => "*";

		public string DefaultDataProvider => ProviderName.SQLiteClassic;

		public IEnumerable<IConnectionStringSettings> ConnectionStrings
		{
			get
			{
				var path = System.IO.Path.GetFullPath(@"..\..\..\..\DB\database.sqlite");

				yield return new ConnectionStringSettings()
				{
					ProviderName = DefaultDataProvider,
					Name = DefaultConfiguration,
					ConnectionString = $"Data Source={path};"
				};
			}
		}
	}
}
