using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Lab02_Bai07 : Form
    {
        public Lab02_Bai07()
        {
            InitializeComponent();
            // Gọi hàm để load danh sách file và thư mục từ ổ đĩa gốc (C:)
            LoadDrives();
            // Thêm sự kiện NodeMouseDoubleClick cho treeView1
            treeView1.NodeMouseDoubleClick += treeView1_NodeMouseDoubleClick;
        }

        private void LoadDrives()
        {
            // Lấy danh sách các ổ đĩa trên máy tính
            DriveInfo[] drives = DriveInfo.GetDrives();

            // Duyệt qua từng ổ đĩa và hiển thị danh sách thư mục
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady && drive.DriveType == DriveType.Fixed) // Loại bỏ ổ đĩa CD/DVD và USB
                {
                    TreeNode driveNode = treeView1.Nodes.Add(drive.Name);
                    LoadDirectories(drive.Name, driveNode);
                }
            }
        }

        private void LoadDirectories(string path, TreeNode parentNode)
        {
            try
            {
                
                parentNode.Nodes.Clear(); // Clear existing nodes
                //thêm node
                string[] directories = Directory.GetDirectories(path);
                foreach (string directory in directories)
                {
                    TreeNode node = new TreeNode(Path.GetFileName(directory));
                    node.Tag = directory;
                    parentNode.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedPath = e.Node.Tag.ToString();

            if (Directory.Exists(selectedPath)) // ktra node là thư mục
            {
                LoadDirectoriesAndFiles(selectedPath, e.Node);
            }
            else if (File.Exists(selectedPath)) // ktr node là 1 file
            {
                ShowFileContent(selectedPath);
            }
        }

        private void LoadDirectoriesAndFiles(string path, TreeNode parentNode)
        {
            try
            {
                // Add subdirectories
                string[] directories = Directory.GetDirectories(path);
                foreach (string directory in directories)
                {
                    TreeNode node = new TreeNode(Path.GetFileName(directory));
                    node.Tag = directory;
                    parentNode.Nodes.Add(node);
                }

                // Add files
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    TreeNode node = new TreeNode(Path.GetFileName(file));
                    node.Tag = file;
                    parentNode.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void ShowFileContent(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    // Kiểm tra loại file
                    string extension = Path.GetExtension(filePath).ToLower();

                    if (extension == ".txt" || extension == ".docx" || extension == ".pdf" )
                    {
                        // Hiển thị nội dung file văn bản trong RichTextBox
                        richTextBox1.Text = File.ReadAllText(filePath);
                        pictureBox1.Image = null; // Xóa hình ảnh nếu có
                    }
                    else if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                    {
                        // Hiển thị hình ảnh trong PictureBox
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Zoom để hình ảnh lớn hơn khung hình
                        pictureBox1.Image = Image.FromFile(filePath);
                        richTextBox1.Text = ""; // Xóa văn bản nếu có
                    }
                    else
                    {
                        MessageBox.Show("Không hỗ trợ loại file này.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null)
            {
                string selectedPath = e.Node.Tag.ToString();
                if (Directory.Exists(selectedPath))
                {
                    LoadDirectoriesAndFiles(selectedPath, e.Node);
                }
                else if (File.Exists(selectedPath))
                {
                    ShowFileContent(selectedPath);
                }
            }
        }


    }
}
