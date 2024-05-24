using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appointment.Models
{
    public class DoctorModel
    {
        [Key]
        public Guid Doctor_Id { get; set; } = Guid.NewGuid();

        [Required]
        public String FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Dob { get; set; }


        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public String NationalId { get; set; }

        [Required]
        public String Address { get; set; }

        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public String Phone { get; set; }

        public String? Avatar { get; set; }
    }
}
