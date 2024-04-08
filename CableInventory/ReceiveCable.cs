/* Title:           Receive Cable
 * Date:            5-22-16
 * Author:          Terry Holmes
 *
 * Description:     This form is used to receive cable */

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
using ReceiveCableDLL;
using PartNumberDLL;
using HandCoilDLL;
using CableInventoryDLL;
using TransactionTypeDLL;
using EventLogDLL;
using CableTransactionIDDLL;
using ProjectsDLL;
using IssueCableDLL;

namespace CableInventory
{
    public partial class ReceiveCable : Form
    {
        //setting up the classes
        MessagesClass TheMessagesClass = new MessagesClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();
        ReceiveCableClass TheReceiveCableClass = new ReceiveCableClass();
        PartNumberClass ThePartNumberClass = new PartNumberClass();
        HandCoilClass TheHandCoilClass = new HandCoilClass();
        CableInventoryClass TheCableInventoryClass = new CableInventoryClass();
        TransactionTypeClass TheTransactionTypeClass = new TransactionTypeClass();
        PleaseWait PleaseWait = new PleaseWait();
        EventLogClass TheEventLog = new EventLogClass();
        CableTransactionIDClass TheCableTransactionIDClass = new CableTransactionIDClass();
        ProjectClass TheProjectClass = new ProjectClass();
        ReceiveCableDataSet TheReceiveCableDataSet;
        IssueCableClass TheIssueCableClass = new IssueCableClass();

        //setting up the data
        TransactionTypeDataSet TheTransactionTypeDataSet;

        //setting the global variables
        string mstrErrorMessage = "";
        string mstrReelType;
        string mstrTransactionType;
        int mintWarehouseID;

        public ReceiveCable()
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
            //this will display the main menu
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

                //loop to fill the combo box
                for(intCounter = 0; intCounter <= intNumberOfRecords; intCounter++)
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

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            //this will clear the grid
            dgvEnteredTransactions.Rows.Clear();
        }

        private void ReceiveCable_Load(object sender, EventArgs e)
        {
            //this will load the form
            //setting local variables
            bool blnFatalError = false;

            PleaseWait.Show();

            //setting controls
            txtWarehouseID.Text = Convert.ToString(Logon.mintPartsWarehouseID);
            mintWarehouseID = Logon.mintPartsWarehouseID;
            txtDate.Text = Convert.ToString(DateTime.Now);

            //setting up the data
            TheReceiveCableDataSet = TheReceiveCableClass.GetReceiveCableInfo();
            
            //loading the combo box
            blnFatalError = LoadComboBox();
            rdoNew.Checked = true;

            PleaseWait.Hide();

            //creating the data grid
            dgvEnteredTransactions.ColumnCount = 8;
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
            dgvEnteredTransactions.Columns[5].Name = "MSR";
            dgvEnteredTransactions.Columns[5].Width = 100;
            dgvEnteredTransactions.Columns[6].Name = "Footage";
            dgvEnteredTransactions.Columns[6].Width = 75;
            dgvEnteredTransactions.Columns[7].Name = "Date";
            dgvEnteredTransactions.Columns[7].Width = 75;

            if(blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage(mstrErrorMessage);

                TheEventLog.CreateEventLogEntry("Receive Cable " + mstrErrorMessage);
            }
        }

