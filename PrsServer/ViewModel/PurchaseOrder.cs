using PrsServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrsServer.ViewModel {
	public class PurchaseOrder {
		public Vendor Vendor { get; set; }
		public User User { get; set; }
		public List<PurchaseRequestLineitem> PurchaseRequestLineitems { get; set; }
		public decimal Subtotal { get; set; } = 0;
		public decimal Tax { get; set; } = 0;
		public decimal Shipping { get; set; } = 0;
		public decimal Total { get; set; } = 0;

		public PurchaseOrder() { }
	}
}