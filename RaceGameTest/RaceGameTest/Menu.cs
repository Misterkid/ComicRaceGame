﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RaceGameTest.Q_Engine;
using System.Timers;
namespace RaceGameTest
{
    partial class Menu : Form
    {
        private Game game;
        private System.Timers.Timer timerTextBox1;
        private string player1Name = "Aladeen";
        private string player2Name = "علاء الدين";
        public Menu(Game _game)
        {
            
            InitializeComponent();
            game = _game;

            jSound.AddSound("greet", "_Sounds\\greet.wav",1);
            jSound.PlaySound("greet");

            jSound.AddSound("menuMusic", "_Sounds\\aladeen_mofo.wav", 0.3f);
            jSound.PlaySoundLooping("menuMusic");
            timerTextBox1 = new System.Timers.Timer();
            timerTextBox1.Interval = 1000;
            timerTextBox1.Elapsed += timerTextBox1_Elapsed;
            timerTextBox1.AutoReset = true;
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        void timerTextBox1_Elapsed(object sender, ElapsedEventArgs e)
        {
            Invoke((MethodInvoker)(() =>
            {
                // Thread safe here, update control positions or whatever yaaay
                textBox1.Text = player1Name;
                textBox2.Text = player2Name;
            }));

        }
        public void button1_Click(object sender, EventArgs e)
        {
            //this.KeyPreview = true;
            jSound.StopAllSounds();
            this.Hide();
            Form1 form1 = new Form1(game);
            form1.Location = this.Location;
            form1.Show();
            form1.game.Initialize();
            textBox1.Text = player1Name;
            textBox2.Text = player2Name;
            form1.SetPlayerNames(textBox1.Text, textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("leave");
        }

        private void groupBox1_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("leave");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            timerTextBox1.Stop();
            timerTextBox1.Start();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            timerTextBox1.Stop();
            timerTextBox1.Start();
        }
    }
}
