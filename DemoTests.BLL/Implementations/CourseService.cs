namespace DemoTests.BLL.Implementations
{
    using DemoTests.DAL;
    using DemoTests.Shared.Entities;
    using SharedLibrary.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CourseService: ICourseService
    {
        private ICourseDataService _courseDataService;
        private ILogger _logger;

        public CourseService(ICourseDataService courseDataService, ILogger logger)
        {
            _courseDataService = courseDataService ?? throw new ArgumentNullException("courseDataService");
            _logger = logger ?? throw new ArgumentNullException("logger");
        }

        public List<Subject> GetSubjects(int? maxHours)
        {
            List<Subject> subjects = _courseDataService.GetSubjects();

            if (subjects == null || subjects.Count == 0)
            {
                throw new NotFoundException();
            }

            if (maxHours != null)
            {
                subjects = subjects.Where(x => x.Hours <= maxHours).ToList();
            }

            return subjects;
        }

        public List<Student> GetStudents(bool orderByName, bool withInactives)
        {
            try
            {
                List<Student> students = _courseDataService.GetStudents();

                _logger.Log("Some message");

                if (students == null || students.Count == 0)
                {
                    throw new NotFoundException();
                }



                return FilterStudents(students, orderByName, withInactives);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Student> GetStudents(Subject subject)
        {
            try
            {
                List<Student> students = _courseDataService.GetStudents(subject.Id);

                if (students == null || students.Count == 0)
                {
                    throw new NotFoundException();
                }

                return students;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal List<Student> Filter(List<Student> students, bool orderByName, bool withInactives)
        {
            return FilterStudents(students, orderByName, withInactives);
        }

        private List<Student> FilterStudents(List<Student> students, bool orderByName, bool withInactives)
        {
            if (!withInactives)
            {
                students = students.Where(x => x.IsActive).ToList();
            }

            if (orderByName)
            {
                return students.OrderBy(x => x.Name).ToList();
            }

            return students.OrderBy(x => x.Surname).ToList();
        }


        public void AssignToSubject(int studentId, int subjectId)
        {
            Student student = _courseDataService.GetStudent(studentId);

            if (student == null)
            {
                throw new InvalidArgumentException("student");
            }

            Subject subject = _courseDataService.GetSubject(subjectId);

            if (subject == null)
            {
                throw new InvalidArgumentException("subject");
            }

            try
            {
                _courseDataService.AssignToSubject(studentId, subjectId);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
