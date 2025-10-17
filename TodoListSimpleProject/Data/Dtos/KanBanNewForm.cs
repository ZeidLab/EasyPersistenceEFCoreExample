using System.ComponentModel.DataAnnotations;

namespace ExampleTodoListProject.Data.Dtos
{
    public class KanBanNewForm
    {
        [Required]
        [StringLength(10, ErrorMessage = "Name length can't be more than 10.")]
        public string Name { get; set; }
    }
}