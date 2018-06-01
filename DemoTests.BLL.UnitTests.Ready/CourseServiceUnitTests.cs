namespace DemoTests.BLL.UnitTests.Ready
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DemoTests.Shared.Entities;
    using FluentAssertions;
    using System.Linq;
    using DAL;
    using FakeItEasy;
    using Implementations;
    using SharedLibrary.Exceptions;
    using System;

    /// <summary>
    /// Summary description for CourseServiceUnitTests
    /// </summary>
    [TestClass]
    public class CourseServiceUnitTests
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

        public CourseServiceUnitTests()
        { }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        //
        // You can use the following additional attributes as you write your tests:
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {

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

        }

        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() {

            _localVar = "some text";
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() {

            _localVar = null;
        }

        //Positivo
        [TestMethod]
        [Description("MSTest - T01 - GetSubjects: OK")]
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

        [DataTestMethod]
        [DataRow(20, 2)]
        [DataRow(40, 3)]
        [Description("MSTest - T02 - GetSubjects - WithParams: OK")]

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

        [TestMethod]
        [Description("MSTest - T03 - GetSubjects: NotFound (Attribute)")]
        [ExpectedException(typeof(NotFoundException))]
        public void GetSubjects_NotFound_Attribute()
        {
            #region Arrange

            A.CallTo(() => _courseDataService.GetSubjects()).Returns(_emptyListSubjects);

            #endregion

            #region Act

            List<Subject> result = _courseService.GetSubjects(20);

            #endregion

            #region Assert

            #endregion
        }

        [TestMethod]
        [Description("MSTest - T04 - GetSubjects: NotFound (Assert)")]
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

        //Excepciones
        [TestMethod]
        [Description("MSTest - T05 - GetStudents: NotFound (Assert)")]
        public void GetStudents_NotFound_Assert()
        {
            #region Arrange

            A.CallTo(() => _courseDataService.GetStudents()).Returns(_emptyListStudents);

            #endregion

            #region Act

            Action result = () => _courseService.GetStudents(true, false);

            #endregion

            #region Assert

            result.Should().Throw<NotFoundException>();

            #endregion
        }

        //Excepciones
        [TestMethod]
        [Description("MSTest - T06 - GetStudents: SystemException (Assert)")]
        public void GetStudents_SystemException()
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

        //Privado
        [TestMethod]
        [Description("MSTest - T07 - FilterStudents: PrivateMethod (PrivateObject)")]
        public void PrivateMethod_FilterStudents_Ok()
        {
            #region Arrange

            var courseServicePrivatesMethods = new PrivateObject(_courseService);

            var orderByName = true;
            var withInactives = false;

            #endregion

            #region Act

            var result = courseServicePrivatesMethods.Invoke("FilterStudents", _students, orderByName, withInactives);

            #endregion

            #region Assert

            //Standard Asserts
            Assert.IsNotNull(result);

            //Fluent Assertion
            result.Should().NotBeNull();

            result.GetType().Should().Be(typeof(List<Student>));
            ((List<Student>)result).Count.Should().Be(5);
            ((List<Student>)result)[0].Name.Should().Be("Alicia");
            ((List<Student>)result)[4].Name.Should().Be("Rosa Maria");
            ((List<Student>)result).Count(x => !x.IsActive).Should().Be(0);

            #endregion
        }

        //Privado
        [TestMethod]
        [Description("MSTest - T08 - FilterStudents: PrivateMethod (Assembly)")]
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
            result.Count.Should().Be(5);
            result[0].Name.Should().Be("Alicia");
            result[4].Name.Should().Be("Rosa Maria");
            result.Count(x => !x.IsActive).Should().Be(0);

            #endregion

        }

        //Excepciones

        [TestMethod]
        [Description("MSTest - T09 - GetStudents By Subject: NotFound (Assert)")]
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

        //Positivo
        [TestMethod]
        [Description("MSTest - T10 - AssignToSubject: OK")]
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
