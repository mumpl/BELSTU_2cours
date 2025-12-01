

namespace LinguaBender.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty ;
        public int RoleId {  get; set; }
        public Role Role { get; set; } = null!;
        public byte[]? Photo { get; set; }


    }
}
