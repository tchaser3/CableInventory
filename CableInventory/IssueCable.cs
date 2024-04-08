/* Title:           Issue Cable
 * Date:            5-30-16
 * Author:          Terry Holmes
 *
 * Description:     This form is used for issuing cable */

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
using DataValidationDLL;
using IssueCableDLL;
using PartNumberDLL;
using HandCoilDLL;
using CableInventoryDLL;
using TransactionTypeDLL;
using EventLogDLL;
using CableTransactionIDDLL;
using ProjectsDLL;
using EmployeeDLL;
using KeyWordDLL;

namespace CableInventory
{
    public partial class IssueCable : Form
    {
        //setting up the class
        MessagesClass TheMessagesClass = new MessagesClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();
        IssueCableClass TheIssueCableClass = new IssueCableClass();
        PartNumberClass ThePartNumberClass = new PartNumberClass();
        HandCoilClass TheHandCoilClass = new HandCoilClass();
        CableInventoryClass TheCableInventoryClass = new CableInventoryClass();
        TransactionTypeClass TheTransactionTypeClass = new TransactionTypeClass();
        PleaseWait PleaseWait = new PleaseWait();
        EventLogClass TheEventLog = new EventLogClass();
        CableTransactionIDClass TheCableTransactionIDClass = new CableTransactionIDClass();
        ProjectClass TheProjectClass = new ProjectClass();
        IssueCableDataSet TheIssueCableDataSet;
        EmployeeClass TheEmployeeClass = new EmployeeClass();
        EmployeeDataSet TheEmployeeDataSet;
        KeyWordClass TheKeyWordClass = new KeyWordClass();
        
        //setting up the data
        TransactionTypeDataSet TheTransactionTypeDataSet;
        PartNumbersDataSet ThePartNumbersDataSet;

        struct Employees
        {
            public int mintEmployeeID;
            public string mstrFirstName;
            public string mstrLastName;
        }

        //setting the part structure
        struct PartNumbers
        {
            public int mintPartID;
            public string mstrPartNumber;
            public string mstrDescription;
            public string mstrPartNoAndName;
        }

        //part structure variables
        PartNumbers[] ThePartNumbers;
        int mintPartCounter;
        int mintPartUpperLimit;

        Employees[] TheEmployees;
        int mintEmployeeCounter;
        int mintEmployeeUpperLimit;

        struct EmployeeResult
        {
            public int mintEmployeeID;
            public string mstrName;
        }

        EmployeeResult[] TheEmployeeResults;
        int mintEmployeeResultCounter;
        int mintEmployeeResultUpperLimit;

        //setting the global variables
        string mstrErrorMessage = "";
        int mintReelID;
        int mintWarehouseID;
        string mstrPartNumber;
        int mintPartID;
        string mstrReelID;
        string mstrProject;
        int mintFootage;
        DateTime mdatTransactionDate;
        
