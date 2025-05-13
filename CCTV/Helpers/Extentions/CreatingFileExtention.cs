namespace CCTV.Helpers.Extentions
{
    public static  class CreatingFileExtention
    {
        public static string CreatingFile(this IFormFile file ,string webRoot, string folderName)
        {
            string fileName = " ";
            if (fileName.Length > 100)
            {
                fileName = Guid.NewGuid() + file.FileName.Substring(file.FileName.Length - 64);
            }
            else
            {
                fileName = Guid.NewGuid() + file.FileName;
            }
            string path = Path.Combine(webRoot, folderName, fileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
        public static void DeleteFile(this string fileName,string webRoot,string folderName)
        {
            string path = Path.Combine(webRoot, folderName, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
