using System;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using UPS.Assignment.EmployeeManagement.ApplicationServices;
using UPS.Assignment.EmployeeManagement.Core;
using UPS.Assignment.EmployeeManagement.Core.Model;


namespace UPS.Assignment.EmployeeManagement.UI
{
    public partial class EmployeeDetails : Form
    {
        private int CurrentPage = 1;
        int PagesCount = 1;
        public EmployeeDetails()
        {
          //  string setting = ConfigurationManager.AppSettings["baseurl"];
            InitializeComponent();
            using (EmployeeService emps = new EmployeeService())
            {
                var res = emps.GetEmployee();
                LoadEmployees(res);
                CurrentPage = 1;
                RefreshPagination();
            }

        }

        #region PageEvents

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            var addEmp = new AddEmployee();
            addEmp.ShowDialog();
        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                var row = dataGridViewEmp.CurrentCell.RowIndex;
                var id = Convert.ToString(dataGridViewEmp.Rows[row].Cells["ID"].Value);
                var name = Convert.ToString(dataGridViewEmp.Rows[row].Cells["FullName"].Value);
                var email = Convert.ToString(dataGridViewEmp.Rows[row].Cells["Email"].Value);
                var gender = Convert.ToString(dataGridViewEmp.Rows[row].Cells["Gender"].Value);
                var status = Convert.ToString(dataGridViewEmp.Rows[row].Cells["Status"].Value);

                var addEmp = new AddEmployee();
                addEmp.LoadData(id, name, email, gender, status);
                addEmp.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = dataGridViewEmp.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewEmp.Rows[selectedrowindex];
                string cellValue = Convert.ToString(selectedRow.Cells["ID"].Value);

                if (string.IsNullOrEmpty(cellValue))
                    MessageBox.Show("Please select a valid row to delete", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);

                using (EmployeeService emps = new EmployeeService())
                {
                    var res = emps.DeleteEmployee(Convert.ToInt32(cellValue));
                    if (res == "Success")
                        MessageBox.Show("Data deleted successfully", "info !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data has not been deleted", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSearch.Clear();
                    LoadEmployees(emps.GetEmployee());
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            ExportDataToCSV();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (EmployeeService emps = new EmployeeService())
            {
                var res = emps.SearchEmployee(txtSearch.Text);
                LoadEmployees(res);
            }
        }

        private void exportData_Click(object sender, EventArgs e)
        {
            ExportDataToCSV();
        }
        #endregion

        #region "Page bind methods"
        /// <summary>
        /// This methos will select all rows in gridview and export them to tab delimited CSV file.
        /// </summary>
        public void ExportDataToCSV()
        {

            dataGridViewEmp.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

            dataGridViewEmp.SelectAll();
            DataObject dataObject = dataGridViewEmp.GetClipboardContent();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Employee Data";
            saveFileDialog.Filter = "CSV file(*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, dataObject.GetText());
            }
            dataGridViewEmp.ClearSelection();
        }
        /// <summary>
        /// Load api response data in grid view
        /// </summary>
        /// <param name="response"></param>
        public void LoadEmployees(EmployeeResponseData response)
        {
            dataGridViewEmp.Rows.Clear();
            if (response != null)
            {
                if (response.MetaData.Pagination.TotalCount > 0)
                {
                    PagesCount = response.MetaData.Pagination.NoOfPages;

                    foreach (Employee emp in response.EmployeeDetails)
                    {
                        var count = dataGridViewEmp.Rows.Count - 1;
                        dataGridViewEmp.Rows.Add();
                        dataGridViewEmp.Rows[count].Cells[0].Value = emp.EmployeeID;
                        dataGridViewEmp.Rows[count].Cells[1].Value = emp.Name;
                        dataGridViewEmp.Rows[count].Cells[2].Value = emp.Email;
                        dataGridViewEmp.Rows[count].Cells[3].Value = emp.Gender;
                        dataGridViewEmp.Rows[count].Cells[4].Value = emp.Status;
                        count++;
                    }
                }
            }
        }

        #endregion'

        #region Pagination

        private void RefreshPagination()
        {
            ToolStripButton[] items = new ToolStripButton[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 };

            //pageStartIndex contains the first button number of pagination.
            int pageStartIndex = 1;

            if (PagesCount > 5 && CurrentPage > 2)
                pageStartIndex = CurrentPage - 2;

            if (PagesCount > 5 && CurrentPage > PagesCount - 2)
                pageStartIndex = PagesCount - 4;

            for (int i = pageStartIndex; i < pageStartIndex + 5; i++)
            {
                if (i > PagesCount)
                {
                    items[i - pageStartIndex].Visible = false;
                }
                else
                {
                    //Changing the page numbers
                    items[i - pageStartIndex].Text = i.ToString(CultureInfo.InvariantCulture);

                    //Setting the Appearance of the page number buttons
                    if (i == CurrentPage)
                    {
                        items[i - pageStartIndex].BackColor = Color.Black;
                        items[i - pageStartIndex].ForeColor = Color.White;
                    }
                    else
                    {
                        items[i - pageStartIndex].BackColor = Color.White;
                        items[i - pageStartIndex].ForeColor = Color.Black;
                    }
                }
            }

            //Enabling or Disalbing pagination first, last, previous , next buttons
            if (CurrentPage == 1)
                btnBackward.Enabled = btnFirst.Enabled = false;
            else
                btnBackward.Enabled = btnFirst.Enabled = true;

            if (CurrentPage == PagesCount)
                btnForward.Enabled = btnLast.Enabled = false;

            else
                btnForward.Enabled = btnLast.Enabled = true;
        }

        //Method that handles the pagination button clicks
        private void ToolStripButtonClick(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton ToolStripButton = ((ToolStripButton)sender);

                //Determining the current page
                if (ToolStripButton == btnBackward)
                    CurrentPage--;
                else if (ToolStripButton == btnForward)
                    CurrentPage++;
                else if (ToolStripButton == btnLast)
                    CurrentPage = PagesCount;
                else if (ToolStripButton == btnFirst)
                    CurrentPage = 1;
                else
                    CurrentPage = Convert.ToInt32(ToolStripButton.Text, CultureInfo.InvariantCulture);

                if (CurrentPage < 1)
                    CurrentPage = 1;
                else if (CurrentPage > PagesCount)
                    CurrentPage = PagesCount;

                //Rebind the Datagridview with the data.
                RebindGridForPageChange(CurrentPage);

                //Change the pagiantions buttons according to page number
                RefreshPagination();
            }
            catch (Exception) { }
        }

        private void RebindGridForPageChange(int CurrentPage)
        {
            using (EmployeeService emps = new EmployeeService())
            {
                var res = emps.GetEmployeePagewise(CurrentPage);
                LoadEmployees(res);
                CurrentPage = 1;
                RefreshPagination();
            }
        }


        #endregion
    }
}
