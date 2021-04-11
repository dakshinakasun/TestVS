using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Class.ContactClass c = new Class.ContactClass();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get the value from the inpute fileds
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNumber.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;


            //Insert data into data using the method we created
            bool success = c.Insert(c);
            if (success == true)
            {
                //successfuly inserted
                MessageBox.Show("New Contact Inserted Successfully");
                //call the clear method
                Clear();
            }
            else
            {
                //Faild to add contact
                MessageBox.Show("Failed to Add new Contact... Try Again...!");
            }

            //Load data on data grid view
            DataTable dt = c.Select();
            dataGridView1.DataSource = dt;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load data on data grid view
            DataTable dt = c.Select();
            dataGridView1.DataSource = dt;
        }

        //method to clear fields
        public void Clear()
        {
            textBoxContactID.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxContactNumber.Text = "";
            textBoxAddress.Text = "";
            comboBoxGender.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the data from textboxes
            c.ContactID = int.Parse(textBoxContactID.Text);
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNumber.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;

            //update data in database
            bool success = c.Update(c);
            if (success == true)
            {
                //Uplode successfully
                MessageBox.Show("Contact has been updated.");

                //Load data on data grid view
                DataTable dt = c.Select();
                dataGridView1.DataSource = dt;

                //call the clear method
                Clear();
            }
            else
            {
                //Failed to updated
                MessageBox.Show("Failed to updated... Tey Again...!");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from data grid view and load the textboxes 
            int rowIndex = e.RowIndex;
            textBoxContactID.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxContactNumber.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxAddress.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
            comboBoxGender.Text = dataGridView1.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Call clare method
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            //get data from the textboxes
            c.ContactID = Convert.ToInt32(textBoxContactID.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                //Successfully Deleted
                MessageBox.Show("Contact Deleted Successfully");

                //refresh data grid view
                //Load data on data grid view
                DataTable dt = c.Select();
                dataGridView1.DataSource = dt;

                //Call clear method
                Clear();
            }
            else
            {
                //Failed to deleted
                MessageBox.Show("Failed to Delete Contact... Try Again...!");
            }
        }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //get the value from textbox
            string keyword = textBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstring);
            SqlDataAdapter oda = new SqlDataAdapter("SELECT * FROM Econtact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
