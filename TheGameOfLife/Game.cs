using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGameOfLife;
using System.Windows.Forms;
 
namespace TheGameOfLife
{
    public class Game
    {
        public int squareLength { get; set; }

        public int occupancy { get; protected set; }

        public int cycles { get; set; }

        public int[,] square { get; protected set; }

        public int setOccupancy(int percent)
        {
            return occupancy = (percent * squareLength * squareLength) / 100;
        }

        public void generateSquare()
        {
            Random rand = new Random();

            square = new int[squareLength, squareLength];

            int tmp_occ = 0;

            while (tmp_occ < occupancy)
            {
                for (int i = 0; i < squareLength; i++)
                {
                    for (int j = 0; j < squareLength; j++)
                    {
                        int tmp_rand = rand.Next(0, 2);

                        if (tmp_occ > 1 && square[i, j] != 0)
                            continue;

                        else if (tmp_rand != 0 && tmp_occ < occupancy)
                        {
                            square[i, j] = tmp_rand;
                            tmp_occ++;
                        }
                            
                    }

                }
            }

        }

        public void calculate()
        {
            int[,] tmp_square = new int[squareLength, squareLength];

            for (int i = 0; i < squareLength; i++)
            {
               
                for (int j = 0; j < squareLength; j++)
                {
                    int neighbors = findNeighbors(i,j);

                    if (square[i, j] == 0 && neighbors == 3)
                        tmp_square[i, j] = 1;

                    else if (square[i, j] == 1 && 2 <= neighbors && neighbors <= 3)
                        tmp_square[i, j] = 1;

                    else
                        tmp_square[i, j] = 0;
                }

            }

            for (int i = 0; i < squareLength; i++)
            {
                for (int j = 0; j < squareLength; j++)
                {
                    square[i, j] = tmp_square[i, j];
                }
            }

        }

        private int findNeighbors(int posx, int posy)
        {
            int neighbors = 0;

            for (int row = posx - 1; row <= posx + 1; row++)
            {
                for (int col = posy - 1; col <= posy + 1; col++)
                {
                    if (!(row == posx && col == posy) && row >= 0 && col >= 0 && row < squareLength && col < squareLength)
                    {
                        if (square[row, col] == 1)
                            neighbors++;
                    }
                }
            }

            return neighbors;
        }
    }
}
