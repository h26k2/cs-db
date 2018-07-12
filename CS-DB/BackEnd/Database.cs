using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace CS_DB.BackEnd {
    class Database {

        private SqlConnection connnection;
        private SqlCommand command;
        private SqlDataReader reader;
         
        public Database() {
            EstablishConnection();
        }

        private void EstablishConnection() {
            try {

                connnection = new SqlConnection
                ("Server = H26K2 ; Database = csdb ;Trusted_Connection = true");
                connnection.Open();

            }
            catch(Exception e) {
                MessageBox.Show("Error occured while establishing connection to database" +
                    e.Message);
            }
        }


        public void AddQuery(string query) {

            try {

                command = new SqlCommand(query, connnection);

            }
            catch(Exception e) {
                MessageBox.Show("Error occured while adding query " + e.Message);
            }

        }

        public int ExecuteQuery() {

            int status = 0;

            try {
                status = command.ExecuteNonQuery();
            }
            catch(Exception e) {
                MessageBox.Show("Error occured while executing query " + e.Message);
            }

            return status;

        }

        public void CloseConnection() {
            try {
                connnection.Close();
            }
            catch(Exception e) {
                MessageBox.Show("Error occured while closing connection " + e.Message);
            }
        }


        public DataTable GetResultSet() {

            DataTable dt = new DataTable();

            try {

                reader = command.ExecuteReader();
                dt.Load(reader);

            }
            catch(Exception e) {
                MessageBox.Show("Error occured while getting resultset " + e.Message);
            }

            return dt;

        }

        public void GetResultSet(List<TextBox> boxes) {
            try {

                reader = command.ExecuteReader();
                if (reader.Read()) {

                    boxes[0].Text = reader.GetInt32(0).ToString();
                    boxes[1].Text = reader.GetString(1);
                    boxes[2].Text = reader.GetString(2);
                    boxes[3].Text = reader.GetString(3);
                    boxes[4].Text = reader.GetString(4);

                }
                else {
                    MessageBox.Show("Oops, we are unable to find records ");
                }
            }
            catch(Exception e) {
                MessageBox.Show("Error occured while getting result " + e.Message);
            }
        }



    }
}
