using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("providers")]
    public class Provider
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }


        [Required]
        [MaxLength(200)]
        [Column("full_name")]
        public string FullName { get; set; }

        [MaxLength(150)]
        [Column("specialty")]
        public string Specialty { get; set; }

        [MaxLength(150)]
        [Column("credentials")]
        public string Credentials { get; set; }

        [MaxLength(200)]
        [Column("practice_name")]
        public string PracticeName { get; set; }

        [MaxLength(300)]
        [Column("address")]
        public string Address { get; set; }

        [MaxLength(20)]
        [Column("phone")]
        public string Phone { get; set; }


        [Column("latitude")]
        public double Latitude { get; set; }
        [Column("longitude")]
        public double Longitude { get; set; }


        [Column("scheduling_integrated")]
        public bool SchedulingIntegrated { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }



        //navigation
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<ProviderInsurance> ProviderInsurances { get; set; } = new List<ProviderInsurance>();

    }
}
