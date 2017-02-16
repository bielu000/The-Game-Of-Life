using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace TheGameOfLife
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Thread thr;

        private void button1_Click(object sender, EventArgs e)
        {
            int squareLenght = Convert.ToInt16(numericUpDown1.Value);
            int occupancyLength = Convert.ToInt16(numericUpDown2.Value);
            int cycles = Convert.ToInt16(numericUpDown3.Value);

            button1.Enabled = false;
            button2.Enabled = true;

            Game game = new Game();

            game.squareLength = squareLenght;
            game.setOccupancy(occupancyLength);
            game.cycles = cycles;
            game.generateSquare();

            this.thr = new Thread(startGame);
            this.thr.Start(game);

            
        }

        public void startGame(object gameObj)
        {
            Game game = (Game)gameObj;

            for (int i = 0; i < game.cycles; i++)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label4.Text = "" + "Cykl: " + (i + 1);
                });

                renderSquare(game);
                game.calculate();

                Application.DoEvents();
                Thread.Sleep(1000);
            }
           
        }

        public void renderSquare(Game game)
        {
            this.Invoke((MethodInvoker)delegate
            {

                dataGridView1.Rows.Clear();

                for (int i = 0; i < game.squareLength; i++)
                {
                    var row = new DataGridViewRow();

                    for (int j = 0; j < game.squareLength; j++)
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell()
                        {
                            Value = game.square[i, j]
                        });

                        if (Convert.ToInt16(row.Cells[j].Value) == 1)
                            row.Cells[j].Style.BackColor = Color.Green;
                        else
                            row.Cells[j].Style.BackColor = Color.Gray;

                        dataGridView1.DefaultCellStyle.SelectionForeColor = row.Cells[j].Style.BackColor;
                        dataGridView1.DefaultCellStyle.SelectionBackColor = row.Cells[j].Style.BackColor;
                        row.Cells[j].Style.ForeColor = row.Cells[j].Style.BackColor;

                    }

                    dataGridView1.ColumnCount = game.squareLength;
                    if (i < game.squareLength)
                        dataGridView1.Rows.Add(row);

                    DataGridViewColumn col = dataGridView1.Columns[i];
                    row.Height = col.Width;

                }
            });
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            button1.Enabled = true;
            button2.Enabled = false;

            this.thr.Abort();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

    }
}
