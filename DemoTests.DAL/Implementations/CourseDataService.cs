namespace DemoTests.DAL.Implementations
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using DemoTests.Shared.Entities;
    using SharedLibrary.Exceptions;

    public class CourseDataService : ICourseDataService
    {
        private string _connectionString;

        public CourseDataService(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new System.ArgumentNullException("connectionString");
            }

            _connectionString = connectionString;
        }

        public List<Student> GetStudents()
        {
            List<Student> rtn = new List<Student>();
            try
            {

                using (SqlConnection connection = new SqlConnection(this._connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Student;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Student student = new Student()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"]
                            };

                            rtn.Add(student);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }

            return rtn;
        }

        public List<Student> GetStudents(int subjectId)
        {
            List<Student> rtn = new List<Student>();
            try
            {
                using (SqlConnection connection = new SqlConnection(this._connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM [demotests].[dbo].[Student] INNER JOIN StudentSubject ON Student.Id = StudentSubject.StudentId WHERE SubjectId = @SubjectId;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        cmd.Parameters.AddWithValue("@StudentId", subjectId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Student student = new Student()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                IsActive = (bool)reader["IsActive"]
                            };

                            rtn.Add(student);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }


            return rtn;
        }

        public Student GetStudent(int studentId)
        {
            Student student = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(this._connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM [demotests].[dbo].[Student] WHERE Id = @StudentId;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            student = new Student()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                IsActive = (bool)reader["IsActive"]
                            };
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }

            return student;
        }

        public List<Subject> GetSubjects()
        {
            List<Subject> rtn = new List<Subject>();
            try
            {

                using (SqlConnection connection = new SqlConnection(this._connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Subjects;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Subject subject = new Subject()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Capacity = (int)reader["Capacity"],
                                Hours = (int)reader["Hours"]
                            };

                            rtn.Add(subject);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }

            return rtn;
        }

        public Subject GetSubject(int subjectId)
        {
            Subject subject = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(this._connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM [demotests].[dbo].[Subjects] WHERE Id = @SubjectId;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubjectId", subjectId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            subject = new Subject()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Capacity = (int)reader["Capacity"],
                                Hours = (int)reader["Hours"]
                            };
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }

            return subject;
        }

        public List<Teacher> GetTeachers()
        {
            List<Teacher> rtn = new List<Teacher>();
            try
            {
                using (SqlConnection connection = new SqlConnection(this._connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Teacher;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Teacher subject = new Teacher()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                SubjectId = (int)reader["SubjectId"]
                            };

                            rtn.Add(subject);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }

            return rtn;
        }

        public void AssignToSubject(int studentId, int subjectId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                Student student = GetStudent(studentId);
                if (student == null)
                {
                    throw new InvalidArgumentException("studentId");
                }

                Subject subject = GetSubject(subjectId);

                if (subject == null)
                {
                    throw new InvalidArgumentException("subjectId");
                }
                

                string sql = "SELECT * FROM[demotests].[dbo].[Student] INNER JOIN StudentSubject ON Student.Id = StudentSubject.StudentId WHERE StudentId = @StudentId";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {

                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        if (rows != 1)
                        {
                            throw new DatabaseException();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        throw new DatabaseException(ex.Message);
                    }
                }
            }
        }
    }
}
