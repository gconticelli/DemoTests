namespace DemoTests.Shared.Entities
{
    using System.Collections.Generic;

    public class Student
    {
        public Student()
        {
            this.Subjects = new List<Subject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsActive { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}
