namespace DemoTests.DAL
{
    using DemoTests.Shared.Entities;
    using System.Collections.Generic;

    public interface ICourseDataService
    {
        List<Student> GetStudents();

        List<Student> GetStudents(int subjectId);

        Student GetStudent(int studentId);

        List<Subject> GetSubjects();

        Subject GetSubject(int subjectId);

        List<Teacher> GetTeachers();

        void AssignToSubject(int studentId, int subjectId);

    }
}
