using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuthenticationSvc.IdentityClasses
{
	public partial class ApplicationUsers : IdentityUser<Guid>
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public override Guid Id
		{
			get { return base.Id; }
			set { base.Id = value; }
		}

	}

	public class ApplicationRoles : IdentityRole<Guid>
	{
		//public ApplicationRoles(string roleName) : base(roleName)
		//{

		//}
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public override Guid Id
		{
			get { return base.Id; }
			set { base.Id = value; }
		}

	}
	public class ApplicationUserRole : IdentityUserRole<Guid>
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid ApplicationUserRoleId { get; set; }

	}
}
