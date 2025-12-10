using Ionic.Zip;

namespace MiddlewareTool.Utility
{
    /// <summary>
    /// DateTimeUtility
    /// </summary>
    public static class FileUtility
    {
        public static void CopyDirectory(string sourceDir, string targetDir, bool overwrite = true)
        {
            try
            {
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }
            }
            catch { }

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(targetDir, Path.GetFileName(file));
                File.Copy(file, destFile, overwrite);
            }

            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(targetDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir);
            }
        }

        public static void MoveDirectory(string sourceDir, string targetDir, bool overwrite = true, bool skipSourceDir = true)
        {
            CopyDirectory(sourceDir, targetDir, overwrite);
            if (skipSourceDir)
            {
                DeleteChildren(sourceDir);
            }
            else
                Directory.Delete(sourceDir, true);
        }

        public static void DeleteChildren(string sourceDir)
        {
            // Xóa toàn bộ nội dung nhưng giữ lại thư mục gốc
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                File.Delete(file);
            }

            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                Directory.Delete(subDir, true);
            }
        }

        public static byte[] ZipFolder(string folderPath)
        {
            var zipFilePath = folderPath + ".zip";
            if (File.Exists(zipFilePath))
                File.Delete(zipFilePath);
            var zipFile = new ZipFile(zipFilePath);
            zipFile.AddDirectory(folderPath);
            zipFile.Save();
            zipFile.Dispose();
            byte[] fileContent = File.ReadAllBytes(zipFilePath);
            var dir = new DirectoryInfo(folderPath);
            foreach (var item in dir.GetFiles())
            {
                item.Delete();
            }
            File.Delete(zipFilePath);
            Directory.Delete(folderPath);

            return fileContent;
        }


    }
}
