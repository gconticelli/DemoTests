namespace DemoTests.BLL.XunitTests.Ready
{
    using DemoTests.BLL.Implementations;
    using DemoTests.DAL;
    using DemoTests.Shared.Entities;
    using FakeItEasy;
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;
    using System;
    using SharedLibrary.Exceptions;
    using System.Linq;

    public class CourseServiceXunitTests
    {
        private static ICourseService _courseService;
        private static ILogger _logger;
        private static ICourseDataService _courseDataService;

        private static List<Student> _students;
        private static List<Student> _emptyListStudents;
        private static List<Student> _someStudents;
        private static List<Subject> _subjects;
        private static List<Subject> _emptyListSubjects;
        private static List<Teacher> _teachers;

        private static string _localVar;

        public CourseServiceXunitTests()
        {
            _courseDataService = A.Fake<ICourseDataService>();

            _logger = A.Fake<ILogger>();

            _courseService = new CourseService(_courseDataService, _logger);

            _students = new List<Student>()
            {
                new Student()
                {
                    Id = 1,
                    Name = "Rosa Maria",
                    Surname = "Acien Zuruta",
                    IsActive = true
                },
                new Student()
                {
                    Id = 2,
                    Name = "Rafael",
                    Surname = "Cueto Avellaneda",
                    IsActive = true
                },
                new Student()
                {
                    Id = 3,
                    Name = "Hugo",
                    Surname = "Fernandez Seguin",
                    IsActive = true
                },
                new Student()
                {
                    Id = 4,
                    Name = "Alicia",
                    Surname = "Galvez Ibarra",
                    IsActive = true
                },
                new Student()
                {
                    Id = 5,
                    Name = "Isabel",
                    Surname = "Lopez Martin",
                    IsActive = true
                },
                new Student()
                {
                    Id = 5,
                    Name = "Maria del Carmen",
                    Surname = "Garcia Cresco",
                    IsActive = false
                },
                new Student()
                {
                    Id = 5,
                    Name = "Marta",
                    Surname = "Gomez Cambronero",
                    IsActive = false
                }
            };
            _emptyListStudents = new List<Student>();
            _someStudents = new List<Student>()
            {
                new Student()
                {
                    Id = 1,
                    Name = "Rosa Maria",
                    Surname = "Acien Zuruta",
                    IsActive = true
                },
                new Student()
                {
                    Id = 2,
                    Name = "Rafael",
                    Surname = "Cueto Avellaneda",
                    IsActive = true
                },
                new Student()
                {
                    Id = 5,
                    Name = "Marta",
                    Surname = "Gomez Cambronero",
                    IsActive = false
                }
            };

            _subjects = new List<Subject>()
            {
                new Subject()
                {
                    Id = 1,
                    Name = "Arte",
                    Capacity = 10,
                    Hours = 20
                },
                new Subject()
                {
                    Id = 2,
                    Name = "Musica",
                    Capacity = 8,
                    Hours = 20
                },
                new Subject()
                {
                    Id = 3,
                    Name = "Inglés",
                    Capacity = 6,
                    Hours = 80
                },
                new Subject()
                {
                    Id = 4,
                    Name = "Fisica",
                    Capacity = 10,
                    Hours = 40
                },
                new Subject()
                {
                    Id = 5,
                    Name = "Informatica",
                    Capacity = 10,
                    Hours = 80
                }
            };
            _emptyListSubjects = new List<Subject>();

            _teachers = new List<Teacher>()
            {
                new Teacher()
                {
                    Id = 1,
                    Name = "Michelangelo",
                    Surname = "Buonarroti",
                    SubjectId = 1
                },
                new Teacher()
                {
                    Id = 2,
                    Name = "Giuseppe",
                    Surname = "Verdi",
                    SubjectId = 2
                },
                new Teacher()
                {
                    Id = 3,
                    Name = "William",
                    Surname = "Shakespeare",
                    SubjectId = 3
                },
                new Teacher()
                {
                    Id = 4,
                    Name = "Albert",
                    Surname = "Einstein",
                    SubjectId = 4
                },
                new Teacher()
                {
                    Id = 5,
                    Name = "Alan",
                    Surname = "Turing",
                    SubjectId = 5
                }
            };

            _localVar = "some text";
        }

        ~CourseServiceXunitTests()
        {
            _localVar = null;
        }


        [Fact(DisplayName = "xUnit - T01 - GetSubjects: OK")]
        public void GetSubjects_Ok()
        {
            #region Arrange

            A.CallTo(() => _courseDataService.GetSubjects()).Returns(_subjects);

            #endregion

            #region Act

            var result = _courseService.GetSubjects(20);

            #endregion

            #region Assert

            result.Should().NotBeNull();
            result.Count.Should().Be(2);

            #endregion
        }

        [Theory(DisplayName = "xUnit - T02 - GetSubjects - WithParams: OK")]
        [InlineData(20, 2)]
        [InlineData(40, 3)]
        public void GetSubjects_Ok_WithParams(int maxHours, int resultCount)
        {
            #region Arrange

            A.CallTo(() => _courseDataService.GetSubjects()).Returns(_subjects);

            #endregion

            #region Act

            var result = _courseService.GetSubjects(maxHours);

            #endregion

            #region Assert

            result.Should().NotBeNull();
            result.Count.Should().Be(resultCount);

            #endregion
        }

        [Fact(DisplayName = "xUnit - T04 - GetSubjects: NotFound (Assert)")]
        public void GetSubjects_NotFound_Assert()
        {
            #region Arrange

            A.CallTo(() => _courseDataService.GetSubjects()).Returns(_emptyListSubjects);

            #endregion

            #region Act

            Action result = () => _courseService.GetSubjects(20);

            #endregion

            #region Assert

            result.Should().Throw<NotFoundException>();

            #endregion
        }



        [Fact(DisplayName = "xUnit - T05 - GetStudents: NotFound (Assert)")]
        public void GetStudents_NotFound_Assert()
        {
            #region Arrange

            A.CallTo(() => _courseDataService.GetStudents()).Returns(_emptyListStudents);

            #endregion

            #region Act
            
            Action action = () => _courseService.GetStudents(true, false);

            #endregion

            #region Assert

            action.Should().Throw<NotFoundException>();

            #endregion
        }

        [Fact(DisplayName = "xUnit - T06 - GetStudents: SystemException (Assert)")]
        public void GetStudents_SystemException_Assert()
        {
            #region Arrange

            A.CallTo(() => _courseDataService.GetStudents()).Throws<Exception>();

            #endregion

            #region Act

            Action result = () => _courseService.GetStudents(true, false);

            #endregion

            #region Assert

            result.Should().Throw<Exception>();

            #endregion
        }

        [Fact(DisplayName = "xUnit - T08 - FilterStudents: PrivateMethod (Assembly)")]
        public void InternalMethod_FilterStudents_Ok()
        {
            #region Arrange

            var orderByName = true;
            var withInactives = false;

            var service = new CourseService(_courseDataService, _logger);

            #endregion

            #region Act

            var result = service.Filter(_students, orderByName, withInactives);

            #endregion

            #region Act

            result.Should().NotBeNull();

            result.GetType().Should().Be(typeof(List<Student>));
            ((List<Student>)result).Count.Should().Be(5);
            ((List<Student>)result)[0].Name.Should().Be("Alicia");
            ((List<Student>)result)[4].Name.Should().Be("Rosa Maria");
            ((List<Student>)result).Count(x => !x.IsActive).Should().Be(0);

            #endregion

        }

        [Fact(DisplayName = "xUnit - T09 - GetStudents By Subject: NotFound (Assert)")]
        public void GetStudents_BySubject_NotFound()
        {
            #region Arrange

            A.CallTo(() => _courseDataService.GetStudents(A<int>.Ignored)).Returns(_emptyListStudents);

            Subject subject = _subjects.First();

            #endregion

            #region Act

            Action result = () => _courseService.GetStudents(subject);

            #endregion

            #region Assert

            result.Should().Throw<NotFoundException>();

            #endregion
        }


        [Fact(DisplayName = "xUnit - T10 - AssignToSubject: OK")]
        public void AssignToSubject_Ok()
        {
            #region Arrange

            Student student = _students.First();

            Subject subject = _subjects.First();

            A.CallTo(() => _courseDataService.GetStudent(A<int>.Ignored)).Returns(_students.First());
            //A.CallTo(() => _courseDataService.GetStudent(student.Id)).Returns(_students.First());

            A.CallTo(() => _courseDataService.GetSubject(A<int>.Ignored)).Returns(_subjects.First());

            #endregion

            #region Act

            _courseService.AssignToSubject(student.Id, subject.Id);
            //_courseService.AssignToSubject(2, subject.Id);

            #endregion

            #region Assert

            A.CallTo(() => _courseDataService.AssignToSubject(A<int>.Ignored, A<int>.Ignored)).MustHaveHappenedOnceExactly();

            #endregion
        }
    }
}
