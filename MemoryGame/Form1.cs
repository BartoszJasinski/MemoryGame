using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!","!","N","N",",",",","k","k"
            ,"b","b","v","v","w","w","z","z"
        };

        Label firstClicked = null;
        Label secondClicked = null;
        int counter = 7;        

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label label = control as Label;
                if (label != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    label.Text = icons[randomNumber];
                    label.ForeColor = label.BackColor;
                    icons.RemoveAt(randomNumber);
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {


        }


        private void label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {

                if (clickedLabel.ForeColor == Color.Black)
                    return;
                

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }
                else
                {
                    secondClicked = clickedLabel;
                    if (checkIcons(firstClicked, secondClicked))
                    {
                        secondClicked.ForeColor = Color.Black;
                        firstClicked = null;
                        secondClicked = null;
                        //if (checkIfPlayerWon())
                        //    winnerFunction();
                        CheckForWinner();

                    }
                    else
                    {
                        secondClicked.ForeColor = Color.Black;
                        timer1.Start();
                    }
                }
            }
        }

        private void winnerFunction()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label label = control as Label;
                if (label != null)
                    label.ForeColor = label.BackColor;
            }
       }
        private bool checkIfPlayerWon()
        {
            if (counter == 0)
                return true;
            counter--;
            return false;
        }

        private bool checkIcons(Label firstLabel, Label secondLabel)
        {
            if (firstLabel.Text == secondLabel.Text && firstLabel != secondLabel)
                return true;


            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // Timer timer1 = sender as Timer;

          //  if (timer1 != null)
                timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            // Go through all of the labels in the TableLayoutPanel, 
            // checking each one to see if its icon is matched
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // If the loop didn’t return, it didn't find
            // any unmatched icons
            // That means the user won. Show a message and close the form
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }
    }
}
