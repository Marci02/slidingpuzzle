using System;
using System.Linq;
using System.Windows.Forms;

namespace sliding_puzzle
{
    public partial class Form1 : Form
    {
        private Button[,] buttons;
        private int emptyRow, emptyCol;
        private int moves;

        public Form1()
        {
            InitializeButtons();
            Shuffle();
            UpdateButtons();
        }

        private void InitializeButtons()
        {
            buttons = new Button[3, 3];
            int buttonSize = 50;
            int padding = 5;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(buttonSize, buttonSize);
                    buttons[i, j].Location = new Point(j * (buttonSize + padding), i * (buttonSize + padding));
                    buttons[i, j].Click += Button_Click;
                    this.Controls.Add(buttons[i, j]);
                }
            }
        }

        private void Shuffle()
        {
            Random random = new Random();
            int[] numbers = new int[8];
            int x = 0;
            for (int i = 1; i <= 8; i++)
            {
                numbers[x] = i;
                x++;
            }

            for (int i = numbers.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }

            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (k < 8)
                        buttons[i, j].Text = numbers[k++].ToString();
                    else
                    {
                        buttons[i, j].Text = "";
                        emptyRow = i;
                        emptyCol = j;
                    }
                }
            }

            moves = 0;
            UpdateMovesLabel();
        }

        private void UpdateButtons()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j].Text = buttons[i, j].Text == "0" ? "" : buttons[i, j].Text;
                    moves++;
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int row = -1, col = -1;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (buttons[i, j] == clickedButton)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
            }

            if ((Math.Abs(row - emptyRow) == 1 && col == emptyCol) || (Math.Abs(col - emptyCol) == 1 && row == emptyRow))
            {
                SwapButtons(row, col, emptyRow, emptyCol);
                moves++;
                UpdateMovesLabel();

                if (CheckForWin())
                {
                    MessageBox.Show($"Megoldottad!");
                    Shuffle();
                }
            }
        }

        private void SwapButtons(int row1, int col1, int row2, int col2)
        {
            string tempText = buttons[row1, col1].Text;
            buttons[row1, col1].Text = buttons[row2, col2].Text;
            buttons[row2, col2].Text = tempText;

            emptyRow = row1;
            emptyCol = col1;
        }

        private bool CheckForWin()
        {
            int number = 1;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 2 && j == 2)
                        continue;

                    if (buttons[i, j].Text != number.ToString())
                        return false;

                    number++;
                }
            }

            return true;
        }

        private void UpdateMovesLabel()
        {
            Label labels = new Label();

            labels.Text = $"Moves: {moves}";

            labels.Location = new Point(5, 50 * 4); 

            this.Controls.Add(labels);
        }


    }
}