using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;

namespace innowise_task_server.Models
{
    public class Fridge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Name must be no shorter than 1 and no longer than 20 characters")]
        [MaxLength(20, ErrorMessage = "Name must be no shorter than 1 and no longer than 20 characters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Name must be no shorter than 3 and no longer than 30 characters")]
        [MaxLength(30, ErrorMessage = "Name must be no shorter than 3 and no longer than 30 characters")]
        public string? OwnerName { get; set; }

        [Required]
        [RegularExpression("^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$")]         //3bfba40f-f256-445d-a86d-78acdd80b485
        public Guid ModelID { get; set; }

        public FridgeModel? Model { get; set;}

        public ICollection<FridgeProduct>? Products { get; set; }
    }
}
