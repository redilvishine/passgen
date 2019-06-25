namespace passgen
{
    partial class MainForm
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
            this.serviceSelector = new System.Windows.Forms.ComboBox();
            this.algoSelector = new System.Windows.Forms.ComboBox();
            this.loginInput = new System.Windows.Forms.TextBox();
            this.masterInput = new System.Windows.Forms.TextBox();
            this.hashOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // serviceSelector
            // 
            this.serviceSelector.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.serviceSelector.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.serviceSelector.FormattingEnabled = true;
            this.serviceSelector.Location = new System.Drawing.Point(12, 12);
            this.serviceSelector.Name = "serviceSelector";
            this.serviceSelector.Size = new System.Drawing.Size(349, 21);
            this.serviceSelector.TabIndex = 0;
            this.serviceSelector.Tag = "Выберите сервис";
            // 
            // algoSelector
            // 
            this.algoSelector.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.algoSelector.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.algoSelector.FormattingEnabled = true;
            this.algoSelector.Items.AddRange(new object[] {
            "Зеленый",
            "Синий",
            "Желтый",
            "Использовать свой...",
            "Создать новый..."});
            this.algoSelector.Location = new System.Drawing.Point(13, 66);
            this.algoSelector.Name = "algoSelector";
            this.algoSelector.Size = new System.Drawing.Size(348, 21);
            this.algoSelector.TabIndex = 2;
            this.algoSelector.Tag = "Выберите алгоритм";
            this.algoSelector.SelectedIndexChanged += new System.EventHandler(this.AlgoSelector_SelectedIndexChanged);
            // 
            // loginInput
            // 
            this.loginInput.Location = new System.Drawing.Point(13, 40);
            this.loginInput.Name = "loginInput";
            this.loginInput.Size = new System.Drawing.Size(348, 20);
            this.loginInput.TabIndex = 1;
            this.loginInput.Tag = "Введите логин";
            // 
            // masterInput
            // 
            this.masterInput.Location = new System.Drawing.Point(13, 94);
            this.masterInput.Name = "masterInput";
            this.masterInput.Size = new System.Drawing.Size(348, 20);
            this.masterInput.TabIndex = 3;
            this.masterInput.Tag = "Введите мастер-ключ";
            // 
            // hashOutput
            // 
            this.hashOutput.BackColor = System.Drawing.SystemColors.Info;
            this.hashOutput.Location = new System.Drawing.Point(12, 130);
            this.hashOutput.Name = "hashOutput";
            this.hashOutput.Size = new System.Drawing.Size(349, 20);
            this.hashOutput.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 165);
            this.Controls.Add(this.hashOutput);
            this.Controls.Add(this.masterInput);
            this.Controls.Add(this.loginInput);
            this.Controls.Add(this.algoSelector);
            this.Controls.Add(this.serviceSelector);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PassGen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox serviceSelector;
        private System.Windows.Forms.ComboBox algoSelector;
        private System.Windows.Forms.TextBox loginInput;
        private System.Windows.Forms.TextBox masterInput;
        private System.Windows.Forms.TextBox hashOutput;
    }
}