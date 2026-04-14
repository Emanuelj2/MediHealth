using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("insurance_plans")]
    public class InsurancePlan
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string PlanName { get; set; }

        [Required]
        [MaxLength(150)]
        public string ProviderName { get; set; }

        public DateTime LastUpdated { get; set; }

        public Guid AdminId { get; set; }

        // Navigation
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
        public ICollection<ProviderInsurance> ProviderInsurances { get; set; } = new List<ProviderInsurance>();
    }
}