using LinqToDB.Data;
using LinqToDB.Tutorial.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToDB.Tutorial.Tests
{
	[TestFixture]
	public class BulkCopyTests: TestBase
	{
		[Test]
		public void BulkCopyTest()
		{
			var data = new DataContext("low")
				.GetTable<Customer>()
				.Select(_ => new Customer()
				{
					Id               = _.Id,
					FullName         = _.FullName,
					Phone            = _.Phone,
					RegistrationTime = _.RegistrationTime,
					Number           = _.Id
				})
				.ToArray();

			using (var db = new TutorialDataConnection())
			{
				db.BulkCopy(data);
			}
		}


	}
}
