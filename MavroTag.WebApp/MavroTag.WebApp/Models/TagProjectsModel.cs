﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MavroTag.WebApp.Models
{
	public class TagProjectsModel
	{
		public List<TagProjectModel> TagProjects { get; set; }
		public List<TagProjectUserPermissionModel> TagProjectUserPermissions { get; set; }
	}
}