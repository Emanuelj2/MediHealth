using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public enum AppointmentStatus
    {
        Pending,
        Approved,
        Denied
    }

    [Table("appointments")]
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }

        [Required]
        public Guid ProviderId { get; set; }

        [ForeignKey(nameof(ProviderId))]
        public Provider Provider { get; set; }

        public DateTime RequestedAt { get; set; }
        public DateTime SlotDateTime { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        [MaxLength(500)]
        public string VisitReason { get; set; }

        [MaxLength(500)]
        public string DenialReason { get; set; }

        public DateTime? RespondedAt { get; set; }
    }
}