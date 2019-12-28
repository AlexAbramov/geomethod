using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Geomethod;

namespace Geomethod.Windows.Forms
{
	#region LocalizedPropertyDescriptor

	public class LocalizedPropertyDescriptor : PropertyDescriptor
	{
		private PropertyDescriptor basePropertyDescriptor; 

		public LocalizedPropertyDescriptor(PropertyDescriptor basePropertyDescriptor) : base(basePropertyDescriptor)
		{
			this.basePropertyDescriptor = basePropertyDescriptor;
		}

		public override bool CanResetValue(object component)
		{
			return basePropertyDescriptor.CanResetValue(component);
		}

		public override Type ComponentType
		{
			get { return basePropertyDescriptor.ComponentType; }
		}

		public override string DisplayName
		{
			get 
			{
				string displayName = "";
				
				foreach( Attribute oAttrib in this.basePropertyDescriptor.Attributes )
				{
					if( oAttrib.GetType().Equals(typeof(LocalizedPropertyAttribute)) )
					{
						displayName = ((LocalizedPropertyAttribute)oAttrib).Name;
						break;
					}
				}				
				if(displayName.Length==0) displayName=this.basePropertyDescriptor.DisplayName;
				else displayName=Locale.Get(displayName);
				return displayName;
			}
		}

		public override string Description
		{
			get
			{
				string displayName = "";			
				
				foreach( Attribute oAttrib in this.basePropertyDescriptor.Attributes )
				{
					if( oAttrib.GetType().Equals(typeof(LocalizedPropertyAttribute)) )
					{
						displayName = ((LocalizedPropertyAttribute)oAttrib).Description;
						break;
					}
				}
				if(displayName.Length==0) displayName=this.basePropertyDescriptor.Description;
				else displayName=Locale.Get(displayName);
				return displayName;
			}
		}

		public override object GetValue(object component)
		{
			return this.basePropertyDescriptor.GetValue(component);
		}

		public override bool IsReadOnly
		{
			get { return this.basePropertyDescriptor.IsReadOnly; }
		}

		public override string Name
		{
			get { return this.basePropertyDescriptor.Name; }
		}

		public override Type PropertyType
		{
			get { return this.basePropertyDescriptor.PropertyType; }
		}

		public override void ResetValue(object component)
		{
			this.basePropertyDescriptor.ResetValue(component);
		}

		public override bool ShouldSerializeValue(object component)
		{
			return this.basePropertyDescriptor.ShouldSerializeValue(component);
		}

		public override void SetValue(object component, object value)
		{
			this.basePropertyDescriptor.SetValue(component, value);
		}
	}
	#endregion

	#region LocalizedObject

	public class LocalizedObject : ICustomTypeDescriptor
	{
        object obj;
        public object Object { get { return obj; } }
        protected LocalizedObject(object obj) { this.obj = obj; }
		private PropertyDescriptorCollection localizedProps;
//		public EventHandler OnDataChanged;

		public String GetClassName()
		{
			return TypeDescriptor.GetClassName(this,true);
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this,true);
		}

		public String GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		public EventDescriptor GetDefaultEvent() 
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public PropertyDescriptor GetDefaultProperty() 
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		public object GetEditor(Type editorBaseType) 
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes) 
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if ( localizedProps == null) 
			{
				// Get the collection of properties
				PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, attributes, true);

				localizedProps = new PropertyDescriptorCollection(null);

				// For each property use a property descriptor of our own that is able to be localized
				foreach( PropertyDescriptor oProp in baseProps )
				{
					localizedProps.Add(new LocalizedPropertyDescriptor(oProp));
				}
			}
			return localizedProps;
		}

		public PropertyDescriptorCollection GetProperties()
		{
			// Only do once
			if ( localizedProps == null) 
			{
				// Get the collection of properties
				PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, true);
				localizedProps = new PropertyDescriptorCollection(null);

				// For each property use a property descriptor of our own that is able to be localized
				foreach( PropertyDescriptor oProp in baseProps )
				{
					localizedProps.Add(new LocalizedPropertyDescriptor(oProp));
				}
			}
			return localizedProps;
		}

		public object GetPropertyOwner(PropertyDescriptor pd) 
		{
			return this;
		}
	}

	#endregion

	#region Attributes

	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
	public class LocalizedCategoryAttribute : CategoryAttribute
	{
		public LocalizedCategoryAttribute() : base() {}
		public LocalizedCategoryAttribute(string name) : base(name) {}


		// This method will be called by the component framework to get a localized
		// string. Note: Only called once on first access.
		protected override string GetLocalizedString( string value )
		{
			string baseStr = base.GetLocalizedString( value );
			return Locale.Get(value);
		}
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
	public class LocalizedPropertyAttribute : Attribute
	{
		private String resourceName = "";
		private String resourceDescription = "";

		public LocalizedPropertyAttribute(String name)
		{
			resourceName = name;
		}

		// The key for a property name into the resource table.
		// If empty the property name will be used by default.
		public String Name
		{
			get {  return resourceName;  }
			set {  resourceName = value;  }
		}

		// The key for a property description into the resource table.
		// If empty the property name appended by 'Description' will be used by default.
		public String Description
		{
			get {  return resourceDescription;  }
			set {  resourceDescription = value;  }
		}
	}
	#endregion
}
