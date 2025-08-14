namespace RowingApp.Common.Responses
{
    public class TeamResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryResponse Country { get; set; }

        public string LogoImagePath { get; set; }

        public string LogoImageFullPath => string.IsNullOrEmpty(LogoImagePath)
          ? $"http://keypress.serveftp.net:88/RowingAppApi/images/Logos/noimage.png"
          : $"http://keypress.serveftp.net:88/RowingAppApi{LogoImagePath.Substring(1)}";
    }
}