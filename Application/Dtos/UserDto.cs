namespace Application.Dtos
{
    using Domain.Models.Enums;

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string CPF { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public Status Status { get; set; }
    }
}
