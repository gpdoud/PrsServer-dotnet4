using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrsServer.Utility {

	public class JsonResponse {

		public static JsonResponse Ok = new JsonResponse();

		public int Code { get; set; } = 0;
		public string Message { get; set; } = "Success";
		public object Data { get; set; }
		public object Error { get; set; }

		public JsonResponse() { }
	}
}