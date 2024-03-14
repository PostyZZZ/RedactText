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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Domash2902
{
    public partial class Form1 : Form
    {
        private List<string> dataList;

        public Form1()
        {
            InitializeComponent();
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            dataList = new List<string>();


        }



        private void сохранитьКакToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataList.Add(richTextBox1.Text);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt|CSV File|*.csv";
            saveFileDialog.Title = "Save Data";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                if (Path.GetExtension(filePath).ToLower() == ".txt")
                {

                    File.WriteAllLines(filePath, dataList);
                }
                else if (Path.GetExtension(filePath).ToLower() == ".csv")
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    {
                        foreach (string line in dataList)
                        {
                            streamWriter.WriteLine(line);
                        }
                    }
                }

                MessageBox.Show("Data saved successfully!");
            }
        }

        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Files|*.txt|CSV Files|*.csv|All Files|*.*";
                openFileDialog.Title = "Open File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Открываем выбранный файл и загружаем его содержимое в RichTextBox
                    string fileName = openFileDialog.FileName;
                    try
                    {
                        richTextBox1.Text = File.ReadAllText(fileName);
                        MessageBox.Show("File opened successfully!");
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Error reading the file: {ex.Message}");
                    }
                }
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size + 1);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Font.Size > 1)
            {
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size - 1);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, richTextBox1.SelectionFont.Style ^ FontStyle.Bold);

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, richTextBox1.SelectionFont.Style ^ FontStyle.Italic);

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, richTextBox1.SelectionFont.Style ^ FontStyle.Underline);

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text;

            if (!string.IsNullOrEmpty(searchText))
            {
                int index = richTextBox1.Find(searchText);

                if (index != -1)
                {
                    richTextBox1.Select(index, searchText.Length);
                    richTextBox1.ScrollToCaret();
                    richTextBox1.Focus(); 
                }
            }
        }
    }
}
