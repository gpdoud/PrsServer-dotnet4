using PrsServer.Models;
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
	public class VendorsController : ApiController
    {
		private PrsDbContext db = new PrsDbContext();

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
