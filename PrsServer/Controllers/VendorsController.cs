using PrsServer.Models;
using PrsServer.Utility;
using PrsServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PrsServer.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class VendorsController : ApiController
    {
		private PrsDbContext db = new PrsDbContext();

		[HttpGet]
		[ActionName("PO")]
		public JsonResponse GeneratePO(int? id) {
			if (id == null)
				return new JsonResponse {
					Message = "id cannot be null."
				};
			var po = new PurchaseOrder();
			po.Vendor = db.Vendors.Find(id);
			var approvedPurchaseRequests = db.PurchaseRequests.Where(pr => pr.Status == "APPROVED").ToList();
			if (approvedPurchaseRequests.Count() == 0)
				return new JsonResponse {
					Message = "Vendor has no products on approved purchase requests."
				};
			var purchaseRequestLines = new List<PurchaseRequestLineitem>();
			foreach(var pr in approvedPurchaseRequests) {
				var approvedLines = pr.PurchaseRequestLineitems.Where(li => li.Product.VendorId == po.Vendor.Id).ToList();
				purchaseRequestLines.AddRange(approvedLines);
			}
			var summaryPurchaseRequestLines = new Dictionary<int, PurchaseRequestLineitem>();
			foreach(var prli in purchaseRequestLines) {
				if(!summaryPurchaseRequestLines.Keys.Contains(prli.ProductId)) {
					prli.Product.Price *= 0.7M; // Cost is 70% of price;
					summaryPurchaseRequestLines.Add(prli.ProductId, prli);
				} else {
					summaryPurchaseRequestLines[prli.ProductId].Quantity += prli.Quantity;
				}
			}
			po.PurchaseRequestLineitems = summaryPurchaseRequestLines.Values.ToList();
			po.Subtotal = po.PurchaseRequestLineitems.Sum(li => li.Product.Price * li.Quantity);
			po.Tax = po.Subtotal * 0.05M;
			po.Shipping = po.Subtotal * 0.1M;
			po.Total = po.Subtotal + po.Tax + po.Shipping;
			return new JsonResponse {
				Message = "Success!",
				Data = po
			};
		}

		[HttpGet]
		public JsonResponse List() {
			return new JsonResponse { Data = db.Vendors.ToList() };
		}
		[HttpGet]
		public JsonResponse Get(int? id) {
			if (id == null)
				return new JsonResponse { Code = -1, Message = "id cannot be null" };
			var vendor = db.Vendors.Find(id);
			if (vendor == null)
				return new JsonResponse { Code = -100, Message = $"id {id} not found" };
			return new JsonResponse { Data = vendor };
		}
		[HttpPost]
		public JsonResponse Create(Vendor vendor) {
			if (vendor == null)
				return new JsonResponse { Code = -100, Message = $"vendor cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -200, Message = $"ModelState is invalid", Error = ModelState };
			db.Vendors.Add(vendor);
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "Vendor create successful!", Data = vendor };
		}
		[HttpPost]
		public JsonResponse Change(Vendor vendor) {
			if (vendor == null)
				return new JsonResponse { Code = -100, Message = $"vendor cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -200, Message = $"ModelState is invalid", Error = ModelState };
			db.Vendors.Attach(vendor);
			db.Entry(vendor).State = System.Data.Entity.EntityState.Modified;
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "Vendor change successful!", Data = vendor };
		}
		[HttpPost]
		public JsonResponse Remove(Vendor vendor) {
			if (vendor == null)
				return new JsonResponse { Code = -100, Message = $"vendor cannot be null" };
			db.Vendors.Attach(vendor);
			db.Entry(vendor).State = System.Data.Entity.EntityState.Deleted;
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "Vendor remove successful!", Data = vendor };
		}
	}
}
