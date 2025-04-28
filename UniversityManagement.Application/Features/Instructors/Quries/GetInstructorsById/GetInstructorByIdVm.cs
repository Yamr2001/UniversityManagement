namespace UniversityManagement.Application.Features.Instructors.Quries.GetInstructorsById
{
    public class GetInstructorByIdVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
