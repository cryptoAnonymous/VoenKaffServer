namespace VoenKaffServer
{
    partial class FormLogin
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
            this.buttonGoAdmin = new System.Windows.Forms.Button();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelUserPassword = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonGoAdmin
            // 
            this.buttonGoAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGoAdmin.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.buttonGoAdmin.Location = new System.Drawing.Point(308, 199);
            this.buttonGoAdmin.Name = "buttonGoAdmin";
            this.buttonGoAdmin.Size = new System.Drawing.Size(162, 45);
            this.buttonGoAdmin.TabIndex = 0;
            this.buttonGoAdmin.Text = "Войти";
            this.buttonGoAdmin.UseVisualStyleBackColor = true;
            this.buttonGoAdmin.Click += new System.EventHandler(this.buttonGoAdmin_Click);
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.labelUserName.Location = new System.Drawing.Point(136, 123);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(142, 20);
            this.labelUserName.TabIndex = 1;
            this.labelUserName.Text = "Имя пользователя";
            // 
            // labelUserPassword
            // 
            this.labelUserPassword.AutoSize = true;
            this.labelUserPassword.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.labelUserPassword.Location = new System.Drawing.Point(216, 160);
            this.labelUserPassword.Name = "labelUserPassword";
            this.labelUserPassword.Size = new System.Drawing.Size(62, 20);
            this.labelUserPassword.TabIndex = 2;
            this.labelUserPassword.Text = "Пароль";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(284, 123);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(209, 20);
            this.textBoxLogin.TabIndex = 3;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(284, 160);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(209, 20);
            this.textBoxPassword.TabIndex = 4;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 427);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.labelUserPassword);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.buttonGoAdmin);
            this.Name = "FormLogin";
            this.Text = "Вход";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGoAdmin;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelUserPassword;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
    }
}