using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace CS_DB.BackEnd {
    class Operations {

        string std_id, std_name, std_email, std_course, std_batch;

        public Operations(string first, string second, string third, string fourth, string fifth) {

            std_id = first;
            std_name = second;
            std_email = third;
            std_course = fourth;
            std_batch = fifth;

        }

        public Boolean CheckForNull() {

            if (std_id.Length != 0 && std_name.Length != 0 && std_email.Length != 0 && std_course.Length != 0 &&
                std_batch.Length != 0) {

                return true;
            }
            else {
                return false;
            }

        }

        public Boolean CheckForUpdation() {

            if (std_id.Length != 0 || std_name.Length != 0 || std_email.Length != 0 || std_course.Length != 0 ||
                std_batch.Length != 0) {

                return true;
            }
            else {
                return false;
            }

        }


        public Boolean CheckForSpaces() {

            if(!std_id.Contains(" ") && !std_email.Contains(" ") && !std_course.Contains(" ") 
                && !std_batch.Contains(" ")) {
                return true;
            }
            else {
                return false;
            }

        }

        public int PerformAction(string query) {

            int status = 0;
            Database db = new Database();
            db.AddQuery(query);
            status = db.ExecuteQuery();
            db.CloseConnection();
            return status;

        }

        public string EstablishUpdateQuery() {

            string query = "update students set ";
            try {
                if (std_id.Length != 0)
                    query += String.Format("id={0},", Convert.ToInt32(std_id));
                if (std_name.Length != 0)
                    query += String.Format("name='{0}',", std_name);
                if (std_email.Length != 0)
                    query += String.Format("email='{0}',", std_email);
                if (std_course.Length != 0)
                    query += String.Format("course='{0}',", std_course);
                if (std_batch.Length != 0)
                    query += String.Format("batch='{0}',", std_batch);

            }
            catch(Exception e) {
                MessageBox.Show("Error occured while establishing query " + e.Message);
            }

            return query;
        }



    }
}
