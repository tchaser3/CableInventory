/* Title:           Upload Initial Cable
 * Date:            6-26-16
 * Author:          Terry Holmes
 *
 * Description:     This form will load the initial cable */

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
using PartNumberDLL;
using CableInventoryDLL;
using EventLogDLL;
using DataValidationDLL;
using KeyWordDLL;
using CableTransactionIDDLL;

namespace CableInventory
{
    public partial class UploadInitialCable : Form
    {
        //setting the classes
        MessagesClass TheMessagesClass = new MessagesClass();
        PartNumberClass ThePartNumberClass = new PartNumberClass();
        CableInventoryClass TheCableInventory = new CableInventoryClass();
        EventLogClass TheEventLogClass = new EventLogClass();
        PleaseWait PleaseWait = new PleaseWait();
        DataValidationClass TheDataValidationClass = new DataValidationClass();
        KeyWordClass TheKeyWordSearchClass = new KeyWordClass();
        CableTransactionIDClass TheCableTransactionIDClass = new CableTransactionIDClass();

        //setting up the data
        PartNumbersDataSet ThePartNumberDataSet;
        ReelDataSet TheReelDataSet;

        //creating part structure
        struct PartNumbers
        {
            public int mintPartID;
            public string mstrPartNumber;
            public string mstrDescription;
        }

        //setting variables for the part number
        PartNumbers[] ThePartNumbers;
        int mintPartCounter;
        int mintPartUpperLimit;

        //setting up the results
        PartNumbers[] SearchResults;
        int mintResultCounter;
        int mintResultUpperLimit;

        //setting global variables
        string mstrErrorMessage;
        int mintPartID;
        int mintWarehouseID;

        public UploadInitialCable()
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

