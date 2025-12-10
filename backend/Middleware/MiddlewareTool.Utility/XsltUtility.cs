using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Xsl;

namespace MiddlewareTool.Utility
{
    /// <summary>
    /// XsltUtility
    /// </summary>
    public class XsltUtility
    {
        /// <summary>
        /// Transform
        /// </summary>
        /// <param name="xmlData">xmlData</param>
        /// <param name="xsl">xsl</param>
        /// <returns></returns>
        public string Transform(string xmlData, string xsl)
        {
            if (string.IsNullOrEmpty(xsl) || string.IsNullOrEmpty(xmlData))
            {
                return string.Empty;
            }
            string m_Html = string.Empty;

            //Xsl
            XmlNode m_XmlNodeXsl = this.CreateXmlNode(xsl);
            XslCompiledTransform m_XslCompiledTransform = new XslCompiledTransform();
            XsltSettings m_XsltSettings = XsltSettings.Default;
            m_XslCompiledTransform.Load(m_XmlNodeXsl, m_XsltSettings, new XmlUrlResolver());

            using (StringReader m_StringReader = new StringReader(xmlData))
            {
                using (XmlReader m_XmlReader = XmlReader.Create(m_StringReader))
                {
                    using (StringWriter m_StringWriter = new StringWriter())
                    {
                        m_XslCompiledTransform.Transform(m_XmlReader, null, m_StringWriter);
                        m_Html = m_StringWriter.ToString();
                    }
                }
            }

            return m_Html;
        }


        /// <summary>
        /// CreateXmlNode
        /// </summary>
        /// <param name="xmlData">xmlData</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Vulnerability", "S2755:XML parsers should not be vulnerable to XXE attacks", Justification = "")]
        private XmlNode CreateXmlNode(string xmlData)
        {
            XmlNode m_XmlNodeData = null;
            XmlDocument m_XmlDocument = new XmlDocument();
            m_XmlDocument.LoadXml(xmlData);
            if (m_XmlDocument.ChildNodes.Count > 0)
            {
                m_XmlNodeData = m_XmlDocument.ChildNodes.Cast<XmlNode>().LastOrDefault();
            }
            return m_XmlNodeData;
        }
    }
}
