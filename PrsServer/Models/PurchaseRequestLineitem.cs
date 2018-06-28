using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrsServer.Models {
	public class PurchaseRequestLineitem {
		public int Id { get; set; }
		public int PurchaseRequestId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }

		[JsonIgnore]
		public virtual PurchaseRequest PurchaseRequest { get; set; }
		public virtual Product Product { get; set; }

		public PurchaseRequestLineitem() { }
	}
}