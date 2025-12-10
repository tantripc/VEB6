using MiddlewareTool.Logs;
using Spire.Xls;
using System;
using System.IO;
using System.Text;

namespace MiddlewareTool.Utility
{
    /// <summary>
    /// XlsxUtility
    /// </summary>
    public class XlsxUtility
    {
        public static void ConvertToCsv(string filePath, byte[] data)
        {
            try
            {
                var stream = new MemoryStream(data);
                var workbook = new Workbook();
                workbook.LoadFromStream(stream);
                var sheet = workbook.Worksheets[0];
                sheet.SaveToFile(filePath, ",", Encoding.UTF8);
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex) { Logging.LogError($"Exception ConvertToCsv: {ex}"); }
        }
    }
}
