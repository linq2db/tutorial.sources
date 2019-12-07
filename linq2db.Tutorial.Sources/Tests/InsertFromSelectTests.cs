using LinqToDB.Tutorial.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LinqToDB.Mapping;

namespace LinqToDB.Tutorial.Tests
{
	[TestFixture]
	public class InsertFromSelectTests: TestBase
	{
		public class TmpCustomer
		{
			[Column(Length = 50)]
			public string TmpFullName { get; set; }

			[Column(Length = 15)]
			public string TmpPhone { get; set; }

			public DateTime TmpRegistrationTime { get; set; }

			public long? TmpNumber { get; set; }
		}

		[Test]
		public void InsertIntoTest()
		{
			using (var db = new TutorialDataConnection())
			{
				var tmpTable = db.CreateTempTable<TmpCustomer>();

				var uniqueQry = from c in db.Customers
								group c by c.Phone into grouped
								select new
								{
									Phone = grouped.Key,
									Number = grouped.Min(_ => _.Number),
									RegistrationTime = grouped.Min(_ => _.RegistrationTime),
									FullName = grouped.Min(_ => _.FullName)
								};

				uniqueQry
					.Into(tmpTable)
						.Value(_ => _.TmpFullName, _ => _.FullName)
						.Value(_ => _.TmpNumber, _ => _.Number)
						.Value(_ => _.TmpRegistrationTime, _ => _.RegistrationTime)
						.Value(_ => _.TmpPhone, _ => _.Phone)
					.Insert();

				db.Customers.Delete();

				tmpTable
					.Insert(db.Customers, _ => new Customer()
					{
						FullName = _.TmpFullName,
						Number = _.TmpNumber,
						Phone = _.TmpPhone,
						RegistrationTime = _.TmpRegistrationTime
					});

				tmpTable.Drop();
			}
		}
	}
}
