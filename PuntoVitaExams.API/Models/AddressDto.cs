namespace PuntoVitaExams.API.Models
{
    public class AddressDto
    {
        //public int AddressId { get; set; }
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public int? FlatNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
    }
}
