using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrsServer.Models {
	public class PurchaseRequest {
		public int Id { get; set; }
		public int UserId { get; set; }
		[Required]
		[StringLength(80)]
		public string Description { get; set; }
		[Required]
		[StringLength(255)]
		public string Justification { get; set; }
		[StringLength(80)]
		public string RejectionReason { get; set; }
		[Required]
		[StringLength(30)]
		public string DeliveryMode { get; set; }
		[Required]
		[StringLength(15)]
		public string Status { get; set; }
		public decimal Total { get; set; } = 0;
		public bool Active { get; set; } = true;

		public virtual List<PurchaseRequestLineitem> PurchaseRequestLineitems { get; set; }

		public virtual User User { get; set; }

		public PurchaseRequest() { }
	}
}