using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinqToDB.Tutorial.Tests
{
	public class ToolsTests : TestBase
	{
		[Test]
		public void GenerateTest()
		{
			var name = GenerateName();
			var phone = GeneratePhone();

			Console.WriteLine($"{name}: {phone}");

			Assert.IsNotEmpty(name);
			Assert.IsNotEmpty(phone);
		}
	}
}
