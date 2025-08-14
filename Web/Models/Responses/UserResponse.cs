
using Common.Enums;

namespace RowingApp.Common.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PicturePath { get; set; }

        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
            ? $"http://keypress.serveftp.net:88/RowingAppiApi/images/Users/nouser.png"
            : $"http://keypress.serveftp.net:88/RowingAppApi{PicturePath.Substring(1)}";

        public UserType UserType { get; set; }


        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }
}