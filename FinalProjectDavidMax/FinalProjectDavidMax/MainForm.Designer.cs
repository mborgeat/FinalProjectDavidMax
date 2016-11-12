using System.Drawing;

namespace FinalProjectDavidMax
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
            this.btnOpenOriginal = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnSaveNewImage = new System.Windows.Forms.Button();
            this.picPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenOriginal
            // 
            this.btnOpenOriginal.Location = new System.Drawing.Point(30, 648);
            this.btnOpenOriginal.Name = "btnOpenOriginal";
            this.btnOpenOriginal.Size = new System.Drawing.Size(234, 82);
            this.btnOpenOriginal.TabIndex = 1;
            this.btnOpenOriginal.Text = "Load Image";
            this.btnOpenOriginal.UseVisualStyleBackColor = true;
            this.btnOpenOriginal.Click += new System.EventHandler(this.click);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(320, 623);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(234, 133);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.click);
            // 
            // btnSaveNewImage
            // 
            this.btnSaveNewImage.Location = new System.Drawing.Point(609, 648);
            this.btnSaveNewImage.Name = "btnSaveNewImage";
            this.btnSaveNewImage.Size = new System.Drawing.Size(234, 82);
            this.btnSaveNewImage.TabIndex = 3;
            this.btnSaveNewImage.Text = "Save Image";
            this.btnSaveNewImage.UseVisualStyleBackColor = true;
            this.btnSaveNewImage.Click += new System.EventHandler(this.click);
            // 
            // picPreview
            // 
            this.picPreview.Image = global::FinalProjectDavidMax.Properties.Resources.Donald_Trump;
            this.picPreview.Location = new System.Drawing.Point(30, 30);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(813, 574);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 4;
            this.picPreview.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 769);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.btnSaveNewImage);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnOpenOriginal);
            this.Name = "MainForm";
            this.Text = "Laplacian 3x3 Edge Filter";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOpenOriginal;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnSaveNewImage;
        private System.Windows.Forms.PictureBox picPreview;


    }
}

