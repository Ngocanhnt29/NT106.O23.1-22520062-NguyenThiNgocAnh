using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;


namespace Lab2
{
    public partial class Lab02_Bai04 : Form
    {
        public class Student
        {
            public string Name { get; set; }
            public int ID { get; set; }
            public string Phone { get; set; }
            public float Subject1 { get; set; }
            public float Subject2 { get; set; }
            public float Subject3 { get; set; }

            public float CalculateAverage()
            {
                return (Subject1 + Subject2 + Subject3) / 3.0f;
            }
        }

        private List<Student> students = new List<Student>();
        private int currentPage = 0;
        

        public Lab02_Bai04()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filepath = ofd.FileName;

                students = ReadStudentsFromFile(filepath);
                ShowStudentsOnPage(currentPage);
                ShowCurrentPage(); 
            }    

        }

        private List<Student> ReadStudentsFromFile(string fileName)
        {
            List<Student> students = new List<Student>();
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    int lineCount = 0;
                    string[] parts = new string[7]; // Mảng để lưu trữ các trường thông tin

                    while ((line = reader.ReadLine()) != null)
                    {
                        // Đọc từng dòng và lưu vào mảng parts
                        parts[lineCount % 7] = line.Trim(); // Thêm trường thông tin vào mảng và xóa khoảng trắng ở đầu và cuối dòng

                        lineCount++;

                        // Nếu đã đọc đủ 7 trường thông tin, tạo sinh viên và thêm vào danh sách
                        if (lineCount % 7 == 0)
                        {
                            string name = parts[0];
                            int studentID;
                            string phone;
                            float subject1, subject2, subject3;

                            if (int.TryParse(parts[1], out studentID) &&
                                parts[2].Length == 10 && parts[2].StartsWith("0") &&
                                float.TryParse(parts[3], out subject1) && subject1 >= 0 && subject1 <= 10 &&
                                float.TryParse(parts[4], out subject2) && subject2 >= 0 && subject2 <= 10 &&
                                float.TryParse(parts[5], out subject3) && subject3 >= 0 && subject3 <= 10)
                            {
                                phone = parts[2];
                                Student student = new Student
                                {
                                    Name = name,
                                    ID = studentID,
                                    Phone = phone,
                                    Subject1 = subject1,
                                    Subject2 = subject2,
                                    Subject3 = subject3
                                };
                                students.Add(student);
                            }
                            else
                            {
                                MessageBox.Show($"Invalid data in line {lineCount / 7}");
                            }
                        }
                    }
                    // Kiểm tra xem còn dữ liệu trong mảng parts không
                    if (parts.All(string.IsNullOrEmpty) == false)
                    {
                        // Tạo sinh viên từ dữ liệu cuối cùng và thêm vào danh sách
                        string name = parts[0];
                        int studentID;
                        string phone;
                        float subject1, subject2, subject3;

                        if (int.TryParse(parts[1], out studentID) &&
                            parts[2].Length == 10 && parts[2].StartsWith("0") &&
                            float.TryParse(parts[3], out subject1) && subject1 >= 0 && subject1 <= 10 &&
                            float.TryParse(parts[4], out subject2) && subject2 >= 0 && subject2 <= 10 &&
                            float.TryParse(parts[5], out subject3) && subject3 >= 0 && subject3 <= 10)
                        {
                            phone = parts[2];
                            Student student = new Student
                            {
                                Name = name,
                                ID = studentID,
                                Phone = phone,
                                Subject1 = subject1,
                                Subject2 = subject2,
                                Subject3 = subject3
                            };
                            students.Add(student);
                        }
                        else
                        {
                            MessageBox.Show($"Invalid data in line {lineCount / 7}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
            }
            return students;
        }

        private void ShowStudentsOnPage(int page)
        {
            richTextBox1.Clear();
            int startIndex = page * 1; // Display 1 student per page

            if (startIndex < 0 || startIndex >= students.Count)
            {
                MessageBox.Show("Invalid page.");
                return;
            }

            Student currentStudent = students[startIndex];
            textBox14.Text = currentStudent.Name;
            textBox13.Text = currentStudent.ID.ToString();
            textBox12.Text = currentStudent.Phone;
            textBox11.Text = currentStudent.Subject1.ToString();
            textBox10.Text = currentStudent.Subject2.ToString();
            textBox9.Text = currentStudent.Subject3.ToString();
            textBox8.Text = currentStudent.CalculateAverage().ToString();

            
           richTextBox1.AppendText($"{currentStudent.Name}\n" +
                                      $"{currentStudent.ID}\n" +
                                      $"{currentStudent.Phone}\n" +
                                     $"{currentStudent.Subject1}\n" +
                                     $"{currentStudent.Subject2}\n" +
                                     $"{currentStudent.Subject3}\n" +
                                     $"Điểm trung bình: {currentStudent.CalculateAverage()}\n\n"); 
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentPage <= 0)
            {
                MessageBox.Show("Không còn sinh viên nào trước đó.");
                return;
            }
            currentPage--;
            ShowStudentsOnPage(currentPage);
            ShowCurrentPage();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentPage++;
            if (currentPage >= students.Count)
            {
                MessageBox.Show("Không còn sinh viên nào.");
                currentPage = students.Count - 1;
                return;
            }
            ShowStudentsOnPage(currentPage);
            ShowCurrentPage();

        }
        private void ShowCurrentPage()
        {
            label7.Text = $" {currentPage + 1} ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các textbox
            string name = textBox1.Text;
            int id = int.Parse(textBox2.Text);
            string phone = textBox3.Text;
            float subject1 = float.Parse(textBox4.Text);
            float subject2 = float.Parse(textBox5.Text);
            float subject3 = float.Parse(textBox6.Text);

            // Tạo sinh viên mới
            Student newStudent = new Student
            {
                Name = name,
                ID = id,
                Phone = phone,
                Subject1 = subject1,
                Subject2 = subject2,
                Subject3 = subject3
            };

            // Thêm sinh viên mới vào danh sách
            students.Add(newStudent);

            // Hiển thị thông tin sinh viên vừa nhập vào
            DisplayStudents();

            // Ghi danh sách sinh viên xuống file
            SaveStudentsToFile("input4.txt");
            // Đọc lại danh sách sinh viên từ file và hiển thị
            students = ReadStudentsFromFile("input4.txt");
           
        }

        private void DisplayStudents()
        {
            // Xóa thông tin cũ trước khi hiển thị
            richTextBox1.Clear();

            // Hiển thị thông tin sinh viên trong richtextbox
            foreach (Student student in students)
            {
                richTextBox1.AppendText($"{student.Name}\n" +
                                        $"{student.ID}\n" +
                                        $"{student.Phone}\n" +
                                        $"{student.Subject1}\n" +
                                        $"{student.Subject2}\n" +
                                        $"{student.Subject3}\n");
            }
        }

        private void SaveStudentsToFile(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, students);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving students to file: " + ex.Message);
            }
        }

        private void LoadStudentsFromFile(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    students = (List<Student>)formatter.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students from file: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadStudentsFromFile("input4.txt");
            DisplayStudents();
        }
    }


}
