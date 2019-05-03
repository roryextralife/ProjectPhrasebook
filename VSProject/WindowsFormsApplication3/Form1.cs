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
        private byte[] byteCheck = new byte[] { 78, 69, 83, 26, 16, 0, 18, 0 };
        private byte[] byteRead = new byte[8];
        private string version = "ChNo3";
        private string filename;
        private OpenFileDialog ofd1;
        private Random seedGen = new Random();
        private StreamWriter sw;
        private int seed = 0;
        private Random r;
        private string filedirectory;
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
                    else label1.Text = ff2rom;

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

            bool seedValid = Int32.TryParse(textBox1.Text, out seed);

            if (!seedValid) seed = 0;

            if (ff2rom != "null")
            {
                if (seed == 0)
                {
                    seed = seedGen.Next();

                }
                r = new Random(seed);

                filedirectory = Path.GetDirectoryName(ff2rom);
                filename = filedirectory + "\\" + seed + "-FF2Randomizer.nes";
                File.Copy(ff2rom, filename , true);
                var spoilername = filedirectory + "\\" + seed + "-FF2Randomizer.txt";
                try
                {
                    using(sw = File.CreateText(spoilername))
                    {
                        sw.WriteLine("################################");
                        sw.WriteLine("# Final Fantasy  II Randomizer #");
                        sw.WriteLine("################################");
                        sw.WriteLine("Note: The Spoiler Log is heavily in development, and currently pretty much useless without referring to the item data table.");
                        sw.WriteLine("For more information on the item data table, check out: https://github.com/roryextralife/ProjectPhrasebook/blob/master/Resources/ItemDetails.csv");
                        sw.WriteLine("Randomizer Version {0}", version);
                        sw.WriteLine("Seed: {0}", seed);
                        sw.WriteLine(" -- Flags --");
                        sw.WriteLine("Items in Shops match Shop Type: {0}", checkBox1.Checked);
                        sw.WriteLine("Early Airship: {0}", checkBox2.Checked);
                        sw.WriteLine("Firion Character Booster: {0}", checkBox3.Checked);
                        sw.WriteLine("----SPOILER LOG STARTS HERE----");

                if (checkBox1.Checked) //FLAG: Shops shuffling sorted by type of shop
                {

                    foreach (int i in RandomizerData.iShopLocs) RandomizeShop(i, 0); //Randomize all Item Shops with Items Only
                    foreach (int i in RandomizerData.wShopLocs) RandomizeShop(i, 1); //Randomize all Weapon Shops with Weapons Only
                    foreach (int i in RandomizerData.aShopLocs) RandomizeShop(i, 2); //Randomize all Armor Shops with Armor Only
                    foreach (int i in RandomizerData.mShopLocs) RandomizeShop(i, 3); //Randomize all Magic Shops with Magic Only

                }
                else {
                    foreach (int i in RandomizerData.iShopLocs) RandomizeShop(i);
                    foreach (int i in RandomizerData.wShopLocs) RandomizeShop(i);
                    foreach (int i in RandomizerData.aShopLocs) RandomizeShop(i);
                    foreach (int i in RandomizerData.mShopLocs) RandomizeShop(i);
                }
                if (checkBox2.Checked) //FLAG: Early Airship
                {
                    using (var stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
                    {
                        stream.Position = 0x0AF4; //Load ROM at Specified Position (Airship Info)
                        stream.WriteByte(0x04); //Set Airship flag
                        stream.WriteByte(0x5B); //Set Airship X
                        stream.WriteByte(0x76); //Set Airship Y
                        stream.Position = 0x038c69; //Load ROM at Specified Position (Cid's Assistant Confirm JSR)
                        stream.WriteByte(0x60); //Replaces Confirm on Cid's Assistant with an RTS to prevent overwriting the ship.
                    }
                }
                if (checkBox3.Checked) //FLAG: Test Mode w/ Firion
                {
                    using (var stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
                    {
                        stream.Position = 0x0F98; //Load ROM at Specified Position (Firion Stats)
                        stream.WriteByte(0x0F); //Max HP
                        stream.WriteByte(0x27);
                        stream.WriteByte(0x0F);
                        stream.WriteByte(0x27);
                        stream.WriteByte(0xE7); //Max MP
                        stream.WriteByte(0x03);
                        stream.WriteByte(0xE7);
                        stream.WriteByte(0x03);
                        stream.WriteByte(0x63); //Max Stats
                        stream.WriteByte(0x63);
                        stream.WriteByte(0x63);
                        stream.WriteByte(0x63);
                        stream.WriteByte(0x63);
                        stream.Position = 0x0FAA;
                        stream.WriteByte(0x83); //Dragon Armor
                        stream.WriteByte(0x00);
                        stream.WriteByte(0x60); //Dual Masamune
                        stream.WriteByte(0x60);
                        stream.Position = 0x0FB0;
                        stream.WriteByte(0x63); //Max Stats
                        stream.WriteByte(0x63);
                        stream.WriteByte(0x63);
                        stream.WriteByte(0x63);
                        stream.WriteByte(0x63);
                    }
                }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                }
                MessageBox.Show("ROM has been Randomized Successfully with a seed of " + seed + "!");
            }
        }
        private void RandomizeShop(int position)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                sw.WriteLine("Shop Address: {0}", position.ToString("X"));
                stream.Position = position; //Load ROM at Specified Position (Shop)
                for (int j = 0; j < 4; j++) //For Each Shop Item
                {
                    int i = 2 * r.Next(0, 0xA8); //Pick Random Item
                    sw.WriteLine("Item {0}: {1}", j+1, RandomizerData.items[i].ToString("X"));
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
                for (int i = 0; i < 8; i++)
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
                    max = 0x52;
                    break;
                case 2: //Armor
                    min = 0x52;
                    max = 0x81;
                    break;
                case 3: //Magic
                    min = 0x81;
                    max = 0xA8;
                    break;
                default: break;
            }
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                sw.WriteLine("Shop Address: {0}", position.ToString("X"));
                stream.Position = position; //Load ROM at Specified Position (Shop)
                for (int j = 0; j < 4; j++) //For Each Shop Item
                {
                    int i = 2 * r.Next(min, max); //Pick Random Item
                    sw.WriteLine("Item {0}: {1}", j+1, RandomizerData.items[i].ToString("X"));
                    stream.WriteByte(RandomizerData.items[i]); //Set Item Byte
                    stream.WriteByte(RandomizerData.items[i + 1]); //Set Price Byte
                }
            }
        }
    }
}
