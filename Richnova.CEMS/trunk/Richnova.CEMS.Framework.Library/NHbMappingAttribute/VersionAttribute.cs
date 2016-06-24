// 
// NHibernate.Mapping.Attributes
// This product is under the terms of the GNU Lesser General Public License.
//
//
//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 2.0.50727.x
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------
//
//
// This source code was auto-generated by Refly, Version=2.21.1.0 (modified).
//
namespace NHibernate.Mapping.Attributes
{
	
	
	/// <summary> </summary>
	[System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, AllowMultiple=true)]
	[System.Serializable()]
	public class VersionAttribute : BaseAttribute
	{
		
		private bool _insertspecified;
		
		private string _access = null;
		
		private string _node = null;
		
		private string _unsavedvalue = null;
		
		private bool _insert = false;
		
		private string _type = null;
		
		private string _name = null;
		
		private string _column = null;
		
		private VersionGeneration _generated = VersionGeneration.Unspecified;
		
		/// <summary> Default constructor (position=0) </summary>
		public VersionAttribute() : 
				base(0)
		{
		}
		
		/// <summary> Constructor taking the position of the attribute. </summary>
		public VersionAttribute(int position) : 
				base(position)
		{
		}
		
		/// <summary> </summary>
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		
		/// <summary> </summary>
		public virtual string Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}
		
		/// <summary> </summary>
		public virtual string Access
		{
			get
			{
				return this._access;
			}
			set
			{
				this._access = value;
			}
		}
		
		/// <summary> </summary>
		public virtual System.Type AccessType
		{
			get
			{
				return System.Type.GetType( this.Access );
			}
			set
			{
				if(value.Assembly == typeof(int).Assembly)
					this.Access = value.FullName.Substring(7);
				else
					this.Access = HbmWriterHelper.GetNameWithAssembly(value);
			}
		}
		
		/// <summary> </summary>
		public virtual string Column
		{
			get
			{
				return this._column;
			}
			set
			{
				this._column = value;
			}
		}
		
		/// <summary> </summary>
		public virtual string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}
		
		/// <summary> </summary>
		public virtual System.Type TypeType
		{
			get
			{
				return System.Type.GetType( this.Type );
			}
			set
			{
				if(value.Assembly == typeof(int).Assembly)
					this.Type = value.FullName.Substring(7);
				else
					this.Type = HbmWriterHelper.GetNameWithAssembly(value);
			}
		}
		
		/// <summary>undefined|any|none|null|0|-1|... </summary>
		public virtual string UnsavedValue
		{
			get
			{
				return this._unsavedvalue;
			}
			set
			{
				this._unsavedvalue = value;
			}
		}
		
		/// <summary>undefined|any|none|null|0|-1|... </summary>
		public virtual object UnsavedValueObject
		{
			get
			{
				return this.UnsavedValue;
			}
			set
			{
				this.UnsavedValue = value==null ? "null" : value.ToString();
			}
		}
		
		/// <summary> </summary>
		public virtual VersionGeneration Generated
		{
			get
			{
				return this._generated;
			}
			set
			{
				this._generated = value;
			}
		}
		
		/// <summary> </summary>
		public virtual bool Insert
		{
			get
			{
				return this._insert;
			}
			set
			{
				this._insert = value;
				_insertspecified = true;
			}
		}
		
		/// <summary> Tells if Insert has been specified. </summary>
		public virtual bool InsertSpecified
		{
			get
			{
				return this._insertspecified;
			}
		}
	}
}
