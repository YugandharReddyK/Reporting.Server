using System;
using System.IO;
using System.Xml;
using Hal.Core.XML;

namespace Hal.Core.XmlDocumentExtensions
{
	public static class XmlDocumentExtensions
	{
		// ************************************* STRUCTURES **************************************
		#region "Structures"
		#endregion

		// *************************************** ENUMS *****************************************
		#region "Enums"
		#endregion

		// ************************************ DLL IMPORTS **************************************
		#region "DLL Imports"
		#endregion

		// ********************************** PRIVATE VARIABLES **********************************
		#region "Private Variables"
		#endregion

		// ********************************* PROTECTED VARIABLES *********************************
		#region "Protected Variables"
		#endregion

		// ************************************** EVENTS *****************************************
		#region "Events"
		#endregion

		// ************************************* DELEGATES ***************************************
		#region "Delegates"
		#endregion

		// ************************************* PROPERTIES **************************************
		#region "Properties"
		#endregion

		// ************************************ CONSTRUCTORS *************************************
		#region "Constructors"
		#endregion

		// *********************************** PUBLIC METHODS ************************************
		#region "Public Methods"
		/// <summary>
		/// LoadStringIntoXmlDocument Method - Loads the string into XML document.
		/// </summary>
		/// <param name="xmlString">String - The XML string to load</param>
		/// <returns>XmlDocument - The new created and loaded XmlDocument</returns>
		public static XmlDocument LoadStringIntoXmlDocument(string xmlString)
		{
			// Create a new XmlDocument
			XmlDocument xmlDoc = new XmlDocument();

			// Load the XML into the XmlDocument
			xmlDoc.LoadXml(xmlString);

			return xmlDoc;
		}

		/// <summary>
		/// LoadXmlString Method - Loads the XML string.
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <param name="xmlString">The XML string.</param>
		public static void LoadXmlString(this XmlDocument xml, string xmlString)
		{
			// Load the XML into the XmlDocument
			xml.LoadXml(xmlString);
		}

		/// <summary>
		/// ElementExists Method - Determines if the specified Element the exists in the XML Document
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static bool ElementExists(this XmlDocument xml, string element)
		{
			return (xml.SelectSingleNode(element) != null);
		}

		/// <summary>
		/// GetKeyValuePair Method - Gets the First key value pair
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <returns>Tuple - Returns a Tuple containing the Name of the Node as the Key and the InnerText of the Node as the Value</returns>
		public static Tuple<string, string> GetKeyValuePair(this XmlDocument xml)
		{
			XmlNode node = xml.FirstChild;

			if (node != null)
			{
				return new Tuple<string, string>(node.Name, node.InnerText);
			}

			return (Tuple<string, string>)null;
		}

		/// <summary>
		/// GetNextKeyValuePair Method - Gets the next key value pair from the specified node
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <param name="node">The node.</param>
		/// <returns>Tuple - Returns a Tuple containing the Name of the Node as the Key and the InnerText of the Node as the Value</returns>
		public static Tuple<string, string> GetNextKeyValuePair(this XmlDocument xml, XmlNode node)
		{
			XmlNode nextNode = node.NextSibling;

			if (nextNode != null)
			{
				return new Tuple<string, string>(nextNode.Name, nextNode.InnerText);
			}

			return (Tuple<string, string>)null;
		}

		/// <summary>
		/// GetElementText Method - Gets the Text of the specified element
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static string GetElementText(this XmlDocument xml, string element)
		{
			XmlNode node = xml.SelectSingleNode(element);

			if (node != null)
			{
				return node.InnerText;
			}

			return null;
		}

		/// <summary>
		/// XMLSpecialCharsEscapeChars Method - Locates special characters in the XML and replaces them with escape chars.
		/// <para>This method is the reverse of the XMLEscapeCharsToSpecialChars Method</para>
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <param name="xmlString">The XML string.</param>
		/// <returns>String - Returns the newly modified XML String</returns>
		public static string XMLSpecialCharsEscapeChars(this XmlDocument xml, string xmlString)
		{
			return XMLHelper.XMLSpecialCharsEscapeChars(xmlString);
			//return xmlString.Replace(":", "-").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;").Replace("|", ".");
		}

		/// <summary>
		/// XMLEscapeCharsToSpecialChars Method - Locates escape chars and replaces them with the special characters
		/// <para>This method is the reverse of the XMLSpecialCharsEscapeChars Method</para>
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <param name="xmlString">The XML string.</param>
		/// <returns>String - Returns the newly modified XML String</returns>
		public static string XMLEscapeCharsToSpecialChars(this XmlDocument xml, string xmlString)
		{
			return XMLHelper.XMLEscapeCharsToSpecialChars(xmlString);
			//return xmlString.Replace("-", ":").Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'").Replace(".", "|");
		}

		/// <summary>
		/// TransformXMLToDataSet Method - Transforms the specified XML File into a DataSet containing one or more DataTables
		/// </summary>
		/// <param name="xml">XmlDocument - A reference to the XmlDocument</param>
		/// <param name="xmlFileName">String - A full path and File Name to a file containing the XML to import</param>
		/// <returns>DataSet - Returns a DataSet with one or more Data Tables containing data from the XML Data</returns>
		public static System.Data.DataSet TransformXMLFileToDataSet(this XmlDocument xml, string xmlFileName)
		{
			System.Data.DataSet dataset = new System.Data.DataSet();

			// Create new FileStream with which to read the schema.
			System.IO.FileStream fsReadXml = new System.IO.FileStream(xmlFileName, System.IO.FileMode.Open);
			try
			{
				dataset.ReadXml(fsReadXml);
				return dataset;
			}
			finally
			{
				fsReadXml.Close();
			}
		}

		/// <summary>
		/// TransformXMLStringToDataSet Method - Transforms the specified XML String into a DataSet containing one or more DataTables
		/// </summary>
		/// <param name="xml">XmlDocument - A reference to the XmlDocument</param>
		/// <param name="xmlString">String - A String containing the XML to import</param>
		/// <returns>DataSet - Returns a DataSet with one or more Data Tables containing data from the XML Data</returns>
		public static System.Data.DataSet TransformXMLStringToDataSet(this XmlDocument xml, string xmlString)
		{
			System.Data.DataSet dataset = new System.Data.DataSet();

			// Create a StringReader to read our XML string
			using (StringReader stringReader = new StringReader(xmlString))
			{
				// Transform the XML into a DataSet
				dataset.ReadXml(stringReader);
			}

			return dataset;
		}
		#endregion

		// *********************************** PRIVATE METHODS ***********************************
		#region "Private Methods"
		#endregion

		// ********************************** PROTECTED METHODS **********************************
		#region "Protected Methods"
		#endregion

		// ********************************** INTERNAL METHODS ***********************************
		#region "Internal Methods"
		#endregion

		// ********************************** INTERNAL CLASSES ***********************************
		#region "Internal Classes"
		#endregion
	}
}
