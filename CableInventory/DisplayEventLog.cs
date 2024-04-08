/* Title:           Display Event Log
 * Date:            9-3-16
 * Author:          Terry Holmes
 * 
 * Description:     This will display the event log*/

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
using EventLogDLL;
using DateSearchDLL;

namespace CableInventory
{
    public partial class DisplayEventLog : Form
    {
        //setting up the classes
        MessagesClass TheMessagesClass = new MessagesClass();
        EventLogClass TheEventLogClass = new EventLogClass();
        DateSearchClass TheDateSearchClass = new DateSearchClass();
        PleaseWait PleaseWait = new PleaseWait();

        //setting up the data
        EventLogDataSet TheEventLogDataSet;
        EventLogResultsDataSet TheEventLogResultsDataSet = new EventLogResultsDataSet();

        //setting up global variables
        int gintEventLogUpperLimit;
        int gintResultsCounter;
        int gintResultsUpperLimit;
        string gstrErrorMessage;

        public DisplayEventLog()
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

        private void DisplayEventLog_Load(object sender, EventArgs e)
        {
            //setting local variables
            bool blnFatalError = false;

            PleaseWait.Show();

            //beginning to call functions
            blnFatalError = LoadEventLogDataSet();
            if (blnFatalError == false)
                blnFatalError = LoadResultsDataSet();

            PleaseWait.Hide();

            //message to user
            if(blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage(gstrErrorMessage);
            }


        }
        private Boolean LoadResultsDataSet()
        {
            //setting local variables
            bool blnFatalError = false;
            int intCounter;

            try
            {
                //getting ready for the loop
                gintResultsCounter = 0;

                //beginning the loop
                for(intCounter = 0; intCounter <= gintEventLogUpperLimit; intCounter++)
                {

                }
            }
            catch (Exception Ex)
            {
                //message to the user
                gstrErrorMessage = Ex.Message;

                //event log entry
                TheEventLogClass.CreateEventLogEntry("Cable Inventory Display Event Log Load Results Data Set Method " + gstrErrorMessage);

                blnFatalError = true;
            }


            //returning value
            return blnFatalError;
        }
        private Boolean LoadEventLogDataSet()
        {
            //setting local variables
            bool blnFatalError = false;

            try
            {
                //loading the data set
                TheEventLogDataSet = TheEventLogClass.GetEventLogInfo();

                //setting up the upper bounds
                gintEventLogUpperLimit = TheEventLogDataSet.eventlog.Rows.Count - 1;

                dgvSearchResults.DataSource = TheEventLogDataSet.eventlog;
            }
            catch (Exception Ex)
            {
                //message to the user
                gstrErrorMessage = Ex.Message;

                //event log entry
                TheEventLogClass.CreateEventLogEntry("Cable Inventory Display Event Log Load Event Log Data Set Method " + gstrErrorMessage);

                blnFatalError = true;
            }

            //returning value
            return blnFatalError;
        }
    }
}
