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
        uint[] offset = new uint[32];
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
            for (int i = 0; i < 32; i++)
            {
                fpanel[i] = new Panel();
                flabel[i] = new Label();
                fbox[i] = new ComboBox();
                ftext[i] = new MaskedTextBox();
                fpanel[i].Size = new Size(350, 30);
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
                ftext[i].Left += 280;
                fbox[i].Items.AddRange(new object[] { "---noServo---",
                                                      "RCSERVO_KONDO_KRS786",       
                                                      "RCSERVO_KONDO_KRS788",       
                                                      "RCSERVO_KONDO_KRS78X",       
                                                      "RCSERVO_KONDO_KRS4014",      
                                                      "RCSERVO_KONDO_KRS4024",      
                                                      "RCSERVO_HITEC_HSR8498",      
                                                      "RCSERVO_FUTABA_S3003",       
                                                      "RCSERVO_SHAYYE_SYS214050",   
                                                      "RCSERVO_TOWERPRO_MG995",     
                                                      "RCSERVO_TOWERPRO_MG996",     
                                                      "RCSERVO_DMP_RS0263",         
                                                      "RCSERVO_DMP_RS1270",         
                                                      "RCSERVO_GWS_S777",           
                                                      "RCSERVO_GWS_S03T",           
                                                      "RCSERVO_GWS_MICRO"});
                fbox[i].SelectedIndex = 0;
                if (i < 10)
                    flabel[i].Text = "SetServo " + i.ToString() + ":";
                else
                    flabel[i].Text = "SetServo" + i.ToString() + ":";

                fpanel[i].Controls.Add(flabel[i]);
                fpanel[i].Controls.Add(fbox[i]);
                fpanel[i].Controls.Add(ftext[i]);
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
                            writer.Write(ftext[j].Text + " ");
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

    }
}
