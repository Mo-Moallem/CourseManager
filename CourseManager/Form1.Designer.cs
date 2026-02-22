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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            MapPanel = new Panel();
            label2 = new Label();
            label1 = new Label();
            crnsTextBox = new TextBox();
            sundayBtn = new Button();
            mondayBtn = new Button();
            tuesdayBtn = new Button();
            wednesdayBtn = new Button();
            trusdayBtn = new Button();
            label3 = new Label();
            resultTextBox = new RichTextBox();
            MapPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MapPanel
            // 
            MapPanel.BackgroundImage = (Image)resources.GetObject("MapPanel.BackgroundImage");
            MapPanel.BorderStyle = BorderStyle.FixedSingle;
            MapPanel.Controls.Add(label2);
            MapPanel.ImeMode = ImeMode.NoControl;
            MapPanel.Location = new Point(11, 56);
            MapPanel.Name = "MapPanel";
            MapPanel.Size = new Size(556, 438);
            MapPanel.TabIndex = 3;
            MapPanel.Click += MapPanel_Click;
            MapPanel.Paint += MapPanel_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(113, 203);
            label2.Name = "label2";
            label2.Size = new Size(0, 20);
            label2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 9);
            label1.Name = "label1";
            label1.Size = new Size(195, 20);
            label1.TabIndex = 0;
            label1.Text = "Enter Student CRN Numbers";
            label1.Click += label1_Click;
            // 
            // crnsTextBox
            // 
            crnsTextBox.Location = new Point(213, 5);
            crnsTextBox.Name = "crnsTextBox";
            crnsTextBox.Size = new Size(774, 27);
            crnsTextBox.TabIndex = 1;
            // 
            // sundayBtn
            // 
            sundayBtn.Location = new Point(791, 73);
            sundayBtn.Name = "sundayBtn";
            sundayBtn.Size = new Size(45, 29);
            sundayBtn.TabIndex = 5;
            sundayBtn.Text = "T";
            sundayBtn.UseVisualStyleBackColor = true;
            sundayBtn.Click += tuesdayBtn_Click;
            // 
            // mondayBtn
            // 
            mondayBtn.Location = new Point(842, 73);
            mondayBtn.Name = "mondayBtn";
            mondayBtn.Size = new Size(45, 29);
            mondayBtn.TabIndex = 5;
            mondayBtn.Text = "W";
            mondayBtn.UseVisualStyleBackColor = true;
            mondayBtn.Click += wednesdayBtn_Click;
            // 
            // tuesdayBtn
            // 
            tuesdayBtn.Location = new Point(893, 73);
            tuesdayBtn.Name = "tuesdayBtn";
            tuesdayBtn.Size = new Size(45, 29);
            tuesdayBtn.TabIndex = 5;
            tuesdayBtn.Text = "R";
            tuesdayBtn.UseVisualStyleBackColor = true;
            tuesdayBtn.Click += trusdayBtn_Click;
            // 
            // wednesdayBtn
            // 
            wednesdayBtn.Location = new Point(741, 73);
            wednesdayBtn.Name = "wednesdayBtn";
            wednesdayBtn.Size = new Size(45, 29);
            wednesdayBtn.TabIndex = 5;
            wednesdayBtn.Text = "M";
            wednesdayBtn.UseVisualStyleBackColor = true;
            wednesdayBtn.Click += mondayBtn_Click;
            // 
            // trusdayBtn
            // 
            trusdayBtn.Location = new Point(689, 73);
            trusdayBtn.Name = "trusdayBtn";
            trusdayBtn.Size = new Size(45, 29);
            trusdayBtn.TabIndex = 5;
            trusdayBtn.Text = "U";
            trusdayBtn.UseVisualStyleBackColor = true;
            trusdayBtn.Click += sundayBtn_Click;
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
            // resultTextBox
            // 
            resultTextBox.Location = new Point(573, 108);
            resultTextBox.Name = "resultTextBox";
            resultTextBox.ReadOnly = true;
            resultTextBox.Size = new Size(414, 386);
            resultTextBox.TabIndex = 7;
            resultTextBox.Text = "Hello, World!";
            resultTextBox.TextChanged += richTextBox1_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(992, 498);
            Controls.Add(label3);
            Controls.Add(sundayBtn);
            Controls.Add(resultTextBox);
            Controls.Add(MapPanel);
            Controls.Add(tuesdayBtn);
            Controls.Add(crnsTextBox);
            Controls.Add(wednesdayBtn);
            Controls.Add(label1);
            Controls.Add(mondayBtn);
            Controls.Add(trusdayBtn);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            MapPanel.ResumeLayout(false);
            MapPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox crnsTextBox;
        private Label label2;
        private Panel MapPanel;
        private Button sundayBtn;
        private Button mondayBtn;
        private Button tuesdayBtn;
        private Button wednesdayBtn;
        private Button trusdayBtn;
        private Label label3;
        private RichTextBox resultTextBox;
    }
}
