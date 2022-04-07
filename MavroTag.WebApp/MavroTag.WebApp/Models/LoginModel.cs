using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MavroTag.WebApp.Models
{
	public class LoginModel
	{
		public string Passphrase { get; set; }
		public string Error { get; set; }
		public string ReturnUrl { get; set; }
	}
}