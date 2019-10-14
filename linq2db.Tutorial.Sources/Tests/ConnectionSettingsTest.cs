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
	public class ConnectionSettingsTest: TestBase
	{
		[Test]
		public void FireTest()
		{
			using(var db = new TutorialDataConnection("*"))
			{
				Assert.IsNotEmpty(db.Customers.ToArray());
			}
		}
	}
}
