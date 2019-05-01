using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.IO;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {

        private string ff2rom = "null";
        private byte[] byteCheck = new byte[] {78, 69, 83, 26, 16, 0, 18, 0};
        private byte[] byteRead = new byte[8];

        private OpenFileDialog ofd1;
        private Random r = new Random();
        public Form1()
        {
            InitializeComponent();
            ofd1 = new OpenFileDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ff2rom = ofd1.FileName;
                    if (!ConfirmROM(ff2rom))
                    {
                        ff2rom = "null";
                        string messageBoxText = "This is not a valid FF2 NES ROM. Please use a valid ROM";
                        string caption = "Invalid ROM";
                        MessageBoxIcon icon = MessageBoxIcon.Error;
                        MessageBoxButtons button = MessageBoxButtons.OK;
                        MessageBox.Show(messageBoxText, caption, button, icon);
                    }
                    label1.Text = ff2rom;

                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security Error. \n\n Error message: {ex.Message}\n\n" +
                        $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(ff2rom != "null")
            {
                if (checkBox1.Checked)
                {
                    RandomizeShop(0x03861D, 1); //Randomize Altair Weapon Shop
                    RandomizeShop(0x038625, 2); //Randomize Altair Armor Shop
                    RandomizeShop(0x03862D, 3); //Randomize Altair Magic Shop 
                    RandomizeShop(0x0386DD, 0); //Randomize Item Shop 1
                    RandomizeShop(0x0386E5, 0); //Randomize Item Shop 2
                    RandomizeShop(0x0386ED, 0); //Randomize Item Shop 3
                }
                else {
                    RandomizeShop(0x03861D); //Randomize Altair Weapon Shop
                    RandomizeShop(0x038625); //Randomize Altair Armor Shop
                    RandomizeShop(0x03862D); //Randomize Altair Magic Shop 
                    RandomizeShop(0x0386DD); //Randomize Item Shop 1
                    RandomizeShop(0x0386E5); //Randomize Item Shop 2
                    RandomizeShop(0x0386ED); //Randomize Item Shop 3
                }
            }
        }
        private void RandomizeShop(int position)
        {
            using (var stream = new FileStream(ff2rom, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = position; //Load ROM at Specified Position (Shop)
                for (int j = 0; j < 4; j++) //For Each Shop Item
                {
                    int i = 2 * r.Next(0, 0x80); //Pick Random Item
                    stream.WriteByte(RandomizerData.items[i]); //Set Item Byte
                    stream.WriteByte(RandomizerData.items[i + 1]); //Set Price Byte
                } 
            }
        }
        private bool ConfirmROM(string ff2rom)
        {
            if (ff2rom == "null") return false;
            using (var stream = new FileStream(ff2rom, FileMode.Open, FileAccess.Read))
            {
                stream.Position = 0;
                stream.Read(byteRead, 0, 8);
                for (int i = 0; i<8; i++)
                {
                    if (byteRead[i] != byteCheck[i]) return false;
                }
            }
            return true;
        }
        private void RandomizeShop(int position, int type)
        {
            int min = 0;
            int max = 0;
            switch (type)
            {
                case 0: //Item
                    min = 0;
                    max = 0x1E;
                    break;
                case 1: //Weapon
                    min = 0x1E;
                    max = 0x54;
                    break;
                case 2: //Armor
                    min = 0x54;
                    max = 0x85;
                    break;
                case 3: //Magic
                    min = 0x85;
                    max = 0xB0;
                    break;
                default: break;
            }
            using (var stream = new FileStream(ff2rom, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = position; //Load ROM at Specified Position (Shop)
                for (int j = 0; j < 4; j++) //For Each Shop Item
                {
                    int i = 2 * r.Next(min, max); //Pick Random Item
                    stream.WriteByte(RandomizerData.items[i]); //Set Item Byte
                    stream.WriteByte(RandomizerData.items[i + 1]); //Set Price Byte
                }
            }
        }
    }
}
