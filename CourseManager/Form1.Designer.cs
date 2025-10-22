namespace CourseManager
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
            Panel panel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label2 = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            label3 = new Label();
            richTextBox1 = new RichTextBox();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(label2);
            panel1.ImeMode = ImeMode.NoControl;
            panel1.Location = new Point(12, 39);
            panel1.Name = "panel1";
            panel1.Size = new Size(624, 598);
            panel1.TabIndex = 3;
            panel1.Paint += panel1_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(113, 202);
            label2.Name = "label2";
            label2.Size = new Size(0, 20);
            label2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(195, 20);
            label1.TabIndex = 0;
            label1.Text = "Enter Student CRN Numbers";
            label1.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(213, 6);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(774, 27);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(791, 73);
            button1.Name = "button1";
            button1.Size = new Size(45, 29);
            button1.TabIndex = 5;
            button1.Text = "T";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(842, 73);
            button2.Name = "button2";
            button2.Size = new Size(45, 29);
            button2.TabIndex = 5;
            button2.Text = "W";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(893, 73);
            button3.Name = "button3";
            button3.Size = new Size(45, 29);
            button3.TabIndex = 5;
            button3.Text = "R";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(740, 73);
            button4.Name = "button4";
            button4.Size = new Size(45, 29);
            button4.TabIndex = 5;
            button4.Text = "M";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(689, 73);
            button5.Name = "button5";
            button5.Size = new Size(45, 29);
            button5.TabIndex = 5;
            button5.Text = "U";
            button5.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(785, 39);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 6;
            label3.Text = "Results";
            label3.Click += label3_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(642, 108);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(345, 529);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "Hello, World!";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(996, 649);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            Controls.Add(panel1);
            Controls.Add(button3);
            Controls.Add(textBox1);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button5);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Panel panel1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Label label3;
        private RichTextBox richTextBox1;
    }
}
