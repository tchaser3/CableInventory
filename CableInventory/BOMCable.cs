/* Title:           BOM Cable
 * Date:            6-20-16
 * Author:          Terry Holmes
 *
 * Description:     This is the form for BOM Cable */

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
using BOMCableDLL;
using PartNumberDLL;
using CableInventoryDLL;
using TransactionTypeDLL;
using EventLogDLL;
using CableTransactionIDDLL;
using ProjectsDLL;

namespace CableInventory
{
    public partial class BOMCable : Form
    {
        //setting the classes up
        MessagesClass TheMessagesClass = new MessagesClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();
        BOMCableClass TheBOMCableClass = new BOMCableClass();
        PartNumberClass ThePartNumberClass = new PartNumberClass();
        CableInventoryClass TheCableInventoryClass = new CableInventoryClass();
        TransactionTypeClass TheTransactionTypeClass = new TransactionTypeClass();
        PleaseWait PleaseWait = new PleaseWait();
        EventLogClass TheEventLog = new EventLogClass();
        CableTransactionIDClass TheCableTransactionIDClass = new CableTransactionIDClass();
        ProjectClass TheProjectClass = new ProjectClass();
        BOMCableDataSet TheBOMCableDataSet;

        //setting the global variables
        string mstrErrorMessage = "";
        int mintWarehouseID;

        public BOMCable()
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

