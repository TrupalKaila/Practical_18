using System.ComponentModel.DataAnnotations;

namespace Practical_17.ViewModels
{
    public class StudentListViewModel
    {
        public int StudentId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }
    }
}
