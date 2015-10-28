using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ui_test1
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Game Game = new Game();
            Game.Show();
            Game.PassValue1(textBox1.Text);
            Game.PassValue2(textBox2.Text);       
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
