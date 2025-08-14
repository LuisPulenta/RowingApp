namespace RowingApp.Common.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImagePath)
           ? $"http://keypress.serveftp.net:88/RowingAppApi/images/Categories/noimage.png"
           : $"http://keypress.serveftp.net:88/RowingAppApi{ImagePath.Substring(1)}";
    }
}