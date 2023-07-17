using System;

namespace EmailManager.Models
{
    public static class GetFileName
    {
        private static string _fileName;
        public static string FileName(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return null;

            try
            {
                int lastSlashIndex = filePath.LastIndexOf(@"\");
                _fileName = filePath.Substring(lastSlashIndex + 1, filePath.Length - lastSlashIndex - 1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _fileName;
        }
    }
}