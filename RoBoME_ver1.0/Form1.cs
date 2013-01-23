//============================================================================================================//
//  _______             _______                                __                _______               __     //
// /\  ____`\          /\  ___ `\                             /\ \              /  _____\             /\ \    //
// \ \ \___\ \     ___ \ \ \__\  \    ___     ____    _  __   \_\ \    ______  /\ \ ____/_    ___     \_\ \   //
//  \ \  __  <    / __`\\ \  ___ <   / __`\  / __ `\ /\`'__\ /' __ \  /\_____\ \ \ \  /\_ \  / __`\  /' __ \  //
//   \ \ \ \_ `\ /\ \_\ \\ \ \__\ `\/\ \_\ \/\ \_\  \\ \ \/ /\  \_\ \ \/_____/  \ \ \_\/_\ \/\ \_\ \/\  \_\ \ // 
//    \ \_\\ `\_\\ \____/ \ \______/\ \____/\ \___/\_\\ \_\ \ \_____/            \ \_______/\ \____/\ \_____/ // 
//     \/_/ `\/_/ \/___/   \/_____/  \/___/  \/___//_/ \/_/  \/____/              \/______/  \/___/  \/____/  //
//                                                                                                            //
//                                                           My web address: http://roboardgod.blogspot.tw/   //
//============================================================================================================//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using System.Media;
using RoBoIO_DotNet;
namespace RoBoME_ver1._0
{
    public partial class Form1 : Form
    {
        private Panel[] fpanel = new Panel[32];
        Label[] flabel = new Label[32];
        MaskedTextBox[] ftext = new MaskedTextBox[32];
        HScrollBar[] fbar = new HScrollBar[32];
        NewMotion Motion;
        public ArrayList ME_Triggerlist;
        public ArrayList ME_Motionlist;
        int framecount = 0;
        Boolean new_obj = false;
        String nfilename = "";
        uint[] autoframe = new uint[32];
        uint[] offset = new uint[32];
        public Boolean onPC = true;
        string motiontestkey = "";
        string[] motionevent = {"New object",
                               "Delete object",
                               "Move object UP",
                               "Move object DOWN",
                                "Copy frame"};
        char[] delimiterChars = { ' ', '\t', '\r', '\n' };
        public Form1()
        {
            InitializeComponent();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            saveFileToolStripMenuItem.Enabled = false;
            optionToolStripMenuItem.Enabled = false;
            

        }
        private void Update_framelist()         //set framelist
        {
            Framelist.Controls.Clear();
            
            for (int i = 0; i < 32; i++)
            {
                if (String.Compare(Motion.fbox[i].Text, "---noServo---") != 0)
                {
                    fpanel[i] = new Panel();
                    flabel[i] = new Label();
                    ftext[i] = new MaskedTextBox();
                    fbar[i] = new HScrollBar();
                    fpanel[i].Size = new Size(260, 30);
                    fpanel[i].Top += i * 30;
                    flabel[i].Size = new Size(40, 18);
                    flabel[i].Top += 5;
                    flabel[i].Left += 5;
                    ftext[i].Size = new Size(45, 22);
                    ftext[i].Left += 45;
                    ftext[i].TextAlign = HorizontalAlignment.Right;
                    
                    ftext[i].Name = i.ToString();
                    ftext[i].TextChanged += new EventHandler(Text_Changed);
                    ftext[i].KeyPress += new KeyPressEventHandler(numbercheck);
                    fbar[i].Size = new Size(160, 22);
                    fbar[i].Left += 95;
                    fbar[i].Maximum = 2409;
                    fbar[i].Minimum = 600;
                    fbar[i].Name = i.ToString();
                    fbar[i].Scroll += new ScrollEventHandler(scroll_event);
                    if (Motionlist.SelectedItem != null)
                    {
                        string[] datas = Motionlist.SelectedItem.ToString().Split(' ');
                        if (String.Compare(datas[0], "[Frame]") == 0)
                        {
                            ME_Motion m = (ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex];
                            ME_Frame f =(ME_Frame)m.Events[Motionlist.SelectedIndex];
                            ftext[i].Text = f.frame[i].ToString();
                            if (int.Parse(ftext[i].Text) <= 2400 && int.Parse(ftext[i].Text) >= 600)
                                fbar[i].Value = int.Parse(ftext[i].Text);
                        }
                    }
                    else
                    {
                        ftext[i].Text = "0";
                    }
                    if (i < 10)
                        flabel[i].Text = "CH " + i.ToString() + ":";
                    else
                        flabel[i].Text = "CH" + i.ToString() + ":";

                    fpanel[i].Controls.Add(flabel[i]);
                    fpanel[i].Controls.Add(ftext[i]);
                    fpanel[i].Controls.Add(fbar[i]);
                    Framelist.Controls.Add(fpanel[i]);
                }
            }

        }
        public void scroll_event(object sender, EventArgs e){   //Scroll event
            
            this.ftext[int.Parse(((HScrollBar)sender).Name)].Text = ((HScrollBar)sender).Value.ToString();


        }
        public void Text_Changed(object sender, EventArgs e){   //Text event
            int n;
            if (int.TryParse(((MaskedTextBox)sender).Text,out n))
            {
                if (int.Parse(((MaskedTextBox)sender).Text) <= 2400 && int.Parse(((MaskedTextBox)sender).Text) >= 600)
                {
                    this.fbar[int.Parse(((MaskedTextBox)sender).Name)].Value = int.Parse(((MaskedTextBox)sender).Text);
                    if (autocheck.Checked == true)
                    {
                        for (int i = 0; i < 32; i++)
                            if (String.Compare(Motion.fbox[i].Text, "---noServo---") != 0)
                                autoframe[i] = uint.Parse(ftext[i].Text);
                        RoBoIO.rcservo_SetAction(autoframe, uint.Parse(delaytext.Text));
                        RoBoIO.rcservo_PlayAction();
                    }
                }
                if (Motionlist.SelectedItem != null)
                {
                    string[] datas = Motionlist.SelectedItem.ToString().Split(' ');
                    if (String.Compare(datas[0], "[Frame]") == 0)
                    {
                        ME_Motion m = (ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex];
                        ME_Frame f = (ME_Frame)m.Events[Motionlist.SelectedIndex];
                        f.frame[int.Parse(((MaskedTextBox)sender).Name)] = int.Parse(((MaskedTextBox)sender).Text);
                    }
                }
            }else
                ((MaskedTextBox)sender).Text = "";
        }
        public void numbercheck(object sender, KeyPressEventArgs e){    //Text number check
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)    //new file
        {
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
            if (Motion != null)
            { 
                if (!onPC)
                    RoBoIO.rcservo_Close();
                DialogResult dialogResult = MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    saveFileToolStripMenuItem_Click(sender, e);
                }
            }
            NewMotion nMotion = new NewMotion();
            nMotion.ShowDialog();
            if (nMotion.DialogResult == DialogResult.OK)
            {
                Motion = nMotion;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                saveFileToolStripMenuItem.Enabled = true;
                optionToolStripMenuItem.Enabled = true;
                NewMotion.Enabled = false;
                ME_Triggerlist = new ArrayList();
                ME_Motionlist = new ArrayList();
                triggerlist.Items.Clear();
                MotionCombo.Items.Clear();
                MotionCombo.Text = "";
                Motionlist.Items.Clear();
                delaytext.Text = "0";
                typecombo.Text = "";
                for (int i = 0; i < 32; i++) {
                    offset[i] = uint.Parse(nMotion.ftext[i].Text);
                }
                if (!onPC)
                {
                    set_RBver();
                    set_RCservo();
                }
            }
            
        }
        private void optionToolStripMenuItem_Click(object sender, EventArgs e)  //option
        {
            Motion.ShowDialog();
            if (Motion.DialogResult == DialogResult.OK)
            {
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
                if (!onPC)
                {
                    RoBoIO.rcservo_Close();
                    set_RBver();
                    set_RCservo();
                }
                Update_framelist();
            }
        }
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)    //save file
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "rbm files (*.rbm)|*.rbm";
            dialog.InitialDirectory = Environment.CurrentDirectory + "\\";
            dialog.Title = "Save File";
            dialog.FileName = nfilename;
            if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName != null)
            {
                nfilename = Path.GetFileName(dialog.FileName);
                TextWriter writer = new StreamWriter(dialog.OpenFile());
                writer.Write("RoBoardVer ");
                writer.Write(Motion.comboBox1.SelectedItem.ToString());
                writer.Write("\n");

                writer.Write("Servo ");
                for (int i = 0; i < 32; i++)
                {
                    ComboBox cb = Motion.fbox[i];
                    writer.Write(cb.Text);
                    if (i != 31)
                        writer.Write(" ");
                }
                writer.Write("\n");

                for (int i = 0; i < ME_Triggerlist.Count; i++ ){
                    ME_Trigger t = (ME_Trigger)ME_Triggerlist[i];
                    if(t.motion != null)
                        writer.Write("Trigger " + t.key + " " + t.motion.name + "\n");
                    else
                        writer.Write("Trigger " + t.key + " null\n");
                }
                for (int i = 0; i < ME_Motionlist.Count; i++)
                {
                    ME_Motion m = (ME_Motion)ME_Motionlist[i];
                    writer.Write("Motion " + m.name + "\n");
                    for (int j = 0; j < m.Events.Count; j++) {
                        if (m.Events[j] is ME_Frame) 
                        {
                            ME_Frame f=(ME_Frame)m.Events[j];
                            writer.Write("frame " + f.delay.ToString() + " ");
                            int count = 0;
                            for(int k = 0; k < 32; k++)
                                if (String.Compare(Motion.fbox[k].Text,"---noServo---") != 0) {
                                    count++;
                                }
                            for (int k = 0; k < 32; k++)
                                if (String.Compare(Motion.fbox[k].Text, "---noServo---") != 0)
                                {
                                    count--;
                                    writer.Write(f.frame[k].ToString());
                                    if (count != 0)
                                        writer.Write(" ");
                                }
                            writer.Write("\n");
                        }
                        else if(m.Events[j] is ME_Delay)
                        {
                            ME_Delay d=(ME_Delay)m.Events[j];
                            writer.Write("delay " + d.delay.ToString() + "\n");
                        }
                        else if (m.Events[j] is ME_Sound)
                        {
                            ME_Sound s = (ME_Sound)m.Events[j];
                            writer.Write("sound " + s.filename + " " + s.delay.ToString() + "\n");
                        }
                        else if (m.Events[j] is ME_Goto)
                        {
                            ME_Goto g = (ME_Goto)m.Events[j];
                            writer.Write("goto " + g.name + " " + g.key + "\n");

                        }
                        else if (m.Events[j] is ME_Flag)
                        {
                            ME_Flag fl = (ME_Flag)m.Events[j];
                            writer.Write("flag " + fl.name + "\n");

                        }
                    }
                    writer.Write("MotionEnd " + m.name);
                    if (i != ME_Motionlist.Count - 1)
                        writer.Write("\n");
                }
                
                writer.Dispose();
                writer.Close();

            }

        }
        private void actionToolStripMenuItem_Click(object sender, EventArgs e)      //load file
        {
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
            if (Motion != null)
            {
                if (!onPC)
                    RoBoIO.rcservo_Close();
                DialogResult dialogResult = MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    saveFileToolStripMenuItem_Click(sender,e);
                }            
            }
            NewMotion nMotion = new NewMotion();
            ME_Triggerlist = new ArrayList();
            ME_Motionlist = new ArrayList();
            string[] rbver = new string[] { "---unset---",
                                            "RB_100b1",
                                            "RB_100b2",
                                            "RB_100b3",
                                            "RB_100",
                                            "RB_100RD",
                                            "RB_110",
                                            "unknow"};
            string[] servo = new string[] { "---noServo---",
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
                                            "RCSERVO_GWS_MICRO"};
            ME_Motion motiontag = null;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "rbm files (*.rbm)|*.rbm";
            dialog.InitialDirectory = Environment.CurrentDirectory + "\\";
            dialog.Title = "Open File";
            String filename = (dialog.ShowDialog() == DialogResult.OK) ? dialog.FileName : null;
            if (filename == null)
            {
                return;
            }
            using (StreamReader reader = new StreamReader(filename))
            {
                nfilename = Path.GetFileName(dialog.FileName);
                triggerlist.Items.Clear();
                MotionCombo.Items.Clear();
                MotionCombo.Text = "";
                Motionlist.Items.Clear();
                delaytext.Text = "0";
                typecombo.Text = "";
                string[] datas = reader.ReadToEnd().Split(delimiterChars);
                for (int i = 0; i < datas.Length; i++)
                {
                    if (String.Compare(datas[i], "RoBoardVer") == 0)
                    {
                        i++;
                        for (int j = 0; j < rbver.Length; j++)
                            if (String.Compare(datas[i], rbver[j]) == 0)
                            {
                                nMotion.comboBox1.SelectedIndex = j;
                            }
                    }
                    else if (String.Compare(datas[i], "Servo") == 0)
                    {
                        for (int k = 0; k < 32; k++)
                        {
                            i++;
                            for (int j = 0; j < servo.Length; j++)
                                if (String.Compare(datas[i], servo[j]) == 0)
                                    nMotion.fbox[k].SelectedIndex = j;
                        }
                    }
                    else if (String.Compare(datas[i], "Trigger") == 0)
                    {
                        ME_Trigger m = new ME_Trigger();
                        i++;
                        m.key = datas[i];
                        i++;
                        if (String.Compare(datas[i], "null") != 0)
                        {
                            m.motion = new ME_Motion();
                            m.motion.name = datas[i];
                            ME_Motionlist.Add(m.motion);
                        }
                        else
                            m.motion = null;
                        ME_Triggerlist.Add(m);
                    }
                    else if (String.Compare(datas[i], "Motion") == 0)
                    {
                        i++;
                        for (int j = 0; j < ME_Motionlist.Count; j++)
                        {
                            motiontag = (ME_Motion)ME_Motionlist[j];
                            if (String.Compare(datas[i], motiontag.name) != 0)
                                motiontag = null;
                            else
                                break;
                        }
                        if (motiontag == null)
                        {
                            motiontag = new ME_Motion();
                            motiontag.name = datas[i];
                            ME_Motionlist.Add(motiontag);
                        }
                    }
                    else if (String.Compare(datas[i], "MotionEnd") == 0)
                    {
                        i++;
                        if (String.Compare(datas[i], motiontag.name) == 0)
                            motiontag = null;
                    }
                    else if (String.Compare(datas[i], "frame") == 0)
                    {

                        ME_Frame nframe = new ME_Frame();
                        i++;
                        nframe.delay = int.Parse(datas[i]);
                        int j = 0;
                        while (j < 32)
                        {
                            if (String.Compare(nMotion.fbox[j].SelectedItem.ToString(), "---noServo---") != 0)
                            {
                                i++;
                                nframe.frame[j] = int.Parse(datas[i]);
                            }
                            else {
                                nframe.frame[j] = 0;
                            }
                            j++;
                        }
                        motiontag.Events.Add(nframe);
                    }
                    else if (String.Compare(datas[i], "delay") == 0)
                    {
                        ME_Delay ndelay = new ME_Delay();
                        i++;
                        ndelay.delay = int.Parse(datas[i]);
                        motiontag.Events.Add(ndelay);
                    }
                    else if (String.Compare(datas[i], "sound") == 0)
                    {
                        ME_Sound nsound = new ME_Sound();
                        i++;
                        nsound.filename = datas[i];
                        i++;
                        nsound.delay = int.Parse(datas[i]);
                        motiontag.Events.Add(nsound);
                    }
                    else if (String.Compare(datas[i], "flag") == 0)
                    {
                        ME_Flag nflag = new ME_Flag();
                        i++;
                        nflag.name = datas[i];
                        motiontag.Events.Add(nflag);
                    }
                    else if (String.Compare(datas[i], "goto") == 0)
                    {
                        ME_Goto ngoto = new ME_Goto();
                        i++;
                        ngoto.name = datas[i];
                        i++;
                        ngoto.key = datas[i];
                        motiontag.Events.Add(ngoto);
                    }
                }
            }
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            optionToolStripMenuItem.Enabled = true;
            saveFileToolStripMenuItem.Enabled = true;
            NewMotion.Enabled = false;
            Motion = nMotion;
            for (int i = 0; i < ME_Motionlist.Count; i++)
            {
                ME_Motion m = (ME_Motion)ME_Motionlist[i];
                MotionCombo.Items.Add(m.name);
            }
            for (int i = 0; i < ME_Triggerlist.Count; i++)
            {
                ME_Trigger t = (ME_Trigger)ME_Triggerlist[i];
                if(t.motion != null)
                    triggerlist.Items.Add("Trigger: " + t.key + "\tMotion: " + t.motion.name);
                else
                    triggerlist.Items.Add("Trigger: " + t.key + "\tMotion: ");

            }
            if (!onPC)
            {
                set_RBver();
                set_RCservo();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (onPC)
            {
                autocheck.Enabled = false;
                capturebutton.Enabled = false;
                MotionTest.Enabled = false;
            }
        }        
        private void typecombo_TextChanged(object sender, EventArgs e)
        {
            Framelist.Controls.Clear();
            if (String.Compare(typecombo.Text, "Frame") == 0)
            {
                if (new_obj) {
                    Motionlist.Items.Insert(Motionlist.SelectedIndex+1,"[Frame] " + MotionCombo.SelectedItem.ToString() + "-" + framecount++.ToString());
                    ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.Insert(Motionlist.SelectedIndex + 1, new ME_Frame());
                    Motionlist.SelectedIndex++;
                }
                delaytext.Enabled = true;
                label2.Enabled = true;
                Framelist.Enabled = true;
                if(!onPC) capturebutton.Enabled = true;
                if (!onPC) autocheck.Enabled= true;
                typecombo.Enabled = false;
                Update_framelist();
                new_obj = false;
            }
            else if (String.Compare(typecombo.Text, "Delay") == 0)
            {
                if (new_obj)
                {
                    Motionlist.Items.Insert(Motionlist.SelectedIndex + 1, "[Delay]");
                    ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.Insert(Motionlist.SelectedIndex + 1, new ME_Delay());
                    Motionlist.SelectedIndex++;
                }
                delaytext.Enabled = true;
                label2.Enabled = true;
                Framelist.Enabled = false;
                if (!onPC) capturebutton.Enabled = false;
                if (!onPC) autocheck.Enabled= false;
                typecombo.Enabled = false;
                new_obj = false;
            }
            else if (String.Compare(typecombo.Text, "Sound") == 0)
            {
                if (new_obj){
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "wav files (*.wav)|*.wav";
                    dialog.InitialDirectory = Environment.CurrentDirectory + "\\";
                    dialog.Title = "Link Sound";
                    String filename = (dialog.ShowDialog() == DialogResult.OK) ? dialog.FileName : null;
                    if (filename != null)
                    {
                            ME_Sound s = new ME_Sound();
                            s.filename = Path.GetFileName(filename);
                            Motionlist.Items.Insert(Motionlist.SelectedIndex + 1, "[Sound] " + Path.GetFileName(filename));
                            ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.Insert(Motionlist.SelectedIndex + 1, s);
                            Motionlist.SelectedIndex++;
                    }
                }
                delaytext.Enabled = true;
                label2.Enabled = true;
                Framelist.Enabled = true;
                if (!onPC) capturebutton.Enabled = false;
                if (!onPC) autocheck.Enabled= false;
                typecombo.Enabled = false;
                new_obj = false;
            }
            else if (String.Compare(typecombo.Text, "Flag") == 0)
            {

                if (new_obj)
                {
                    Motionlist.Items.Insert(Motionlist.SelectedIndex + 1, "[Flag]");
                    ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.Insert(Motionlist.SelectedIndex + 1, new ME_Flag());
                    Motionlist.SelectedIndex++;
                }
                delaytext.Text = "";
                delaytext.Enabled = false;
                label2.Enabled = false;
                Framelist.Enabled = true;
                if (!onPC) capturebutton.Enabled = false;
                if (!onPC) autocheck.Enabled= false;
                typecombo.Enabled = false;
                new_obj = false;
            }
            else if (String.Compare(typecombo.Text, "Goto") == 0)
            {
                
                if(new_obj){
                    Motionlist.Items.Insert(Motionlist.SelectedIndex + 1, "[Goto]");
                    ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.Insert(Motionlist.SelectedIndex + 1, new ME_Goto());
                    Motionlist.SelectedIndex++;
                }
                delaytext.Text = "";
                delaytext.Enabled = false;
                label2.Enabled = false;
                Framelist.Enabled = true;
                if (!onPC) capturebutton.Enabled = false;
                if (!onPC) autocheck.Enabled= false;
                typecombo.Enabled = false;
                new_obj = false;
            }
            else if (String.Compare(typecombo.Text, "Select type") == 0)
            {
                new_obj = true;
                typecombo.Enabled = true;
                delaytext.Enabled = false;
                delaytext.Text = "";
                if (!onPC) capturebutton.Enabled = false;
                if (!onPC) autocheck.Enabled= false;
                Framelist.Enabled = false;
            }
        }
        private void MotionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (autocheck.Checked == true)
            {
                RoBoIO.rcservo_EnterCaptureMode();
                autocheck.Checked = false;
            }
            groupBox1.Enabled = false;
            Motionlist.Items.Clear();
            framecount = 0;
            for(int i = 0;i < ME_Motionlist.Count; i ++){
                ME_Motion m = (ME_Motion)ME_Motionlist[i];
                if(String.Compare(MotionCombo.SelectedItem.ToString(),m.name.ToString()) == 0){
                    for(int j = 0; j < m.Events.Count; j++){
                        if(m.Events[j] is ME_Frame){
                            Motionlist.Items.Add("[Frame] " + ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).name + "-" + framecount);
                            framecount++;
                        }else if(m.Events[j] is ME_Delay){
                            ME_Delay d = (ME_Delay)m.Events[j];
                            Motionlist.Items.Add("[Delay]");
                        }
                        else if (m.Events[j] is ME_Sound){
                            ME_Sound s = (ME_Sound)m.Events[j];
                            Motionlist.Items.Add("[Sound] " + s.filename);

                        }else if (m.Events[j] is ME_Goto){
                            ME_Goto g = (ME_Goto)m.Events[j];
                            Motionlist.Items.Add("[Goto] " + g.name);

                        }else if (m.Events[j] is ME_Flag){
                            ME_Flag fl = (ME_Flag)m.Events[j];
                            Motionlist.Items.Add("[Flag] " + fl.name);
                        
                        }
                    
                    }
                    break;
                }
            }
        }
        private void gototext(object sender, EventArgs e)
        {
            if (((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex] is ME_Flag)
                ((ME_Flag)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).name = ((MaskedTextBox)sender).Text;
            else if (((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex] is ME_Goto)
                ((ME_Goto)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).name = ((MaskedTextBox)sender).Text;

        }
        private void gotobutton(object sender, EventArgs e)
        {
            TriggerSet ts = new TriggerSet(((ME_Goto)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).key);
            ts.ShowDialog();
            if (ts.DialogResult == DialogResult.OK)
            {
                ((ME_Goto)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).key = ts.Keyvalue.Text;
            }
        }
        private void Motionlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Motionlist.SelectedItem != null) {
                groupBox1.Enabled = true;
                string[] datas = Motionlist.SelectedItem.ToString().Split(' ');
                if (String.Compare(datas[0], "[Frame]") == 0)
                {
                    typecombo.SelectedIndex = 0;
                    delaytext.Text = ((ME_Frame)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).delay.ToString();
                    Update_framelist();
                    if (autocheck.Checked == true)
                    {
                        for (int i = 0; i < 32; i++)
                            if (String.Compare(Motion.fbox[i].Text, "---noServo---") != 0)
                            {
                                autoframe[i] = (uint)((ME_Frame)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).frame[i] + offset[i];
                                
                            }
                        RoBoIO.rcservo_SetAction(autoframe, (uint)((ME_Frame)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).delay);
                        autocheck.Enabled = false;
                        while (RoBoIO.rcservo_PlayAction() != RoBoIO.RCSERVO_PLAYEND) ;
                        autocheck.Enabled = true;
                    }
                }
                else if (String.Compare(datas[0], "[Delay]") == 0)
                {
                    typecombo.SelectedIndex = 1;
                    delaytext.Text = ((ME_Delay)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).delay.ToString();
                }
                else if (String.Compare(datas[0], "[Sound]") == 0)
                {
                    typecombo.SelectedIndex = 2; 
                    delaytext.Text = ((ME_Sound)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).delay.ToString();

                }
                else if (String.Compare(datas[0], "[Flag]") == 0)
                {
                    typecombo.SelectedIndex = 4;
                    Framelist.Controls.Clear();
                    Label xlabel = new Label();
                    xlabel.Text = "Name: ";
                    xlabel.Size = new Size(40, 22);

                    MaskedTextBox xtext = new MaskedTextBox();
                    xtext.TextChanged += new EventHandler(gototext);
                    xtext.Text = ((ME_Flag)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).name;
                    xtext.Size = new Size(160, 22);
                    Framelist.Controls.Add(xlabel);
                    Framelist.Controls.Add(xtext);

                }
                else if (String.Compare(datas[0], "[Goto]") == 0)
                {
                    typecombo.SelectedIndex = 3;
                    Framelist.Controls.Clear();
                    Label xlabel = new Label();
                    xlabel.Text = "Name: ";
                    xlabel.Size = new Size(40, 22);
                    MaskedTextBox xtext = new MaskedTextBox();
                    xtext.TextChanged += new EventHandler(gototext);
                    xtext.Text = ((ME_Goto)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).name;
                    xtext.Size = new Size(160, 22);
                    Button xbutton = new Button();
                    xbutton.Text = "set keys";
                    xbutton.Click += new EventHandler(gotobutton);
                    Framelist.Controls.Add(xlabel);
                    Framelist.Controls.Add(xtext);
                    Framelist.Controls.Add(xbutton);
                }
                
            }
        }
        private void delaytext_TextChanged(object sender, EventArgs e)
        {
            if (Motionlist.SelectedItem != null && String.Compare(delaytext.Text,"") != 0)
            {
                if (((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex] is ME_Frame)
                {
                    ((ME_Frame)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).delay = int.Parse(((MaskedTextBox)sender).Text);
                }
                else if (((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex] is ME_Delay)
                {
                    ((ME_Delay)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).delay = int.Parse(((MaskedTextBox)sender).Text);
                }
                else if (((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex] is ME_Sound)
                {
                    ((ME_Sound)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).delay = int.Parse(((MaskedTextBox)sender).Text);
                }
            }
        }
        private void delaytext_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void Motionlist_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right && MotionCombo.SelectedItem != null)
            {
                Motionlist.SelectedIndex = Motionlist.IndexFromPoint(e.X, e.Y);
                if (Motionlist.SelectedItem == null)
                {
                    contextMenuStrip1.Items.Add("New object");
                    contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(Motionlistevent);
                    contextMenuStrip1.Closed += new ToolStripDropDownClosedEventHandler(Motionlistcloseevent);
                    contextMenuStrip1.Show(new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y));
                }
                else if (((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex] is ME_Frame)
                {
                    for (int i = 0; i < motionevent.Length; i++)
                        contextMenuStrip1.Items.Add(motionevent[i]);
                    contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(Motionlistevent);
                    contextMenuStrip1.Closed += new ToolStripDropDownClosedEventHandler(Motionlistcloseevent);
                    contextMenuStrip1.Show(new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y));
                }
                else if (Motionlist.SelectedItem != null)
                {
                    for (int i = 0; i < motionevent.Length - 1; i++)
                        contextMenuStrip1.Items.Add(motionevent[i]);
                    contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(Motionlistevent);
                    contextMenuStrip1.Closed += new ToolStripDropDownClosedEventHandler(Motionlistcloseevent);
                    contextMenuStrip1.Show(new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y));
                }
            }
        }
        private void Motionlistevent(object sender, ToolStripItemClickedEventArgs e)
        {
            int n;
            for (int i = 0; i < motionevent.Length; i++)
                if (String.Compare(e.ClickedItem.Text, motionevent[i]) == 0)
                    switch (i) {
                        case 0:
                            groupBox1.Enabled = true;
                            typecombo.Text = "Select type";
                            break;
                        case 1:
                            ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.RemoveAt(Motionlist.SelectedIndex);
                            Motionlist.Items.Remove(Motionlist.SelectedItem);
                            typecombo.Enabled = false;
                            typecombo.Text = "";
                            delaytext.Enabled = false;
                            delaytext.Text = "";
                            capturebutton.Enabled = false;
                            autocheck.Enabled= false;
                            Framelist.Controls.Clear();
                            Framelist.Enabled = false;
                            break;
                        case 2:
                            n = Motionlist.SelectedIndex;
                            ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.Insert(n - 1, ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[n]);
                            ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.RemoveAt(n + 1);
                            Motionlist.Items.Insert(n - 1,Motionlist.SelectedItem);
                            Motionlist.Items.RemoveAt(n + 1);
                            break;
                        case 3:
                            n = Motionlist.SelectedIndex;
                            ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.Insert(n + 2, ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[n]);
                            ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.RemoveAt(n);
                            Motionlist.Items.Insert(n + 2, Motionlist.SelectedItem);
                            Motionlist.Items.RemoveAt(n);
                            break;
                        case 4:
                            Motionlist.Items.Insert(Motionlist.SelectedIndex + 1, "[Frame] " + MotionCombo.SelectedItem.ToString() + "-" + framecount++.ToString());
                            ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.Insert(Motionlist.SelectedIndex + 1, new ME_Frame());
                            Array.Copy(((ME_Frame)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).frame, ((ME_Frame)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex + 1]).frame,32);
                            ((ME_Frame)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex + 1]).delay = ((ME_Frame)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[Motionlist.SelectedIndex]).delay;
                            Motionlist.SelectedIndex++;
                            break;
                    }
        }
        private void Motionlistcloseevent(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.ItemClicked -= Motionlistevent;
            contextMenuStrip1.Closed -= Motionlistcloseevent;
        }
        private void triggerlist_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                triggerlist.SelectedIndex = triggerlist.IndexFromPoint(e.X, e.Y);
                if (triggerlist.SelectedItem != null)
                {
                    contextMenuStrip1.Items.Add("New Trigger");
                    contextMenuStrip1.Items.Add("Delete Trigger");
                    contextMenuStrip1.Items.Add("Set Trigger");
                    contextMenuStrip1.Items.Add("Link Motion");
                    contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(triggerlistevent);
                    contextMenuStrip1.Closed += new ToolStripDropDownClosedEventHandler(triggerlistcloseevent);
                    contextMenuStrip1.Show(new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y));
                }
                else
                {
                    contextMenuStrip1.Items.Add("New Trigger");
                    contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(triggerlistevent);
                    contextMenuStrip1.Closed += new ToolStripDropDownClosedEventHandler(triggerlistcloseevent);
                    contextMenuStrip1.Show(new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y));                
                }
            }
        }
        private void triggerlistevent(object sender, ToolStripItemClickedEventArgs e)
        {
            if (String.Compare(e.ClickedItem.Text, "New Trigger") == 0) {
                ME_Trigger t = new ME_Trigger();
                t.motion = null;
                t.key = "";
                ME_Triggerlist.Insert(triggerlist.SelectedIndex+1,t);
                if(t.motion != null)
                    triggerlist.Items.Insert(triggerlist.SelectedIndex+1, "Trigger: " + t.key + "\tMotion: " + t.motion.name);
                else
                    triggerlist.Items.Insert(triggerlist.SelectedIndex + 1, "Trigger: " + t.key + "\tMotion: ");
            }
            else if (String.Compare(e.ClickedItem.Text, "Delete Trigger") == 0)
            {
                ME_Triggerlist.RemoveAt(triggerlist.SelectedIndex);
                triggerlist.Items.RemoveAt(triggerlist.SelectedIndex);
            }
            else if (String.Compare(e.ClickedItem.Text, "Set Trigger") == 0)
            {
                TriggerSet ts = new TriggerSet(((ME_Trigger)ME_Triggerlist[triggerlist.SelectedIndex]).key);
                ts.ShowDialog();
                if (ts.DialogResult == DialogResult.OK)
                {
                    ((ME_Trigger)ME_Triggerlist[triggerlist.SelectedIndex]).key = ts.Keyvalue.Text;
                    if (((ME_Trigger)ME_Triggerlist[triggerlist.SelectedIndex]).motion != null)
                    {
                        triggerlist.Items.Insert(triggerlist.SelectedIndex, "Trigger: " + ((ME_Trigger)ME_Triggerlist[triggerlist.SelectedIndex]).key + "\tMotion: " + ((ME_Trigger)ME_Triggerlist[triggerlist.SelectedIndex]).motion.name);
                        triggerlist.Items.RemoveAt(triggerlist.SelectedIndex);
                    }
                    else
                    {
                        triggerlist.Items.Insert(triggerlist.SelectedIndex, "Trigger: " + ((ME_Trigger)ME_Triggerlist[triggerlist.SelectedIndex]).key + "\tMotion: ");
                        triggerlist.Items.RemoveAt(triggerlist.SelectedIndex);
                    }
                }
            }
            else if (String.Compare(e.ClickedItem.Text, "Link Motion") == 0)
            {
                if (MotionCombo.SelectedIndex >= 0)
                {
                    ((ME_Trigger)ME_Triggerlist[triggerlist.SelectedIndex]).motion = (ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex];
                    triggerlist.Items.Insert(triggerlist.SelectedIndex, "Trigger: " + ((ME_Trigger)ME_Triggerlist[triggerlist.SelectedIndex]).key + "\tMotion: " + ((ME_Trigger)ME_Triggerlist[triggerlist.SelectedIndex]).motion.name);
                    triggerlist.Items.RemoveAt(triggerlist.SelectedIndex);
                }
                else {
                    MessageBox.Show("Please choose a Motion.");
                }
            }

        }
        private void triggerlistcloseevent(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.ItemClicked -= triggerlistevent;
            contextMenuStrip1.Closed -= triggerlistcloseevent;
        }
        private void MotionCombo_TextChanged(object sender, EventArgs e)
        {
            Boolean new_mot = true;
            NewMotion.Enabled = false;
            if (String.Compare(MotionCombo.Text, "") == 0)
                new_mot = false;
            for (int i = 0; i < MotionCombo.Items.Count; i++) 
                if (String.Compare(MotionCombo.Text, MotionCombo.Items[i].ToString()) == 0)
                    new_mot = false;
            if (new_mot) {
                NewMotion.Enabled = true;            
            }
        }
        private void NewMotion_Click(object sender, EventArgs e)
        {
            if (MotionCombo.Text.IndexOf(" ") == -1)
            {
                MotionCombo.Items.Add(MotionCombo.Text);
                ME_Motion m = new ME_Motion();
                m.name = MotionCombo.Text;
                ME_Motionlist.Add(m);
                MotionCombo.SelectedIndex = MotionCombo.Items.Count - 1;
                Motionlist.Controls.Clear();
            }
            else
                MessageBox.Show("Motion name should without space.");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Motion != null)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    saveFileToolStripMenuItem_Click(sender, e);
                }
            }
            if(!onPC)
                RoBoIO.rcservo_Close();

        }        
        private void capturebutton_Click(object sender, EventArgs e)
        {
            if (autocheck.Checked == true)
            {
                RoBoIO.rcservo_EnterCaptureMode();
                autocheck.Checked = false;
            }
            RoBoIO.rcservo_EnterCaptureMode();
            uint[] frame =new uint[32];
            RoBoIO.rcservo_CapAll(frame);
            for (int i = 0; i < 32; i++)
                if (String.Compare(Motion.fbox[i].Text, "---noServo---") != 0)
                    ftext[i].Text = frame[i].ToString(); 
        }
        private void set_RCservo()
        {
            uint usepin = 0;
            for (int i = 0; i < 32; i++)
            {
                if (String.Compare(Motion.fbox[i].Text, "---noServo---") != 0)
                {
                    usepin += (uint)Math.Pow(2, i);
                }
                if (String.Compare(Motion.fbox[i].Text, "RCSERVO_KONDO_KRS786") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_KONDO_KRS786);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_KONDO_KRS788") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_KONDO_KRS788);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_KONDO_KRS78X") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_KONDO_KRS78X);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_KONDO_KRS4014") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_KONDO_KRS4014);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_KONDO_KRS4024") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_KONDO_KRS4024);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_HITEC_HSR8498") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_HITEC_HSR8498);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_FUTABA_S3003") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_FUTABA_S3003);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_SHAYYE_SYS214050") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_SHAYYE_SYS214050);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_TOWERPRO_MG995") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_TOWERPRO_MG995);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_TOWERPRO_MG996") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_TOWERPRO_MG996);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_DMP_RS0263") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_DMP_RS0263);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_DMP_RS1270") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_DMP_RS1270);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_GWS_S777") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_GWS_S777);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_GWS_S03T") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_GWS_S03T);
                else if (String.Compare(Motion.fbox[i].Text, "RCSERVO_GWS_MICRO") == 0)
                    RoBoIO.rcservo_SetServo(i, RoBoIO.RCSERVO_GWS_MICRO);
            }
            if(!RoBoIO.rcservo_Init(usepin))
                MessageBox.Show("RC servo init fail. Error message:" + RoBoIO.roboio_GetErrMsg());
        }
        private void MotionTest_KeyDown(object sender, KeyEventArgs e)
        {
            string a, b;
            if (e.Modifiers == Keys.Control)
                a = "ctrl";
            else if (e.Modifiers == Keys.Alt)
                a = "alt";
            else if (e.Modifiers == Keys.Shift)
                a = "shift";
            else
                a = "";
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
                b = ((char)e.KeyCode).ToString();
            else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                b = ((char)e.KeyCode).ToString();
            else if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
                b = "F" + ((int)(e.KeyCode - Keys.F1 + 1)).ToString();
            else if (e.KeyCode == Keys.Up)
                b = "Up";
            else if (e.KeyCode == Keys.Down)
                b = "Down";
            else if (e.KeyCode == Keys.Left)
                b = "Left";
            else if (e.KeyCode == Keys.Right)
                b = "Right";
            else
                b = "";

            if (String.Compare(a, "") != 0 && String.Compare(b, "") != 0)
                motiontestkey=(a + "+" + b);
            else if (String.Compare(b, "") != 0)
                motiontestkey=(b);
            else if (String.Compare(b, "") == 0)
                motiontestkey=(a);
            motiontestkey ="";
        }
        private void MotionTest_Click(object sender, EventArgs e)
        {
            motiontestkey = "";
            if (MotionCombo.SelectedItem != null)
            {
                if (autocheck.Checked == true)
                {
                    RoBoIO.rcservo_EnterCaptureMode();
                    autocheck.Checked = false;
                }
                RoBoIO.rcservo_EnterPlayMode();
                MotionTest.Enabled = false;
                SoundPlayer sp = null;
                for (int j = 0; j < ((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events.Count; j++)
                {
                    Motionlist.SelectedIndex = j;
                    if (((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j] is ME_Frame)
                    {

                        for (int i = 0; i < 32; i++)
                            if (String.Compare(Motion.fbox[i].Text, "---noServo---") != 0)
                                autoframe[i] = (uint)((ME_Frame)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j]).frame[i] + offset[i];
                        RoBoIO.rcservo_SetAction(autoframe, (uint)((ME_Frame)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j]).delay);
                        while (RoBoIO.rcservo_PlayAction() != RoBoIO.RCSERVO_PLAYEND) ;

                    }
                    else if (((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j] is ME_Delay)
                    {
                        RoBoIO.delay_ms((uint)((ME_Delay)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j]).delay);
                    }
                    else if (((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j] is ME_Sound)
                    {
                        sp = new System.Media.SoundPlayer(Application.StartupPath + "\\" + ((ME_Sound)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j]).filename);
                        sp.Play();
                        RoBoIO.delay_ms((uint)((ME_Sound)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j]).delay);
                    }
                    else if (((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j] is ME_Goto)
                    {
                        if (string.Compare(motiontestkey, ((ME_Goto)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j]).key) == 0)
                        for (int k = 0; k < j; k++){
                            if(((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[k] is ME_Flag){
                                if (String.Compare(((ME_Goto)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[j]).name,
                                                ((ME_Flag)((ME_Motion)ME_Motionlist[MotionCombo.SelectedIndex]).Events[k]).name) == 0)
                                    j = k;
                            }

                        }
                    }
                }
                MotionTest.Enabled = true;
                if(sp != null)
                    sp.Stop();
                RoBoIO.rcservo_EnterCaptureMode();
            }
        }        
        private void set_RBver(){
            if (String.Compare(Motion.comboBox1.Text, "RB_100b1") == 0)
            {
                if (!RoBoIO.roboio_SetRBVer(RoBoIO.RB_100b1))
                    MessageBox.Show("Set version " + Motion.comboBox1.Text+ " fail. Error message:" + RoBoIO.roboio_GetErrMsg());
            }
            else if (String.Compare(Motion.comboBox1.Text, "RB_100b2") == 0){
                if (!RoBoIO.roboio_SetRBVer(RoBoIO.RB_100b2))
                    MessageBox.Show("Set version " + Motion.comboBox1.Text + " fail. Error message:" + RoBoIO.roboio_GetErrMsg());
            }
            else if (String.Compare(Motion.comboBox1.Text, "RB_100b3") == 0){
                if (!RoBoIO.roboio_SetRBVer(RoBoIO.RB_100b3))
                    MessageBox.Show("Set version " + Motion.comboBox1.Text + " fail. Error message:" + RoBoIO.roboio_GetErrMsg());
            }
            else if (String.Compare(Motion.comboBox1.Text, "RB_100") == 0){
                if (!RoBoIO.roboio_SetRBVer(RoBoIO.RB_100))
                    MessageBox.Show("Set version " + Motion.comboBox1.Text + " fail. Error message:" + RoBoIO.roboio_GetErrMsg());
            }
            else if (String.Compare(Motion.comboBox1.Text, "RB_100RD") == 0){
                if (!RoBoIO.roboio_SetRBVer(RoBoIO.RB_100RD))
                    MessageBox.Show("Set version " + Motion.comboBox1.Text + " fail. Error message:" + RoBoIO.roboio_GetErrMsg());
            }
            else if (String.Compare(Motion.comboBox1.Text, "RB_110") == 0){
                if (!RoBoIO.roboio_SetRBVer(RoBoIO.RB_110))
                    MessageBox.Show("Set version " + Motion.comboBox1.Text + " fail. Error message:" + RoBoIO.roboio_GetErrMsg());
            }
            else if (String.Compare(Motion.comboBox1.Text, "unknow") == 0){
                if (!RoBoIO.roboio_SetRBVer(RoBoIO.roboio_GetRBVer()))
                    MessageBox.Show("Set version " + Motion.comboBox1.Text + " fail. Error message:" + RoBoIO.roboio_GetErrMsg());
            }
        }
        private void autocheck_CheckedChanged(object sender, EventArgs e)
        {

            if (autocheck.Checked == true && int.Parse(delaytext.Text) < 0)
            {
                MessageBox.Show("Please set the correct delay time.");
                autocheck.Checked = false;
            }
            else if (autocheck.Checked == true)
            {

                RoBoIO.rcservo_EnterPlayMode();
                for (int i = 0; i < 32; i++)
                    if (String.Compare(Motion.fbox[i].Text, "---noServo---") != 0)
                        autoframe[i] = uint.Parse(ftext[i].Text) + offset[i];
                RoBoIO.rcservo_SetAction(autoframe, uint.Parse(delaytext.Text));
                autocheck.Enabled = false;
                autocheck.Capture = false;
                while (RoBoIO.rcservo_PlayAction() != RoBoIO.RCSERVO_PLAYEND) ;
                autocheck.Capture = true;
                autocheck.Enabled = true;
            }else if (autocheck.Checked == false){
                RoBoIO.rcservo_EnterCaptureMode();
            }
        }
        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://roboardgod.blogspot.tw/2013/01/robome.html");
            
        }
    }
}