﻿//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;

using LinqToDB;
using LinqToDB.Mapping;

namespace LinqToDB.Tutorial.Models
{
	/// <summary>
	/// Database       : central
	/// Data Source    : central
	/// Server Version : 3.19.3
	/// </summary>
	public partial class TutorialDataConnection : LinqToDB.Data.DataConnection
	{
		public ITable<BonusTransaction> BonusTransactions { get { return this.GetTable<BonusTransaction>(); } }
		public ITable<Customer>         Customers         { get { return this.GetTable<Customer>(); } }

		public TutorialDataConnection()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public TutorialDataConnection(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table("BonusTransaction")]
	public partial class BonusTransaction
	{
		[Column, NotNull] public DateTime Time       { get; set; } // datetime
		[Column, NotNull] public long     CustomerId { get; set; } // integer
		[Column, NotNull] public decimal  Amount     { get; set; } // decimal

		#region Associations

		/// <summary>
		/// FK_BonusTransaction_0_0
		/// </summary>
		[Association(ThisKey="CustomerId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="FK_BonusTransaction_0_0", BackReferenceName="BonusTransactions")]
		public Customer Customer { get; set; }

		#endregion
	}

	[Table("Customer")]
	public partial class Customer : IId
	{
		[PrimaryKey,                Identity   ] public long     Id               { get; set; } // integer
		[Column,                    NotNull    ] public string   FullName         { get; set; } // varchar(50)
		[Column,                    NotNull    ] public string   Phone            { get; set; } // varchar(15)
		[Column(SkipOnInsert=true), NotNull    ] public DateTime RegistrationTime { get; set; } // datetime
		[Column,                       Nullable] public long?    Number           { get; set; } // integer
		[Column,                       Nullable] public decimal? BonusAmount      { get; set; } // decimal

		#region Associations

		/// <summary>
		/// FK_BonusTransaction_0_0_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="CustomerId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<BonusTransaction> BonusTransactions { get; set; }

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Customer Find(this ITable<Customer> table, long Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}
	}
}

#pragma warning restore 1591