        private void UploadInitialCable_Load(object sender, EventArgs e)
        {
            //setting local variables
            bool blnFatalError = false;

            PleaseWait.Show();

            //work goes here
            blnFatalError = LoadPartStructure();
            if (blnFatalError == false)
                blnFatalError = LoadOtherDataSets();

            //creating the data grid
            dgvSearchResults.ColumnCount = 5;
            dgvSearchResults.Columns[0].Name = "Part ID";
            dgvSearchResults.Columns[0].Width = 75;
            dgvSearchResults.Columns[1].Name = "Warehouse ID";
            dgvSearchResults.Columns[1].Width = 75;
            dgvSearchResults.Columns[2].Name = "Reel ID";
            dgvSearchResults.Columns[2].Width = 75;
            dgvSearchResults.Columns[3].Name = "TWC Reel ID";
            dgvSearchResults.Columns[3].Width = 75;
            dgvSearchResults.Columns[4].Name = "Footage";
            dgvSearchResults.Columns[4].Width = 75;

            //getting the warehouse id
            mintWarehouseID = Logon.mintPartsWarehouseID;

            PleaseWait.Hide();

            if(blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage(mstrErrorMessage);
            }

        }
        private bool LoadOtherDataSets()
        { 
            //setting local variables
            bool blnFatalError = false;

            try
            {
                //loading the data set
                TheReelDataSet = TheCableInventory.GetReelInfo();

                ClearControls();
                SetControlsReadOnly(true);
            }
            catch (Exception ex)
            {
                //message to user
                mstrErrorMessage = ex.Message;

                //event log entry
                TheEventLogClass.CreateEventLogEntry("Upload Initial Cable " + mstrErrorMessage);

                blnFatalError = true;
            }

            //returning value
            return blnFatalError;
        }
        private bool LoadPartStructure()
        {
            //setting local variables
            bool blnFatalError = false;
            int intCounter;
            int intNumberOfRecords;
            string strPartNumber;
            bool blnIsNotInteger = false;
            bool blnIsNotTWCPart = true;

            try
            {
                //loading the data set
                ThePartNumberDataSet = ThePartNumberClass.GetPartNumbersInfo();

                //getting ready for the loop
                intNumberOfRecords = ThePartNumberDataSet.partnumbers.Rows.Count - 1;
                ThePartNumbers = new PartNumbers[intNumberOfRecords + 1];
                SearchResults = new PartNumbers[intNumberOfRecords + 1];
                mintPartCounter = 0;

                //doing the loop
                for(intCounter = 0; intCounter <= intNumberOfRecords; intCounter++)
                {
                    //setting variables
                    blnIsNotInteger = true;
                    blnIsNotTWCPart = false;

                    //getting the part number
                    strPartNumber = Convert.ToString(ThePartNumberDataSet.partnumbers.Rows[intCounter][1]).ToUpper();

                    //checking part
                    blnIsNotInteger = TheDataValidationClass.VerifyIntegerData(strPartNumber);
                    if(blnIsNotInteger == false)
                    {
                        blnIsNotTWCPart = ThePartNumberClass.CheckTimeWarnerPart(strPartNumber);
                    }

                    //if statetment
                    if(blnIsNotInteger == false)
                        if(blnIsNotTWCPart == false)
                        {
                            //loading the structure
                            ThePartNumbers[mintPartCounter].mintPartID = Convert.ToInt32(ThePartNumberDataSet.partnumbers.Rows[intCounter][0]);
                            ThePartNumbers[mintPartCounter].mstrPartNumber = strPartNumber;
                            ThePartNumbers[mintPartCounter].mstrDescription = Convert.ToString(ThePartNumberDataSet.partnumbers.Rows[intCounter][2]).ToUpper();
                            mintPartUpperLimit = mintPartCounter;
                            mintPartCounter++;
                        }
                }

                mintPartCounter = 0;
            }
            catch (Exception ex)
            {
                //message to user
                mstrErrorMessage = ex.Message;

                //event log entry
                TheEventLogClass.CreateEventLogEntry("Upload Initial Cable " + mstrErrorMessage);

                blnFatalError = true;
            }

            //returning value
            return blnFatalError;
        }
        private void SetControlsReadOnly(bool valueBoolean)
        {
            txtFootage.ReadOnly = valueBoolean;
            txtReelID.ReadOnly = valueBoolean;
            txtTWCReelID.ReadOnly = valueBoolean;
        }
        private void ClearControls()
        {
            txtFootage.Text = "";
            txtReelID.Text = "";
            txtTWCReelID.Text = "";
            cboCable.Items.Clear();
            cboCable.Items.Add("SELECT");
            cboCable.SelectedIndex = 0;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //setting local variables
            string strPartNumberFromTable;
            string strPartNumberForSearch;
            int intCounter;
            bool blnKeyWordNotFound = true;
            bool blnItemNotFound = true;
            bool blnNothingWasFound = true;

            //checking for data
            strPartNumberForSearch = txtEnterDescription.Text;
            txtPartNumber.Text = "";

            if(strPartNumberForSearch == "")
            {
                TheMessagesClass.ErrorMessage("No Text Was Entered");
                return;
            }

            //getting ready for the loop
            mintResultCounter = 0;

            //for loop
            for(intCounter = 0; intCounter <= mintPartUpperLimit; intCounter++)
            {
                //setting the boolean
                blnItemNotFound = true;

                //getting the part number
                strPartNumberFromTable = ThePartNumbers[intCounter].mstrPartNumber;

                //checking to see if the part numbers match
                if(strPartNumberForSearch == strPartNumberFromTable)
                {
                    blnItemNotFound = false;
                }
                else
                {
                    //keywording the description
                    strPartNumberFromTable = ThePartNumbers[intCounter].mstrDescription;

                    //checking the keyword
                    blnKeyWordNotFound = TheKeyWordSearchClass.FindKeyWord(strPartNumberForSearch, strPartNumberFromTable);

                    //if statement
                    if(blnKeyWordNotFound == false)
                    {
                        blnItemNotFound = false;
                    }
                }

                if(blnItemNotFound == false)
                {
                    blnNothingWasFound = false;

                    //loading results structure
                    SearchResults[mintResultCounter].mintPartID = ThePartNumbers[intCounter].mintPartID;
                    SearchResults[mintResultCounter].mstrPartNumber = ThePartNumbers[intCounter].mstrPartNumber;
                    SearchResults[mintResultCounter].mstrDescription = ThePartNumbers[intCounter].mstrDescription;
                    mintResultUpperLimit = mintResultCounter;
                    mintResultCounter++;
                }
            }

            if(blnNothingWasFound == true)
            {
                TheMessagesClass.InformationMessage("No Parts Were Found");
                txtEnterDescription.Text = "";
            }
            else
            {
                LoadComboBox();
                mintResultCounter = 0;
                btnAdd.Enabled = false;
            }
            
        }
        private void LoadComboBox()
        {
            //this will load the combo box
            //setting local variables
            int intCounter;

            //setting up for the loop
            cboCable.Items.Clear();
            cboCable.Items.Add("SELECT");

            //loop to load the combo box
            for(intCounter = 0; intCounter <= mintResultUpperLimit; intCounter++)
            {
                cboCable.Items.Add(SearchResults[intCounter].mstrDescription);
            }

            cboCable.SelectedIndex = 0;
        }

