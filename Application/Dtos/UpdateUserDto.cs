using Domain.Models.Enums;

namespace Application.Dtos
{
    public class UpdateUserDto
    {
        public string? Name { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? CPF { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public DateTime? BirthDate { get; set; }
        public EmploymentType? EmploymentType { get; set; }
        public Status? Status { get; set; }
    }
}
