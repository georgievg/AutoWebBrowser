namespace Face_Browser_Demo
{
    partial class Form1
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
            this.autoBrowser1 = new Auto_Browser.AutoBrowser();
            this.SuspendLayout();
            // 
            // autoBrowser1
            // 
            this.autoBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoBrowser1.Location = new System.Drawing.Point(0, 0);
            this.autoBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.autoBrowser1.Name = "autoBrowser1";
            this.autoBrowser1.Size = new System.Drawing.Size(620, 458);
            this.autoBrowser1.TabIndex = 0;
            this.autoBrowser1.TaskExecutionDelay = 100;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 458);
            this.Controls.Add(this.autoBrowser1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Auto_Browser.AutoBrowser autoBrowser1;
    }
}

