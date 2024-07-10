namespace PuntoVitaExams.API.Models
{
    public class UserToDisplayDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }

        public int RoleId { get; set; }
    }
}
