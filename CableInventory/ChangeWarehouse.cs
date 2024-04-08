/* Title:           Change Warehouse
 * Date:            6-26-16
 * Author:          Terry Holmes
 *
 * Description:     This form will change the warehouse */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagesDLL;
using EmployeeDLL;
using KeyWordDLL;

namespace CableInventory
{
    public partial class ChangeWarehouse : Form
    {
        //setting the classes
        MessagesClass TheMessagesClass = new MessagesClass();
        EmployeeClass TheEmployeeClass = new EmployeeClass();
        KeyWordClass TheKeyWordClass = new KeyWordClass();

        //setting the data set
        EmployeeDataSet TheEmployeeDataSet;

        public ChangeWarehouse()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this will close the program
            TheMessagesClass.CloseTheProgram();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            MainMenu MainMenu = new MainMenu();
            MainMenu.Show();
            this.Close();
        }

        private void btnAdminMenu_Click(object sender, EventArgs e)
        {
            AdminMenu AdminMenu = new AdminMenu();
            AdminMenu.Show();
            this.Close();
        }
        private bool LoadComboBox()
        {
            //setting local variables
            bool blnFatalError = false;
            int intCounter;
            int intNumberOfRecords;
            bool blnKeyWordNotFound;
            string strFirstName;
            string strLastName;
            string strActive;

            //setting up the data
            TheEmployeeDataSet = TheEmployeeClass.GetEmployeeInfo();

            //getting the number of records
            intNumberOfRecords = TheEmployeeClass.EmployeeNumberOfRecords();
            cboWarehouse.Items.Add("SELECT");

            //loop
            for (intCounter = 0; intCounter <= intNumberOfRecords; intCounter++)
            {
                //getting the variables
                strLastName = Convert.ToString(TheEmployeeDataSet.employees.Rows[intCounter][2]).ToUpper();
                strFirstName = Convert.ToString(TheEmployeeDataSet.employees.Rows[intCounter][1]).ToUpper();
                strActive = Convert.ToString(TheEmployeeDataSet.employees.Rows[intCounter][5]).ToUpper();

                //if statements
                if (strLastName == "PARTS")
                    if (strActive == "YES")
                    {
                        blnKeyWordNotFound = TheKeyWordClass.FindKeyWord("TWC", strFirstName);

                        if (blnKeyWordNotFound == false)
                        {
                            cboWarehouse.Items.Add(strFirstName);
                        }
                    }
            }

            //setting the selected index
            cboWarehouse.SelectedIndex = 0;

            //return to calling method
            return blnFatalError;
        }

        private void ChangeWarehouse_Load(object sender, EventArgs e)
        {
            //this will load the combo box
            LoadComboBox();
        }

        private void btnChangeWarehouse_Click(object sender, EventArgs e)
        {
            //this will load the variable
            if(cboWarehouse.Text == "SELECT")
            {
                TheMessagesClass.ErrorMessage("The Combo Box Was Set to Select");
                return;
            }

            //getting the new warehouse id
            Logon.mintPartsWarehouseID = TheEmployeeClass.GetEmployeeID(cboWarehouse.Text, "PARTS");

            TheMessagesClass.InformationMessage("The Warehouse Has Been Changed");
        }
    }
}
