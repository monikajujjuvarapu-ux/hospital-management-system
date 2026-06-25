using Backend_API.data;
using Backend_API.models;
using MySql.Data.MySqlClient;

namespace Backend_API.repositories
{
    public class AppointmentRepository
    {
        private readonly DbConnection _db;

        public AppointmentRepository(DbConnection db)
        {
            _db = db;
        }

        // ADD
        public void AddAppointment(Appointment a)
        {
            using var conn = _db.CreateConnection();
            conn.Open();

            string query = @"
                INSERT INTO Appointments
                (PatientId, DoctorId, AppointmentDate, AppointmentTime, Status)
                VALUES
                (@PatientId, @DoctorId, @AppointmentDate, @AppointmentTime, 'Scheduled')";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PatientId", a.PatientId);
            cmd.Parameters.AddWithValue("@DoctorId", a.DoctorId);
            cmd.Parameters.AddWithValue("@AppointmentDate", a.AppointmentDate);
            cmd.Parameters.AddWithValue("@AppointmentTime", a.AppointmentTime);

            cmd.ExecuteNonQuery();
        }

        // GET ALL
        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> list = new List<Appointment>();

            using var conn = _db.CreateConnection();
            conn.Open();

            string query = "SELECT * FROM Appointments";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Appointment
                {
                    AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                    PatientId = Convert.ToInt32(reader["PatientId"]),
                    DoctorId = Convert.ToInt32(reader["DoctorId"]),
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                    AppointmentTime = reader["AppointmentTime"].ToString(),
                    Status = reader["Status"].ToString()
                });
            }

            return list;
        }

        // CANCEL (SOFT DELETE)
        public void CancelAppointment(int id)
        {
            using var conn = _db.CreateConnection();
            conn.Open();

            string query = "UPDATE Appointments SET Status='Cancelled' WHERE AppointmentId=@id";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}