        private void BOMCable_Load(object sender, EventArgs e)
        {
            //this will load the data set
            TheBOMCableDataSet = TheBOMCableClass.GetBOMCableInfo();

            //this will load the controls
            mintWarehouseID = Logon.mintPartsWarehouseID;
            txtTransactionID.Text = Convert.ToString(TheCableTransactionIDClass.CreateInventoryTransasctionID());
            txtWarehouseID.Text = Convert.ToString(mintWarehouseID);
            txtDate.Text = Convert.ToString(DateTime.Now);

            //creating the data grid
            dgvEnteredTransactions.ColumnCount = 6;
            dgvEnteredTransactions.Columns[0].Name = "Transaction ID";
            dgvEnteredTransactions.Columns[0].Width = 75;
            dgvEnteredTransactions.Columns[1].Name = "Part Number";
            dgvEnteredTransactions.Columns[1].Width = 75;
            dgvEnteredTransactions.Columns[2].Name = "Warehouse ID";
            dgvEnteredTransactions.Columns[2].Width = 75;
            dgvEnteredTransactions.Columns[3].Name = "Project ID";
            dgvEnteredTransactions.Columns[3].Width = 100;
            dgvEnteredTransactions.Columns[4].Name = "Footage";
            dgvEnteredTransactions.Columns[4].Width = 75;
            dgvEnteredTransactions.Columns[5].Name = "Date";
            dgvEnteredTransactions.Columns[5].Width = 75;
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            //this will clear the grid
            dgvEnteredTransactions.Rows.Clear();
        }
        private void ClearControls()
        {
            txtFootage.Text = "";
            txtPartNumber.Text = "";
            txtProjectID.Text = "";
            txtTransactionID.Text = Convert.ToString(TheCableTransactionIDClass.CreateInventoryTransasctionID());
            txtDate.Text = Convert.ToString(DateTime.Now);
        }
        private void LoadGrid()
        {
            //this will fill the grid
            string[] NewGridRow;

            NewGridRow = new string[] { txtTransactionID.Text, txtPartNumber.Text, txtWarehouseID.Text, txtProjectID.Text, txtFootage.Text, txtDate.Text };
            dgvEnteredTransactions.Rows.Add(NewGridRow);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intPartID = 0;
            string strPartNumber;
            string strProject = "";
            int intProjectID = 0;
            int intFootage = 0;
            DateTime datTransactionDate = DateTime.Now;
            string strValueForValidation;
            bool blnFatalError = false;
            bool blnThereIsAProblem = false;
            bool blnPartNumberExists;
            bool blnProjectExists;
            int intFootageFromTable;

            mstrErrorMessage = "";

            //beginning data validation
            strPartNumber = txtPartNumber.Text;
            blnFatalError = TheDataValidationClass.VerifyTextData(strPartNumber);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "Part Number Was Not Entered\n";
            }
            else
            {
                blnFatalError = ThePartNumberClass.CheckTimeWarnerPart(strPartNumber);
                if(blnFatalError == true)
                {
                    blnThereIsAProblem = true;
                    mstrErrorMessage = mstrErrorMessage + "Part Number Entered Is Not a Time Warner Part Number\n";
                }
            }
            strProject = txtProjectID.Text;
            blnFatalError = TheDataValidationClass.VerifyTextData(strProject);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Project Was Not Entered\n";
            }
            strValueForValidation = txtDate.Text;
            blnFatalError = TheDataValidationClass.VerifyDateData(strValueForValidation);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Date is not in the Correct Format\n";
            }
            strValueForValidation = txtFootage.Text;
            blnFatalError = TheDataValidationClass.VerifyIntegerData(strValueForValidation);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Footage is not an Integer\n";
            }
            else
            {
                intFootage = Convert.ToInt32(strValueForValidation);
            }
            if(blnThereIsAProblem == true)
            {
                TheMessagesClass.ErrorMessage(mstrErrorMessage);
                return;
            }

            //checking the part number
            blnPartNumberExists = ThePartNumberClass.VerifyPartNumber(strPartNumber);
            if(blnPartNumberExists == false)
            {
                TheMessagesClass.ErrorMessage("This Part Number Entered Does Not Exist\nTherefore This Cable Cannot Be On A BOM");
                return;
            }
            else
            {
                intPartID = ThePartNumberClass.FindPartID(strPartNumber);
                intFootageFromTable = TheCableInventoryClass.GetTotalTWCFootageForPartandWarehouse(intPartID, mintWarehouseID);

                if(intFootage > intFootageFromTable)
                {
                    TheMessagesClass.InformationMessage("You Are Attempting To Report More Cable\nThan Available, The Transaction Failed");
                    return;
                }
            }

            //filling boolean variables
            blnProjectExists = TheProjectClass.VerifyTWCProjectID(strProject);

            //if statements
            if (blnProjectExists == false)
            {
                Logon.mstrTWCProjectID = strProject;
                Logon.mstrSelectedButton = "RECEIVE";

                AddProject AddProject = new AddProject();
                AddProject.ShowDialog();
            }

            //getting the project id
            intProjectID = TheProjectClass.FindProjectID(strProject);

            //try catch for exceptions
            try
            {
                //performing table adjustment
                blnFatalError = TheCableInventoryClass.ReportBOMCableFromTWC(intPartID, mintWarehouseID, intFootage);

                if(blnFatalError == true)
                {
                    TheMessagesClass.ErrorMessage("The Inventory Was Not Updated, Contact IT");
                    return;
                }

                //creating new table entry
                BOMCableDataSet.bomcableRow NewTableRow = TheBOMCableDataSet.bomcable.NewbomcableRow();

                //filling the fields
                NewTableRow.TransactionID = Convert.ToInt32(txtTransactionID.Text);
                NewTableRow.PartID = intPartID;
                NewTableRow.WarehouseID = mintWarehouseID;
                NewTableRow.ProjectID = intProjectID;
                NewTableRow.Footage = intFootage;
                NewTableRow.Date = Convert.ToDateTime(txtDate.Text);

                //updating the data set
                TheBOMCableDataSet.bomcable.Rows.Add(NewTableRow);
                TheBOMCableClass.UpdateBOMCableDB(TheBOMCableDataSet);

                LoadGrid();
                ClearControls();
                TheMessagesClass.InformationMessage("The Transaction Has Been Saved");
            }
            catch (Exception Ex)
            {
                //message to user
                TheMessagesClass.ErrorMessage(Ex.Message);

                //creating event log entry
                TheEventLog.CreateEventLogEntry("BOM Cable " + Ex.Message);
            }
        }
    }
}
