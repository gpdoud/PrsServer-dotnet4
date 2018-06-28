using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrsServer.Models {
	public class Vendor {
		public int Id { get; set; }
		[Index("IDX_VendorCode", IsUnique = true)]
		[Required]
		[StringLength(20)]
		public string Code { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		[Required]
		[StringLength(50)]
		public string Address { get; set; }
		[Required]
		[StringLength(50)]
		public string City { get; set; }
		[Required]
		[StringLength(2)]
		public string State { get; set; }
		[Required]
		[StringLength(10)]
		public string Zip { get; set; }
		[StringLength(12)]
		public string Phone { get; set; }
		[StringLength(250)]
		public string Email { get; set; }
		public bool IsPreApproved { get; set; } = false;
		public bool Active { get; set; } = true;

		public Vendor() { }
	}
}