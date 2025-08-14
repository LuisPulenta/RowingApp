namespace RowingApp.Common.Responses
{
    public class ProductImageResponse
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public int ProductId { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImagePath)
           ? $"http://keypress.serveftp.net:88/RowingAppApi/images/Products/noimage.png"
        : $"http://keypress.serveftp.net:88/RowingAppApi{ImagePath.Substring(1)}";
    }
}