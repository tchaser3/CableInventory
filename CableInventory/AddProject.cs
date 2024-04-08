/* Title:           Add Project
 * Date:            5-28-16
 * Author:          Terry Holmes
 *
 * Description:     This is the form to add the project */

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
using ProjectsDLL;
using EventLogDLL;
using EmployeeDLL;
using KeyWordDLL;
using DataValidationDLL;

namespace CableInventory
{
    public partial class AddProject : Form
    {
        //setting up the classes
        MessagesClass TheMessagesClass = new MessagesClass();
        ProjectClass TheProjectClass = new ProjectClass();
        EventLogClass TheEventLogClass = new EventLogClass();
        EmployeeClass TheEmployeeClass = new EmployeeClass();
        KeyWordClass TheKeyWordClass = new KeyWordClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();

        //setting up the data
        InternalProjectDataSet TheInternalProjectsDataSet;
        EmployeeDataSet TheEmployeeDataSet;

        string mstrPartsWarehouse;
                
        public AddProject()
        {
            InitializeComponent();
        }

        private void AddProject_Load(object sender, EventArgs e)
        {
            //setting local varibles
            int intCounter;
            int intNumberOfRecords;
            string strFirstNameFromTable;
            string strLastNameFromTable;
            bool blnKeyWordNotFound;

            //this will load up the controls
            TheInternalProjectsDataSet = TheProjectClass.GetInternalProjectInfo();
            TheEmployeeDataSet = TheEmployeeClass.GetEmployeeInfo();

            //loading the controls
            txtMSRNumber.Text = Logon.mstrMSRNumber;
            txtProjectID.Text = Logon.mstrTWCProjectID;

            //loading variables
            mstrPartsWarehouse = TheEmployeeClass.FindEmployeeFirstNamewithID(Logon.mintPartsWarehouseID);
            intNumberOfRecords = TheEmployeeDataSet.employees.Rows.Count - 1;

            //running loop
            for(intCounter = 0; intCounter <= intNumberOfRecords; intCounter++)
            {
                //getting the variable
                strFirstNameFromTable = Convert.ToString(TheEmployeeDataSet.employees.Rows[intCounter][1]).ToUpper();
                strLastNameFromTable = Convert.ToString(TheEmployeeDataSet.employees.Rows[intCounter][2]).ToUpper();

                //if statement
                if(strLastNameFromTable == "WAREHOUSE")
                {
                    blnKeyWordNotFound = TheKeyWordClass.FindKeyWord(strFirstNameFromTable, mstrPartsWarehouse);

                    if(blnKeyWordNotFound == false)
                    {
                        mstrPartsWarehouse = strFirstNameFromTable;
                    }
                }
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //this will load the controls
            //setting local variables
            bool blnFatalError = false;
            bool blnThereIsAProblem = false;
            string strTWCProjectID;
            string strMSRNumber;
            string strProjectName;
            string strErrorMessage = "";

            try
            {
                //beginning data validation
                strTWCProjectID = txtProjectID.Text;
                blnFatalError = TheDataValidationClass.VerifyTextData(strTWCProjectID);
                if (blnFatalError == true)
                {
                    blnThereIsAProblem = true;
                    strErrorMessage = strErrorMessage + "The TWC Project ID Was Not Entered\n";
                }
                strMSRNumber = txtMSRNumber.Text;
                if((blnFatalError == true) && (Logon.mstrSelectedButton == "RECEIVE"))
                {
                    blnThereIsAProblem = true;
                    strErrorMessage = strErrorMessage + "The MSR Number Was Not Entered\n";
                }
                if(blnThereIsAProblem == true)
                {
                    TheMessagesClass.ErrorMessage(strErrorMessage);
                    return;
                }

                //getting the description
                strProjectName = txtProjectName.Text;

                //setting up to save the project
                InternalProjectDataSet.internalprojectsRow NewTableRow = TheInternalProjectsDataSet.internalprojects.NewinternalprojectsRow();

                //loading the required variables
                NewTableRow.internalProjectID = TheProjectClass.CreateInternalProjectID();
                NewTableRow.TWCControlNumber = strTWCProjectID;
                NewTableRow.MSRNumber = strMSRNumber;
                NewTableRow.ProjectName = strProjectName;
                NewTableRow.SupplyingWarehouse = mstrPartsWarehouse;

                //adding the row
                TheInternalProjectsDataSet.internalprojects.Rows.Add(NewTableRow);
                TheProjectClass.UpdateInternalProjectDB(TheInternalProjectsDataSet);

                this.Close();
            }
            catch (Exception Ex)
            {
                //message to user
                TheMessagesClass.ErrorMessage(Ex.Message);

                //creating event log entry
                TheEventLogClass.CreateEventLogEntry("Add Projects in Cable Inventory " + Ex.Message);
            }
        }
    }
}
