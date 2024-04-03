using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab2
{
    public partial class Lab02_Bai01 : Form
    {
        public Lab02_Bai01()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();                          //Tạo một đối tượng mới của lớp OpenFileDialog, cho phép người dùng chọn một tệp từ hộp thoại mở tệp.

                if (ofd.ShowDialog() == DialogResult.OK)                        // kiểm tra nếu người dùng đã chọn một tệp
                {
                    string filePath = ofd.FileName;                                 //Lưu đường dẫn của tệp được chọn vào biến filePath.

                    string content;
                    using (StreamReader sr = new StreamReader(filePath))                //Mở tệp được chọn và đọc nội dung của nó, sử dụng lớp StreamReader để đọc dữ liệu từ tệp.
                    {
                        content = sr.ReadToEnd();                               //Đọc toàn bộ nội dung của tệp và lưu vào biến content.
                    }
                    richTextBox1.Text = content;
                }
                   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);                        //Nếu có lỗi xảy ra trong quá trình đọc tệp, hiển thị một hộp thoại thông báo lỗi với thông điệp là lỗi đã xảy ra và thông tin chi tiết về lỗi đó.
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string content = richTextBox1.Text.ToUpper();                   //lấy nd trên richtextbox rồi chuyển thành in hoa

                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)                        // Nếu người dùng chọn tệp và nhấn OK
                {
                    string filePath = ofd.FileName;
                    
                    using (StreamWriter sw = new StreamWriter(filePath))            //Mở tệp được chọn và tạo một đối tượng StreamWriter để ghi dữ liệu vào tệp.
                    {
                        sw.Write(content);                                              //ghi nd chữ hoa vào tệp đã chọn
                    }
                    MessageBox.Show("Đã ghi nội dung vào file");
                    richTextBox1.Text = content.ToString();
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
