namespace CoreLibarary.Models
{

    public enum Roles
        {
            Administrator = 1,
            User = 2
        }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles RoleId { get; set; }
    }
}