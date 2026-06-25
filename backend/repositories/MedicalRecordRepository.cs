using Backend_API.data;
using Backend_API.models;
using MySql.Data.MySqlClient;

namespace Backend_API.repositories
{
    public class MedicalRecordRepository
    {
        private readonly DbConnection _db;

        public MedicalRecordRepository(DbConnection db)
        {
            _db = db;
        }

        // ADD
        public void AddMedicalRecord(MedicalRecord m)
        {
            using var conn = _db.CreateConnection();
            conn.Open();

            string query = @"
                INSERT INTO MedicalRecords
                (PatientId, Diagnosis, Description, Prescription, VisitDate)
                VALUES
                (@PatientId, @Diagnosis, @Description, @Prescription, @VisitDate)";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PatientId", m.PatientId);
            cmd.Parameters.AddWithValue("@Diagnosis", m.Diagnosis);
            cmd.Parameters.AddWithValue("@Description", m.Description);
            cmd.Parameters.AddWithValue("@Prescription", m.Prescription);
            cmd.Parameters.AddWithValue("@VisitDate", m.VisitDate);

            cmd.ExecuteNonQuery();
        }

        // GET ALL
        public List<MedicalRecord> GetAllMedicalRecords()
        {
            List<MedicalRecord> list = new List<MedicalRecord>();

            using var conn = _db.CreateConnection();
            conn.Open();

            string query = "SELECT * FROM MedicalRecords";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new MedicalRecord
                {
                    RecordId = Convert.ToInt32(reader["RecordId"]),
                    PatientId = Convert.ToInt32(reader["PatientId"]),
                    Diagnosis = reader["Diagnosis"].ToString(),
                    Description = reader["Description"].ToString(),
                    Prescription = reader["Prescription"].ToString(),
                    VisitDate = Convert.ToDateTime(reader["VisitDate"])
                });
            }

            return list;
        }

        // UPDATE
        public void UpdateMedicalRecord(MedicalRecord m)
        {
            using var conn = _db.CreateConnection();
            conn.Open();

            string query = @"
                UPDATE MedicalRecords
                SET 
                    Diagnosis = @Diagnosis,
                    Description = @Description,
                    Prescription = @Prescription,
                    VisitDate = @VisitDate
                WHERE RecordId = @RecordId";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@RecordId", m.RecordId);
            cmd.Parameters.AddWithValue("@Diagnosis", m.Diagnosis);
            cmd.Parameters.AddWithValue("@Description", m.Description);
            cmd.Parameters.AddWithValue("@Prescription", m.Prescription);
            cmd.Parameters.AddWithValue("@VisitDate", m.VisitDate);

            cmd.ExecuteNonQuery();
        }
    }
}