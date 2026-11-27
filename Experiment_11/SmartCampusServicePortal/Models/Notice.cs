using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampusServicePortal.Models
{
    [Table("Notices")]
    public class Notice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        public string? Description { get; set; }

        public DateTime? PostedDate { get; set; }
    }
}