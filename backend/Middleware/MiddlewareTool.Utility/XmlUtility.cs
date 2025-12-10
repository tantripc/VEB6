using System;
using System.IO;
using System.Xml.Serialization;

namespace MiddlewareTool.Utility
{
    /// <summary>
    /// XmlUtility
    /// </summary>
    public class XmlUtility
    {
        /// <summary>
        /// Xml Serialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S3966:Objects should not be disposed more than once", Justification = "")]
        public static string Serialize<T>(T value)
        {
            string m_Xml = string.Empty;

            XmlSerializer m_XmlSerializer = new XmlSerializer(typeof(T));
            using (var m_MemoryStream = new MemoryStream())
            {
                m_XmlSerializer.Serialize(m_MemoryStream, value);
                m_MemoryStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader m_StreamReader = new StreamReader(m_MemoryStream))
                {
                    m_Xml = m_StreamReader.ReadToEnd();
                }
            }

            return m_Xml;
        }
        ///// <summary>
        ///// XML De Serialize
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="xml"></param>
        ///// <returns></returns>
        ////public static T DeSerialize<T>(string xml)
        ////{
        ////    T m_Value = default(T);

        ////    if (!string.IsNullOrEmpty(xml))
        ////    {
        ////        XmlSerializer m_XmlSerializer = new XmlSerializer(typeof(T));
        ////        using (StringReader m_StringReader = new StringReader(xml))
        ////        {
        ////            m_Value = (T)m_XmlSerializer.Deserialize(m_StringReader);
        ////        }
        ////    }

        ////    return m_Value;
        ////}
        /// <summary>
        /// Xml Serialize
        /// </summary>
        /// <typeparam name="T">DTO</typeparam>
        /// <param name="value">value</param>
        /// <param name="types">type</param>
        /// <returns></returns>

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S3966:Objects should not be disposed more than once", Justification = "")]
        public static string Serialize<T>(T value, Type[] types)
        {
            string m_Xml = string.Empty;

            XmlSerializer m_XmlSerializer = new XmlSerializer(typeof(T), types);
            using (MemoryStream m_MemoryStream = new MemoryStream())
            {
                m_XmlSerializer.Serialize(m_MemoryStream, value);
                m_MemoryStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader m_StreamReader = new StreamReader(m_MemoryStream))
                {
                    m_Xml = m_StreamReader.ReadToEnd();
                }
            }

            return m_Xml;
        }
    }
}
