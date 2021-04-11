using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Class
{
    class ContactClass
    {
        //Getters and setters properties
        //Acts as data carrier in our application

        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //Select data from database
        public DataTable Select()
        {
            //step 1: database connection
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                //step 2: writting sql query
                string sql = "SELECT * FROM Econtact";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        // Insert Data into database
        public bool Insert(ContactClass c)
        {
            //creating default return type and setting its values to false
            bool isSuccess = false;
            //step 1: connect Database
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //Step 2: create a sql query to insert data
                string sql = "INSERT INTO Econtact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //creating sql commend using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creat parameter to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //open connection here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query successfully then value of rows will be generate than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Method to update data in database from our app
        public bool Update(ContactClass c)
        {
            //create a default return type and set its default value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //sql to update data in our database
                string sql = "UPDATE Econtact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
                //create sql commend
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameters to add values
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //open database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query rund successfylly then the value of rows will be grater than zero else values will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //method to delete data from database
        public bool Delete(ContactClass c)
        {
            //create a default values and set its values to false
            bool isSuccess = false;
            //create sql connection
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //sql to delete data
                string sql = "DELETE FROM Econtact WHERE ContactID=@ContactID";

                //create sql commend
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactId", c.ContactID);

                //open connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query rund successfylly then the value of rows will be grater than zero else values will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                //cloase connection
                conn.Close();
            }
            return isSuccess;
        }
    }
}
