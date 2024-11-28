namespace BudgetApp_V2
{
    partial class StartMenu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartMenu));
            this.budgetReportButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.confirmLabel = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.amountLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.transDateLabel = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.transactionAmountTextBox = new System.Windows.Forms.TextBox();
            this.transactionDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.transactionDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.charityBalanceLabel = new System.Windows.Forms.Label();
            this.amountCalculatedLabel = new System.Windows.Forms.Label();
            this.wagesAndTaxesButton = new System.Windows.Forms.Button();
            this.openDBButton1 = new System.Windows.Forms.Button();
            this.updateDbButton = new System.Windows.Forms.Button();
            this.cancelUpdateButton = new System.Windows.Forms.Button();
            this.deleteTransactionButton1 = new System.Windows.Forms.Button();
            this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fromLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.transactionTypeFilterComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // budgetReportButton
            // 
            this.budgetReportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.budgetReportButton.Location = new System.Drawing.Point(157, 62);
            this.budgetReportButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.budgetReportButton.Name = "budgetReportButton";
            this.budgetReportButton.Size = new System.Drawing.Size(149, 38);
            this.budgetReportButton.TabIndex = 4;
            this.budgetReportButton.Text = "Budget Report";
            this.budgetReportButton.UseVisualStyleBackColor = true;
            this.budgetReportButton.Click += new System.EventHandler(this.budgetReportButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(313, 62);
            this.exitButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(149, 38);
            this.exitButton.TabIndex = 7;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // confirmLabel
            // 
            this.confirmLabel.AutoSize = true;
            this.confirmLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.confirmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmLabel.ForeColor = System.Drawing.Color.Black;
            this.confirmLabel.Location = new System.Drawing.Point(273, 465);
            this.confirmLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.confirmLabel.Name = "confirmLabel";
            this.confirmLabel.Size = new System.Drawing.Size(89, 20);
            this.confirmLabel.TabIndex = 23;
            this.confirmLabel.Text = "Click again.";
            this.confirmLabel.Visible = false;
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(321, 425);
            this.clearButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(76, 30);
            this.clearButton.TabIndex = 21;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.Location = new System.Drawing.Point(241, 425);
            this.submitButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(76, 30);
            this.submitButton.TabIndex = 20;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryLabel.Location = new System.Drawing.Point(143, 350);
            this.categoryLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(69, 17);
            this.categoryLabel.TabIndex = 19;
            this.categoryLabel.Text = "Category:";
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountLabel.Location = new System.Drawing.Point(143, 306);
            this.amountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(143, 17);
            this.amountLabel.TabIndex = 18;
            this.amountLabel.Text = "Transaction Amount: ";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.Location = new System.Drawing.Point(143, 257);
            this.descriptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(83, 17);
            this.descriptionLabel.TabIndex = 17;
            this.descriptionLabel.Text = "Description:";
            // 
            // transDateLabel
            // 
            this.transDateLabel.AutoSize = true;
            this.transDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transDateLabel.Location = new System.Drawing.Point(143, 209);
            this.transDateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.transDateLabel.Name = "transDateLabel";
            this.transDateLabel.Size = new System.Drawing.Size(137, 17);
            this.transDateLabel.TabIndex = 16;
            this.transDateLabel.Text = "Date of Transaction:";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(313, 350);
            this.categoryComboBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(238, 25);
            this.categoryComboBox.Sorted = true;
            this.categoryComboBox.TabIndex = 15;
            this.categoryComboBox.SelectedIndexChanged += new System.EventHandler(this.CategoryComboBox_SelectedIndexChanged);
            // 
            // transactionAmountTextBox
            // 
            this.transactionAmountTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transactionAmountTextBox.Location = new System.Drawing.Point(313, 306);
            this.transactionAmountTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.transactionAmountTextBox.Name = "transactionAmountTextBox";
            this.transactionAmountTextBox.Size = new System.Drawing.Size(238, 23);
            this.transactionAmountTextBox.TabIndex = 14;
            this.transactionAmountTextBox.TextChanged += new System.EventHandler(this.transactionAmountTextBox_TextChanged);
            // 
            // transactionDescriptionTextBox
            // 
            this.transactionDescriptionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transactionDescriptionTextBox.Location = new System.Drawing.Point(313, 255);
            this.transactionDescriptionTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.transactionDescriptionTextBox.Name = "transactionDescriptionTextBox";
            this.transactionDescriptionTextBox.Size = new System.Drawing.Size(238, 23);
            this.transactionDescriptionTextBox.TabIndex = 13;
            // 
            // transactionDateTimePicker
            // 
            this.transactionDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transactionDateTimePicker.Location = new System.Drawing.Point(313, 208);
            this.transactionDateTimePicker.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.transactionDateTimePicker.Name = "transactionDateTimePicker";
            this.transactionDateTimePicker.Size = new System.Drawing.Size(238, 23);
            this.transactionDateTimePicker.TabIndex = 12;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.description,
            this.Column2,
            this.id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(631, 162);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(470, 135);
            this.dataGridView1.TabIndex = 24;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Date";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            // 
            // description
            // 
            this.description.HeaderText = "Description";
            this.description.MinimumWidth = 8;
            this.description.Name = "description";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Amount";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.MinimumWidth = 8;
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(785, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 20);
            this.label1.TabIndex = 25;
            this.label1.Text = "Recent Transactions";
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox.Location = new System.Drawing.Point(202, 389);
            this.checkBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(271, 22);
            this.checkBox.TabIndex = 29;
            this.checkBox.Text = "Apply 10+% towards charity balance?";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.Visible = false;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // charityBalanceLabel
            // 
            this.charityBalanceLabel.AutoSize = true;
            this.charityBalanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charityBalanceLabel.Location = new System.Drawing.Point(237, 497);
            this.charityBalanceLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.charityBalanceLabel.Name = "charityBalanceLabel";
            this.charityBalanceLabel.Size = new System.Drawing.Size(46, 18);
            this.charityBalanceLabel.TabIndex = 30;
            this.charityBalanceLabel.Text = "label2";
            // 
            // amountCalculatedLabel
            // 
            this.amountCalculatedLabel.AutoSize = true;
            this.amountCalculatedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountCalculatedLabel.Location = new System.Drawing.Point(554, 306);
            this.amountCalculatedLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.amountCalculatedLabel.Name = "amountCalculatedLabel";
            this.amountCalculatedLabel.Size = new System.Drawing.Size(35, 13);
            this.amountCalculatedLabel.TabIndex = 31;
            this.amountCalculatedLabel.Text = "label2";
            // 
            // wagesAndTaxesButton
            // 
            this.wagesAndTaxesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wagesAndTaxesButton.Location = new System.Drawing.Point(157, 105);
            this.wagesAndTaxesButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.wagesAndTaxesButton.Name = "wagesAndTaxesButton";
            this.wagesAndTaxesButton.Size = new System.Drawing.Size(149, 38);
            this.wagesAndTaxesButton.TabIndex = 32;
            this.wagesAndTaxesButton.Text = "Wages and Taxes";
            this.wagesAndTaxesButton.UseVisualStyleBackColor = true;
            this.wagesAndTaxesButton.Click += new System.EventHandler(this.wagesAndTaxesButton_Click);
            // 
            // openDBButton1
            // 
            this.openDBButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openDBButton1.Location = new System.Drawing.Point(313, 105);
            this.openDBButton1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.openDBButton1.Name = "openDBButton1";
            this.openDBButton1.Size = new System.Drawing.Size(149, 38);
            this.openDBButton1.TabIndex = 33;
            this.openDBButton1.Text = "Open DB";
            this.openDBButton1.UseVisualStyleBackColor = true;
            this.openDBButton1.Click += new System.EventHandler(this.openDBButton1_Click);
            // 
            // updateDbButton
            // 
            this.updateDbButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateDbButton.Location = new System.Drawing.Point(1105, 130);
            this.updateDbButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.updateDbButton.Name = "updateDbButton";
            this.updateDbButton.Size = new System.Drawing.Size(91, 38);
            this.updateDbButton.TabIndex = 34;
            this.updateDbButton.Text = "Update";
            this.updateDbButton.UseVisualStyleBackColor = true;
            this.updateDbButton.Visible = false;
            this.updateDbButton.Click += new System.EventHandler(this.updateDbButton_Click);
            // 
            // cancelUpdateButton
            // 
            this.cancelUpdateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelUpdateButton.Location = new System.Drawing.Point(1105, 173);
            this.cancelUpdateButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cancelUpdateButton.Name = "cancelUpdateButton";
            this.cancelUpdateButton.Size = new System.Drawing.Size(91, 42);
            this.cancelUpdateButton.TabIndex = 35;
            this.cancelUpdateButton.Text = "Cancel Update";
            this.cancelUpdateButton.UseVisualStyleBackColor = true;
            this.cancelUpdateButton.Visible = false;
            this.cancelUpdateButton.Click += new System.EventHandler(this.cancelUpdateButton_Click);
            // 
            // deleteTransactionButton1
            // 
            this.deleteTransactionButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteTransactionButton1.Location = new System.Drawing.Point(1105, 220);
            this.deleteTransactionButton1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.deleteTransactionButton1.Name = "deleteTransactionButton1";
            this.deleteTransactionButton1.Size = new System.Drawing.Size(91, 38);
            this.deleteTransactionButton1.TabIndex = 36;
            this.deleteTransactionButton1.Text = "Delete";
            this.deleteTransactionButton1.UseVisualStyleBackColor = true;
            this.deleteTransactionButton1.Visible = false;
            this.deleteTransactionButton1.Click += new System.EventHandler(this.deleteTransactionButton1_Click);
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDateTimePicker.Location = new System.Drawing.Point(631, 105);
            this.fromDateTimePicker.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.Size = new System.Drawing.Size(238, 23);
            this.fromDateTimePicker.TabIndex = 37;
            this.fromDateTimePicker.ValueChanged += new System.EventHandler(this.fromDateTimePicker_ValueChanged);
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDateTimePicker.Location = new System.Drawing.Point(872, 105);
            this.toDateTimePicker.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.Size = new System.Drawing.Size(238, 23);
            this.toDateTimePicker.TabIndex = 38;
            this.toDateTimePicker.ValueChanged += new System.EventHandler(this.toDateTimePicker_ValueChanged);
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromLabel.Location = new System.Drawing.Point(628, 86);
            this.fromLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(44, 17);
            this.fromLabel.TabIndex = 39;
            this.fromLabel.Text = "From:";
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toLabel.Location = new System.Drawing.Point(869, 86);
            this.toLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(29, 17);
            this.toLabel.TabIndex = 40;
            this.toLabel.Text = "To:";
            // 
            // transactionTypeFilterComboBox
            // 
            this.transactionTypeFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transactionTypeFilterComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transactionTypeFilterComboBox.FormattingEnabled = true;
            this.transactionTypeFilterComboBox.Location = new System.Drawing.Point(755, 131);
            this.transactionTypeFilterComboBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.transactionTypeFilterComboBox.Name = "transactionTypeFilterComboBox";
            this.transactionTypeFilterComboBox.Size = new System.Drawing.Size(238, 25);
            this.transactionTypeFilterComboBox.Sorted = true;
            this.transactionTypeFilterComboBox.TabIndex = 41;
            this.transactionTypeFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.transactionTypeFilterComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(628, 134);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 17);
            this.label2.TabIndex = 42;
            this.label2.Text = "Transaction Type:";
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1284, 456);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.transactionTypeFilterComboBox);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.fromLabel);
            this.Controls.Add(this.toDateTimePicker);
            this.Controls.Add(this.fromDateTimePicker);
            this.Controls.Add(this.deleteTransactionButton1);
            this.Controls.Add(this.cancelUpdateButton);
            this.Controls.Add(this.updateDbButton);
            this.Controls.Add(this.openDBButton1);
            this.Controls.Add(this.wagesAndTaxesButton);
            this.Controls.Add(this.amountCalculatedLabel);
            this.Controls.Add(this.charityBalanceLabel);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.confirmLabel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.transDateLabel);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.transactionAmountTextBox);
            this.Controls.Add(this.transactionDescriptionTextBox);
            this.Controls.Add(this.transactionDateTimePicker);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.budgetReportButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "StartMenu";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.StartMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button budgetReportButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label confirmLabel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label transDateLabel;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.TextBox transactionAmountTextBox;
        private System.Windows.Forms.TextBox transactionDescriptionTextBox;
        private System.Windows.Forms.DateTimePicker transactionDateTimePicker;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Label charityBalanceLabel;
        private System.Windows.Forms.Label amountCalculatedLabel;
        private System.Windows.Forms.Button wagesAndTaxesButton;
        private System.Windows.Forms.Button openDBButton1;
        private System.Windows.Forms.Button updateDbButton;
        private System.Windows.Forms.Button cancelUpdateButton;
        private System.Windows.Forms.Button deleteTransactionButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DateTimePicker fromDateTimePicker;
        private System.Windows.Forms.DateTimePicker toDateTimePicker;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.ComboBox transactionTypeFilterComboBox;
        private System.Windows.Forms.Label label2;
    }
}

