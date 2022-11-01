using System.Data.Entity.Core.Objects.DataClasses;

namespace CursorFinderHost.Models
{
    internal class User
    {


        internal User(string name, int UserToken, UserRole role)
        {
            Name = name;
            Token = UserToken;
            Role = role;
        }
        public string Name { get; set; }
        public int Token { get; }
        public UserRole Role { get; set; }
    }

    internal enum UserRole
    {
        User,
        Admin,
    }
}
