using System;

namespace Sem_Benes.Model
{
    public enum UserRole { AppUser, Admin }

    [Serializable()]
    public class User
    {
        public long Id { get; set; }
        public UserRole Role { get; set; }

        public bool IsAdmin
        {
            get { return Role.Equals(UserRole.Admin); }
            set { 
                Role = value ? UserRole.Admin : UserRole.AppUser;
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {
            
        }

        public User(string firstName, string lastName, string password, UserRole role, string username)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Role = role;
            Username = username;
        }
    }
}