        private void cboCable_SelectedIndexChanged(object sender, EventArgs e)
        {
            //setting local variables
            int intSelectedIndex;

            if(cboCable.Text != "SELECT")
            {
                //this will load the controls
                intSelectedIndex = cboCable.SelectedIndex - 1;

                mintPartID = SearchResults[intSelectedIndex].mintPartID;

                txtPartNumber.Text = SearchResults[intSelectedIndex].mstrPartNumber;

                btnAdd.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //setting local variables
            bool blnFatalError = false;
            bool blnThereIsAProblem = false;
            string strValueForValidation;
            int intFootage = 0;
            string strReelID;
            string strTWCReelID;
            bool blnReelExists = false;
            string[] NewGridRow;

            if(btnAdd.Text == "Add")
            {
                SetControlsReadOnly(false);
                btnAdd.Text = "Save";
            }
            else
            {
                //loading variables
                strValueForValidation = txtFootage.Text;
                strReelID = txtReelID.Text;
                strTWCReelID = txtTWCReelID.Text;

                mstrErrorMessage = "";

                //beginning data validation
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
                blnFatalError = TheDataValidationClass.VerifyTextData(strReelID);
                if(blnFatalError == true)
                {
                    blnThereIsAProblem = true;
                    mstrErrorMessage = mstrErrorMessage + "The Reel ID was not Entered\n";
                }
                if(blnThereIsAProblem == true)
                {
                    TheMessagesClass.ErrorMessage(mstrErrorMessage);
                    return;
                }

                blnReelExists = TheCableInventory.VerifyReelExists(strReelID, mintPartID, mintWarehouseID);

                if(blnReelExists == true)
                {
                    TheMessagesClass.InformationMessage("The Reel Has Been Inputted Already");
                    return;
                }

                //try catch
                try
                {
                    //creating new row
                    ReelDataSet.reelRow NewTableRow = TheReelDataSet.reel.NewreelRow();

                    //filling the fields
                    NewTableRow.TransactionID = TheCableTransactionIDClass.CreateInventoryTransasctionID();
                    NewTableRow.WarehouseID = mintWarehouseID;
                    NewTableRow.PartID = mintPartID;
                    NewTableRow.Footage = intFootage;
                    NewTableRow.ReelID = strReelID;
                    NewTableRow.TWCReelID = strTWCReelID;

                    TheReelDataSet.reel.Rows.Add(NewTableRow);
                    TheCableInventory.UpdateReelDB(TheReelDataSet);

                    //updating tables
                    TheCableInventory.AddCableToTWC(mintPartID, mintWarehouseID, intFootage);
                    TheCableInventory.AddCableToWH(mintPartID, mintWarehouseID, intFootage);

                    //creating new grid
                    NewGridRow = new string[] { Convert.ToString(mintPartID), Convert.ToString(mintWarehouseID), strReelID, strTWCReelID, Convert.ToString(intFootage) };
                    dgvSearchResults.Rows.Add(NewGridRow);

                    //this will set the controls back
                    ClearControls();
                    SetControlsReadOnly(true);
                    txtEnterDescription.Text = "";
                    txtPartNumber.Text = "";
                    btnAdd.Text = "Add";
                    btnAdd.Enabled = false;
                    TheMessagesClass.InformationMessage("The Transaction Has Been Saved");
                }
                catch (Exception Ex)
                {
                    //message to user
                    TheMessagesClass.ErrorMessage(Ex.Message);

                    //create event log
                    TheEventLogClass.CreateEventLogEntry("Upload Initial Cable " + Ex.Message);
                }
            }
        }
    }
}
