using LinqToDB.Tutorial.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqToDB.Tutorial.Tests
{
	public class InsertTests : TestBase
	{
		[Test]
		public void InsertTest()
		{
			using (var db = new TutorialDataConnection())
			{
				var customer = new Customer()
				{
					FullName = GenerateName(),
					Phone = GeneratePhone(),
					RegistrationTime = DateTime.Now
				};

				var res = db.Insert(customer);
				Assert.AreNotEqual(0, res);
			}
		}

		[Test]
		public async Task InsertTestAsync()
		{
			using (var db = new TutorialDataConnection())
			{
				var customer = new Customer()
				{
					FullName = GenerateName(),
					Phone = GeneratePhone(),
					RegistrationTime = DateTime.Now
				};

				var res = await db.InsertAsync(customer);
				Assert.AreNotEqual(0, res);
			}
		}

		[Test]
		public void InsertWithInt64IdentityTest()
		{
			using (var db = new TutorialDataConnection())
			{
				var customer1 = new Customer()
				{
					FullName         = GenerateName(),
					Phone            = GeneratePhone(),
					RegistrationTime = DateTime.Now
				};

				var id1 = db.InsertWithInt64Identity(customer1);
				Assert.AreNotEqual(0, id1);

				var customer2 = new Customer()
				{
					FullName         = GenerateName(),
					Phone            = GeneratePhone(),
					RegistrationTime = DateTime.Now
				};

				var id2 = db.InsertWithInt64Identity(customer2);
				Assert.AreNotEqual(0, id2);

				Assert.AreEqual(id1 + 1, id2);
			}
		}

		[Test]
		public void InsertValuesTest()
		{
			using (var db = new TutorialDataConnection())
			{
				var res = db.Customers
					.Value(_ => _.FullName,    GenerateName())
					.Value(_ => _.Phone, () => GeneratePhone())
					.InsertWithInt64Identity();

				Assert.AreNotEqual(0, res);
			}
		}

		[Test]
		public void InsertValuesExpressionTest()
		{
			using (var db = new TutorialDataConnection())
			{
				var qry = db.Customers
					.Value(_ => _.FullName,    GenerateName())
					.Value(_ => _.Phone, () => GeneratePhone());

				qry = qry
					.Value(_ => _.RegistrationTime, () => Sql.CurrentTimestamp);

				var res = qry.InsertWithInt64Identity();

				Assert.AreNotEqual(0, res);
			}
		}

		public static DateTime?[] RegistrationTimes = { null as DateTime?, DateTime.Now };

		[Test]
		public void InsertValuesQueryCompositionTest([ValueSource(nameof(RegistrationTimes))] DateTime? registarionTime)
		{
			using (var db = new TutorialDataConnection())
			{
				var qry = db.Customers
					.Value(_ => _.FullName,    GenerateName())
					.Value(_ => _.Phone, () => GeneratePhone());

				if (registarionTime.HasValue)
					qry = qry
						.Value(_ => _.RegistrationTime, () => Sql.CurrentTimestamp);

				var res = qry.InsertWithInt64Identity();

				Assert.AreNotEqual(0, res);
			}
		}

		[Test]
		public void InsertNewTest()
		{
			using (var db = new TutorialDataConnection())
			{
				var res = db.Customers
					.InsertWithInt64Identity(() => new Customer()
					{
						FullName         = GenerateName(),
						Phone            = GeneratePhone(),
						RegistrationTime = Sql.CurrentTimestamp
					});

				Assert.AreNotEqual(0, res);
			}
		}
	}
}
