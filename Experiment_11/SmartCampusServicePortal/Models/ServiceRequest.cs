using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampusServicePortal.Models
{
    [Table("ServiceRequests")]
    public class ServiceRequest
    {
        [Key]
        public int Id { get; set; }

        public int? StudentId { get; set; }

        [StringLength(150)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [StringLength(30)]
        public string? Status { get; set; } = "Pending";

        public DateTime? CreatedAt { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }
    }
}