using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrsServer.Models {
	public class Product {
		public int Id { get; set; }
		public int VendorId { get; set; }
		[Required]
		[StringLength(50)]
		[Index("IDX_ProductCode", IsUnique = true)]
		public string PartNumber { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		public decimal Price { get; set; }
		[Required]
		[StringLength(50)]
		public string Unit { get; set; } = "EACH";
		[StringLength(255)]
		public string PhotoPath { get; set; }
		public bool Active { get; set; } = true;

		public virtual Vendor Vendor { get; set; }

		public Product() { }
	}
}