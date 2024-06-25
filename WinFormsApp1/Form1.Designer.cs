namespace WinFormsApp1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label_giris = new Label();
            button_go = new Button();
            textbox_boyutGir2 = new TextBox();
            label1 = new Label();
            textbox_boyutGir1 = new TextBox();
            textBox_isim = new TextBox();
            textBox_id = new TextBox();
            label_isim = new Label();
            label_id = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // label_giris
            // 
            label_giris.AccessibleRole = AccessibleRole.Text;
            label_giris.AutoSize = true;
            label_giris.BackColor = Color.Transparent;
            label_giris.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_giris.ForeColor = SystemColors.ButtonHighlight;
            label_giris.Location = new Point(238, 161);
            label_giris.Name = "label_giris";
            label_giris.Size = new Size(0, 34);
            label_giris.TabIndex = 0;
            // 
            // button_go
            // 
            button_go.BackColor = Color.OrangeRed;
            button_go.BackgroundImageLayout = ImageLayout.None;
            button_go.Cursor = Cursors.AppStarting;
            button_go.FlatStyle = FlatStyle.Popup;
            button_go.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_go.ForeColor = SystemColors.ControlLightLight;
            button_go.Location = new Point(218, 307);
            button_go.Name = "button_go";
            button_go.Size = new Size(439, 59);
            button_go.TabIndex = 1;
            button_go.Text = "yeni harita oluştur!";
            button_go.UseVisualStyleBackColor = false;
            button_go.Click += button_go_Click;
            // 
            // textbox_boyutGir2
            // 
            textbox_boyutGir2.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            textbox_boyutGir2.Location = new Point(532, 192);
            textbox_boyutGir2.Name = "textbox_boyutGir2";
            textbox_boyutGir2.Size = new Size(125, 40);
            textbox_boyutGir2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(509, 78);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(171, 34);
            label1.TabIndex = 3;
            label1.Text = "BOYUT GİR";
            // 
            // textbox_boyutGir1
            // 
            textbox_boyutGir1.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            textbox_boyutGir1.Location = new Point(532, 129);
            textbox_boyutGir1.Name = "textbox_boyutGir1";
            textbox_boyutGir1.Size = new Size(125, 40);
            textbox_boyutGir1.TabIndex = 4;
            // 
            // textBox_isim
            // 
            textBox_isim.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            textBox_isim.Location = new Point(218, 129);
            textBox_isim.Name = "textBox_isim";
            textBox_isim.Size = new Size(125, 40);
            textBox_isim.TabIndex = 5;
            // 
            // textBox_id
            // 
            textBox_id.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            textBox_id.Location = new Point(218, 195);
            textBox_id.Name = "textBox_id";
            textBox_id.Size = new Size(125, 40);
            textBox_id.TabIndex = 6;
            // 
            // label_isim
            // 
            label_isim.AutoSize = true;
            label_isim.BackColor = Color.Transparent;
            label_isim.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label_isim.ForeColor = SystemColors.ButtonHighlight;
            label_isim.Location = new Point(112, 135);
            label_isim.Name = "label_isim";
            label_isim.RightToLeft = RightToLeft.Yes;
            label_isim.Size = new Size(88, 34);
            label_isim.TabIndex = 7;
            label_isim.Text = ": İSİM";
            label_isim.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_id
            // 
            label_id.AutoSize = true;
            label_id.BackColor = Color.Transparent;
            label_id.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label_id.ForeColor = SystemColors.ButtonHighlight;
            label_id.Location = new Point(141, 195);
            label_id.Name = "label_id";
            label_id.RightToLeft = RightToLeft.Yes;
            label_id.Size = new Size(59, 34);
            label_id.TabIndex = 8;
            label_id.Text = ": İD";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(438, 135);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.Yes;
            label2.Size = new Size(50, 34);
            label2.TabIndex = 9;
            label2.Text = ": x";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Hotel De Paris", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(438, 192);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.Yes;
            label3.Size = new Size(49, 34);
            label3.TabIndex = 10;
            label3.Text = ": y";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(19F, 49F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(834, 475);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label_id);
            Controls.Add(label_isim);
            Controls.Add(textBox_id);
            Controls.Add(textBox_isim);
            Controls.Add(textbox_boyutGir1);
            Controls.Add(label1);
            Controls.Add(textbox_boyutGir2);
            Controls.Add(button_go);
            Controls.Add(label_giris);
            Font = new Font("Docktrin", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ActiveCaptionText;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(7);
            Name = "Form1";
            Text = "Giriş";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label label_giris;
        private Button button_go;
        private TextBox textbox_boyutGir2;
        private Label label1;
        private TextBox textbox_boyutGir1;
        private TextBox textBox_isim;
        private TextBox textBox_id;
        private Label label_isim;
        private Label label_id;
        private Label label2;
        private Label label3;
    }
}
