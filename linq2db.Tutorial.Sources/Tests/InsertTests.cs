using LinqToDB.Tutorial.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqToDB.Tutorial.Tests
{
	public class InsertTests: TestBase
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
				var customer = new Customer()
				{
					FullName = GenerateName(),
					Phone = GeneratePhone(),
					RegistrationTime = DateTime.Now
				};

				var id1 = db.InsertWithInt64Identity(customer);
				Assert.AreNotEqual(0, id1);

				var id2 = db.InsertWithInt64Identity(customer);
				Assert.AreNotEqual(0, id2);

				Assert.AreEqual(id1 + 1, id2);
			}
		}

	}
}
