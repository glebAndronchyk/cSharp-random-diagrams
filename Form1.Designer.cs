namespace lb5_2
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
            this.buildButton = new System.Windows.Forms.Button();
            this.randomPointsButton = new System.Windows.Forms.Button();
            this.multithreadToggle = new System.Windows.Forms.CheckBox();
            this.randomPointsAmount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.randomPointsAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // buildButton
            // 
            this.buildButton.Location = new System.Drawing.Point(12, 12);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(75, 23);
            this.buildButton.TabIndex = 0;
            this.buildButton.Text = "Build";
            this.buildButton.UseVisualStyleBackColor = true;
            this.buildButton.Click += new System.EventHandler(this.buildButton_click);
            // 
            // randomPointsButton
            // 
            this.randomPointsButton.Location = new System.Drawing.Point(136, 12);
            this.randomPointsButton.Name = "randomPointsButton";
            this.randomPointsButton.Size = new System.Drawing.Size(174, 23);
            this.randomPointsButton.TabIndex = 1;
            this.randomPointsButton.Text = "Place random points";
            this.randomPointsButton.UseVisualStyleBackColor = true;
            this.randomPointsButton.Click += new System.EventHandler(this.placeRandomPointsButton_click);
            // 
            // multithreadToggle
            // 
            this.multithreadToggle.AutoSize = true;
            this.multithreadToggle.Location = new System.Drawing.Point(13, 42);
            this.multithreadToggle.Name = "multithreadToggle";
            this.multithreadToggle.Size = new System.Drawing.Size(78, 17);
            this.multithreadToggle.TabIndex = 3;
            this.multithreadToggle.Text = "Multithread";
            this.multithreadToggle.UseVisualStyleBackColor = true;
            this.multithreadToggle.CheckedChanged += new System.EventHandler(this.multithreadToggle_CheckedChanged);
            // 
            // randomPointsAmount
            // 
            this.randomPointsAmount.Location = new System.Drawing.Point(93, 15);
            this.randomPointsAmount.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.randomPointsAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.randomPointsAmount.Name = "randomPointsAmount";
            this.randomPointsAmount.Size = new System.Drawing.Size(37, 20);
            this.randomPointsAmount.TabIndex = 4;
            this.randomPointsAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.randomPointsAmount);
            this.Controls.Add(this.multithreadToggle);
            this.Controls.Add(this.randomPointsButton);
            this.Controls.Add(this.buildButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.randomPointsAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buildButton;
        private System.Windows.Forms.Button randomPointsButton;
        private System.Windows.Forms.CheckBox multithreadToggle;
        private System.Windows.Forms.NumericUpDown randomPointsAmount;
    }
}