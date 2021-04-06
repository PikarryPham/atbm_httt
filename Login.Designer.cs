
namespace atbm
{
    partial class Login
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
            this.label1 = new System.Windows.Forms.Label();
            this.timkiemuserrolelbl = new System.Windows.Forms.Label();
            this.usernamelogin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passlogin = new System.Windows.Forms.TextBox();
            this.btnlogin = new System.Windows.Forms.Button();
            this.warning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(336, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đăng nhập ";
            // 
            // timkiemuserrolelbl
            // 
            this.timkiemuserrolelbl.AutoSize = true;
            this.timkiemuserrolelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timkiemuserrolelbl.Location = new System.Drawing.Point(76, 145);
            this.timkiemuserrolelbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timkiemuserrolelbl.Name = "timkiemuserrolelbl";
            this.timkiemuserrolelbl.Size = new System.Drawing.Size(171, 25);
            this.timkiemuserrolelbl.TabIndex = 8;
            this.timkiemuserrolelbl.Text = "Admin username: ";
            // 
            // usernamelogin
            // 
            this.usernamelogin.Location = new System.Drawing.Point(266, 148);
            this.usernamelogin.Margin = new System.Windows.Forms.Padding(4);
            this.usernamelogin.Name = "usernamelogin";
            this.usernamelogin.Size = new System.Drawing.Size(366, 22);
            this.usernamelogin.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 203);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "Admin password: ";
            // 
            // passlogin
            // 
            this.passlogin.Location = new System.Drawing.Point(266, 206);
            this.passlogin.Margin = new System.Windows.Forms.Padding(4);
            this.passlogin.Name = "passlogin";
            this.passlogin.PasswordChar = '*';
            this.passlogin.Size = new System.Drawing.Size(366, 22);
            this.passlogin.TabIndex = 9;
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(326, 278);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(171, 57);
            this.btnlogin.TabIndex = 11;
            this.btnlogin.Text = "Đăng nhập";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // warning
            // 
            this.warning.AutoSize = true;
            this.warning.ForeColor = System.Drawing.Color.Red;
            this.warning.Location = new System.Drawing.Point(374, 258);
            this.warning.Name = "warning";
            this.warning.Size = new System.Drawing.Size(0, 17);
            this.warning.TabIndex = 12;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 383);
            this.Controls.Add(this.warning);
            this.Controls.Add(this.btnlogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.passlogin);
            this.Controls.Add(this.timkiemuserrolelbl);
            this.Controls.Add(this.usernamelogin);
            this.Controls.Add(this.label1);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timkiemuserrolelbl;
        private System.Windows.Forms.TextBox usernamelogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passlogin;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Label warning;
    }
}