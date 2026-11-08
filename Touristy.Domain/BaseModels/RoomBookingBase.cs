using System.ComponentModel.DataAnnotations;

namespace Touristy.Domain.BaseModels
{
    public class RoomBookingBase : IValidatableObject
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date < DateTime.Now.Date)
            {
                yield return new ValidationResult("Booking date cannot be in the past.", new[] { "Date" });
            }
        }
    }
}
