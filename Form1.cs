﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lego_Harry_Potter_Save_Reader
{
    public partial class Form1 : Form
    {
        Functions f = new Functions();
        int[] TimerContents = new int[3];
        public Form1()
        {
            InitializeComponent();
        }
        private void update()
        {
            try
            {
                Properties.Settings.Default.PreviousSaveFile = textBox1.Text;
                Properties.Settings.Default.PreviousTextFile = textBox2.Text;
                Properties.Settings.Default.Save();
                float percentage;
                string filename = textBox1.Text;
                if (filename.EndsWith(".LEGO®_Harry_Potter™SaveGameData"))
                {
                    percentage = (BitConverter.ToSingle(f.GetRawBytes(filename, 0x2028, 4), 0)) / 2;
                }
                else if (filename.EndsWith(".LEGOHarryPotter2SaveGameData"))
                {
                    percentage = 50 + ((BitConverter.ToSingle(f.GetRawBytes(filename, 0x202C, 4), 0)) / 2);
                }
                else
                {
                    throw new System.IO.IOException("Incompatable File Type");
                }
                label1.Text = percentage + "%";
                File.WriteAllText(@textBox2.Text, percentage.ToString("0.0") + "%");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                textBox1.Text = openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.ShowDialog();
                textBox2.Text = saveFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                button3.Text = "Start";
                timer1.Stop();
            }
            else
            {
                button3.Text = "Stop";
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            update();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.PreviousSaveFile;
            textBox2.Text = Properties.Settings.Default.PreviousTextFile;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled)
            {
                button6.Text = "Start";
                timer2.Stop();
            }
            else
            {
                button6.Text = "Stop";
                timer2.Start();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog2.ShowDialog();
                textBox3.Text = saveFileDialog2.FileName;
                string sTimerContents = "";
                if (File.Exists(textBox3.Text))
                {
                    sTimerContents = f.LoadTimer(textBox3.Text);
                    label2.Text = sTimerContents;
                    string[] saTimerContents = sTimerContents.Split(':');
                    Array.Reverse(saTimerContents);
                    TimerContents = Array.ConvertAll(saTimerContents, int.Parse); //TimerContents[0] is seconds, [1] is mins, [2] is hours
                }
                else
                {
                    File.WriteAllText(@textBox3.Text, "");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.TopMost)
            {
                this.TopMost = false;
                button8.Font = new Font(button8.Font, FontStyle.Regular);
            }
            else
            {
                this.TopMost = true;
                button8.Font = new Font(button8.Font, FontStyle.Bold);
            }
        }
    }
}
