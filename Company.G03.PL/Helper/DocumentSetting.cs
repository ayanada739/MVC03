using Elfie.Serialization;

namespace Company.G03.PL.Helper
{
    public static class DocumentSetting
    {
        //1. Upload
        public static string Upload(IFormFile file, string folderName)
        {
            //1. Get Location of Folder

            //string folderPath = $"C:\\Users\\nabil\\source\\repos\\Company.G03.Solution\\Company.G03.PL\\wwwroot\\Files\\{folderName}";

            //string folderPath = Directory.GetCurrentDirectory() + $"\\wwwroot\\Files\\{folderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory() , $"wwwroot\\Files\\{folderName}");


            //2. Get File Name and make it Unique

            string fileName = $"{Guid.NewGuid()}{folderName}";

            //3. Get File Path : FolderPath + FileName

            string filePath = Path.Combine(folderPath, fileName);


            //4. File Stream: Data Per Sec

            using var FileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(FileStream);

            return fileName;

        }

        //2. Delete
        public static void Delete(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Files\\{folderName}", fileName);

            if(File.Exists(filePath))
               File.Delete(filePath);
        }
    }
}
