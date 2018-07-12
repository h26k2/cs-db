using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_DB.Forms {
    public partial class InputModal : Form {

        private int id = 0;

        public InputModal(string title , string action) {
            InitializeComponent();
            this.Text = title;
            this.title.Text = title;
            this.action.Text = action;
        }

        private void button1_Click(object sender, EventArgs e) {

            string input = this.textBox1.Text;

            if(input.Length != 0 && !input.Contains(" ")) {
                id = Convert.ToInt32(input);
                this.Visible = false;
            }
            else {
                MessageBox.Show("Error occured! please don't use NULL values or SPACES");
            }


        }

        public int GetUserInput() {
            return id;
        }

    }
}
