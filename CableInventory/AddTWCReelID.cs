/* Title:           Add TWC Reel ID
 * Date:            7-1-16
 * Author:          Terry Holmes
 *
 * Description:     This is the form for updating the TWC Reel ID */

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
using KeyWordDLL;
using EventLogDLL;
using DataValidationDLL;

namespace CableInventory
{
    public partial class AddTWCReelID : Form
    {
        //setting up the classes
        MessagesClass TheMessagesClass = new MessagesClass();
        PartNumberClass ThePartNumberClass = new PartNumberClass();
        CableInventoryClass TheCableInventoryClass = new CableInventoryClass();
        KeyWordClass TheKeyWordClass = new KeyWordClass();
        PleaseWait PleaseWait = new PleaseWait();
        EventLogClass TheEventLogClass = new EventLogClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();

        //setting up data
        PartNumbersDataSet ThePartNumberDataSet;
        ReelDataSet TheReelDataSet;

        //setting up structures
        struct PartNumbers
        {
            public int mintPartID;
            public string mstrPartNumber;
            public string mstrDescription;
        }

        //variables for the structure
        PartNumbers[] ThePartNumbers;
        int mintPartCounter;
        int mintPartUpperLimit;

        PartNumbers[] SearchResults;
        int mintResultCounter;
        int mintResultUpperLimit;

        struct Reels
        {
            public int mintTransactionID;
            public int mintPartID;
            public int mintWarehouseID;
            public string mstrReelID;
            public string mstrTWCReelID;
            public int mintFootage;
        }

        //variable for reel structure
        Reels[] TheReels;
        int mintReelCounter;
        int mintReelUpperLimit;

        //setting up global variables
        string mstrErrorMessage;
        int mintPartID;
        int mintWarehouseID;

        public AddTWCReelID()
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

