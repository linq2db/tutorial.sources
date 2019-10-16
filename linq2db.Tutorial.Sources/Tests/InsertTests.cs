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
	}
}
