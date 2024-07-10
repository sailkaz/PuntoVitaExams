namespace PuntoVitaExams.API.Models
{
    public class StudentsWhoPassedTheExamDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string Result { get; set; }
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public int? FlatNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string SailingExamNumber { get; set; }
    }
}
