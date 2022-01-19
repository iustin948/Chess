using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace chess
{
	public partial class Form1 : Form
    {
		PictureBox[,] matrice = new PictureBox[100, 100];
		Patrat[,] M = new Patrat[100, 100];
		Patrat reset = new Patrat();
		Patrat a = new Patrat();
		int x_white = 7, y_white = 4;
		int x_black = 0, y_black = 4;
		int turn = 1;
		int cnt = 0;
		int _X, _Y;
		int II, JJ;
		int EnPassant = 0;
		int pressed = 0;
		int tile_size = 120;
		int space = 20;
		bool check = false;
		//string folder = Directory.GetParent();
		Image alb = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\pionA.PNG");
		Image negru = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\pionN.PNG");
		Image path = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\path.PNG");
		Image turaN = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\tura.PNG");
		Image turaA = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\turaN.PNG");
		Image nebunA = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\nebunA.PNG");
		Image nebunN = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\nebunN.PNG");
		Image calA = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\calA.PNG");
		Image calN = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\calN.PNG");
		Image reginaA = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\reginaA.PNG");
		Image reginaN = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\reginaN.PNG");
		Image regeA = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\regeA.PNG");
		Image regeN = Image.FromFile(Directory.GetCurrentDirectory() + @"\Resources\regeN.PNG");
		PictureBox q = new PictureBox();
		PictureBox f = new PictureBox();
		PictureBox c = new PictureBox();
		PictureBox d = new PictureBox();
		private void Prom(object sender, EventArgs e)
        {
			PictureBox b = sender as PictureBox;
			string t = (string)b.Tag;
			if(t == "regina")
            {
				if (turn == -1)
					M[II, JJ].box.BackgroundImage = reginaA;
				else M[II, JJ].box.BackgroundImage = reginaN;
				M[II, JJ].state = "regina";
				M[II, JJ].color = -turn;
			}

			if (t == "cal")
			{
				if (turn == -1)
					M[II, JJ].box.BackgroundImage = calA;
				else M[II, JJ].box.BackgroundImage = calN;
				M[II, JJ].state = "cal";
				M[II, JJ].color = -turn;
			}

			if (t == "tura")
			{
				if (turn == -1)
					M[II, JJ].box.BackgroundImage = turaN;
				else M[II, JJ].box.BackgroundImage = turaA;
				M[II, JJ].state = "tura";
				M[II, JJ].color = -turn;
			}

			if (t == "nebun")
			{
				if (turn == -1)
					M[II, JJ].box.BackgroundImage = nebunA;
				else M[II, JJ].box.BackgroundImage = nebunN;
				M[II, JJ].state = "nebun";
				M[II, JJ].color = -turn;
			}
			this.Controls.Remove(q);
			this.Controls.Remove(f);
			this.Controls.Remove(c);
			this.Controls.Remove(d);
		}
		void Promovare(int culoare)
        {
			
			q.Location = new Point(1002, 260);
			q.Size = new Size(tile_size, tile_size);
			q.BackgroundImageLayout = ImageLayout.Stretch;
			q.BackColor = Color.NavajoWhite;
			q.Click += Prom;
			q.Tag = "regina";
			if (turn == 1) q.BackgroundImage = reginaA;
			else q.BackgroundImage = reginaN;
			this.Controls.Add(q);

			f.Location = new Point(1002, 382);
			f.Size = new Size(tile_size, tile_size);
			f.BackgroundImageLayout = ImageLayout.Stretch;
			f.BackColor = Color.NavajoWhite;
			f.Tag = "cal";
			if (turn == 1) f.BackgroundImage = calA;
			else f.BackgroundImage = calN;
			f.Click += Prom;
			this.Controls.Add(f);

			c.Location = new Point(1002, 504);
			c.Size = new Size(tile_size, tile_size);
			c.BackgroundImageLayout = ImageLayout.Stretch;
			c.BackColor = Color.NavajoWhite;
			c.Tag = "tura";
			if (turn == 1)  c.BackgroundImage = turaN;
			else c.BackgroundImage = turaA;
			c.Click += Prom;
			this.Controls.Add(c);

			d.Location = new Point(1002, 626);
			d.Size = new Size(tile_size, tile_size);
			d.BackgroundImageLayout = ImageLayout.Stretch;
			d.BackColor = Color.NavajoWhite;
			d.Tag = "nebun";
			if (turn == 1) d.BackgroundImage = nebunA;
			else d.BackgroundImage = nebunN;
			d.Click += Prom;
			this.Controls.Add(d);
		}

		void Tabla_Joc()
        {
			this.Size = new Size(1400, 1080);
			//this.BackColor
			Panel border1 = new Panel();
			Panel border2 = new Panel();
			Panel border3 = new Panel();
			Panel border4 = new Panel();

			border1.Size = new Size(space, tile_size * 8 + space);
			border2.Size = new Size(space, tile_size * 8 + space * 2);
			border3.Size = new Size(tile_size * 8 + space, space);
			border4.Size = new Size(tile_size * 8 + space * 2, space);

			border1.Location = new Point(0, 0);
			border2.Location = new Point(tile_size * 8 + space, 0);
			border3.Location = new Point(0, 0);
			border4.Location = new Point(0, tile_size * 8 + space);

			border1.BackColor = Color.Black;
			border2.BackColor = Color.Black;
			border3.BackColor = Color.Black;
			border4.BackColor = Color.Black;

			this.Controls.Add(border1);
			this.Controls.Add(border2);
			this.Controls.Add(border3);
			this.Controls.Add(border4);

			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
				{
					PictureBox b = new PictureBox();
					b.Location = new Point(j * tile_size + space, i * tile_size + space);
					b.Size = new Size(tile_size, tile_size);
					b.BackgroundImageLayout = ImageLayout.Stretch;
			
					this.Controls.Add(b);
					
					int x = i * 10 + j;
					b.Click += pictureBox1_Click;
					b.Tag = x;
					M[i, j] = new Patrat();
					M[i, j].box = b;
					//M[i, j].x = i;
					if (i % 2 == 0)
					{
						if (j % 2 == 0)
						{
							b.BackColor = Color.SandyBrown;
						}
						else
						{
							//b.BackgroundImage = white;
							b.BackColor = Color.SaddleBrown;
						}
					}
					else
					{
						if (j % 2 == 1)
						{
							//b.BackgroundImage = brown;
							b.BackColor = Color.SandyBrown;
						}
						else
						{
							//b.BackgroundImage = white;
							b.BackColor = Color.SaddleBrown;
						}
					}
				}

		}

		void Pioni()
        {
			for (int j = 0; j < 8; j++)
			{
				M[1, j].box.BackgroundImage = negru;
				M[1, j].state = "pion";
				M[1, j].color = -1;
			}

			for (int j = 0; j < 8; j++)
			{
				M[6, j].box.BackgroundImage = alb;
				M[6, j].state = "pion";
				M[6, j].color = 1;
			}
		}

		void Ture()
        {
			
			M[7, 0].box.BackgroundImage = turaN;
			M[7, 0].state = "tura";
			M[7, 0].color = 1;
			
			M[7, 7].box.BackgroundImage = turaN;
			M[7, 7].state = "tura";
			M[7, 7].color = 1;
			
			M[0, 0].box.BackgroundImage = turaA;
			M[0, 0].state = "tura";
			M[0, 0].color = -1;
				
			M[0, 7].box.BackgroundImage = turaA;
			M[0, 7].state = "tura";
			M[0, 7].color = -1;
		}

		void Nebuni()
        {
			M[0, 2].box.BackgroundImage = nebunN;
			M[0, 2].state = "nebun";
			M[0, 2].color = -1;

			M[0, 5].box.BackgroundImage = nebunN;
			M[0, 5].state = "nebun";
			M[0, 5].color = -1;

			M[7, 2].box.BackgroundImage = nebunA;
			M[7, 2].state = "nebun";
			M[7, 2].color = 1;

			M[7, 5].box.BackgroundImage = nebunA;
			M[7, 5].state = "nebun";
			M[7, 5].color = 1;
			
		}

		void Caluti()
        {
			M[0, 1].box.BackgroundImage = calN;
			M[0, 1].state = "cal";
			M[0, 1].color = -1;

			M[0, 6].box.BackgroundImage = calN;
			M[0, 6].state = "cal";
			M[0, 6].color = -1;

			M[7, 1].box.BackgroundImage = calA;
			M[7, 1].state = "cal";
			M[7, 1].color = 1;

			M[7, 6].box.BackgroundImage = calA;
			M[7, 6].state = "cal";
			M[7, 6].color = 1;
		}

		void Regine()
        {
			M[0, 3].box.BackgroundImage = reginaN;
			M[0, 3].state = "regina";
			M[0, 3].color = -1;

			M[7, 3].box.BackgroundImage = reginaA;
			M[7, 3].state = "regina";
			M[7, 3].color = 1;

		}

		void Regi()
        {
			M[0, 4].box.BackgroundImage = regeN;
			M[0, 4].state = "rege";
			M[0, 4].color = -1;
			
			

			M[7, 4].box.BackgroundImage = regeA;
			M[7, 4].state = "rege";
			M[7, 4].color = 1;
			
		}
		private void Form1_Load_1(object sender, EventArgs e)
		{
			this.Controls.Clear();
			turn = 1;
			Tabla_Joc();
			Pioni();
			Ture();
			Nebuni();
			Caluti();
			Regine();
			Regi();

			Button b = new Button();
			b.Size = new Size(150, 60);
			b.Click += Form1_Load_1;
			b.BackColor = Color.Gray;
			b.Text = "New Game";
			b.Location = new Point(1220, 930);
			this.Controls.Add(b);
		}
	}
}
