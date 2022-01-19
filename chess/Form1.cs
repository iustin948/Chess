using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess
{
	
	public partial class Form1 : Form
	{   
		public Form1()
		{
					InitializeComponent();
		}

		bool Inside(int i, int j)
        {
			if (i >= 0 && i <= 7 && j >= 0 && j <= 7)
				return true;
			return false;

        }

		void Delete()
        {
			for (int n = 0; n < 8; n++)
				for (int m = 0; m < 8; m++)
				{
					if (M[n, m].state == "path")
						M[n, m].state = "empty";
					if (M[n, m].box.BackgroundImage == path)
						M[n, m].box.BackgroundImage = null;
					M[n, m].Take.Clear();
				}
		}

        private void pictureBox1_Click(object sender, EventArgs e)
        {
			
			PictureBox b = sender as PictureBox;
			int x = (int)b.Tag;
			int i, j;
			i = x / 10; j = x % 10;
			if(pressed == 0)
            {
				a = M[i, j];
				a.x = i;
				a.y = j;
				a.Take.Clear();
				if (a.state == "empty" || a.color != turn)
					return;

				/*if(a.state != "empty" && a.color == 1)
                {
					string t = a.state;
					a.state = "empty";
					if (CheckForCheck(x_white, y_white, a.color) && turn == a.color)
					{
						MessageBox.Show("Nu poti muta!");
						a.state = t;
						return;
					}
					a.state = t;
					//MessageBox.Show(a.state);
                }*/
				//if (cnt > 1)
				{
					if (a.state == "pion")
						Mutare_Pion(i, j);

					else if (a.state == "tura")
						Mutare_Tura(i, j);

					else if (a.state == "nebun")
						Mutare_Nebun(i, j);

					else if (a.state == "cal")
						Mutare_Cal(i, j);
					else if (a.state == "regina")
						Mutare_Regina(i, j);
					else if (a.state == "rege")
						Mutare_Rege(i, j);
					pressed = 1;
				}
				
            }

			else if(pressed == 1)
            {
				pressed = 0;
				if(((a.state == "tura" && M[i ,j].state == "rege") ||(a.state == "rege" && M[i, j].state == "tura"))  && a.color == M[i,j].color)
                {
					
					Patrat swap = new Patrat();
					if (!(a.pion.FirstMove == 1 && M[i, j].pion.FirstMove == 1)) return;
					if (a.x != i) return;
					
					int X = a.x;
					if(a.state == "rege")
                    {
						swap.state = M[i, j].state;
						swap.box.BackgroundImage = M[i, j].box.BackgroundImage;
						swap.pion = M[i, j].pion;
						swap.color = M[i, j].color;
						swap.x = i;
						swap.y = j;

						M[i, j].state = a.state;
						M[i, j].box.BackgroundImage = a.box.BackgroundImage;
						M[i, j].pion = a.pion;
						M[i, j].color = a.color;
						M[i, j].x = a.x;
						M[i, j].y = a.y;

						a.state = swap.state;
						a.box.BackgroundImage = swap.box.BackgroundImage;
						a.pion = swap.pion;
						a.color = swap.color;
						a.x = swap.x;
						a.y = swap.y;

					}
					
					if (a.y == 0)
                    {
					
						for (int d = 1; d < 4; d++)
							if (!(M[X, d].state == "path" || M[X, d].state == "empty")) return;
						
						M[X, 3].state = a.state;
						M[X, 3].box.BackgroundImage = a.box.BackgroundImage;
						M[X, 3].pion = a.pion;
						M[X, 3].color = a.color;

						M[X, 2].state = M[i,j].state;
						M[X, 2].box.BackgroundImage = M[i, j].box.BackgroundImage;
						M[X, 2].pion = M[i, j].pion;
						M[X, 2].color = M[i, j].color;
						
						if(CheckForCheck(X,2,turn))
                        {
							M[X, 0].state = a.state;
							M[X, 0].box.BackgroundImage = a.box.BackgroundImage;
							M[X, 0].pion = a.pion;
							M[X, 0].color = a.color;

							
							M[X, 4].state = M[X, 2].state;
							M[X, 4].box.BackgroundImage = M[X, 2].box.BackgroundImage;
							M[X, 4].pion = M[X, 2].pion;
							M[X, 4].color = M[X, 2].color;

							M[X, 3].state = "empty";
							M[X, 3].box.BackgroundImage = null;

							M[X, 2].state = "empty";
							M[X, 2].box.BackgroundImage = null;
							Delete();
							pressed = 0;
							return;
						}
						a.state = "empty";
						a.box.BackgroundImage = null;

						M[i, j].state = "empty";
						M[i, j].box.BackgroundImage = null;
						if (turn == 1)
							y_white = 2;
						else y_black = 2;
					M[X, 3].pion.FirstMove = 0;
					M[X, 2].pion.FirstMove = 0;
					}

					if (a.y == 7)
					{
						for (int d = 5; d < 6; d++)
							if (!(M[X, d].state == "path" || M[X, d].state == "empty")) return;
						//MessageBox.Show("intra");

						M[X, 5].state = a.state;
						M[X, 5].box.BackgroundImage = a.box.BackgroundImage;
						M[X, 5].pion = a.pion;
						M[X, 5].color = a.color;

						M[X, 6].state = M[i, j].state;
						M[X, 6].box.BackgroundImage = M[i, j].box.BackgroundImage;
						M[X, 6].pion = M[i, j].pion;
						M[X, 6].color = M[i, j].color;

						if (CheckForCheck(X, 6, turn))
						{
							M[X, 7].state = a.state;
							M[X, 7].box.BackgroundImage = a.box.BackgroundImage;
							M[X, 7].pion = a.pion;
							M[X, 7].color = a.color;

							M[X, 4].state = M[X, 6].state;
							M[X, 4].box.BackgroundImage = M[X, 6].box.BackgroundImage;
							M[X, 4].pion = M[X, 6].pion;
							M[X, 4].color = M[X, 6].color;

							M[X, 6].state = "empty";
							M[X, 6].box.BackgroundImage = null;

							M[X, 5].state = "empty";
							M[X, 5].box.BackgroundImage = null;
							Delete();
							pressed = 0;
							return;
						}
						a.state = "empty";
						a.box.BackgroundImage = null;

						M[i, j].state = "empty";
						M[i, j].box.BackgroundImage = null;
						
						if (turn == 1)
							y_white = 2;
						else y_black = 2;
						M[X, 5].pion.FirstMove = 0;
					M[X, 6].pion.FirstMove = 0;
					}
					turn *= -1;
					if (turn == 1)
						check = CheckForCheck(x_white, y_white, turn);
					else check = CheckForCheck(x_black, y_black, turn);
				}
				if (M[i, j].state == "path")         ////////////////////////////// Mover
				{
					if(check == true)
                    {
						bool verif;
						//MessageBox.Show("aaaa");
						M[i, j].state = a.state;
						a.state = "empty";

						if (M[i, j].state == "rege")
							verif = CheckForCheck(i, j, turn);
						else if (turn == 1) verif = CheckForCheck(x_white, y_white, turn);
						else  verif = CheckForCheck(x_black, y_black, turn);

						a.state = M[i, j].state;
						M[i, j].state = "empty";

						
						if (verif == true)
						{
							MessageBox.Show("Miscare invalida!");
							for (int n = 0; n < 8; n++)
								for (int m = 0; m < 8; m++)
								{
									if (M[n, m].state == "path")
										M[n, m].state = "empty";
									if (M[n, m].box.BackgroundImage == path)
										M[n, m].box.BackgroundImage = null;
								}
							pressed = 0;
							return;
						}
						
					}
						

					if(EnPassant == 1)
                    {
						M[_X, _Y].state = "empty";
						EnPassant = 0;
                    }
					if (a.state == "pion" && a.pion.FirstMove == 1 && (i == 3 || i == 4))
					{
						M[i + turn, j].state = "EnPassant";
						EnPassant = 1;
						_X = i + turn;
						_Y = j;

					}
					if ( a.pion.FirstMove == 1)
						a.pion.FirstMove = 0;

					M[i, j].state = a.state;
					M[i, j].box.BackgroundImage = a.box.BackgroundImage;
					M[i, j].pion = a.pion;
					M[i, j].color = a.color;

					if (a.state == "rege" && a.color == 1)
					{
						x_white = i;
						y_white = j;
					}
					else if (a.state == "rege" && a.color == -1)
					{
						x_black = i;
						y_black = j;
					}
					if (M[i, j].state == "pion" && (i == 0 || i == 7))
					{
						II = i;
						JJ = j;
						Promovare(turn);
					}
					a.state = "empty";
					a.box.BackgroundImage = null;
					a.Take.Clear();
					
					turn *= -1;
					if (turn == 1)
						check = CheckForCheck(x_white, y_white, turn);
					else check = CheckForCheck(x_black, y_black, turn);
					cnt++;
					pressed = 0;
				}
				else if (M[i, j].state == "EnPassant" && a.state == "pion" && i + turn == a.x && Math.Abs(a.y - j) == 1) //////////////////////// En Passant
                {
					M[0, 0].box.BackColor = Color.White;
					M[i, j].state = a.state;
					M[i, j].box.BackgroundImage = a.box.BackgroundImage;
					M[i, j].pion = a.pion;
					M[i, j].color = a.color;

					M[i + turn,j].state = "empty";
					M[i + turn,j].box.BackgroundImage = null;
					
					a.state = "empty";
					a.box.BackgroundImage = null;
					turn *= -1;
					if (turn == 1)
						check = CheckForCheck(x_white, y_white, turn);
					else check = CheckForCheck(x_black, y_black, turn);
					pressed = 0;
				}
				else if (M[i, j].state != "path" && a != M[i,j]) ///////////////////////////////////////////////////////////////////// Taker
				{
					if (EnPassant == 1)
					{
						M[_X, _Y].state = "empty";
						EnPassant = 0;
					}
					if (a.Take.Contains(M[i, j]))
					{
						M[i, j].state = a.state;
						M[i, j].box.BackgroundImage = a.box.BackgroundImage;
						M[i, j].pion = a.pion;
						M[i, j].color = a.color;
						M[i, j].attacked = 0;

						if (a.state == "rege" && a.color == 1)
						{
							x_white = i;
							y_white = j;
						}
						else if (a.state == "rege" && a.color == -1)
						{
							x_black = i;
							y_black = j;

						}

						if (M[i, j].state == "pion" && (i == 0 || i == 7))
						{
							II = i;
							JJ = j;
							Promovare(turn);
						}
						a.Take.Clear();
						a.state = "empty";
						a.box.BackgroundImage = null;

						turn *= -1;
						if (turn == 1)
							check = CheckForCheck(x_white, y_white, turn);
						else check = CheckForCheck(x_black, y_black, turn);
						cnt++;
					}
					pressed = 0;
				}

				pressed = 0;
				Delete();

				if (check)
				{
					if (check)
					{
						if (Checkmate()) MessageBox.Show("sah mat");
						else MessageBox.Show("sah");

					}
				}
				

			}
			
		}

  
    }
}


