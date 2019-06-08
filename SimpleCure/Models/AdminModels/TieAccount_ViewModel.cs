using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.AdminModels
{
    public class TieAccount_ViewModel : ResponseBase
    {
        public int ID { get; set; }
        [Required]
        public string Login { get; set; }
    }
}