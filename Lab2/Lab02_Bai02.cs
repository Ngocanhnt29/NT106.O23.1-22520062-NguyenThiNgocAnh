using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Lab02_Bai02 : Form
    {
        public Lab02_Bai02()
        {
            InitializeComponent();
        }

        private string FormatFileSize(long size)
        {
            const int byteConversion = 1024;
            double bytes = Convert.ToDouble(size);

            if (bytes >= byteConversion)
            {
                return string.Format("{0:0.##} KB", bytes / byteConversion); //lấy 2 chữ số sau dấu thập phân
            }
            else
            {
                return string.Format("{0} bytes", size);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();//Tạo một đối tượng mới của lớp OpenFileDialog, cho phép người dùng chọn một tệp từ hộp thoại mở tệp.

                if (ofd.ShowDialog() == DialogResult.OK) // kiểm tra nếu người dùng đã chọn một tệp
                {
                    string filePath = ofd.FileName;//Lưu đường dẫn của tệp được chọn vào biến filePath.
                    
                    string safeFileName = ofd.SafeFileName;

                    FileInfo fileInfo = new FileInfo(filePath);

                    textBox1.Text = safeFileName;//ten
                    textBox2.Text = FormatFileSize( fileInfo.Length) ;//size
                    textBox3.Text = fileInfo.FullName;//url

                    int nlines = 0;
                    int nwords = 0;
                    int ncharac = 0;

                    using(StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while((line = sr.ReadLine()) != null) 
                        { 
                            nlines++;

                            string[ ] word = line.Split(' ');
                            nwords += word.Length;
                            ncharac += line.Length;
                        }
                    }

                    textBox4.Text = nlines.ToString();
                    textBox5.Text = nwords.ToString();
                    textBox6.Text = ncharac.ToString();

                    string content;
                    using (StreamReader sr = new StreamReader(filePath))//Mở tệp được chọn và đọc nội dung của nó, sử dụng lớp StreamReader để đọc dữ liệu từ tệp.
                    {
                        content = sr.ReadToEnd();//Đọc toàn bộ nội dung của tệp và lưu vào biến content.
                    }
                    richTextBox1.Text = content;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);//Nếu có lỗi xảy ra trong quá trình đọc tệp, hiển thị một hộp thoại thông báo lỗi với thông điệp là lỗi đã xảy ra và thông tin chi tiết về lỗi đó.
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
