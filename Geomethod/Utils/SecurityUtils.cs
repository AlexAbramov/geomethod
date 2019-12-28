using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Geomethod
{
	public class SecurityUtils
	{
		public bool IsUserAdministrator()
		{
			bool isAdmin;
			try
			{
				WindowsIdentity user = WindowsIdentity.GetCurrent();
				WindowsPrincipal principal = new WindowsPrincipal(user);
				isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
			}
			catch
			{
				isAdmin = false;
			}
			return isAdmin;
		}
		private void CreateFolders(string companyFolderPath, string applicationFolderPath, bool allUsers)
		{
			DirectoryInfo directoryInfo;
			DirectorySecurity directorySecurity;
			AccessRule rule;
			SecurityIdentifier securityIdentifier = new SecurityIdentifier
				(WellKnownSidType.BuiltinUsersSid, null);
			if (!Directory.Exists(companyFolderPath))
			{
				directoryInfo = Directory.CreateDirectory(companyFolderPath);
				bool modified;
				directorySecurity = directoryInfo.GetAccessControl();
				rule = new FileSystemAccessRule(
					securityIdentifier,
					FileSystemRights.Write |
					FileSystemRights.ReadAndExecute |
					FileSystemRights.Modify,
					AccessControlType.Allow);
				directorySecurity.ModifyAccessRule(AccessControlModification.Add, rule, out modified);
				directoryInfo.SetAccessControl(directorySecurity);
			}
			if (!Directory.Exists(applicationFolderPath))
			{
				directoryInfo = Directory.CreateDirectory(applicationFolderPath);
				if (allUsers)
				{
					bool modified;
					directorySecurity = directoryInfo.GetAccessControl();
					rule = new FileSystemAccessRule(
						securityIdentifier,
						FileSystemRights.Write |
						FileSystemRights.ReadAndExecute |
						FileSystemRights.Modify,
						InheritanceFlags.ContainerInherit |
						InheritanceFlags.ObjectInherit,
						PropagationFlags.InheritOnly,
						AccessControlType.Allow);
					directorySecurity.ModifyAccessRule(AccessControlModification.Add, rule, out modified);
					directoryInfo.SetAccessControl(directorySecurity);
				}
			}
		}
	}
}
