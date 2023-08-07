using System;

namespace EmailManager.Models
{
    public static class GetFileData
    {
        private static string _fileName;
        public static string FileNameFromPath(string filePath)
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

        public static string FileNameFromStreamFileName(string streamFileName)
        {
            if (string.IsNullOrWhiteSpace(streamFileName))
                return null;

            try
            {
                int lastDotIndex = streamFileName.LastIndexOf(@".");
                _fileName = streamFileName.Substring(0, streamFileName.Length - FileExtension(streamFileName).Length - 1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _fileName;
        }

        public static string FileExtension(string streamFileName)
        {
            if (string.IsNullOrWhiteSpace(streamFileName))
                return null;

            try
            {
                int lastDotIndex = streamFileName.LastIndexOf(@".");
                _fileName = streamFileName.Substring(lastDotIndex + 1, streamFileName.Length - lastDotIndex - 1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _fileName;
        }
    }
}