        public IssueCable()
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
            //this will show the main menu
            MainMenu MainMenu = new MainMenu();
            MainMenu.Show();
            this.Close();
        }
        private bool LoadComboBox()
        {
            //setting local variables
            bool blnFatalError = false;
            int intCounter;
            int intNumberOfRecords;

            try
            {
                //filling the data set
                TheTransactionTypeDataSet = TheTransactionTypeClass.GetTransactionTypeInfo();

                //getting ready for the loop
                intNumberOfRecords = TheTransactionTypeDataSet.transactiontype.Rows.Count - 1;
                cboReturnType.Items.Add("SELECT");
                cboReturnType.Items.Add("PEELED");

                //loop to fill the combo box
                for (intCounter = 0; intCounter <= intNumberOfRecords; intCounter++)
                {
                    cboReturnType.Items.Add(Convert.ToString(TheTransactionTypeDataSet.transactiontype.Rows[intCounter][1]).ToUpper());
                }

                cboReturnType.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                //creating the error message
                mstrErrorMessage = Ex.Message;

                //setting variable
                blnFatalError = true;
            }

            //setting return value
            return blnFatalError;
        }
        private void IssueCable_Load(object sender, EventArgs e)
        {
            //this will load the form
            //setting local variables
            bool blnFatalError = false;

            PleaseWait.Show();
            btnSave.Enabled = false;
            
            //setting controls
            txtWarehouseID.Text = Convert.ToString(Logon.mintPartsWarehouseID);
            mintWarehouseID = Logon.mintPartsWarehouseID;
            txtDate.Text = Convert.ToString(DateTime.Now);

            //setting up the data
            TheIssueCableDataSet = TheIssueCableClass.GetIssueCableInfo();

            //loading the combo box
            blnFatalError = LoadComboBox();
            if (blnFatalError == false)
                blnFatalError = LoadEmployeeStructure();
            if (blnFatalError == false)
                blnFatalError = LoadPartStructure();
            
            PleaseWait.Hide();

            //creating the data grid
            dgvEnteredTransactions.ColumnCount = 7;
            dgvEnteredTransactions.Columns[0].Name = "Transaction ID";
            dgvEnteredTransactions.Columns[0].Width = 75;
            dgvEnteredTransactions.Columns[1].Name = "Part Number";
            dgvEnteredTransactions.Columns[1].Width = 75;
            dgvEnteredTransactions.Columns[2].Name = "Warehouse ID";
            dgvEnteredTransactions.Columns[2].Width = 75;
            dgvEnteredTransactions.Columns[3].Name = "Project ID";
            dgvEnteredTransactions.Columns[3].Width = 100;
            dgvEnteredTransactions.Columns[4].Name = "Reel ID";
            dgvEnteredTransactions.Columns[4].Width = 100;
            dgvEnteredTransactions.Columns[5].Name = "Footage";
            dgvEnteredTransactions.Columns[5].Width = 75;
            dgvEnteredTransactions.Columns[6].Name = "Date";
            dgvEnteredTransactions.Columns[6].Width = 75;

            if (blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage(mstrErrorMessage);

                TheEventLog.CreateEventLogEntry("Receive Cable " + mstrErrorMessage);
            }
        }
        private bool LoadPartStructure()
        {
            //setting local variables
            bool blnFatalError = false;
            int intCounter;
            int intNumberOfRecords;
            string strPartNumber = "";
            bool blnIsNotTWCPartNumber;

            try
            {
                //loading the data set
                ThePartNumbersDataSet = ThePartNumberClass.GetPartNumbersInfo();
                
                //getting ready for the loop
                intNumberOfRecords = ThePartNumbersDataSet.partnumbers.Rows.Count - 1;
                ThePartNumbers = new PartNumbers[intNumberOfRecords + 1];
                mintPartCounter = 0;

                //loop to fill the structure
                for(intCounter = 0; intCounter <= intNumberOfRecords; intCounter++)
                {
                    strPartNumber = Convert.ToString(ThePartNumbersDataSet.partnumbers[intCounter].PartNumber);

                    //testing for twc part number
                    blnIsNotTWCPartNumber = ThePartNumberClass.CheckTimeWarnerPart(strPartNumber);

                    if (blnIsNotTWCPartNumber == false)
                    {
                        ThePartNumbers[mintPartCounter].mintPartID = ThePartNumbersDataSet.partnumbers[intCounter].PartID;
                        ThePartNumbers[mintPartCounter].mstrPartNumber = strPartNumber;
                        ThePartNumbers[mintPartCounter].mstrDescription = ThePartNumbersDataSet.partnumbers[intCounter].Description;
                        ThePartNumbers[mintPartCounter].mstrPartNoAndName = strPartNumber + " " + ThePartNumbers[mintPartCounter].mstrDescription;
                        mintPartUpperLimit = mintPartCounter;
                        mintPartCounter++;
                    }
                }

                mintPartCounter = 0;
            }
            catch (Exception Ex)
            {
                //loading up the event log
                TheEventLog.CreateEventLogEntry("Issue Cable Load Part Structure " + Ex.Message);

                mstrErrorMessage = Ex.Message;

                blnFatalError = true;
            }

            //returning value
            return blnFatalError;
        }
        private bool LoadEmployeeStructure()
        {
            //setting local variables
            bool blnFatalError = false;
            int intNumberOfRecords;
            int intCounter;
            string strActive;

            try
            {
                TheEmployeeDataSet = TheEmployeeClass.GetEmployeeInfo();

                //setting up for the loop
                intNumberOfRecords = TheEmployeeDataSet.employees.Rows.Count - 1;
                TheEmployees = new Employees[intNumberOfRecords + 1];
                TheEmployeeResults = new EmployeeResult[intNumberOfRecords + 1];
                mintEmployeeCounter = 0;

                //beginning loop
                for (intCounter = 0; intCounter <= intNumberOfRecords; intCounter++)
                {
                    //seeing the employee is active
                    strActive = Convert.ToString(TheEmployeeDataSet.employees.Rows[intCounter][5]).ToUpper();

                    if(strActive == "YES")
                    {
                        //loading the structure
                        TheEmployees[mintEmployeeCounter].mintEmployeeID = Convert.ToInt32(TheEmployeeDataSet.employees.Rows[intCounter][0]);
                        TheEmployees[mintEmployeeCounter].mstrFirstName = Convert.ToString(TheEmployeeDataSet.employees.Rows[intCounter][1]).ToUpper();
                        TheEmployees[mintEmployeeCounter].mstrLastName = Convert.ToString(TheEmployeeDataSet.employees.Rows[intCounter][2]).ToUpper();
                        mintEmployeeUpperLimit = mintEmployeeCounter;
                        mintEmployeeCounter++;
                    }
                }

                cboSelectEmployee.Items.Add("SELECT");
                cboSelectEmployee.SelectedIndex = 0;
                mintEmployeeCounter = 0;
            }
            catch (Exception Ex)
            {
                mstrErrorMessage = Ex.Message;

                blnFatalError = true;
            }

            //returning value
            return blnFatalError;
        }
        private void cboReturnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //checking other combo box
            if(cboSelectEmployee.Text != "SELECT")
            {
                btnSave.Enabled = true;
            }
        }
        private void CreateNewRecord()
        {
            //this will create a new record
            txtTransactionID.Text = Convert.ToString(TheCableTransactionIDClass.CreateInventoryTransasctionID());
            txtDate.Text = Convert.ToString(DateTime.Now);
        }
        private void ClearTextBoxes()
        {
            txtFootage.Text = "";
            txtPartNumber.Text = "";
            txtProjectID.Text = "";
            txtReelID.Text = "";
            txtTransactionID.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //setting local variables
            int intProjectID = 0;
            DateTime datTransactionDate = DateTime.Now;
            int intFootageFromTable;
            bool blnProjectExists;
            bool blnFatalError = false;
            
            //getting the footage of the table
           intFootageFromTable = TheCableInventoryClass.GetReelFootage(mintPartID, mintWarehouseID, mstrReelID);

            //checking to see if the reel was peeled
           if(cboReturnType.Text == "PEELED")
           {
                if (mintFootage > intFootageFromTable)
                {
                    TheMessagesClass.InformationMessage("The Amount Issued Is Larger Than On The Reel");
                    return;
                }
            }
            else
            {
                mintFootage = intFootageFromTable;
            }

            //filling boolean variables
            blnProjectExists = TheProjectClass.VerifyTWCProjectID(mstrProject);

            //if statements
            if (blnProjectExists == false)
            {
                Logon.mstrTWCProjectID = mstrProject;
                Logon.mstrSelectedButton = "ISSUE";

                AddProject AddProject = new AddProject();
                AddProject.ShowDialog();
            }

            intProjectID = TheProjectClass.FindProjectID(mstrProject);            

            //creating record entries
            blnFatalError = TheCableInventoryClass.SubtractFromReel(mintWarehouseID, mintPartID, mstrReelID, mintFootage);

            if (blnFatalError == false)
                blnFatalError = TheCableInventoryClass.IssueCableFromWH(mintPartID, mintWarehouseID, mintFootage);
                                 
            if(blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage("There Has been a Problem, Please Contact IT");
                return;
            }

            try
            {
                IssueCableDataSet.issuecableRow NewTableRow = TheIssueCableDataSet.issuecable.NewissuecableRow();

                NewTableRow.TransactionID = Convert.ToInt32(txtTransactionID.Text);
                NewTableRow.PartID = mintPartID;
                NewTableRow.WarehouseID = mintWarehouseID;
                NewTableRow.EmployeeID = Convert.ToInt32(txtEmployeeID.Text);
                NewTableRow.ProjectID = intProjectID;
                NewTableRow.ReelID = mstrReelID;
                NewTableRow.Footage = mintFootage;
                NewTableRow.Date = datTransactionDate;

                //if reel was issued
                if(cboReturnType.Text == "REEL")
                {
                    NewTableRow.ReelIssued = true;
                }
                else
                {
                    NewTableRow.ReelIssued = false;
                }

                //updating the table
                TheIssueCableDataSet.issuecable.Rows.Add(NewTableRow);
                TheIssueCableClass.UpdateIssueCableDB(TheIssueCableDataSet);

                LoadDataGrid();
                ClearControls();
                TheMessagesClass.InformationMessage("Transaction Saved");
                cboReturnType.SelectedIndex = 0;
                CreateNewRecord();
                cboSelectEmployee.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                TheEventLog.CreateEventLogEntry("Cable Inventory Receive Cable " + Ex.Message);
                TheMessagesClass.ErrorMessage(Ex.Message);
            }
         
        }
        private void ClearControls()
        {
            txtFootage.Text = "";
            txtPartNumber.Text = "";
            txtProjectID.Text = "";
            txtReelID.Text = "";
            txtTransactionID.Text = "";
        }
        private void LoadDataGrid()
        {
            //this will load the data grid
            string[] NewGridRow;

            NewGridRow = new string[] {txtTransactionID.Text, txtPartNumber.Text, txtWarehouseID.Text, txtProjectID.Text, txtReelID.Text, txtFootage.Text, txtDate.Text };
            dgvEnteredTransactions.Rows.Add(NewGridRow);
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            //this will close the data grid
            dgvEnteredTransactions.Rows.Clear();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //setting local variables
            int intEmployeeCounter;
            int intPartCounter;
            bool blnFatalError = false;
            bool blnThereIsAProblem = false;
            bool blnKeyWordNotFound = false;
            string strValueForValidation;
            string strLastNameForSearch;
            bool blnEmployeeFound = false;
            bool blnPartFound = false;
            bool blnReelIsFound = false;
            int intFootageFromTable = 0;
            int intPartID = 0;

            //setting the variable
            mstrErrorMessage = "";

            cboSelectEmployee.Items.Clear();
            cboSelectEmployee.Items.Add("SELECT");

            //performing data validation
            strLastNameForSearch = txtEnterLastName.Text;
            blnFatalError = TheDataValidationClass.VerifyTextData(strLastNameForSearch);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Last Name Was Not Entered\n";
            }
            mstrProject = txtProjectID.Text;
            blnFatalError = TheDataValidationClass.VerifyTextData(mstrProject);
            if (blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "Project ID Not Entered\n";
            }

            mstrPartNumber = txtPartNumber.Text;
            blnFatalError = TheDataValidationClass.VerifyTextData(mstrPartNumber);
            if (blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The TWC Part Number is not Correct\n";
            }
            
            mstrReelID = txtReelID.Text;
            blnFatalError = TheDataValidationClass.VerifyTextData(mstrReelID);
            if (blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Reel ID Was Not Entered\n";
            }
            strValueForValidation = txtFootage.Text;
            blnFatalError = TheDataValidationClass.VerifyIntegerData(strValueForValidation);
            if (blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Footage Entered is not an Integer\n";
            }
            else
            {
                mintFootage = Convert.ToInt32(strValueForValidation);
            }
            strValueForValidation = txtDate.Text;
            blnFatalError = TheDataValidationClass.VerifyDateData(strValueForValidation);
            if (blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Date Information is not a Date\n";
            }
            else
            {
                mdatTransactionDate = Convert.ToDateTime(strValueForValidation);
            }
            if (blnThereIsAProblem == true)
            {
                TheMessagesClass.ErrorMessage(mstrErrorMessage);
                return;
            }

            SetControlsReadOnly(true);

            mintEmployeeResultCounter = 0;

            //filling the combo box
            for(intEmployeeCounter = 0; intEmployeeCounter <= mintEmployeeUpperLimit; intEmployeeCounter++)
            {
                blnKeyWordNotFound = TheKeyWordClass.FindKeyWord(strLastNameForSearch, TheEmployees[intEmployeeCounter].mstrLastName);

                if(blnKeyWordNotFound == false)
                {
                    TheEmployeeResults[mintEmployeeResultCounter].mstrName = TheEmployees[intEmployeeCounter].mstrFirstName + " " + TheEmployees[intEmployeeCounter].mstrLastName;
                    TheEmployeeResults[mintEmployeeResultCounter].mintEmployeeID = TheEmployees[intEmployeeCounter].mintEmployeeID;
                    cboSelectEmployee.Items.Add(TheEmployeeResults[mintEmployeeResultCounter].mstrName);
                    mintEmployeeResultUpperLimit = mintEmployeeResultCounter;
                    mintEmployeeResultCounter++;
                    blnEmployeeFound = true;
                }
            }

            if(blnEmployeeFound == false)
            {
                TheMessagesClass.InformationMessage("The Employee Was Not Found");
                return;
            }

            //setting the employee combo box
            mintEmployeeResultCounter = 0;
            cboSelectEmployee.SelectedIndex = 0;
            
            //checking parts 
            // The part number is searched by first part number and then by description
             
            for(intPartCounter = 0; intPartCounter <= mintPartUpperLimit; intPartCounter++)
            {
                blnPartFound = false;
                blnKeyWordNotFound = TheKeyWordClass.FindKeyWord(mstrPartNumber, ThePartNumbers[intPartCounter].mstrPartNumber);

                if(blnKeyWordNotFound == false)
                {
                    intPartID = ThePartNumbers[intPartCounter].mintPartID;
                    blnPartFound = true;
                }
                if(blnKeyWordNotFound == true)
                {
                    blnKeyWordNotFound = TheKeyWordClass.FindKeyWord(mstrPartNumber, ThePartNumbers[intPartCounter].mstrDescription);

                    if(blnKeyWordNotFound == false)
                    {
                        intPartID = ThePartNumbers[intPartCounter].mintPartID;
                        blnPartFound = true;
                    }
                }

                if(blnPartFound == true)
                {
                    blnReelIsFound = TheCableInventoryClass.VerifyReelExists(mstrReelID, intPartID, mintWarehouseID);

                    if (blnReelIsFound == true)
                    {
                        mintPartID = intPartID;

                        intFootageFromTable = TheCableInventoryClass.GetReelFootage(mintPartID, mintWarehouseID, mstrReelID);

                        if(intFootageFromTable < mintFootage)
                        {
                            TheMessagesClass.InformationMessage("The Footage Issued is Larger Than Remaing On Reel\nPlease Confirm Your Footage Before Continuing");
                            txtFootage.ReadOnly = false;
                        }
                    }
                }
            }

            //getting the id
            txtTransactionID.Text = Convert.ToString(TheCableTransactionIDClass.CreateInventoryTransasctionID());
        }
       
        private void cboSelectEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            //setting local variables
            int intCounter;
            string strNameForSearch = cboSelectEmployee.Text;

            if(cboSelectEmployee.Text != "SELECT")
            {
                for(intCounter = 0; intCounter <= mintEmployeeResultUpperLimit; intCounter++)
                {
                    if(strNameForSearch == TheEmployeeResults[intCounter].mstrName)
                    {
                        txtEmployeeID.Text = Convert.ToString(TheEmployeeResults[intCounter].mintEmployeeID);
                        btnSave.Enabled = true;
                    }
                }
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        private void SetControlsReadOnly(bool valueBoolean)
        {
            txtDate.ReadOnly = valueBoolean;
            txtEmployeeID.ReadOnly = valueBoolean;
            txtEnterLastName.ReadOnly = valueBoolean;
            txtFootage.ReadOnly = valueBoolean;
            txtPartNumber.ReadOnly = valueBoolean;
            txtProjectID.ReadOnly = valueBoolean;
            txtReelID.ReadOnly = valueBoolean;
            txtTransactionID.ReadOnly = valueBoolean;
            txtWarehouseID.ReadOnly = valueBoolean;
        }

        private void btnDisplayCable_Click(object sender, EventArgs e)
        {
            //this will display the cable
            DisplayCable DisplayCable = new DisplayCable();
            DisplayCable.ShowDialog();
        }
    }
    
}
