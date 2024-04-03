using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab2
{
    public partial class Lab02_Bai03 : Form
    {
        public Lab02_Bai03()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();              //Tạo một đối tượng mới của lớp OpenFileDialog, cho phép người dùng chọn một tệp từ hộp thoại mở tệp.

                if (ofd.ShowDialog() == DialogResult.OK)                // kiểm tra nếu người dùng đã chọn một tệp
                {
                    string filePath = ofd.FileName;                       //Lưu đường dẫn của tệp được chọn vào biến filePath.
                    string content;
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        content = sr.ReadToEnd();
                    }
                    richTextBox1.Text = content;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);              //Nếu có lỗi xảy ra trong quá trình đọc tệp, hiển thị một hộp thoại thông báo lỗi với thông điệp là lỗi đã xảy ra và thông tin chi tiết về lỗi đó.
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = richTextBox1.Lines;
                StringBuilder resultBuilder = new StringBuilder();
                // Duyệt qua từng dòng và thực hiện tính toán
                foreach (string line in lines)
                {
                    double result = Calculate(line);
                    resultBuilder.AppendLine($"{line} = {result}");
                }
                richTextBox1.Text = resultBuilder.ToString();
            }   
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }


        }

        private double Calculate(string expression)
        {
            // Thêm khoảng trắng trước và sau mỗi toán tử (+, -, *, /)
            expression = Regex.Replace(expression, @"(?<=[\+\-\*/])|(?=[\+\-\*/])", " ");

            // Tách các toán hạng và toán tử
            string[] parts = expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Tạo mảng lưu trữ các toán hạng
            List<double> numbers = new List<double>();
            foreach (string part in parts)
            {
                double number;
                if (double.TryParse(part, out number))
                {
                    numbers.Add(number);
                }
                else
                {
                    if (part != "+" && part != "-")
                    {
                        throw new FormatException($"Invalid number format: {part}");
                    }
                }
            }

            // Tạo mảng lưu trữ các toán tử
            List<char> operators = new List<char>();
            foreach (char c in expression)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    operators.Add(c);
                }
            }

            // Thực hiện tính toán
            double result = numbers[0];
            for (int i = 0; i < operators.Count; i++)
            {
                char op = operators[i];
                double nextNumber = numbers[i + 1];
                switch (op)
                {
                    case '+':
                        result += nextNumber;
                        break;
                    case '-':
                        result -= nextNumber;
                        break;
                    case '*':
                        result *= nextNumber;
                        break;
                    case '/':
                        result /= nextNumber;
                        break;
                }
            }

            return result;
        }




    }




}
      

        

