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
	public enum PrimitiveArrayFetch
	{
		
		/// <summary>Default value (don't use it)</summary>
		Unspecified,
		
		/// <summary>join</summary>
		[System.Xml.Serialization.XmlEnumAttribute("join")]
		Join,
		
		/// <summary>select</summary>
		[System.Xml.Serialization.XmlEnumAttribute("select")]
		Select,
		
		/// <summary>subselect</summary>
		[System.Xml.Serialization.XmlEnumAttribute("subselect")]
		Subselect,
	}
}
