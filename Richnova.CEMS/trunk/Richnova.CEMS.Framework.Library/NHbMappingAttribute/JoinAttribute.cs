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
	public class JoinAttribute : BaseAttribute
	{
		
		private bool _optionalspecified;
		
		private string _schema = null;
		
		private bool _inverse = false;
		
		private JoinFetch _fetch = JoinFetch.Unspecified;
		
		private bool _optional = false;
		
		private string _subselect = null;
		
		private string _catalog = null;
		
		private bool _inversespecified;
		
		private string _table = null;
		
		/// <summary> Default constructor (position=0) </summary>
		public JoinAttribute() : 
				base(0)
		{
		}
		
		/// <summary> Constructor taking the position of the attribute. </summary>
		public JoinAttribute(int position) : 
				base(position)
		{
		}
		
		/// <summary> </summary>
		public virtual string Table
		{
			get
			{
				return this._table;
			}
			set
			{
				this._table = value;
			}
		}
		
		/// <summary> </summary>
		public virtual string Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				this._schema = value;
			}
		}
		
		/// <summary> </summary>
		public virtual string Catalog
		{
			get
			{
				return this._catalog;
			}
			set
			{
				this._catalog = value;
			}
		}
		
		/// <summary> </summary>
		public virtual string Subselect
		{
			get
			{
				return this._subselect;
			}
			set
			{
				this._subselect = value;
			}
		}
		
		/// <summary> </summary>
		public virtual JoinFetch Fetch
		{
			get
			{
				return this._fetch;
			}
			set
			{
				this._fetch = value;
			}
		}
		
		/// <summary> </summary>
		public virtual bool Inverse
		{
			get
			{
				return this._inverse;
			}
			set
			{
				this._inverse = value;
				_inversespecified = true;
			}
		}
		
		/// <summary> Tells if Inverse has been specified. </summary>
		public virtual bool InverseSpecified
		{
			get
			{
				return this._inversespecified;
			}
		}
		
		/// <summary> </summary>
		public virtual bool Optional
		{
			get
			{
				return this._optional;
			}
			set
			{
				this._optional = value;
				_optionalspecified = true;
			}
		}
		
		/// <summary> Tells if Optional has been specified. </summary>
		public virtual bool OptionalSpecified
		{
			get
			{
				return this._optionalspecified;
			}
		}
	}
}
