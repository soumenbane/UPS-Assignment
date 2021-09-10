using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UPS.Assignment.EmployeeManagement.ApplicationServices;

namespace UPS.Assignment.EmployeeManagement.UI
{

    /// <summary>
    /// This class will add and edit employee details.
    /// </summary>

    public partial class AddEmployee : Form
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }       
        private void btnSave_Click(object sender, EventArgs e)
        {
            string res = string.Empty;
            string id = txtIdNo.Text;

            var name = txtFullName.Text;
            var email = txtEmail.Text;
            var gender = comboBoxGender.Text;
            var status = comboBoxStatus.Text;

            using (EmployeeService emps = new EmployeeService())
            {
                if (string.IsNullOrEmpty(txtIdNo.Text))
                    res = emps.AddEmployee(name, email, gender, status);
                else
                    res = emps.EditEmployee(Convert.ToInt32(id), name, email, gender, status);

                if (res == "Success")
                    MessageBox.Show("Data Saved successfully", "info !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data has not been Saved", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            Validation(txtFullName, "Full name");
        }
        private void txtEmail_MouseLeave(object sender, EventArgs e)
        {
            EmailValidation(txtEmail, "Email");
        }

        #region Methods
        /// <summary>
        /// This method will set the values on controls received from the selected row.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="gender"></param>
        /// <param name="status"></param>

        public void LoadData(string id, string name, string email, string gender, string status)
        {
            txtIdNo.Text = id;
            txtFullName.Text = name;
            txtEmail.Text = email;
            comboBoxGender.Text = gender;
            comboBoxStatus.Text = status;

            txtIdNo.Enabled = false;
        }
        
        /// <summary>
        /// This method valid the textoBox full name, if you put a number it return an error
        /// </summary>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool Validation(TextBox t, string name)
        {
            int n;
            bool error = false;

            if (int.TryParse(t.Text, out n))
            {
                error = true;
                MessageBox.Show("Invalid character", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return error;

        }

        /// <summary>
        /// This method will check email format, and if format incorrect it return error
        /// </summary>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool EmailValidation(TextBox t, string name)
        {
            
            bool error = false;
            bool isEmail = Regex.IsMatch(t.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                error = true;
                MessageBox.Show("Invalid email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return error;

        }

        #endregion

       
    }
}
