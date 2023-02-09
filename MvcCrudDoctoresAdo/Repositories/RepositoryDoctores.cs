using MvcCrudDoctoresAdo.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcCrudDoctoresAdo.Repositories
{
    public class RepositoryDoctores
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryDoctores()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023;TrustServerCertificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Doctor> GetDoctores()
        {
            string sql = "SELECT * FROM DOCTOR";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<Doctor> doctores = new List<Doctor>();
            while (this.reader.Read())
            {
                Doctor doc = new Doctor();
                doc.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                doc.IdDoctor = int.Parse(this.reader["DOCTOR_NO"].ToString());
                doc.Apellido = this.reader["APELLIDO"].ToString();
                doc.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                doc.Salario = int.Parse(this.reader["SALARIO"].ToString());
                doctores.Add(doc);
            }
            this.reader.Close();
            this.cn.Close();
            return doctores;
        }

        public List<Hospital> GetHospitales()
        {
            string sql = "SELECT HOSPITAL_COD, NOMBRE FROM HOSPITAL";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<Hospital> hospitales = new List<Hospital>();
            while (this.reader.Read())
            {
                Hospital hos = new Hospital();
                hos.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                hos.Nombre = this.reader["NOMBRE"].ToString();
                hospitales.Add(hos);
            }
            this.reader.Close();
            this.cn.Close();
            return hospitales;
        } 

        public void CrearDoctor(int idhospital, int iddoctor, string apellido, string especialidad, int salario)
        {
            string sql = "INSERT INTO DOCTOR VALUES (@IDHOSPITAL, @IDDOCTOR, @APELLIDO, @ESPECIALIDAD, @SALARIO)";

            SqlParameter pamidhos = new SqlParameter("@IDHOSPITAL", idhospital);
            this.com.Parameters.Add(pamidhos);

            SqlParameter pamiddoc = new SqlParameter("@IDDOCTOR", iddoctor);
            this.com.Parameters.Add(pamiddoc);

            SqlParameter pamapellido = new SqlParameter("@APELLIDO", apellido);
            this.com.Parameters.Add(pamapellido);

            SqlParameter pamesp = new SqlParameter("@ESPECIALIDAD", especialidad);
            this.com.Parameters.Add(pamesp);

            SqlParameter pamsalario = new SqlParameter("@SALARIO", salario);
            this.com.Parameters.Add(pamsalario);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public Doctor FindDoctor(int id)
        {
            string sql = "SELECT * FROM DOCTOR WHERE DOCTOR_NO = @IDDOCTOR";

            SqlParameter pamid = new SqlParameter("@IDDOCTOR", id);
            this.com.Parameters.Add(pamid);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.reader.Read();
            Doctor doc = new Doctor();
            doc.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
            doc.IdDoctor = int.Parse(this.reader["DOCTOR_NO"].ToString());
            doc.Apellido = this.reader["APELLIDO"].ToString();
            doc.Especialidad = this.reader["ESPECIALIDAD"].ToString();
            doc.Salario = int.Parse(this.reader["SALARIO"].ToString());
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return doc;
        }

        public void UpdateDoctor(int idHospital, int idDoctor, string apellido, string especialidad, int salario)
        {
            string sql = "UPDATE DOCTOR SET HOSPITAL_COD = @IDHOSPITAL, APELLIDO = @NOMBRE, ESPECIALIDAD = @ESP, SALARIO = @SALARIO WHERE DOCTOR_NO = @ID";

            SqlParameter pamidhospital = new SqlParameter("@IDHOSPITAL", idHospital);
            this.com.Parameters.Add(pamidhospital);

            SqlParameter pamiddoctor = new SqlParameter("@ID", idDoctor);
            this.com.Parameters.Add(pamiddoctor);

            SqlParameter pamapellido = new SqlParameter("@NOMBRE", apellido);
            this.com.Parameters.Add(pamapellido);

            SqlParameter pamesp = new SqlParameter("@ESP", especialidad);
            this.com.Parameters.Add(pamesp);

            SqlParameter pamsalario = new SqlParameter("@SALARIO", salario);
            this.com.Parameters.Add(pamsalario);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void DeleteDoctor(int id)
        {
            string sql = "DELETE FROM DOCTOR WHERE DOCTOR_NO = @ID";

            SqlParameter pamid = new SqlParameter("@ID", id);
            this.com.Parameters.Add(pamid);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
