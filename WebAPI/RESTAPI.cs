using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace WebAPI
{
    public class RESTAPI
    {
        string fullname, email, mobile, gender, birthdate;
        List<string> errors = new List<string>();
        public RESTAPI(string fullname, string email, string mobile, string birthdate, string gender)
        {
            this.fullname = fullname;
            this.email = email;
            this.mobile = mobile;
            this.birthdate = birthdate;
            this.gender = gender;
            doValidate();


        }
        private void doValidate()
        {

            Validate(new Regex(@"^[A-Za-z ,.]+$"), this.fullname, "Full name is missing or invalid format.");
            Validate(new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"), this.email, "Email address is missing or invalid.");
            Validate(new Regex(@"^(09|\+639)\d{9}$"), this.mobile, "Mobile must be valid 11 digit Philippine Mobile Number.Value starts with 09 or +639");
            Validate(new Regex(@"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$"), this.birthdate, "Birth Date is required and must follow YYYY-mm-dd format");
            Validate(new Regex(@"^M(ale)?$|^F(emale)?$"), this.gender, "Gender is required. Value accepted is Male or Female or M or F");

            if (errors.Count == 0)
                this.SaveToDb();


        }
        private void SaveToDb()
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost;Port=3306;Database=propelrr;UID=root;PWD=root;");
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                MySqlCommand cmd = new MySqlCommand("SaveData", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("fullname", this.fullname);
                cmd.Parameters.AddWithValue("email", this.email);
                cmd.Parameters.AddWithValue("mobile", this.mobile);
                cmd.Parameters.AddWithValue("birthdate", this.birthdate);
                cmd.Parameters.AddWithValue("age", this.computeAge());
                cmd.Parameters.AddWithValue("gender", this.gender);
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
        private void Validate(Regex reg, string reference, string error)
        {
            if (String.IsNullOrEmpty(reference) || !reg.IsMatch(reference))
                errors.Add(error);
        }
        private int computeAge()
        {
            DateTime bdate = new DateTime(int.Parse(this.birthdate.Substring(0, 4)), int.Parse(this.birthdate.Substring(5, 2)), int.Parse(this.birthdate.Substring(8, 2)));
            var today = DateTime.Today;
            var age = today.Year - bdate.Year;
            if (bdate.Date > today.AddYears(-age)) age--;
            return age;
        }
        public List<string> GetErrors()
        {
            return errors;
        }


    }
}
