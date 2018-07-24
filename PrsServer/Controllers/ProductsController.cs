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
	public class ProductsController : ApiController
    {
		private PrsDbContext db = new PrsDbContext();

		[HttpGet]
		public JsonResponse List() {
			return new JsonResponse { Data = db.Products.ToList() };
		}
		[HttpGet]
		public JsonResponse Get(int? id) {
			if (id == null)
				return new JsonResponse { Code = -1, Message = "id cannot be null" };
			var product = db.Products.Find(id);
			if (product == null)
				return new JsonResponse { Code = -100, Message = $"id {id} not found" };
			return new JsonResponse { Data = product };
		}
		[HttpPost]
		public JsonResponse Create(Product product) {
			if (product == null)
				return new JsonResponse { Code = -100, Message = $"product cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -200, Message = $"ModelState is invalid", Error = ModelState };
			db.Products.Add(product);
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "Product create successful!", Data = product };
		}
		[HttpPost]
		public JsonResponse Change(Product product) {
			product.Vendor = null;
			if (product == null)
				return new JsonResponse { Code = -100, Message = $"product cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -200, Message = $"ModelState is invalid", Error = ModelState };
			db.Products.Attach(product);
			db.Entry(product).State = System.Data.Entity.EntityState.Modified;
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "Product change successful!", Data = product };
		}
		[HttpPost]
		public JsonResponse Remove(Product product) {
			if (product == null)
				return new JsonResponse { Code = -100, Message = $"product cannot be null" };
			db.Products.Attach(product);
			db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "Product remove successful!", Data = product };
		}
	}
}
