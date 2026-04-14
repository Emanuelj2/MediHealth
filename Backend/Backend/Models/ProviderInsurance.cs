using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("insurance_providers")]
    public class ProviderInsurance
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ProviderId { get; set; }

        [ForeignKey(nameof(ProviderId))]
        public Provider Provider { get; set; }

        [Required]
        public Guid InsurancePlanId { get; set; }

        [ForeignKey(nameof(InsurancePlanId))]
        public InsurancePlan InsurancePlan { get; set; }

        public bool InNetwork { get; set; }

        public DateTime? VerifiedAt { get; set; }
    }
}