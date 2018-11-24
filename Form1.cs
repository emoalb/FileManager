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
namespace FileManager
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private DriveInfo[] AllDrives;
        private Form2 form2 = new Form2();
        private void refreshList(String directory)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(directory);
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(directory);
            if (Directory.Exists(directory))
            {
                try
                {
                    DirectoryInfo[] directories = dirinfo.GetDirectories();
                    if (directories.Length > 0)
                    {
                        foreach (DirectoryInfo dir in directories)
                        {

                            TreeNode node = treeView1.Nodes[0].Nodes.Add(dir.Name);
                            node.ImageIndex = node.SelectedImageIndex = 1;
                            


                        }
                    }
                    foreach (FileInfo file in dirinfo.GetFiles())
                    {
                        TreeNode node = treeView1.Nodes[0];
                        node.ImageIndex = node.SelectedImageIndex = 1;
                        if (file.Exists)
                        {
                            TreeNode nodef = treeView1.Nodes[0].Nodes.Add(file.Name);
                            nodef.ImageIndex = nodef.SelectedImageIndex = 2;

                        }
                    }
                }
                catch (Exception ex)
                {
                    Show_Info(null, ex.Message);
                }
                treeView1.Nodes[0].Expand();

            }
        }
        private void Show_Info(DriveInfo driveName, String message)
        {
            if (driveName!=null)
            {
                try
                {
                    form2.label1.Text = driveName.Name;
                    form2.label2.Text = driveName.DriveType.ToString();
                    form2.label3.Text = driveName.VolumeLabel;
                    form2.label4.Text = driveName.DriveFormat;
                    form2.label5.Text = driveName.AvailableFreeSpace.ToString();
                    form2.label6.Text = driveName.TotalFreeSpace.ToString();
                    form2.label7.Text = driveName.TotalSize.ToString();
                }catch(Exception ex)
                {
                    form2.label9.Text = form2.label9.Text + "\n" + ex.Message;
                }
            
            }
            if (message != null)
            {
                form2.label9.Text = form2.label9.Text +"\n" +message;
            }
            form2.Show();
        }

        private void Load_Drive(String driveName)
        {
            refreshList(driveName);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
          AllDrives  = DriveInfo.GetDrives();
            
            int index = 0;
            foreach (DriveInfo drive in AllDrives)
            {
                comboBox1.Items.Add(drive.Name);
                index++;
            }
            comboBox1.SelectedIndex = 0;

            Load_Drive(AllDrives[comboBox1.SelectedIndex].Name);
            Show_Info(AllDrives[comboBox1.SelectedIndex],null);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            Load_Drive(AllDrives[comboBox1.SelectedIndex].Name);
            Show_Info(AllDrives[comboBox1.SelectedIndex],null);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            form2.label9.Text = form2.label9.Text + "\n" + treeView1.SelectedNode.Text;
       
        }
    }

    }

