namespace DemoTests.BLL
{
    using DemoTests.Shared.Entities;
    using System.Collections.Generic;

    public interface ICourseService
    {
        List<Subject> GetSubjects(int? maxHours);

        List<Student> GetStudents(bool orderByName, bool withInactives);

        List<Student> GetStudents(Subject subject);

        void AssignToSubject(int studentId, int subjectId);
    }
}