        private void cboReturnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboReturnType.Text != "SELECT")
            {
                //setting variables
                mstrReelType = cboReturnType.Text;
                btnSave.Enabled = true;

                //clearing the controls
                ClearTextBoxes();

                if(mstrTransactionType == "NEW")
                    if(mstrReelType == "HAND COIL")
                    {
                        TheMessagesClass.InformationMessage("If This is a Hand Coil, Please Select Return");
                        cboReturnType.SelectedIndex = 0;
                        btnSave.Enabled = false;
                    }

                //this will create a new record
                CreateNewRecord();
                

            }
        }

        private void rdoNew_CheckedChanged(object sender, EventArgs e)
        {
            //setting controls
            mstrTransactionType = "NEW";
            txtMSR.ReadOnly = false;
            txtProjectID.ReadOnly = false;
            cboReturnType.SelectedIndex = 0;
            btnSave.Enabled = false;
        }

        private void rdoReturn_CheckedChanged(object sender, EventArgs e)
        {
            mstrTransactionType = "RETURN";
            txtMSR.ReadOnly = true;
            txtProjectID.ReadOnly = true;
            cboReturnType.SelectedIndex = 0;
            btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //setting local variables
            int intPartID = 0;
            string strPartNumber;
            string strProject = "";
            int intProjectID = 0;
            string strReelID = "";
            string strMSR = "";
            int intFootage = 0;
            DateTime datTransactionDate = DateTime.Now;
            string strValueForValidation;
            bool blnFatalError = false;
            bool blnThereIsAProblem = false;
            bool blnPartNumberExists;
            bool blnProjectExists;
            bool blnReelIDExists = false;
            string[] NewGridRows;
            
            //setting the variable
            mstrErrorMessage = "";
            
            if(mstrTransactionType == "NEW")
            {
                strProject = txtProjectID.Text;
                blnFatalError = TheDataValidationClass.VerifyTextData(strProject);
                if(blnFatalError == true)
                {
                    blnThereIsAProblem = true;
                    mstrErrorMessage = mstrErrorMessage + "Project ID Not Entered\n";
                }
            }

            //data validation on the rest of the form
            strPartNumber = txtPartNumber.Text;
            blnFatalError = TheDataValidationClass.VerifyIntegerData(strPartNumber);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The TWC Part Number is not Correct\n";
            }
            else
            {
                blnFatalError = ThePartNumberClass.CheckTimeWarnerPart(strPartNumber);
                if(blnFatalError == true)
                {
                    blnThereIsAProblem = true;
                    mstrErrorMessage = mstrErrorMessage + "The TWC Number is not Correct\n";
                }
            }
            strReelID = txtReelID.Text;
            blnFatalError = TheDataValidationClass.VerifyTextData(strReelID);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Reel ID Was Not Entered\n";
            }
            strValueForValidation = txtFootage.Text;
            blnFatalError = TheDataValidationClass.VerifyIntegerData(strValueForValidation);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Footage Entered is not an Integer\n";
            }
            else
            {
                intFootage = Convert.ToInt32(strValueForValidation);
            }
            strValueForValidation = txtDate.Text;
            blnFatalError = TheDataValidationClass.VerifyDateData(strValueForValidation);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Date Information is not a Date\n";
            }
            else
            {
                datTransactionDate = Convert.ToDateTime(strValueForValidation);
            }
            if(blnThereIsAProblem == true)
            {
                TheMessagesClass.ErrorMessage(mstrErrorMessage);
                return;
            }

            //checking part number
            blnPartNumberExists = ThePartNumberClass.VerifyPartNumber(strPartNumber);

            if(blnPartNumberExists == false)
            {
                Logon.mstrPartNumber = strPartNumber;
                AddPartNumber AddPartNumber = new AddPartNumber();
                AddPartNumber.ShowDialog();
            }

            //setting the part id
            intPartID = ThePartNumberClass.FindPartID(strPartNumber);

            //if statement to see if the MSR and Project Have to be checked
            if(mstrTransactionType == "NEW")
            {
                //filling boolean variables
                blnProjectExists = TheProjectClass.VerifyTWCProjectID(strProject);
                
                //if statements
                if(blnProjectExists == false)
                {
                        Logon.mstrTWCProjectID = strProject;
                        Logon.mstrSelectedButton = "RECEIVE";

                        AddProject AddProject = new AddProject();
                        AddProject.ShowDialog();
                }
            }

            //adding to tables
            if(mstrTransactionType == "NEW")
            {
                //getting the project id
                intProjectID = TheProjectClass.FindProjectIDWithTWCIDandMSR(strMSR, strProject);

                //checking to see if the reel exists
                blnReelIDExists = TheCableInventoryClass.VerifyReelExists(strReelID, intPartID, mintWarehouseID);

                if (blnReelIDExists == true)
                {
                    TheMessagesClass.ErrorMessage("The Reel ID Exists");
                    return;
                }

                TheCableInventoryClass.AddCableToTWC(intPartID, mintWarehouseID, intFootage);
                TheCableInventoryClass.CreateNewReel(mintWarehouseID, intPartID, strReelID, "", intFootage);
            }
            if(mstrTransactionType == "RETURN")
            {
                if(mstrReelType == "REEL")
                {
                    //checking to see if the reel exists
                    blnReelIDExists = TheCableInventoryClass.VerifyReelExists(strReelID, intPartID, mintWarehouseID);

                    if(blnReelIDExists == false)
                    {
                        TheMessagesClass.ErrorMessage("The Reel Does Not Exist, Please Receive the Reel as New");
                        return;
                    }
                    else
                    {
                        //updating tables
                        TheIssueCableClass.ReelReturnedIssueTransactioin(intPartID, mintWarehouseID, strReelID, intFootage);
                        TheCableInventoryClass.AddToReel(mintWarehouseID, intPartID, strReelID, intFootage);
                    }
                }
                else
                {
                    TheHandCoilClass.CreateNewHandCoil(intPartID, mintWarehouseID, datTransactionDate, intFootage, strReelID);
                    TheCableInventoryClass.CreateNewReel(mintWarehouseID, intPartID, strReelID, "", intFootage);
                }
            }

            //adding to the warehouse table
            TheCableInventoryClass.AddCableToWH(intPartID, mintWarehouseID, intFootage);

            //adding to the receive table
            ReceiveCableDataSet.receivecableRow NewTableRow = TheReceiveCableDataSet.receivecable.NewreceivecableRow();

            //loading up the table
            NewTableRow.TransactionID = TheCableTransactionIDClass.CreateInventoryTransasctionID();
            NewTableRow.PartID = intPartID;
            NewTableRow.WarehouseID = mintWarehouseID;
            NewTableRow.ProjectID = intProjectID;
            NewTableRow.ReelID = strReelID;
            NewTableRow.MSR = strMSR;
            NewTableRow.Footage = intFootage;
            NewTableRow.Date = datTransactionDate;

            //updating the table
            TheReceiveCableDataSet.receivecable.Rows.Add(NewTableRow);
            TheReceiveCableClass.UpdateReceiveCableDB(TheReceiveCableDataSet);

            //adding items to the grid
            NewGridRows = new string[] { txtTransactionID.Text, txtPartNumber.Text, txtWarehouseID.Text, txtProjectID.Text, txtReelID.Text, txtMSR.Text, txtFootage.Text, txtDate.Text };
            dgvEnteredTransactions.Rows.Add(NewGridRows);
            ClearTextBoxes();
            cboReturnType.SelectedIndex = 0;
        }

        private void CreateNewRecord()
        {
            //this will create a new record
            txtTransactionID.Text = Convert.ToString(TheCableTransactionIDClass.CreateInventoryTransasctionID());

            if(mstrReelType == "HAND COIL")
            {
                //creating new hand coil
                txtReelID.Text = TheCableTransactionIDClass.CreateHandcoilID();
                txtMSR.Text = "";
            }
            else if(mstrReelType == "REEL")
            {
                if (mstrTransactionType == "NEW")
                {
                    txtReelID.Text = Convert.ToString(TheCableTransactionIDClass.CreateReelID());
                }
                else
                {
                    txtMSR.Text = "";
                }
            }
        }
        private void ClearTextBoxes()
        {
            txtFootage.Text = "";
            txtPartNumber.Text = "";
            txtProjectID.Text = "";
            txtReelID.Text = "";
            txtTransactionID.Text = "";
        }
    }
}
