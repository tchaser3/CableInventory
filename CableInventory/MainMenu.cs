/* Title:           Main Menu
 * Date:            5-22-16
 * Author:          Terry Holmes
 *
 * Description:     This form is the main menu */

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

namespace CableInventory
{
    public partial class MainMenu : Form
    {
        //setting up the class
        MessagesClass TheMessagesClass = new MessagesClass();

        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this will close the program
            TheMessagesClass.CloseTheProgram();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox AboutBox = new AboutBox();
            AboutBox.ShowDialog();
        }

        private void btnAdministrativeMenu_Click(object sender, EventArgs e)
        {
            AdminMenu AdminMenu = new AdminMenu();
            AdminMenu.Show();
            this.Close();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            TheMessagesClass.UnderDevelopment();
        }

        private void btnViewProjects_Click(object sender, EventArgs e)
        {
            TheMessagesClass.UnderDevelopment();
        }

        private void btnViewCableInventory_Click(object sender, EventArgs e)
        {
            TheMessagesClass.UnderDevelopment();
        }

        private void btnBOMCable_Click(object sender, EventArgs e)
        {
            BOMCable BOMCable = new BOMCable();
            BOMCable.Show();
            this.Close();
        }

        private void btnIssueCable_Click(object sender, EventArgs e)
        {
            IssueCable IssueCable = new IssueCable();
            IssueCable.Show();
            this.Close();
        }

        private void btnReceiveCable_Click(object sender, EventArgs e)
        {
            //this will open the Receive Cable
            ReceiveCable ReceiveCable = new ReceiveCable();
            ReceiveCable.Show();
            this.Close();
        }
    }
}
