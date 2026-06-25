using Backend_API.data;
using Backend_API.models;
using MySql.Data.MySqlClient;

namespace Backend_API.repositories
{
    public class DoctorRepository
    {
        private readonly DbConnection _db;

        public DoctorRepository(DbConnection db)
        {
            _db = db;
        }

        // ADD DOCTOR
        public void AddDoctor(doctor d)
        {
            using var conn = _db.CreateConnection();
            conn.Open();

            string query = @"
                INSERT INTO Doctors
                (DoctorName, Specialization, Phone, Email)
                VALUES
                (@DoctorName, @Specialization, @Phone, @Email)";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@DoctorName", d.DoctorName);
            cmd.Parameters.AddWithValue("@Specialization", d.Specialization);
            cmd.Parameters.AddWithValue("@Phone", d.Phone);
            cmd.Parameters.AddWithValue("@Email", d.Email);

            cmd.ExecuteNonQuery();
        }

        // GET ALL DOCTORS
        public List<doctor> GetAllDoctors()
        {
            List<doctor> list = new List<doctor>();

            using var conn = _db.CreateConnection();
            conn.Open();

            string query = "SELECT * FROM Doctors";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new doctor
                {
                    DoctorId = Convert.ToInt32(reader["DoctorId"]),
                    DoctorName = reader["DoctorName"].ToString(),
                    Specialization = reader["Specialization"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString()
                });
            }

            return list;
        }
    }
}