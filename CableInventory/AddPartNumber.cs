/* Title:           Add Part Number
 * Date:            5-24-16
 * Author:          Terry Holmes
 *
 * Description:     This is the form to add a part number during data entry */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventLogDLL;
using PartNumberDLL;
using MessagesDLL;
using DataValidationDLL;

namespace CableInventory
{
    public partial class AddPartNumber : Form
    {
        //setting up the classes
        MessagesClass TheMessagesClass = new MessagesClass();
        EventLogClass TheEventLogClass = new EventLogClass();
        PartNumberClass thePartNumberclass = new PartNumberClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();

        //setting up the data
        PartNumbersDataSet ThePartNumbersDataSet;

        public AddPartNumber()
        {
            InitializeComponent();
        }

        private void AddPartNumber_Load(object sender, EventArgs e)
        {
            //this is the form load event
            try
            {
                //loading the data set
                ThePartNumbersDataSet = thePartNumberclass.GetPartNumbersInfo();

                //loading the control
                txtPartNumber.Text = Logon.mstrPartNumber;
            }
            catch(Exception Ex)
            {
                //message to user
                TheMessagesClass.ErrorMessage(Ex.Message);

                //creating event log
                TheEventLogClass.CreateEventLogEntry("Add Part Numbers TWC Cable " + Ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //setting local variables
            bool blnFatalError = false;
            string strDescription;

            //performing data validation
            strDescription = txtDescription.Text;
            blnFatalError = TheDataValidationClass.VerifyTextData(strDescription);
            if(blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage("Description Was Not Added");
                return;
            }

            //try catch for exceptions
            try
            {
                //saving the record
                PartNumbersDataSet.partnumbersRow NewTableRow = ThePartNumbersDataSet.partnumbers.NewpartnumbersRow();

                //setting the fields
                NewTableRow.PartID = thePartNumberclass.CreatePartID();
                NewTableRow.PartNumber = txtPartNumber.Text;
                NewTableRow.Description = txtDescription.Text;
                NewTableRow.TimeWarnerPart = "YES";
                NewTableRow.Active = "YES";
                NewTableRow.Type = "NO PART TYPE GIVEN";
                NewTableRow.PartType = "NO PART TYPE GIVEN";
                NewTableRow.Price = 0.00;

                //updating the data
                ThePartNumbersDataSet.partnumbers.Rows.Add(NewTableRow);
                thePartNumberclass.UpdatePartNumbersDB(ThePartNumbersDataSet);

                this.Close();
            }
            catch (Exception Ex)
            {
                //message to user
                TheMessagesClass.ErrorMessage(Ex.Message);

                //creating event log
                TheEventLogClass.CreateEventLogEntry("Add Part Numbers TWC Cable " + Ex.Message);
            }
        }
    }
}
