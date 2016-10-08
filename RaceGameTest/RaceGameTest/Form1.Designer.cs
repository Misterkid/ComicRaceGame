namespace RaceGameTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pitStopPlayer2 = new System.Windows.Forms.Label();
            this.lapsPlayer2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.player2speedText = new System.Windows.Forms.Label();
            this.fuelbarrplayer2 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pitStopPlayer1 = new System.Windows.Forms.Label();
            this.lapsPlayer1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.player1speedText = new System.Windows.Forms.Label();
            this.fuelbarrplayer1 = new System.Windows.Forms.ProgressBar();
            this.CountDownText = new System.Windows.Forms.Label();
            this.EndGameScreen = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndGameScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(986, 33);
            this.label2.TabIndex = 10;
            this.label2.Text = "0:0:0";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(941, 11);
            this.Exit.Margin = new System.Windows.Forms.Padding(2);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(56, 19);
            this.Exit.TabIndex = 9;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pitStopPlayer2);
            this.groupBox2.Controls.Add(this.lapsPlayer2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.player2speedText);
            this.groupBox2.Controls.Add(this.fuelbarrplayer2);
            this.groupBox2.Location = new System.Drawing.Point(847, 637);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(150, 81);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // pitStopPlayer2
            // 
            this.pitStopPlayer2.AutoSize = true;
            this.pitStopPlayer2.Location = new System.Drawing.Point(73, 15);
            this.pitStopPlayer2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pitStopPlayer2.Name = "pitStopPlayer2";
            this.pitStopPlayer2.Size = new System.Drawing.Size(44, 13);
            this.pitStopPlayer2.TabIndex = 10;
            this.pitStopPlayer2.Text = "Pitstops";
            // 
            // lapsPlayer2
            // 
            this.lapsPlayer2.AutoSize = true;
            this.lapsPlayer2.Location = new System.Drawing.Point(4, 15);
            this.lapsPlayer2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lapsPlayer2.Name = "lapsPlayer2";
            this.lapsPlayer2.Size = new System.Drawing.Size(30, 13);
            this.lapsPlayer2.TabIndex = 9;
            this.lapsPlayer2.Text = "Laps";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-2, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "label4";
            // 
            // player2speedText
            // 
            this.player2speedText.AutoSize = true;
            this.player2speedText.Location = new System.Drawing.Point(4, 43);
            this.player2speedText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.player2speedText.Name = "player2speedText";
            this.player2speedText.Size = new System.Drawing.Size(32, 13);
            this.player2speedText.TabIndex = 5;
            this.player2speedText.Text = "km/h";
            // 
            // fuelbarrplayer2
            // 
            this.fuelbarrplayer2.Location = new System.Drawing.Point(4, 58);
            this.fuelbarrplayer2.Margin = new System.Windows.Forms.Padding(2);
            this.fuelbarrplayer2.Name = "fuelbarrplayer2";
            this.fuelbarrplayer2.Size = new System.Drawing.Size(130, 19);
            this.fuelbarrplayer2.TabIndex = 1;
            this.fuelbarrplayer2.Value = 100;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pitStopPlayer1);
            this.groupBox1.Controls.Add(this.lapsPlayer1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.player1speedText);
            this.groupBox1.Controls.Add(this.fuelbarrplayer1);
            this.groupBox1.Location = new System.Drawing.Point(11, 637);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(150, 81);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // pitStopPlayer1
            // 
            this.pitStopPlayer1.AutoSize = true;
            this.pitStopPlayer1.Location = new System.Drawing.Point(66, 15);
            this.pitStopPlayer1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pitStopPlayer1.Name = "pitStopPlayer1";
            this.pitStopPlayer1.Size = new System.Drawing.Size(44, 13);
            this.pitStopPlayer1.TabIndex = 9;
            this.pitStopPlayer1.Text = "Pitstops";
            // 
            // lapsPlayer1
            // 
            this.lapsPlayer1.AutoSize = true;
            this.lapsPlayer1.Location = new System.Drawing.Point(4, 15);
            this.lapsPlayer1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lapsPlayer1.Name = "lapsPlayer1";
            this.lapsPlayer1.Size = new System.Drawing.Size(30, 13);
            this.lapsPlayer1.TabIndex = 8;
            this.lapsPlayer1.Text = "Laps";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "label3";
            // 
            // player1speedText
            // 
            this.player1speedText.AutoSize = true;
            this.player1speedText.Location = new System.Drawing.Point(4, 43);
            this.player1speedText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.player1speedText.Name = "player1speedText";
            this.player1speedText.Size = new System.Drawing.Size(32, 13);
            this.player1speedText.TabIndex = 2;
            this.player1speedText.Text = "km/h";
            // 
            // fuelbarrplayer1
            // 
            this.fuelbarrplayer1.Location = new System.Drawing.Point(4, 58);
            this.fuelbarrplayer1.Margin = new System.Windows.Forms.Padding(2);
            this.fuelbarrplayer1.Name = "fuelbarrplayer1";
            this.fuelbarrplayer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fuelbarrplayer1.Size = new System.Drawing.Size(130, 19);
            this.fuelbarrplayer1.TabIndex = 1;
            this.fuelbarrplayer1.Value = 100;
            // 
            // CountDownText
            // 
            this.CountDownText.BackColor = System.Drawing.Color.Transparent;
            this.CountDownText.Font = new System.Drawing.Font("Microsoft Sans Serif", 128F);
            this.CountDownText.ForeColor = System.Drawing.Color.Green;
            this.CountDownText.Location = new System.Drawing.Point(18, 211);
            this.CountDownText.Name = "CountDownText";
            this.CountDownText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CountDownText.Size = new System.Drawing.Size(979, 193);
            this.CountDownText.TabIndex = 11;
            this.CountDownText.Text = "Start!";
            this.CountDownText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EndGameScreen
            // 
            this.EndGameScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EndGameScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EndGameScreen.Location = new System.Drawing.Point(0, 0);
            this.EndGameScreen.Name = "EndGameScreen";
            this.EndGameScreen.Size = new System.Drawing.Size(1008, 729);
            this.EndGameScreen.TabIndex = 12;
            this.EndGameScreen.TabStop = false;
            this.EndGameScreen.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.EndGameScreen);
            this.Controls.Add(this.CountDownText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Aladeen Racing";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndGameScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label player2speedText;
        private System.Windows.Forms.ProgressBar fuelbarrplayer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label player1speedText;
        private System.Windows.Forms.ProgressBar fuelbarrplayer1;
        private System.Windows.Forms.Label CountDownText;
        private System.Windows.Forms.Label pitStopPlayer2;
        private System.Windows.Forms.Label lapsPlayer2;
        private System.Windows.Forms.Label pitStopPlayer1;
        private System.Windows.Forms.Label lapsPlayer1;
        private System.Windows.Forms.PictureBox EndGameScreen;


    }
}

