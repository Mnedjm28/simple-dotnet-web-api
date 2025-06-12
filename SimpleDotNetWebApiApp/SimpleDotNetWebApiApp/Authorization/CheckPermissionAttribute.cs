﻿using SimpleDotNetWebApiApp.Data;
using System.Security;

namespace SimpleDotNetWebApiApp.Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CheckPermissionAttribute : Attribute
    {
        public CheckPermissionAttribute(Permission permission)
        {
            Permission = permission;
        }
        public Permission Permission { get; set; }
    }
}
