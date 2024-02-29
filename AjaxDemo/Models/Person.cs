using System.Data.SqlClient;

namespace AjaxDemo.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
    public class PersonRepo
    {
        private string _connectionString;
        public PersonRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Person> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People";
            connection.Open();
            var reader = command.ExecuteReader();
            var people = new List<Person>();
            while (reader.Read())
            {
                people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }
        public void AddPerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                "VALUES (@first,@last,@age)";
            command.Parameters.AddWithValue("@first", person.FirstName);
            command.Parameters.AddWithValue("@last", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public void DeletePerson(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM People WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public Person GetPerson(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            var reader = command.ExecuteReader();
            var person = new Person();
            while (reader.Read())
            {
                person.Id = (int)reader["Id"];
                person.FirstName = (string)reader["FirstName"];
                person.LastName = (string)reader["LastName"];
                person.Age = (int)reader["Age"];
            }
            return person;
        }
        public void EditPerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE People SET FirstName=@firstName, LastName=@lastName,Age=@age WHERE Id = @id";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            command.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
