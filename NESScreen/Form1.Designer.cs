
using System.Drawing;

namespace NESScreen
{
    partial class NESView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.NES_screen_output = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NES_screen_output)).BeginInit();
            this.SuspendLayout();
            // 
            // NES_screen_output
            // 
            this.NES_screen_output.Location = new System.Drawing.Point(0, 0);
            this.NES_screen_output.Name = "NES_screen_output";
            this.NES_screen_output.Size = new System.Drawing.Size(256, 240);
            this.NES_screen_output.TabIndex = 0;
            this.NES_screen_output.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StartEmulation);
            // 
            // NESView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 665);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.NES_screen_output);
            this.Name = "NESView";
            this.Text = "NES Emulator";
            ((System.ComponentModel.ISupportInitialize)(this.NES_screen_output)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox NES_screen_output;
        private System.Windows.Forms.Button button1;
    }
}

