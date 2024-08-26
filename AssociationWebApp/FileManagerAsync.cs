namespace AssociationWebApp
{
    public class FileManagerAsync
    {
        public async Task<string> PostFileAsync(IFormFile formFile)
        {
            try
            {

                if (formFile != null)
                {
                    var filePath = Path.Combine("wwwroot/FileUploads", formFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    return filePath;
                }
                else return "null";

            }
            catch (Exception ex) { return "null"; }

        }
        public async Task<string> UpdateFileAsync(IFormFile formFile)
        {
            try
            {

                if (formFile != null)
                {
                    var filePath = Path.Combine("wwwroot/FileUploads", formFile.FileName);
                    System.IO.File.Delete(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    return filePath;
                }
                else return "null";

            }
            catch (Exception e)
            {
                return "null";
            }
        }
        public async Task DeleteFileAsync(string images)
        {
            try
            {
                if (!string.IsNullOrEmpty(images))
                {
                    var filePath = Path.Combine("wwwroot", images);
                    System.IO.File.Delete(filePath);
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
