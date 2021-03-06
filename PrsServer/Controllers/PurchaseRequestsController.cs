﻿using PrsServer.Models;
using PrsServer.Utility;
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
	public class PurchaseRequestsController : ApiController
    {
		private PrsDbContext db = new PrsDbContext();

		[HttpGet]
		public JsonResponse List() {
			return new JsonResponse { Data = db.PurchaseRequests.ToList() };
		}
		[HttpGet]
		public JsonResponse Get(int? id) {
			if (id == null)
				return new JsonResponse { Code = -1, Message = "id cannot be null" };
			var purchaseRequest = db.PurchaseRequests.Find(id);
			if (purchaseRequest == null)
				return new JsonResponse { Code = -100, Message = $"id {id} not found" };
			return new JsonResponse { Data = purchaseRequest };
		}
		[HttpPost]
		public JsonResponse Create(PurchaseRequest purchaseRequest) {
			if (purchaseRequest == null)
				return new JsonResponse { Code = -100, Message = $"purchaseRequest cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -200, Message = $"ModelState is invalid", Error = ModelState };
			db.PurchaseRequests.Add(purchaseRequest);
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "PurchaseRequest create successful!", Data = purchaseRequest };
		}
		[HttpPost]
		public JsonResponse Change(PurchaseRequest purchaseRequest) {
			purchaseRequest.User = null;
			purchaseRequest.PurchaseRequestLineitems = null;
			if (purchaseRequest == null)
				return new JsonResponse { Code = -100, Message = $"purchaseRequest cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -200, Message = $"ModelState is invalid", Error = ModelState };
			db.PurchaseRequests.Attach(purchaseRequest);
			db.Entry(purchaseRequest).State = System.Data.Entity.EntityState.Modified;
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "PurchaseRequest change successful!", Data = purchaseRequest };
		}
		[HttpPost]
		public JsonResponse Remove(PurchaseRequest purchaseRequest) {
			if (purchaseRequest == null)
				return new JsonResponse { Code = -100, Message = $"purchaseRequest cannot be null" };
			db.PurchaseRequests.Attach(purchaseRequest);
			db.Entry(purchaseRequest).State = System.Data.Entity.EntityState.Deleted;
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "PurchaseRequest remove successful!", Data = purchaseRequest };
		}
	}
}
