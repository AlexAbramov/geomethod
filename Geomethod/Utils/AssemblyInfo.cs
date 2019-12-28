using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Geomethod
{
    public class AssemblyInfo
    {
        Assembly assembly;
        string assemblyTitle = null;
        string assemblyVersion = null;
        string assemblyDescription = null;
        string assemblyProduct = null;
        string assemblyCopyright = null;
        string assemblyCompany = null;
        string clrVersion = null;

        public AssemblyInfo(Assembly assembly) { this.assembly = assembly; }
        public AssemblyInfo() { this.assembly = Assembly.GetEntryAssembly(); }

        public string AssemblyTitle
        {
            get
            {
                if (assemblyTitle == null)
                {
                    // Get all Title attributes on this assembly
                    object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                    // If there is at least one Title attribute
                    if (attributes.Length > 0)
                    {
                        // Select the first one
                        AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                        // If it is not an empty string, return it
                        assemblyTitle = titleAttribute.Title;
                    }
                    // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                    else assemblyTitle = System.IO.Path.GetFileNameWithoutExtension(assembly.CodeBase);
                }
                return assemblyTitle;
            }
        }

        public string AssemblyVersion
        {
            get
            {
                if (assemblyVersion == null)
                {
                    assemblyVersion = assembly.GetName().Version.ToString();
                }
                return assemblyVersion;
            }
        }

        public string AssemblyDescription
        {
            get
            {
                if (assemblyDescription == null)
                {
                    // Get all Description attributes on this assembly
                    object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                    // If there aren't any Description attributes, return an empty string
                    assemblyDescription = attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
                    // If there is a Description attribute, return its value
                }
                return assemblyDescription;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                if (assemblyProduct == null)
                {
                    // Get all Product attributes on this assembly
                    object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                    // If there aren't any Product attributes, return an empty string
                    assemblyProduct = attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
                    // If there is a Product attribute, return its value
                }
                return assemblyProduct;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                if (assemblyCopyright == null)
                {
                    // Get all Copyright attributes on this assembly
                    object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                    // If there aren't any Copyright attributes, return an empty string
                    assemblyCopyright = (attributes.Length == 0) ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
                    // If there is a Copyright attribute, return its value
                }
                return assemblyCopyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                if (assemblyCompany == null)
                {
                    // Get all Company attributes on this assembly
                    object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                    // If there aren't any Company attributes, return an empty string
                    assemblyCompany = attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
                    // If there is a Company attribute, return its value
                }
                return assemblyCompany;
            }
        }

        public string CLRVersion
        {
            get
            {
                if (clrVersion == null)
                {
                    clrVersion = Environment.Version.ToString();
                }
                return clrVersion;
            }
        }

    }
}
