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
namespace RaceGameTest
{
    partial class Menu : Form
    {
        private Game game;
        public Menu(Game _game)
        {
            InitializeComponent();
            game = _game;

            jSound.AddSound("greet", "_Sounds\\greet.wav",1);
            jSound.PlaySound("greet");

            jSound.AddSound("menuMusic", "_Sounds\\aladeen_mofo.wav", 0.3f);
            jSound.PlaySoundLooping("menuMusic");
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
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
            form1.SetPlayerNames(textBox1.Text, textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
