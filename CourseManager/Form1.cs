using System.Collections;
using System.Collections.Generic;

namespace CourseManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void sundayBtn_Click(object sender, EventArgs e)
        {
            List<int> crrns = getCrns();
            string message = "Button 1 Clicked\nCRNs: " + string.Join(", ", crrns);
            MessageBox.Show(message);
        }
        private void mondayBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 2 Clicked");
        }
        private void tuesdayBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 3 Clicked");
        }

        private void wednesdayBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 4 Clicked");
        }
        
        private void trusdayBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 5 Clicked");
        }
        public List<int> getCrns()
        {
            List<int> crns = new List<int>();
            string[] crnsStr = crnsBox.Text.Split(' ');
            foreach (string crn in crnsStr)
            {
                if (int.TryParse(crn.Trim(), out int crnInt))
                {
                    crns.Add(crnInt);
                }
            }
           
            return crns;
        }
    }
}
