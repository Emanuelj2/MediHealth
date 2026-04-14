using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("patients")]
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public Guid InsurancePlanId { get; set; }

        [ForeignKey(nameof(InsurancePlanId))]
        public InsurancePlan InsurancePlan { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}