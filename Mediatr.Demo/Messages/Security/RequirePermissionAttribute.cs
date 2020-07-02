using System;

namespace Messages.Security
{
	public class RequirePermissionAttribute
		: Attribute
	{
		private readonly ApplicationPermission permission;

		public RequirePermissionAttribute(ApplicationPermission permission)
		{
			this.permission = permission;
		}

		public ApplicationPermission Permission
		{
			get { return this.permission; }
		}
	}
}
