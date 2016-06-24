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
	public class OneToManyAttribute : BaseAttribute
	{
		
		private NotFoundMode _notfound = NotFoundMode.Unspecified;
		
		private bool _embedxml = true;
		
		private string _node = null;
		
		private string _entityname = null;
		
		private bool _embedxmlspecified;
		
		private string _class = null;
		
		/// <summary> Default constructor (position=0) </summary>
		public OneToManyAttribute() : 
				base(0)
		{
		}
		
		/// <summary> Constructor taking the position of the attribute. </summary>
		public OneToManyAttribute(int position) : 
				base(position)
		{
		}
		
		/// <summary> </summary>
		public virtual string Class
		{
			get
			{
				return this._class;
			}
			set
			{
				this._class = value;
			}
		}
		
		/// <summary> </summary>
		public virtual System.Type ClassType
		{
			get
			{
				return System.Type.GetType( this.Class );
			}
			set
			{
				if(value.Assembly == typeof(int).Assembly)
					this.Class = value.FullName.Substring(7);
				else
					this.Class = HbmWriterHelper.GetNameWithAssembly(value);
			}
		}
		
		/// <summary> </summary>
		public virtual NotFoundMode NotFound
		{
			get
			{
				return this._notfound;
			}
			set
			{
				this._notfound = value;
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
		public virtual bool EmbedXml
		{
			get
			{
				return this._embedxml;
			}
			set
			{
				this._embedxml = value;
				_embedxmlspecified = true;
			}
		}
		
		/// <summary> Tells if EmbedXml has been specified. </summary>
		public virtual bool EmbedXmlSpecified
		{
			get
			{
				return this._embedxmlspecified;
			}
		}
		
		/// <summary> </summary>
		public virtual string EntityName
		{
			get
			{
				return this._entityname;
			}
			set
			{
				this._entityname = value;
			}
		}
		
		/// <summary> </summary>
		public virtual System.Type EntityNameType
		{
			get
			{
				return System.Type.GetType( this.EntityName );
			}
			set
			{
				if(value.Assembly == typeof(int).Assembly)
					this.EntityName = value.FullName.Substring(7);
				else
					this.EntityName = HbmWriterHelper.GetNameWithAssembly(value);
			}
		}
	}
}