        private void AddTWCReelID_Load(object sender, EventArgs e)
        {
            //setting up local variables
            bool blnFatalError = false;

            PleaseWait.Show();

            //Setting warehouse id
            mintWarehouseID = Logon.mintPartsWarehouseID;

            blnFatalError = LoadPartStructure();
            if (blnFatalError == false)
                blnFatalError = LoadReelStructure();

            btnUpdate.Enabled = false;

            PleaseWait.Hide();

            if(blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage(mstrErrorMessage);
            }
        }
        private bool LoadReelStructure()
        {
            //setting local variables
            bool blnFatalError = false;
            int intCounter;
            int intNumberOfRecords;
            int intWarehouseIDFromTable;
            bool blnItemNotFound = true;

            try
            {
                //loading the data set
                TheReelDataSet = TheCableInventoryClass.GetReelInfo();

                //setting up for the loop
                intNumberOfRecords = TheReelDataSet.reel.Rows.Count - 1;
                TheReels = new Reels[intNumberOfRecords + 1];
                mintReelCounter = 0;

                //beginning loop
                for(intCounter = 0; intCounter <= intNumberOfRecords; intCounter++)
                {
                    //getting the warehouse id
                    intWarehouseIDFromTable = Convert.ToInt32(TheReelDataSet.reel.Rows[intCounter][2]);

                    if(mintWarehouseID == intWarehouseIDFromTable)
                    {
                        TheReels[mintReelCounter].mintTransactionID = Convert.ToInt32(TheReelDataSet.reel.Rows[intCounter][0]);
                        TheReels[mintReelCounter].mintPartID = Convert.ToInt32(TheReelDataSet.reel.Rows[intCounter][1]);
                        TheReels[mintReelCounter].mintWarehouseID = intWarehouseIDFromTable;
                        TheReels[mintReelCounter].mstrReelID = Convert.ToString(TheReelDataSet.reel.Rows[intCounter][3]).ToUpper();
                        TheReels[mintReelCounter].mstrTWCReelID = Convert.ToString(TheReelDataSet.reel.Rows[intCounter][4]).ToUpper();
                        TheReels[mintReelCounter].mintFootage = Convert.ToInt32(TheReelDataSet.reel.Rows[intCounter][5]);
                        mintReelUpperLimit = mintReelCounter;
                        mintReelCounter++;
                    }
                }

                mintReelCounter = 0;
            }
            catch (Exception Ex)
            {
                //message to user
                mstrErrorMessage = Ex.Message;

                //log entry
                TheEventLogClass.CreateEventLogEntry("Add TWC Reel ID " + Ex.Message);

                blnFatalError = true;
            }

            return blnFatalError;
        }
        private bool LoadPartStructure()
        {
            //setting local variables
            bool blnFatalError = false;
            int intCounter;
            int intNumberOfRecords;
            bool blnIsNotAnInteger;
            bool blnIsNotTWCPart;
            string strPartNumber;

            try
            {
                //filling the data set
                ThePartNumberDataSet = ThePartNumberClass.GetPartNumbersInfo();

                //getting ready for the loop
                intNumberOfRecords = ThePartNumberDataSet.partnumbers.Rows.Count - 1;
                ThePartNumbers = new PartNumbers[intNumberOfRecords + 1];
                SearchResults = new PartNumbers[intNumberOfRecords + 1];
                mintPartCounter = 0;

                //running loop
                for(intCounter = 0; intCounter <= intNumberOfRecords; intCounter++)
                {
                    //getting the part number
                    strPartNumber = Convert.ToString(ThePartNumberDataSet.partnumbers.Rows[intCounter][1]).ToUpper();

                    //checking to see if the part number is an integer
                    blnIsNotAnInteger = TheDataValidationClass.VerifyIntegerData(strPartNumber);

                    if(blnIsNotAnInteger == false)
                    {
                        //checking to see if the part number is a time warner part
                        blnIsNotTWCPart = ThePartNumberClass.CheckTimeWarnerPart(strPartNumber);

                        if(blnIsNotTWCPart == false)
                        {
                            ThePartNumbers[mintPartCounter].mintPartID = Convert.ToInt32(ThePartNumberDataSet.partnumbers.Rows[intCounter][0]);
                            ThePartNumbers[mintPartCounter].mstrPartNumber = strPartNumber;
                            ThePartNumbers[mintPartCounter].mstrDescription = Convert.ToString(ThePartNumberDataSet.partnumbers.Rows[intCounter][2]).ToUpper();
                            mintPartUpperLimit = mintPartCounter;
                            mintPartCounter++;
                        }
                    }
                }

                //setting the counter
                mintPartCounter = 0;
            }
            catch (Exception Ex)
            {
                //message to user
                mstrErrorMessage = Ex.Message;

                //log entry
                TheEventLogClass.CreateEventLogEntry("Add TWC Reel ID " + Ex.Message);

                blnFatalError = true;
            }

            return blnFatalError;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //setting local variables
            int intPartCounter;
            int intReelCounter;
            int intPartID = 0;
            string strPartDescriptionFromTable;
            string strPartDescriptionForSearch;
            string strReelID;
            bool blnFatalError = false;
            bool blnThereIsAProblem = false;
            bool blnKeyWordNotFound = true;
            bool blnIsNotTWCPart = true;
            bool blnItemNotFound = true;
            bool blnIsNotAnInteger = false;
            bool blnPartNumberFound = false;
            int intReelSelectedIndex = 0;

            //performing data validation
            strPartDescriptionForSearch = txtEnterDescription.Text;
            strReelID = txtReelID.Text;
            mstrErrorMessage = "";
            btnUpdate.Enabled = false;

            blnFatalError = TheDataValidationClass.VerifyTextData(strPartDescriptionForSearch);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Part Description Was Not Entered\n";
            }
            blnFatalError = TheDataValidationClass.VerifyTextData(strReelID);
            if(blnFatalError == true)
            {
                blnThereIsAProblem = true;
                mstrErrorMessage = mstrErrorMessage + "The Reel ID Was Not Entered\n";
            }
            if(blnThereIsAProblem == true)
            {
                TheMessagesClass.ErrorMessage(mstrErrorMessage);
                return;
            }

            //checking to see if the entered information is a TWC Part Number
            blnIsNotAnInteger = TheDataValidationClass.VerifyIntegerData(strPartDescriptionForSearch);
            if(blnIsNotAnInteger == false)
            {
                blnIsNotTWCPart = ThePartNumberClass.CheckTimeWarnerPart(strPartDescriptionForSearch);
            }

