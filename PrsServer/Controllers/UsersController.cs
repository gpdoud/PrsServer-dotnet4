using PrsServer.Models;
using PrsServer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PrsServer.Controllers {

	public class UsersController : ApiController {

		private PrsDbContext db = new PrsDbContext();

		[HttpGet]
		public JsonResponse Authenticate(string username, string password) {
			if (username == null || password == null)
				return new JsonResponse { Code = -2, Message = "Authentication failed" };
			var user = db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
			if (user == null)
				return new JsonResponse { Code = -2, Message = "Authentication failed" };
			return new JsonResponse { Data = user };
		}

		[HttpGet]
		public JsonResponse List() {
			return new JsonResponse { Data = db.Users.ToList() };
		}
		[HttpGet]
		public JsonResponse Get(int? id) {
			if (id == null)
				return new JsonResponse { Code = -1, Message = "id cannot be null" };
			var user = db.Users.Find(id);
			if (user == null)
				return new JsonResponse { Code = -100, Message = $"id {id} not found" };
			return new JsonResponse { Data = user };
		}
		[HttpPost]
		public JsonResponse Create(User user) {
			if (user == null)
				return new JsonResponse { Code = -100, Message = $"user cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -200, Message = $"ModelState is invalid", Error = ModelState };
			db.Users.Add(user);
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "User create successful!", Data = user };
		}
		[HttpPost]
		public JsonResponse Change(User user) {
			if (user == null)
				return new JsonResponse { Code = -100, Message = $"user cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -200, Message = $"ModelState is invalid", Error = ModelState };
			db.Users.Attach(user);
			db.Entry(user).State = System.Data.Entity.EntityState.Modified;
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "User change successful!", Data = user };
		}
		[HttpPost]
		public JsonResponse Remove(User user) {
			if (user == null)
				return new JsonResponse { Code = -100, Message = $"user cannot be null" };
			db.Users.Attach(user);
			db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
			var recsAffected = db.SaveChanges();
			return new JsonResponse { Message = "User remove successful!", Data = user };
		}
	}
}
