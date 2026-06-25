using Backend_API.data;
using Backend_API.models;
using MySql.Data.MySqlClient;

namespace Backend_API.repositories
{
    public class PatientRepository
    {
        private readonly DbConnection _db;

        public PatientRepository(
            DbConnection db
        )
        {
            _db = db;
        }

        public List<patient> GetAllPatients()
        {
            List<patient> patients =
            new List<patient>();

            using var conn =
            _db.CreateConnection();

            conn.Open();

            string query =
            "SELECT * FROM Patients";

            MySqlCommand cmd =
            new MySqlCommand(query, conn);

            using var reader =
            cmd.ExecuteReader();

            while(reader.Read())
            {
                patients.Add(
                new patient
                {
                    PatientId =
                    Convert.ToInt32(
                    reader["PatientId"]),

                    Name =
                    reader["Name"].ToString(),

                    Age =
                    Convert.ToInt32(
                    reader["Age"]),

                    Gender =
                    reader["Gender"].ToString(),

                    Phone =
                    reader["Phone"].ToString(),

                    Address =
                    reader["Address"].ToString(),

                    Status =
                    reader["Status"].ToString()
                });
            }

            return patients;
        }

        public void AddPatient(patient p)
        {
            using var conn =
            _db.CreateConnection();

            conn.Open();

            string query =
            @"INSERT INTO Patients
            (
                Name,
                Age,
                Gender,
                Phone,
                Address,
                Status
            )

            VALUES
            (
                @Name,
                @Age,
                @Gender,
                @Phone,
                @Address,
                @Status
            )";

            MySqlCommand cmd =
            new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue(
            "@Name", p.Name);

            cmd.Parameters.AddWithValue(
            "@Age", p.Age);

            cmd.Parameters.AddWithValue(
            "@Gender", p.Gender);

            cmd.Parameters.AddWithValue(
            "@Phone", p.Phone);

            cmd.Parameters.AddWithValue(
            "@Address", p.Address);

            cmd.Parameters.AddWithValue(
            "@Status", p.Status);

            cmd.ExecuteNonQuery();
        }

        public void UpdatePatient
        (
            int id,
            patient p
        )
        {
            using var conn =
            _db.CreateConnection();

            conn.Open();

            string query =
            @"UPDATE Patients

            SET
            Name=@Name,
            Age=@Age,
            Gender=@Gender,
            Phone=@Phone,
            Address=@Address,
            Status=@Status

            WHERE PatientId=@Id";

            MySqlCommand cmd =
            new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue(
            "@Id", id);

            cmd.Parameters.AddWithValue(
            "@Name", p.Name);

            cmd.Parameters.AddWithValue(
            "@Age", p.Age);

            cmd.Parameters.AddWithValue(
            "@Gender", p.Gender);

            cmd.Parameters.AddWithValue(
            "@Phone", p.Phone);

            cmd.Parameters.AddWithValue(
            "@Address", p.Address);

            cmd.Parameters.AddWithValue(
            "@Status", p.Status);

            cmd.ExecuteNonQuery();
        }

        public void DeletePatient(int id)
        {
            using var conn = _db.CreateConnection();
            conn.Open();

            using var trans = conn.BeginTransaction();
            try
            {
                // Delete associated appointments first
                string deleteApptQuery = "DELETE FROM Appointments WHERE PatientId=@Id";
                using var cmdAppt = new MySqlCommand(deleteApptQuery, conn, trans);
                cmdAppt.Parameters.AddWithValue("@Id", id);
                cmdAppt.ExecuteNonQuery();

                // Delete associated medical records first
                string deleteRecordQuery = "DELETE FROM MedicalRecords WHERE PatientId=@Id";
                using var cmdRecord = new MySqlCommand(deleteRecordQuery, conn, trans);
                cmdRecord.Parameters.AddWithValue("@Id", id);
                cmdRecord.ExecuteNonQuery();

                // Finally delete the patient
                string deletePatientQuery = "DELETE FROM Patients WHERE PatientId=@Id";
                using var cmdPatient = new MySqlCommand(deletePatientQuery, conn, trans);
                cmdPatient.Parameters.AddWithValue("@Id", id);
                cmdPatient.ExecuteNonQuery();

                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }
    }
}