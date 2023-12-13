using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LethalCompanySaveModifier
{
    public partial class GameSaves : Form
    {
        public GameSaves()
        {
            InitializeComponent();
            LoadFileNamesToListBox();
        }

        private void LoadFileNamesToListBox()
        {

            string searchPattern = "LCSaveFile*";

            try
            {
                string[] files = Directory.GetFiles(CryptoLogic.GameSavePath, searchPattern);
                foreach (string file in files)
                {
                    listBox1.Items.Add(Path.GetFileName(file));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            CryptoLogic.Decrypt();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CryptoLogic.GameSaveFile = CryptoLogic.GameSavePath + listBox1.SelectedItem.ToString();
        } 
    }
}
