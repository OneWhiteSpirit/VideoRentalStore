namespace VideoRentalStore.GUI_Classes
{
    partial class ViewClientCardForReturn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewClientCardForReturn));
            this.panel1 = new System.Windows.Forms.Panel();
            this.DebtTextBox = new System.Windows.Forms.TextBox();
            this.AgeTextBox = new System.Windows.Forms.TextBox();
            this.ClientDataGridView = new System.Windows.Forms.DataGridView();
            this.RegistrDateTextBox = new System.Windows.Forms.TextBox();
            this.GenderTextBox = new System.Windows.Forms.TextBox();
            this.ClientTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DebtTextBox);
            this.panel1.Controls.Add(this.AgeTextBox);
            this.panel1.Controls.Add(this.ClientDataGridView);
            this.panel1.Controls.Add(this.RegistrDateTextBox);
            this.panel1.Controls.Add(this.GenderTextBox);
            this.panel1.Controls.Add(this.ClientTextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 378);
            this.panel1.TabIndex = 4;
            // 
            // DebtTextBox
            // 
            this.DebtTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DebtTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DebtTextBox.Location = new System.Drawing.Point(191, 138);
            this.DebtTextBox.Name = "DebtTextBox";
            this.DebtTextBox.ReadOnly = true;
            this.DebtTextBox.Size = new System.Drawing.Size(377, 18);
            this.DebtTextBox.TabIndex = 5;
            // 
            // AgeTextBox
            // 
            this.AgeTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AgeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AgeTextBox.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AgeTextBox.Location = new System.Drawing.Point(191, 77);
            this.AgeTextBox.Name = "AgeTextBox";
            this.AgeTextBox.ReadOnly = true;
            this.AgeTextBox.Size = new System.Drawing.Size(377, 18);
            this.AgeTextBox.TabIndex = 3;
            // 
            // ClientDataGridView
            // 
            this.ClientDataGridView.AllowUserToAddRows = false;
            this.ClientDataGridView.AllowUserToDeleteRows = false;
            this.ClientDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.ClientDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClientDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClientDataGridView.Location = new System.Drawing.Point(-1, 177);
            this.ClientDataGridView.Name = "ClientDataGridView";
            this.ClientDataGridView.ReadOnly = true;
            this.ClientDataGridView.Size = new System.Drawing.Size(785, 200);
            this.ClientDataGridView.TabIndex = 2;
            this.ClientDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClientDataGridView_CellContentClick);
            // 
            // RegistrDateTextBox
            // 
            this.RegistrDateTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RegistrDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RegistrDateTextBox.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RegistrDateTextBox.Location = new System.Drawing.Point(191, 107);
            this.RegistrDateTextBox.Name = "RegistrDateTextBox";
            this.RegistrDateTextBox.ReadOnly = true;
            this.RegistrDateTextBox.Size = new System.Drawing.Size(377, 18);
            this.RegistrDateTextBox.TabIndex = 4;
            // 
            // GenderTextBox
            // 
            this.GenderTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GenderTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GenderTextBox.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GenderTextBox.Location = new System.Drawing.Point(191, 47);
            this.GenderTextBox.Name = "GenderTextBox";
            this.GenderTextBox.ReadOnly = true;
            this.GenderTextBox.Size = new System.Drawing.Size(377, 18);
            this.GenderTextBox.TabIndex = 2;
            // 
            // ClientTextBox
            // 
            this.ClientTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ClientTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClientTextBox.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClientTextBox.Location = new System.Drawing.Point(191, 17);
            this.ClientTextBox.Name = "ClientTextBox";
            this.ClientTextBox.ReadOnly = true;
            this.ClientTextBox.Size = new System.Drawing.Size(377, 18);
            this.ClientTextBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(14, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Имеется ли задолжность:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Клиент:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(14, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Дата регистрации:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(14, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Пол:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(14, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Возраст:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ViewClientCardForReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 403);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ViewClientCardForReturn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Карта клиента: возврат фильма";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewClientCardForReturn_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox DebtTextBox;
        public System.Windows.Forms.TextBox AgeTextBox;
        public System.Windows.Forms.DataGridView ClientDataGridView;
        public System.Windows.Forms.TextBox RegistrDateTextBox;
        public System.Windows.Forms.TextBox GenderTextBox;
        public System.Windows.Forms.TextBox ClientTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}