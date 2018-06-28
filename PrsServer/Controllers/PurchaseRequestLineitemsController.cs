using PrsServer.Models;
using PrsServer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PrsServer.Controllers
{
    public class PurchaseRequestLineitemsController : ApiController
    {
		private PrsDbContext db = new PrsDbContext();

		[HttpGet]
		public JsonResponse List() {
			return new JsonResponse { Data = db.PurchaseRequestLineitems.ToList() };
		}
		[HttpGet]
		public JsonResponse Get(int? id) {
			if (id == null)
				return new JsonResponse { Code = -1, Message = "id cannot be null" };
			var purchaseRequestLineitem = db.PurchaseRequestLineitems.Find(id);
			if (purchaseRequestLineitem == null)
				return new JsonResponse { Code = -100, Message = $"id {id} not found" };
			return new JsonResponse { Data = purchaseRequestLineitem };
		}
		[HttpPost]
		public JsonResponse Create(PurchaseRequestLineitem purchaseRequestLineitem) {
			if (purchaseRequestLineitem == null)
				return new JsonResponse { Code = -100, Message = $"purchaseRequestLineitem cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -200, Message = $"ModelState is invalid", Error = ModelState };
			db.PurchaseRequestLineitems.Add(purchaseRequestLineitem);
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "PurchaseRequestLineitem create successful!", Data = purchaseRequestLineitem };
		}
		[HttpPost]
		public JsonResponse Change(PurchaseRequestLineitem purchaseRequestLineitem) {
			if (purchaseRequestLineitem == null)
				return new JsonResponse { Code = -100, Message = $"purchaseRequestLineitem cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -200, Message = $"ModelState is invalid", Error = ModelState };
			db.PurchaseRequestLineitems.Attach(purchaseRequestLineitem);
			db.Entry(purchaseRequestLineitem).State = System.Data.Entity.EntityState.Modified;
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "PurchaseRequestLineitem change successful!", Data = purchaseRequestLineitem };
		}
		[HttpPost]
		public JsonResponse Remove(PurchaseRequestLineitem purchaseRequestLineitem) {
			if (purchaseRequestLineitem == null)
				return new JsonResponse { Code = -100, Message = $"purchaseRequestLineitem cannot be null" };
			db.PurchaseRequestLineitems.Attach(purchaseRequestLineitem);
			db.Entry(purchaseRequestLineitem).State = System.Data.Entity.EntityState.Deleted;
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "PurchaseRequestLineitem remove successful!", Data = purchaseRequestLineitem };
		}
	}
}
