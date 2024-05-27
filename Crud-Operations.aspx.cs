using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Employee_CRUD_Operations
{
    public partial class Crud_Operations : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VQEM8ES;Initial Catalog=CRUD-operations;Integrated Security=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DisplayData();
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    Debug.WriteLine(ex.Message);
                    string alertMessage = $"alert('{ex.Message}');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerScript", alertMessage, true);
                }
            }
        }

        protected void InsertDetails(object sender, EventArgs e)
        {
            try
            {
                int employeeId = Convert.ToInt32(employee_id.Text); // Assuming employee ID is an integer
                var employeeName = employee_name.Text;
                var employeeEmail = employee_email_id.Text;
                long employeeMobileNo = Convert.ToInt64(employee_mobile_no.Text);
                DateTime employeeDOB = Convert.ToDateTime(employee_dob.Text);

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Employee_Details (Employee_ID, Employee_Name, Employee_Email_ID, Employee_Mobile_No, Employee_DOB) VALUES (@EmployeeId, @EmployeeName, @EmployeeEmail, @EmployeeMobileNo, @EmployeeDOB)", con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@EmployeeName", employeeName);
                    cmd.Parameters.AddWithValue("@EmployeeEmail", employeeEmail);
                    cmd.Parameters.AddWithValue("@EmployeeMobileNo", employeeMobileNo);
                    cmd.Parameters.AddWithValue("@EmployeeDOB", employeeDOB);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Debug.WriteLine(ex.Message);
                string alertMessage = $"alert('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerScript1", alertMessage, true);
            }
            finally
            {
                con.Close();
                DisplayData();
            }
        }

        protected void UpdateDetails(object sender, EventArgs e)
        {
            try
            {
                int employeeId = Convert.ToInt32(((sender as Button).NamingContainer.FindControl("employee_id") as TextBox).Text);
                var employeeName = ((sender as Button).NamingContainer.FindControl("employee_name") as TextBox).Text;
                var employeeEmail = ((sender as Button).NamingContainer.FindControl("employee_email_id") as TextBox).Text;
                long employeeMobileNo = Convert.ToInt64(((sender as Button).NamingContainer.FindControl("employee_mobile_no") as TextBox).Text);
                DateTime employeeDOB = Convert.ToDateTime(((sender as Button).NamingContainer.FindControl("employee_dob") as TextBox).Text);
                DateTime currentDate = DateTime.Now;

                using (SqlCommand cmd = new SqlCommand("UPDATE Employee_Details SET Employee_Name=@EmployeeName, Employee_Email_ID=@EmployeeEmail, Employee_Mobile_No=@EmployeeMobileNo, Employee_DOB=@EmployeeDOB, Last_Modified_Date=@CurrentDate WHERE Employee_ID=@EmployeeId AND IsDeleted = 0", con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@EmployeeName", employeeName);
                    cmd.Parameters.AddWithValue("@EmployeeEmail", employeeEmail);
                    cmd.Parameters.AddWithValue("@EmployeeMobileNo", employeeMobileNo);
                    cmd.Parameters.AddWithValue("@EmployeeDOB", employeeDOB);
                    cmd.Parameters.AddWithValue("@CurrentDate", currentDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Debug.WriteLine(ex.Message);
                string alertMessage = $"alert('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerScript2", alertMessage, true);
            }
            finally
            {
                con.Close();
                DisplayData();
            }
        }

        protected void DeleteRecords(object sender, EventArgs e)
        {
            try
            {
                int employeeId = Convert.ToInt32(((sender as Button).NamingContainer.FindControl("employee_id") as TextBox).Text);

                using (SqlCommand cmd = new SqlCommand("UPDATE Employee_Details SET IsDeleted = 1 WHERE Employee_ID = @EmployeeId", con)) //soft delete method is used for incase of recovery
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Debug.WriteLine(ex.Message);
                string alertMessage = $"alert('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerScript3", alertMessage, true);
            }
            finally
            {
                con.Close();
                DisplayData();
            }
        }

        protected void DisplayData()
        {
            try
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Employee_ID, Employee_Name, Employee_Email_ID, Employee_Mobile_No, Employee_DOB,Last_Modified_Date\r\nFROM Employee_Details\r\nWHERE IsDeleted = 0\r\n", con))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvEmployee.DataSource = dt;
                    gvEmployee.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Debug.WriteLine(ex.Message);
                string alertMessage = $"alert('{ex.Message}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerScript4", alertMessage, true);
            }
        }

    }
}