            for(intPartCounter = 0; intPartCounter <= mintPartUpperLimit; intPartCounter++)
            {
                blnPartNumberFound = false;

                if(blnIsNotTWCPart == false)
                {
                    //checking the part number
                    strPartDescriptionFromTable = ThePartNumbers[intPartCounter].mstrPartNumber;

                    if(strPartDescriptionForSearch == strPartDescriptionFromTable)
                    {
                        intPartID = ThePartNumbers[intPartCounter].mintPartID;
                        blnPartNumberFound = true;
                    }
                }
                else if(blnPartNumberFound == false)
                {
                    //getting the part description
                    strPartDescriptionFromTable = ThePartNumbers[intPartCounter].mstrDescription;

                    blnKeyWordNotFound = TheKeyWordClass.FindKeyWord(strPartDescriptionForSearch, strPartDescriptionFromTable);

                    if (blnKeyWordNotFound == false)
                    {
                        intPartID = ThePartNumbers[intPartCounter].mintPartID;
                        blnPartNumberFound = true;
                    }
                }

                if(blnPartNumberFound == true)
                {
                    //checking the reel id
                    for (intReelCounter = 0; intReelCounter <= mintReelUpperLimit; intReelCounter++)
                    {
                       if(intPartID == TheReels[intReelCounter].mintPartID)
                            if(strReelID == TheReels[intReelCounter].mstrReelID)
                            {
                                intReelSelectedIndex = intReelCounter;
                                blnItemNotFound = false;
                            }
                    }
                }
                
            }

            if(blnItemNotFound == true)
            {
                TheMessagesClass.InformationMessage("The Information Entered Does Match Any Existing Reel");
                ClearControls();
            }
            else
            {
                //loading the controls
                txtPartNumber.Text = ThePartNumberClass.FindPartNumber(intPartID);
                txtPartID.Text = Convert.ToString(TheReels[intReelSelectedIndex].mintPartID);
                txtTransactionID.Text = Convert.ToString(TheReels[intReelSelectedIndex].mintTransactionID);
                txtFootage.Text = Convert.ToString(TheReels[intReelSelectedIndex].mintTransactionID);
                txtWarehouseID.Text = Convert.ToString(TheReels[intReelSelectedIndex].mintWarehouseID);
                txtTWCReelID.Text = Convert.ToString(TheReels[intReelSelectedIndex].mstrTWCReelID);
                btnUpdate.Enabled = true;
            }
        }
        private void ClearControls()
        {
            txtEnterDescription.Text = "";
            txtFootage.Text = "";
            txtPartID.Text = "";
            txtPartNumber.Text = "";
            txtReelID.Text = "";
            txtTransactionID.Text = "";
            txtTWCReelID.Text = "";
            txtWarehouseID.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //setting local variables
            int intTransactionID;
            string strTWCReelID;
            int intPartID;
            bool blnTWCReelIDExists;
            bool blnFatalError = false;

            //performing data validation
            strTWCReelID = txtTWCReelID.Text;
            intPartID = Convert.ToInt32(txtPartID.Text);
            intTransactionID = Convert.ToInt32(txtTransactionID.Text);

            if(strTWCReelID == "")
            {
                TheMessagesClass.ErrorMessage("The TWC Reel ID Was Not Entered");
                return;
            }

            //checking to see if the reel id exists
            blnTWCReelIDExists = TheCableInventoryClass.CheckTWCIDExists(intPartID, mintWarehouseID, strTWCReelID);

            //if statements
            if(blnTWCReelIDExists == true)
            {
                TheMessagesClass.InformationMessage("The TWC Reel ID Exist for this Part Number");
                return;
            }

            blnFatalError = TheCableInventoryClass.UpdateTWCReelID(intTransactionID, strTWCReelID);

            if(blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage("There Was a Problem, Contact ID");
            }
            else
            {
                TheMessagesClass.InformationMessage("The Record Has Been Updated");
                ClearControls();
                btnUpdate.Enabled = false;
            }
        }
    }
}
