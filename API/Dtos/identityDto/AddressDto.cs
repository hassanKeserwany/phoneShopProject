using System.ComponentModel.DataAnnotations;

namespace API.Dtos.identityDto
{
    public class AddressDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }

        //we have to make some validation in dto because its the the place we recieve data from form user
        
    }
}
