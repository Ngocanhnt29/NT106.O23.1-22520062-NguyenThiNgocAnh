using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class lab02 : Form
    {
        public lab02()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lab02_Bai01 Bai1 = new Lab02_Bai01();
            Bai1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lab02_Bai02 Bai2 = new Lab02_Bai02();
            Bai2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lab02_Bai03 Bai3 = new Lab02_Bai03();
            Bai3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Lab02_Bai04 Bai4 = new Lab02_Bai04();
            Bai4.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Lab02_Bai05 Bai5 = new Lab02_Bai05();
            Bai5.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Lab02_Bai06 Bai6 = new Lab02_Bai06();
            Bai6.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Lab02_Bai07 Bai7 = new Lab02_Bai07();
            Bai7.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
