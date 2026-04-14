using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("providers")]
    public class Provider
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Column("speciality")]
        public string Speciality { get; set; } = string.Empty;
        [Required]
        [Column("office_address")]
        public string Office_Address { get; set; } = string.Empty;
        [Required]
        [Column("phone")]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [Column("credentials")]
        public string Credentials { get; set; } = string.Empty;
    }
}
