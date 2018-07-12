using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CS_DB.BackEnd;
using System.Collections;
using CS_DB.Forms;

namespace CS_DB {
    public partial class Form1 : Form {

        string std_id, std_name, std_email, std_course, std_batch;

        public Form1() {
            InitializeComponent();
            GetData();
        }


        private void button2_Click(object sender, EventArgs e) {

            //For Updating Data

            GetUserInputtedData();

            Operations op = new Operations(std_id, std_name, std_email, std_course, std_batch);

            if (op.CheckForUpdation()) {

                if (op.CheckForSpaces()) {

                    Forms.InputModal modal = new Forms.InputModal("Update Record","Update");
                    modal.ShowDialog();
                    int existing_id = modal.GetUserInput();
                    if(existing_id != 0) {
                        string temp_query = op.EstablishUpdateQuery();
                        if (temp_query[temp_query.Length - 1].Equals(',')) {
                            temp_query = temp_query.RemoveComa();
                        }

                        temp_query += String.Format(" where id = {0}", existing_id);

                        int affected = op.PerformAction(temp_query);

                        if(affected == 1) {
                            MessageBox.Show("Record Successufully Updated!");
                        }
                        else {
                            MessageBox.Show("Unable to Update record");
                        }
                    }
                    else {
                        MessageBox.Show("Unable to Update record");
                    }


                }
                else {
                    MessageBox.Show("SPACES are not allowed, You can only use SPACES in Name field. Please make" +
                        "sure that you don't use SPACES in text boxes excluding name ");
                }

            }
            else {
                MessageBox.Show("Please fill at least one field to update data");
            }

            ClearFields();
            GetData();
        }

        private void button3_Click(object sender, EventArgs e) {

            //for deleting data

            InputModal modal = new InputModal("Delete Record","Delete");
            modal.ShowDialog();
            int id = modal.GetUserInput();
            string temp_query = String.Format("delete from students where id = {0}", id);
            Database db = new Database();
            db.AddQuery(temp_query);
            int status = db.ExecuteQuery();
            db.CloseConnection();
            if(status == 1) {
                MessageBox.Show("Record Deleted Successfully!");
            }
            else {
                MessageBox.Show("Unable to delete record");
            }
            GetData();

        }

        private void button6_Click(object sender, EventArgs e) {
            ClearFields();
        }

        private void button5_Click(object sender, EventArgs e) {
            GetData();
        }

        private void button4_Click(object sender, EventArgs e) {

            InputModal modal = new InputModal("Find Records", "find");
            modal.ShowDialog();
            int returned_id = modal.GetUserInput();

            if(returned_id != 0) {
                string temp_query = String.Format("select * from students where id = {0}", returned_id);

                List<TextBox> TextBoxes = new List<TextBox>();

                TextBoxes.Add(textBox1);
                TextBoxes.Add(textBox2);
                TextBoxes.Add(textBox3);
                TextBoxes.Add(textBox4);
                TextBoxes.Add(textBox5);

                Database db = new Database();
                db.AddQuery(temp_query);
                db.GetResultSet(TextBoxes);
                db.CloseConnection();

            }

        }

        private void GetData() {

            Database db = new Database();
            string temp_query = "select * from students";
            db.AddQuery(temp_query);
            DataTable dt = db.GetResultSet();
            this.dataGridView1.DataSource = dt;
            db.CloseConnection();

        }
        

        private void button1_Click(object sender, EventArgs e) {

            //For Inserting Data

            GetUserInputtedData();

            Operations op = new Operations(std_id , std_name,std_email, std_course ,std_batch);

            if (op.CheckForNull()) {

                if (op.CheckForSpaces()) {

                    string temp_query = String.Format
                        ("insert into students values ( {0} , '{1}' , '{2}' , '{3}' , '{4}' ) ",
                        Convert.ToInt32(std_id) , std_name , std_email , std_course , std_batch);

                    int status = op.PerformAction(temp_query);

                    if(status == 1) {
                        MessageBox.Show("Hurray! Your record has been successfully added.");
                    }
                    else {
                        MessageBox.Show("Oops! we are unable to add record");
                    }

                }
                else {
                    MessageBox.Show("SPACES are not allowed, You can only use SPACES in Name field. Please make" +
                        "sure that you don't use SPACES in text boxes excluding name ");
                }

            }
            else {
                MessageBox.Show("NULL values are not allowed, please fill data in every field");
            }

            ClearFields();
            GetData();

        }

        private void ClearFields() {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }

        private void GetUserInputtedData() {

            std_id = textBox1.Text;
            std_name = textBox2.Text;
            std_email = textBox3.Text;
            std_course = textBox4.Text;
            std_batch = textBox5.Text;

        }


    }


    static class ExtensionMethods {
        public static string RemoveComa(this string query) {
            StringBuilder temp = new StringBuilder(query);
            temp.Remove(query.Length - 1, 1);
            temp.Insert(query.Length - 1, " ");
            query = temp.ToString();
            return query;
        }
    }

}
