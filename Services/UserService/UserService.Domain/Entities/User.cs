using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(20)]
        public string FirstName { get; internal set; }

        [MaxLength(40)]
        public string LastName { get; internal set; }

        [MaxLength(20)]
        public string? Mobile { get; internal set; }

        [MaxLength(40)]
        public string? Email { get; internal set; }

        private User() { }
        public User(string firstName, string lastName, string? mobile, string? email)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetMobile(mobile);
            SetEmail(email);
        }
        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("FirstName cannot be empty!");

            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("LastName cannot be empty!");

            LastName = lastName;
        }

        public void SetMobile(string? mobile)
        {
            Mobile = mobile;
        }

        public void SetEmail(string? email)
        {
            Email = email;
        }
    }
}
