using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models
{
	public class Customer
	{
		public Customer()
		{
			Uid = Guid.NewGuid();
		}

		public Guid Uid { get; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string Address { get; set; }

		public string FullName
		{
			get { return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName); }
		}
	}
}
