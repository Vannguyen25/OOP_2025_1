using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOP_Semester.Models
{
    [Table("petmessenger")]
    public class PetMessenger
    {
        [Key, Column(Order = 0)]
        public int PetID { get; set; }

        [Key, Column(Order = 1)]
        public int MesID { get; set; }

        // --- Navigation Properties ---
        [ForeignKey("PetID")]
        public virtual Pet? Pet { get; set; }

        [ForeignKey("MesID")]
        public virtual Messenger? Messenger { get; set; }
    }
}