using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RoBoME_ver1._0
{
    public partial class NewMotion : Form
    {

        public Panel[] fpanel = new Panel[32];
        public Label[] flabel = new Label[32];
        public ComboBox[] fbox = new ComboBox[32];
        public MaskedTextBox[] ftext = new MaskedTextBox[32];
        public MaskedTextBox[] ftext2 = new MaskedTextBox[32];
        public MaskedTextBox[] ftext3 = new MaskedTextBox[32];
        public MaskedTextBox[] ftext4 = new MaskedTextBox[32];
        uint[] offset = new uint[32];
        uint[] homeframe = new uint[32];
        uint[] Max = new uint[32];
        uint[] min = new uint[32];
        char[] delimiterChars = { ' ', '\t', '\r', '\n' };
        public NewMotion()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new object[] { "---unset---",
                                                    "RB_100b1",
                                                    "RB_100b2",
                                                    "RB_100b3",
                                                    "RB_100",
                                                    "RB_100RD",
                                                    "RB_110",
                                                    "unknow"});
            comboBox1.SelectedIndex = 0;
            if (File.Exists("offset.txt"))
                using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + "\\offset.txt"))
                {
                    string[] datas = reader.ReadToEnd().Split(delimiterChars);
                    for (int i = 0; i < 32; i++)
                    {
                        offset[i] = uint.Parse(datas[i]);
                    }
                    
                }
            else
                for (int i = 0; i < 32; i++)
                {
                    offset[i] = 0;
                }
            if (File.Exists("homeframe.txt"))
                using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + "\\homeframe.txt"))
                {
                    string[] datas = reader.ReadToEnd().Split(delimiterChars);
                    for (int i = 0; i < 32; i++)
                    {
                        homeframe[i] = uint.Parse(datas[i]);
                    }

                }
            else
                for (int i = 0; i < 32; i++)
                {
                    homeframe[i] = 1500;
                }
            if (File.Exists("Range.txt"))
                using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + "\\Range.txt"))
                {
                    string[] datas = reader.ReadToEnd().Split(delimiterChars);
                    for (int i = 0; i < 32; i++){
                        min[i] = uint.Parse(datas[i]);
                        Max[i] = uint.Parse(datas[i+32]);
                    }
                }
            else
                for (int i = 0; i < 32; i++)
                {
                    min[i] = 600;
                    Max[i] = 2300;
                }
            for (int i = 0; i < 32; i++)
            {
                fpanel[i] = new Panel();
                flabel[i] = new Label();
                fbox[i] = new ComboBox();
                ftext[i] = new MaskedTextBox();
                ftext2[i] = new MaskedTextBox();
                ftext3[i] = new MaskedTextBox();
                ftext4[i] = new MaskedTextBox();
                fpanel[i].Size = new Size(520, 30);
                fpanel[i].Top += i * 30;
                flabel[i].Size = new Size(65, 18);
                flabel[i].Top += 5;
                flabel[i].Left += 5;
                fbox[i].Size = new Size(185, 22);
                fbox[i].Left += 70;
                ftext[i].Text = offset[i].ToString();
                ftext[i].TextAlign = HorizontalAlignment.Right;
                ftext[i].KeyPress += new KeyPressEventHandler(numbercheck);
                ftext[i].Size = new Size(50, 22);
                ftext[i].Left += 270;
                ftext2[i].Text = homeframe[i].ToString();
                ftext2[i].TextAlign = HorizontalAlignment.Right;
                ftext2[i].KeyPress += new KeyPressEventHandler(numbercheck);
                ftext2[i].Size = new Size(50, 22);
                ftext2[i].Left += 350;
                ftext2[i].Enabled = false;

                ftext3[i].Text = min[i].ToString();
                ftext3[i].TextAlign = HorizontalAlignment.Right;
                ftext3[i].KeyPress += new KeyPressEventHandler(numbercheck);
                ftext3[i].Size = new Size(40, 22);
                ftext3[i].Left += 410;
                ftext3[i].Enabled = false;

                ftext4[i].Text = Max[i].ToString();
                ftext4[i].TextAlign = HorizontalAlignment.Right;
                ftext4[i].KeyPress += new KeyPressEventHandler(numbercheck);
                ftext4[i].Size = new Size(40, 22);
                ftext4[i].Left += 460;
                ftext4[i].Enabled = false;
                fbox[i].Items.AddRange(new object[] { "---noServo---",
                                                      "KONDO_KRS786",       
                                                      "KONDO_KRS788",       
                                                      "KONDO_KRS78X",       
                                                      "KONDO_KRS4014",      
                                                      "KONDO_KRS4024",      
                                                      "HITEC_HSR8498",      
                                                      "FUTABA_S3003",       
                                                      "SHAYYE_SYS214050",   
                                                      "TOWERPRO_MG995",     
                                                      "TOWERPRO_MG996",     
                                                      "DMP_RS0263",         
                                                      "DMP_RS1270",         
                                                      "GWS_S777",           
                                                      "GWS_S03T",           
                                                      "GWS_MICRO"});
                fbox[i].SelectedIndex = 0;
                if (i < 10)
                    flabel[i].Text = "SetServo " + i.ToString() + ":";
                else
                    flabel[i].Text = "SetServo" + i.ToString() + ":";

                fpanel[i].Controls.Add(flabel[i]);
                fpanel[i].Controls.Add(fbox[i]);
                fpanel[i].Controls.Add(ftext[i]);
                fpanel[i].Controls.Add(ftext2[i]);
                fpanel[i].Controls.Add(ftext3[i]);
                fpanel[i].Controls.Add(ftext4[i]);
                channelver.Controls.Add(fpanel[i]);
            }
        }
        public void numbercheck(object sender, KeyPressEventArgs e)
        {    //Text number check
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8 & (int)e.KeyChar != 189)
            {
                e.Handled = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 32; i++) {
                if (uint.Parse(ftext[i].Text) != offset[i]){
                    using (TextWriter writer = new StreamWriter(Environment.CurrentDirectory + "\\offset.txt"))
                    {
                        for (int j = 0; j < 32; j++)
                        {
                            if (string.Compare(ftext[j].Text, "") == 0)
                            {
                                ftext[j].Text = "0";
                            }
                            writer.Write(ftext[j].Text + " ");
                        }
                    }
                    break;
                }
            }
            for (int i = 0; i < 32; i++)
            {
                if (uint.Parse(ftext2[i].Text) != homeframe[i])
                {
                    using (TextWriter writer = new StreamWriter(Environment.CurrentDirectory + "\\homeframe.txt"))
                    {
                        for (int j = 0; j < 32; j++)
                        {
                            if (string.Compare(ftext[j].Text, "") == 0)
                            {
                                ftext2[j].Text = "1500";
                            }
                            writer.Write(ftext2[j].Text + " ");
                        }
                    }
                    break;
                }
            }
            for (int i = 0; i < 32; i++)
            {
                if (uint.Parse(ftext3[i].Text) != min[i] || uint.Parse(ftext4[i].Text) != Max[i])
                {
                    using (TextWriter writer = new StreamWriter(Environment.CurrentDirectory + "\\Range.txt"))
                    {
                        for (int j = 0; j < 32; j++)
                        {
                            if (string.Compare(ftext3[j].Text, "") == 0)
                            {
                                ftext3[j].Text = "600";
                            }
                            writer.Write(ftext3[j].Text + " ");
                        }
                        for (int j = 0; j < 32; j++)
                        {
                            if (string.Compare(ftext4[j].Text, "") == 0)
                            {
                                ftext4[j].Text = "2400";
                            }
                            writer.Write(ftext4[j].Text + " ");
                        }
                    }
                    break;
                }
            }
            if (String.Compare(comboBox1.SelectedItem.ToString(), "---unset---") == 0)
                MessageBox.Show("Error:\nYou have not choice your RoBoard version.");
            else
                this.DialogResult = DialogResult.OK;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.Compare(comboBox1.Text, "RB_110") == 0) {
                for (int i = 16; i < 32; i++ )
                {
                    fpanel[i].Enabled = false;
                    fbox[i].SelectedIndex = 0;
                }
            }
            else if (string.Compare(comboBox1.Text, "unknow") == 0 || string.Compare(comboBox1.Text, "---unset---") == 0)
            {
                
            }
            else
            {
                for (int i = 24; i < 32; i++)
                {
                    fpanel[i].Enabled = false;
                    fbox[i].SelectedIndex = 0;
                }
            
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < 32; i++)
                {
                    ftext2[i].Enabled = true;
                }

            }
            else {
                for (int i = 0; i < 32; i++)
                {
                    ftext2[i].Enabled = false;
                }

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                for (int i = 0; i < 32; i++)
                {
                    ftext3[i].Enabled = true;
                    ftext4[i].Enabled = true;
                }

            }
            else
            {
                for (int i = 0; i < 32; i++)
                {
                    ftext3[i].Enabled = false;
                    ftext4[i].Enabled = false;
                }

            }

        }

    }
}
