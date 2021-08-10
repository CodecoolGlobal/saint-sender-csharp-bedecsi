namespace SaintSender.Core.Models
{
    public class Credentials
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public Credentials(string emailAddress, string password, string name)
        {
            EmailAddress = emailAddress;
            Password = password;
            Name = name;
        }
    }
}
