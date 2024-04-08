/* Title:           Display Cable
 * Date:            9-10-16
 * Author:          Terry Holmes
 * 
 * Description:     This form is used to display the cable */

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
using MessagesDLL;
using CableInventoryDLL;
using EmployeeDLL;
using DataValidationDLL;
using DateSearchDLL;

namespace CableInventory
{
    public partial class DisplayCable : Form
    {
        //setting up the classes
        EventLogClass TheEventLogClass = new EventLogClass();
        MessagesClass TheMessagesClass = new MessagesClass();
        CableInventoryClass TheCableInventoryClass = new CableInventoryClass();
        EmployeeClass TheEmployeeClass = new EmployeeClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();
        DateSearchClass TheDateSearchClass = new DateSearchClass();
        PleaseWait PleaseWait = new PleaseWait();

        //setting up the data
        ReelDataSet TheReelDataSet;
        ReelDataSet TheResultsDataSet = new ReelDataSet();
        EmployeeDataSet TheEmployeeDataSet;
        EmployeeDataSet TheResultsEmployeeDataSet = new EmployeeDataSet();

        //setting global variables
        int gintCableInventoryUpperLimit;
        int gintEmployeeUpperLimit;

        int gintEmployeeResultCounter;
        int gintEmployeeResutlUpperLimit;
        int gintCableResultCounter;
        int gintCableResultUpperLimit;
        int gintWarehouseID;

        string gstrErrorMessage;

        public DisplayCable()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this wil close the form
            this.Close();
        }

        private void DisplayCable_Load(object sender, EventArgs e)
        {
            //setting local variables
            bool blnFatalError = false;

            PleaseWait.Show();

            gintWarehouseID = Logon.mintPartsWarehouseID;

            blnFatalError = LoadReelDataSet();

            PleaseWait.Hide();

            //message to user
            if(blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage(gstrErrorMessage);
            }
        }
        private bool LoadReelDataSet()
        {
            bool blnFatalError = false;
            int intCounter;
            int intNumberOfRecords;

            try
            {
                //loading data set
                TheReelDataSet = TheCableInventoryClass.GetReelInfo();

                intNumberOfRecords = TheReelDataSet.reel.Rows.Count - 1;

                //loop
                for(intCounter = 0; intCounter <= intNumberOfRecords; intCounter++)
                {
                    if(TheReelDataSet.reel[intCounter].WarehouseID == gintWarehouseID)
                    {

                    }
                }
            }
            catch (Exception Ex)
            {
                gstrErrorMessage = Ex.Message;

                TheEventLogClass.CreateEventLogEntry("Cable Inventory Load Warehouse Cable Inventory " + Ex.Message);

                blnFatalError = true;
            }


            //returning value
            return blnFatalError;
        }
    }
}
