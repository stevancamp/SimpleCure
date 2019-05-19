using System.ComponentModel.DataAnnotations;

namespace SimpleCure.Models.AdminModels
{
    public class EditOrderInfoProductGroup_ViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Group Name is required")]
        public string GroupName { get; set; }
    }
}