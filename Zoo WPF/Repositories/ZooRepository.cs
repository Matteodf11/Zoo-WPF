using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Zoo_WPF.Models;

namespace Zoo_WPF.Repositories
{
    public class ZooRepository
    {
        private readonly string _connectionString;

        public ZooRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Zoo> GetZoos()
        {
            var zoos = new List<Zoo>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Zoo", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        zoos.Add(new Zoo
                        {
                            Id = (int)reader["Id"],
                            Ubicacion = (string)reader["Ubicacion"]
                        });
                    }
                }
            }
            return zoos;
        }

        public List<Animal> GetAnimals()
        {
            var animals = new List<Animal>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Animal", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        animals.Add(new Animal
                        {
                            Id = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"]
                        });
                    }
                }
            }
            return animals;
        }

        public List<Animal> GetAnimalsInZoo(int zooId)
        {
            var animals = new List<Animal>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT a.Id, a.Nombre FROM Animal a INNER JOIN Relaciones az ON a.Id = az.AnimalId WHERE az.ZooId = @ZooId", connection);
                command.Parameters.AddWithValue("@ZooId", zooId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        animals.Add(new Animal
                        {
                            Id = (int)reader["Id"],
                            Nombre = (string)reader["Nombre"]
                        });
                    }
                }
            }
            return animals;
        }

        public void AddZoo(Zoo zoo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Zoo (Ubicacion) VALUES (@Ubicacion)", connection);
                command.Parameters.AddWithValue("@Ubicacion", zoo.Ubicacion);
                command.ExecuteNonQuery();
            }
        }

        public void AddAnimal(Animal animal)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Animal (Nombre) VALUES (@Nombre)", connection);
                command.Parameters.AddWithValue("@Nombre", animal.Nombre);
                command.ExecuteNonQuery();
            }
        }

        public void AddAnimalToZoo(int zooId, int animalId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Relaciones (ZooId, AnimalId) VALUES (@ZooId, @AnimalId)", connection);
                command.Parameters.AddWithValue("@ZooId", zooId);
                command.Parameters.AddWithValue("@AnimalId", animalId);
                command.ExecuteNonQuery();
            }
        }

        public void RemoveAnimalFromZoo(int zooId, int animalId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Relaciones WHERE ZooId = @ZooId AND AnimalId = @AnimalId", connection);
                command.Parameters.AddWithValue("@ZooId", zooId);
                command.Parameters.AddWithValue("@AnimalId", animalId);
                command.ExecuteNonQuery();
            }
        }

        public void RemoveZoo(int zooId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Zoo WHERE Id = @ZooId", connection);
                command.Parameters.AddWithValue("@ZooId", zooId);
                command.ExecuteNonQuery();
            }
        }

        public void RemoveAnimal(int animalId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Animal WHERE Id = @AnimalId", connection);
                command.Parameters.AddWithValue("@AnimalId", animalId);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateZoo(Zoo zoo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Zoo SET Ubicacion = @Ubicacion WHERE Id = @ZooId", connection);
                command.Parameters.AddWithValue("@Ubicacion", zoo.Ubicacion);
                command.Parameters.AddWithValue("@ZooId", zoo.Id);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateAnimal(Animal animal)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Animal SET Nombre = @Nombre WHERE Id = @AnimalId", connection);
                command.Parameters.AddWithValue("@Nombre", animal.Nombre);
                command.Parameters.AddWithValue("@AnimalId", animal.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
