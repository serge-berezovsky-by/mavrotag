using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MavroTag.WebApp.Models
{
	public class TagProjectUserPermissionsModel
	{
		public long TagProjectId { get; set; }
		public string TagProjectName { get; set; }
		public List<TagProjectUserPermissionModel> TagProjectUserPermissions { get; set; }
    }
}