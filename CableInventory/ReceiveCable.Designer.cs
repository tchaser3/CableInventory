namespace CableInventory
{
    partial class ReceiveCable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.btnClearGrid = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cboReturnType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoNew = new System.Windows.Forms.RadioButton();
            this.rdoReturn = new System.Windows.Forms.RadioButton();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWarehouseID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProjectID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReelID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMSR = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFootage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.dgvEnteredTransactions = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTransactionID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnteredTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CableInventory.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(688, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(171, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 45;
            this.pictureBox1.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(12, 108);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(1532, 58);
            this.Label1.TabIndex = 44;
            this.Label1.Text = "Receive Cable";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(422, 421);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(163, 60);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainMenu.Location = new System.Drawing.Point(422, 357);
            this.btnMainMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(163, 60);
            this.btnMainMenu.TabIndex = 12;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // btnClearGrid
            // 
            this.btnClearGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearGrid.Location = new System.Drawing.Point(422, 293);
            this.btnClearGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClearGrid.Name = "btnClearGrid";
            this.btnClearGrid.Size = new System.Drawing.Size(163, 60);
            this.btnClearGrid.TabIndex = 11;
            this.btnClearGrid.Text = "Clear Grid";
            this.btnClearGrid.UseVisualStyleBackColor = true;
            this.btnClearGrid.Click += new System.EventHandler(this.btnClearGrid_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(422, 229);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(163, 60);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cboReturnType
            // 
            this.cboReturnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReturnType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReturnType.FormattingEnabled = true;
            this.cboReturnType.Location = new System.Drawing.Point(229, 216);
            this.cboReturnType.Name = "cboReturnType";
            this.cboReturnType.Size = new System.Drawing.Size(165, 28);
            this.cboReturnType.TabIndex = 2;
            this.cboReturnType.SelectedIndexChanged += new System.EventHandler(this.cboReturnType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 23);
            this.label2.TabIndex = 53;
            this.label2.Text = "Select Reel or Hand Coil";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rdoNew
            // 
            this.rdoNew.AutoSize = true;
            this.rdoNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNew.Location = new System.Drawing.Point(110, 178);
            this.rdoNew.Name = "rdoNew";
            this.rdoNew.Size = new System.Drawing.Size(63, 24);
            this.rdoNew.TabIndex = 0;
            this.rdoNew.TabStop = true;
            this.rdoNew.Text = "New";
            this.rdoNew.UseVisualStyleBackColor = true;
            this.rdoNew.CheckedChanged += new System.EventHandler(this.rdoNew_CheckedChanged);
            // 
            // rdoReturn
            // 
            this.rdoReturn.AutoSize = true;
            this.rdoReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoReturn.Location = new System.Drawing.Point(231, 178);
            this.rdoReturn.Name = "rdoReturn";
            this.rdoReturn.Size = new System.Drawing.Size(80, 24);
            this.rdoReturn.TabIndex = 1;
            this.rdoReturn.TabStop = true;
            this.rdoReturn.Text = "Return";
            this.rdoReturn.UseVisualStyleBackColor = true;
            this.rdoReturn.CheckedChanged += new System.EventHandler(this.rdoReturn_CheckedChanged);
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPartNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartNumber.Location = new System.Drawing.Point(227, 289);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.Size = new System.Drawing.Size(165, 27);
            this.txtPartNumber.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 23);
            this.label3.TabIndex = 57;
            this.label3.Text = "Part Number";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 324);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 23);
            this.label4.TabIndex = 59;
            this.label4.Text = "Warehouse ID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWarehouseID
            // 
            this.txtWarehouseID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWarehouseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWarehouseID.Location = new System.Drawing.Point(227, 322);
            this.txtWarehouseID.Name = "txtWarehouseID";
            this.txtWarehouseID.ReadOnly = true;
            this.txtWarehouseID.Size = new System.Drawing.Size(165, 27);
            this.txtWarehouseID.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 357);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(213, 23);
            this.label5.TabIndex = 61;
            this.label5.Text = "Project ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProjectID
            // 
            this.txtProjectID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjectID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjectID.Location = new System.Drawing.Point(229, 355);
            this.txtProjectID.Name = "txtProjectID";
            this.txtProjectID.Size = new System.Drawing.Size(163, 27);
            this.txtProjectID.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 390);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 23);
            this.label6.TabIndex = 63;
            this.label6.Text = "Reel ID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReelID
            // 
            this.txtReelID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReelID.Location = new System.Drawing.Point(229, 388);
            this.txtReelID.Name = "txtReelID";
            this.txtReelID.Size = new System.Drawing.Size(163, 27);
            this.txtReelID.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 423);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(213, 23);
            this.label7.TabIndex = 65;
            this.label7.Text = "MSR";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMSR
            // 
            this.txtMSR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMSR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMSR.Location = new System.Drawing.Point(229, 421);
            this.txtMSR.Name = "txtMSR";
            this.txtMSR.Size = new System.Drawing.Size(163, 27);
            this.txtMSR.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 456);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(213, 23);
            this.label8.TabIndex = 67;
            this.label8.Text = "Footage";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFootage
            // 
            this.txtFootage.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFootage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFootage.Location = new System.Drawing.Point(229, 454);
            this.txtFootage.Name = "txtFootage";
            this.txtFootage.Size = new System.Drawing.Size(163, 27);
            this.txtFootage.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 489);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(213, 23);
            this.label9.TabIndex = 69;
            this.label9.Text = "Date";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDate
            // 
            this.txtDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Location = new System.Drawing.Point(229, 487);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(163, 27);
            this.txtDate.TabIndex = 9;
            // 
            // dgvEnteredTransactions
            // 
            this.dgvEnteredTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnteredTransactions.Location = new System.Drawing.Point(608, 229);
            this.dgvEnteredTransactions.Name = "dgvEnteredTransactions";
            this.dgvEnteredTransactions.RowTemplate.Height = 24;
            this.dgvEnteredTransactions.Size = new System.Drawing.Size(936, 252);
            this.dgvEnteredTransactions.TabIndex = 70;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 257);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(213, 23);
            this.label10.TabIndex = 72;
            this.label10.Text = "TransactionID";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTransactionID
            // 
            this.txtTransactionID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTransactionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransactionID.Location = new System.Drawing.Point(227, 255);
            this.txtTransactionID.Name = "txtTransactionID";
            this.txtTransactionID.ReadOnly = true;
            this.txtTransactionID.Size = new System.Drawing.Size(165, 27);
            this.txtTransactionID.TabIndex = 71;
            // 
            // ReceiveCable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 539);
            this.ControlBox = false;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTransactionID);
            this.Controls.Add(this.dgvEnteredTransactions);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFootage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMSR);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtReelID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtProjectID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWarehouseID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPartNumber);
            this.Controls.Add(this.rdoReturn);
            this.Controls.Add(this.rdoNew);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboReturnType);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClearGrid);
            this.Controls.Add(this.btnMainMenu);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Label1);
            this.Name = "ReceiveCable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReceiveCable";
            this.Load += new System.EventHandler(this.ReceiveCable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnteredTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnMainMenu;
        internal System.Windows.Forms.Button btnClearGrid;
        internal System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cboReturnType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoNew;
        private System.Windows.Forms.RadioButton rdoReturn;
        private System.Windows.Forms.TextBox txtPartNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWarehouseID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProjectID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtReelID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMSR;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFootage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.DataGridView dgvEnteredTransactions;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTransactionID;
    }
}