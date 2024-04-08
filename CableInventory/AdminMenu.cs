/* Title:           Admin Menu
 * Date:            6-26-16
 * Author:          Terry Holmes
 *
 * Description:     This is the admin menu*/

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
namespace CableInventory
{
    public partial class AdminMenu : Form
    {
        //setting up the classes
        MessagesClass TheMessagesClass = new MessagesClass();
        PartNumberClass ThePartNumberClass = new PartNumberClass();
        PleaseWait PleaseWait = new PleaseWait();

        public AdminMenu()
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

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox AboutBox = new AboutBox();
            AboutBox.ShowDialog();
        }

        private void btnChangeWarehouse_Click(object sender, EventArgs e)
        {
            ChangeWarehouse ChangeWarehouse = new ChangeWarehouse();
            ChangeWarehouse.Show();
            this.Close();
        }

        private void btnUploadInitialCable_Click(object sender, EventArgs e)
        {
            UploadInitialCable UploadInitialCable = new UploadInitialCable();
            UploadInitialCable.Show();
            this.Close();
        }

        private void btnVoidCableTransactions_Click(object sender, EventArgs e)
        {
            TheMessagesClass.UnderDevelopment();
        }

        private void btnAdjustCableTotals_Click(object sender, EventArgs e)
        {
            TheMessagesClass.UnderDevelopment();
        }

        private void btnAddTWCReelID_Click(object sender, EventArgs e)
        {
            //this will open the Add TWC Reel ID
            AddTWCReelID AddTWCReelID = new AddTWCReelID();
            AddTWCReelID.Show();
            this.Close();
        }

        private void btnAdjustCableReel_Click(object sender, EventArgs e)
        {
            TheMessagesClass.UnderDevelopment();
        }

        private void btnFixPartNumberNull_Click(object sender, EventArgs e)
        {
            //setting local variables
            bool blnFatalError;

            PleaseWait.Show();

            //running sub routine
            blnFatalError = ThePartNumberClass.FixDBNullPartNumber();

            PleaseWait.Hide();

            //message to user
            if(blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage("There Has Been a Massive Failure\nPlease Contact IT");
            }
            else
            {
                TheMessagesClass.InformationMessage("The Update Has Completed");
            }
        }

        private void btnAbout_Click_1(object sender, EventArgs e)
        {
            //this will display the about box
            AboutBox AboutBox = new AboutBox();
            AboutBox.ShowDialog();
        }

        private void btnDisplayEventLog_Click(object sender, EventArgs e)
        {
            DisplayEventLog DisplayEventLog = new DisplayEventLog();
            DisplayEventLog.Show();
            this.Close();
        }

        private void btnCreateEmployee_Click(object sender, EventArgs e)
        {
            TheMessagesClass.UnderDevelopment();
        }
    }
}
