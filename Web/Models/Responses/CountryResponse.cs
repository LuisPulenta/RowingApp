using System.Collections.Generic;

namespace RowingApp.Common.Responses
{
    public class CountryResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FlagImagePath { get; set; }

        public string FlagImageFullPath => string.IsNullOrEmpty(FlagImagePath)
          ? $"http://keypress.serveftp.net:88/RowingAppApi/images/Flags/noimage.png"
          : $"http://keypress.serveftp.net:88/RowingAppApi{FlagImagePath.Substring(1)}";

        public ICollection<DepartmentResponse> Departments { get; set; }

        public ICollection<TeamResponse> Teams { get; set; }
    }
}