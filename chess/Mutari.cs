using System.Windows.Forms;

namespace chess
{
    public partial class Form1 : Form
    {
        int[] dx = { 0, 0, 1, -1, 1, 1, -1, -1 };
        int[] dy = { 1, -1, 0, 0, 1, -1, -1, 1 };

        int[] x_cal = { 2, 2, -2, -2, 1, 1, -1, -1 };
        int[] y_cal = { 1, -1, 1, -1, 2, -2, 2, -2 };

        bool Checkmate()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (M[i, j].color == turn)
                    {
                        if (M[i, j].state == "pion")
                        { Mutare_Pion(i, j); }

                        else if (M[i, j].state == "tura")
                        { Mutare_Tura(i, j); }

                        else if (M[i, j].state == "nebun")
                        { Mutare_Nebun(i, j); }

                        else if (M[i, j].state == "cal")
                        { Mutare_Cal(i, j);  }

                        else if (M[i, j].state == "regina")
                        { Mutare_Regina(i, j);  }

                        else if (M[i, j].state == "rege")
                        { Mutare_Rege(i, j);  }

                        }
                }
            bool verif = true;
            for (int n = 0; n < 8; n++)
                for (int m = 0; m < 8; m++)
                {
                    if (M[n, m].state == "path")
                    {
                        // MessageBox.Show("path");
                        M[n, m].state = "empty";
                        verif = false;
                    }
                    else if (M[n, m].state != "empty" && M[n, m].Take.Count != 0 && M[n,m].color == turn ) { verif = false; }
                        if (M[n, m].box.BackgroundImage == path)
                        M[n, m].box.BackgroundImage = null;
                }
            return verif;
        }

        bool Verificare_Path(int i, int j, int x, int y)
        {
            bool v = false;
            if (Inside(x, y) && M[i, j].state == "rege")
            {
                if (M[x, y].state == "empty")
                {
                    if (Inside(x - 1, y - 1) && M[x - 1, y - 1].state == "pion" && M[x - 1, y - 1].color != turn) return false;
                    if (Inside(x - 1, y + 1) && M[x - 1, y + 1].state == "pion" && M[x - 1, y + 1].color != turn) return false;
                    M[x, y].state = M[i, j].state;
                    M[x, y].color = M[i, j].color;
                    M[i, j].state = "empty";    
                
                        v = CheckForCheck(x, y, turn);
                    M[i, j].state = M[x, y].state;
                    M[x, y].state = "empty";

                    if (v == true) return false;
                    return true;
                }
                else
                {
                   
                    M[i, j].state = "empty";
                    v = CheckForCheck(x, y, turn);
                    M[i, j].state = "rege";
                    if (v == true) return false;
                    return true;
                }
            }
            if (Inside(x, y) && M[x, y].state != "empty" && M[i, j].state == "pion")
            {
                string t;
                M[i, j].state = "empty";
                t = M[x, y].state;
                M[x, y].state = "pion";
                if (turn == 1)
                    v = CheckForCheck(x_white, y_white, turn);
                else if (turn == -1)
                    v = CheckForCheck(x_black, y_black, turn);

                M[i, j].state = "pion";
                M[x, y].state = t;
                if (v == true) return false;
                return true;
            }
                if (Inside(x, y) && M[x, y].state == "empty")
            {
              
                M[x, y].state = M[i, j].state;
                M[x, y].color = M[i, j].color;
                M[i, j].state = "empty";

               
                if (turn == 1)
                    v = CheckForCheck(x_white, y_white, turn);
                else if (turn == -1)
                    v = CheckForCheck(x_black, y_black, turn);
                M[i, j].state = M[x, y].state;
                M[x, y].state = "empty";
               

            }
            if (v == true) return false;
            return true;
        }

        void Mutare_Pion(int i, int j)
        {
            if (Inside(i - turn, j) == true && M[i - turn, j].state == "empty" && Verificare_Path(i, j, i - turn, j))

            {
                M[i - turn, j].box.BackgroundImage = path;
                M[i - turn, j].state = "path";
                if (M[i,j].pion.FirstMove == 1 && M[i - turn * 2, j].state == "empty")
                {
                    M[i - turn * 2, j].box.BackgroundImage = path;
                    M[i - turn * 2, j].state = "path";
                }
            }
            if (Inside(i - turn, j - 1) == true && Verificare_Path(i, j, i - turn, j - 1) && M[i - turn, j - 1].state != "empty" && M[i - turn, j - 1].state != "EnPassant" && M[i,j].color != M[i - turn, j - 1].color ) M[i,j].Take.Add(M[i - turn, j - 1]);
            if (Inside(i - turn, j + 1) == true && Verificare_Path(i, j, i - turn, j - 1) && M[i - turn, j + 1].state != "empty" && M[i - turn, j + 1].state != "EnPassant" && M[i,j].color != M[i - turn, j + 1].color ) M[i,j].Take.Add(M[i - turn, j + 1]);

        }

        void Mutare_Tura(int i, int j)
        {
            //M[0, 0].box.BackColor = Color.White;
            int k, x, y ,l ;
           
                if (turn == 1)
                { 
                    k = x_white;
                    l = y_white;
                }
                else 
                { 
                    k = x_black; 
                    l = y_black;
                }
            
            for (int d = 0; d < 4; d++)
            {
                x = i + dx[d];
                y = j + dy[d];
                if (check == false)
                {
                    if (Inside(x, y) && Verificare_Path(i, j, x, y))
                    {
                        for (; Inside(x, y) && M[x, y].state == "empty"; x += dx[d], y += dy[d]) ////////// jos
                        {
                            M[x, y].box.BackgroundImage = path;
                            M[x, y].state = "path";
                        }
                        if (Inside(x, y) && M[x, y].state != "rege" && M[i,j].color != M[x, y].color) M[i,j].Take.Add(M[x, y]);
                    }
                }
                else if(check == true)
                {
                    Patrat store = new Patrat();
                    store.state = M[i, j].state;
                    store.color = M[i, j].color;

                    M[i, j].state = "empty";
                    bool exit = false;
                    for (; Inside(x, y) && M[x, y].state == "empty" && exit == false; x += dx[d], y += dy[d])
                    {
                        M[x, y].state = store.state;
                        M[x, y].color = store.color;
                        if (CheckForCheck(k, l, turn) == false) exit = true;
                        M[x, y].state = "empty";
                    }
                    if (Inside(x, y) && M[x, y].state != "empty" && M[x, y].state != "rege" && M[i,j].color != M[x, y].color)
                    {
                   
                        M[x, y].color = M[x, y].color * -1;
                        if (CheckForCheck(k, l, turn) == false) exit = true;
                        M[x, y].color = M[x, y].color * -1;

                        if(exit == true) M[i,j].Take.Add(M[x, y]);
                    }
                    x = i + dx[d];
                    y = j + dy[d];
                    if (exit == true)
                        for (; Inside(x, y) && M[x, y].state == "empty"; x += dx[d], y += dy[d]) ////////// jos
                        {
                            M[x, y].box.BackgroundImage = path;
                            M[x, y].state = "path";
                        }
                    
                    M[i, j].state = store.state;
                    M[i, j].color = store.color;
                }
            }
        }

        void Mutare_Nebun(int i, int j)
        {
            int k, l, x, y;
            if (turn == 1)
            {
                k = x_white;
                l = y_white;
            }
            else
            {
                k = x_black;
                l = y_black;
            }
            for (int d = 4; d < 8; d++)
            {
                x = i + dx[d];
                y = j + dy[d];
                if (check == false)
                {
                    if (Inside(x, y) && Verificare_Path(i, j, x, y))
                    {
                        for (; Inside(x, y) && M[x, y].state == "empty"; x += dx[d], y += dy[d]) ////////// jos
                        {
                            M[x, y].box.BackgroundImage = path;
                            M[x, y].state = "path";
                        }
                        if (Inside(x, y) && M[x, y].state != "rege" && M[i,j].color != M[x, y].color) M[i,j].Take.Add(M[x, y]);
                    }
                }
                else if (check == true)
                {
                    Patrat store = new Patrat();
                    store.state = M[i, j].state;
                    store.color = M[i, j].color;

                    M[i, j].state = "empty";
                    bool exit = false;
                    for (; Inside(x, y) && M[x, y].state == "empty" && exit == false; x += dx[d], y += dy[d])
                    {
                        M[x, y].state = store.state;
                        M[x, y].color = store.color;
                        if (CheckForCheck(k, l, turn) == false) exit = true;
                        M[x, y].state = "empty";
                    }
                    if (Inside(x, y) && M[x, y].state != "rege" && M[i,j].color != M[x, y].color)
                    {
                        M[x, y].color = M[x, y].color * -1;
                        if (CheckForCheck(k, l, turn) == false) exit = true;
                        M[x, y].color = M[x, y].color * -1;

                        M[i,j].Take.Add(M[x, y]);
                    }
                    x = i + dx[d];
                    y = j + dy[d];
                    if (exit == true)
                        for (; Inside(x, y) && M[x, y].state == "empty"; x += dx[d], y += dy[d]) ////////// jos
                        {
                            M[x, y].box.BackgroundImage = path;
                            M[x, y].state = "path";
                        }

                    M[i, j].state = store.state;
                    M[i, j].color = store.color;
                }
            }
        }

        void Mutare_Cal(int i, int j)
        {
            int x, y;
            for (int d = 0; d < 8; d++)
            {
                x = i + x_cal[d];
                y = j + y_cal[d];
                if (Inside(x, y) && Verificare_Path(i, j, x, y) && M[x, y].state == "empty")
                {
                    M[x, y].box.BackgroundImage = path;
                    M[x, y].state = "path";
                }
                if (Inside(x, y) && M[x, y].state != "rege" && M[i, j].color != M[x, y].color)
                {
                    bool v;
                    string t = M[x, y].state;
                    M[i, j].state = "empty";
                    M[x, y].state = "cal";
                    if (turn == 1)
                        v = CheckForCheck(x_white, y_white, turn);
                    else 
                        v = CheckForCheck(x_black, y_black, turn);
                    M[i, j].state = "cal";
                    M[x, y].state = t;
                    if (v == false) 
                    M[i, j].Take.Add(M[x, y]);
                }
            }

        }

        void Mutare_Regina(int i, int j)
        {
            Mutare_Tura(i, j);
            Mutare_Nebun(i, j);
        }

        void Mutare_Rege(int i, int j)
        {
            int x, y;
            for (int k = 0; k < 8; k++)
            {
                x = i + dx[k];
                y = j + dy[k];
                if (Inside(x, y) && M[x, y].state == "empty" && Verificare_Path(i, j, x, y)) 
                {
                    M[x, y].box.BackgroundImage = path;
                    M[x, y].state = "path";
                }
                if (Inside(x, y) && M[x, y].state != "rege" && M[i,j].color != M[x, y].color && Verificare_Path(i, j, x, y)) M[i,j].Take.Add(M[x, y]);
            }
        }

        bool CheckForCheck(int i, int j, int color)
        {
            int k, x, y;

            ////////////////////         TURA              /////////////////////////
            for (int d = 0; d < 4; d++)
            {
                x = i + dx[d];
                y = j + dy[d];
                for (; Inside(x, y) && (M[x, y].state == "empty" || M[x, y].state == "path"); x += dx[d], y += dy[d]) ;
                if (Inside(x, y) && (M[x, y].state == "tura" || M[x, y].state == "regina") && M[x, y].color != color) return true;
            }

            /////////////////          NEBUN             ///////////////////////////
            for (int d = 4; d < 8; d++)
            {
                x = i + dx[d];
                y = j + dy[d];
                for (; Inside(x, y) && (M[x, y].state == "empty" || M[x, y].state == "path"); x += dx[d], y += dy[d]) ;
                if (Inside(x, y) && (M[x, y].state == "nebun" || M[x, y].state == "regina") && M[x, y].color != color) return true;
            }

            /////////////////          CAL             //////////////////////////////
            for (int d = 0; d < 8; d++)
            {
                x = i + x_cal[d];
                y = j + y_cal[d];
                if (Inside(x, y) && M[x, y].state == "cal" && M[x, y].color != color) return true;
            }


            /////////////          PION                   //////////////////////////
            if (Inside(i - turn, j - 1) && M[i - turn, j - 1].state == "pion" && M[i - turn, j - 1].color != color) return true;
            if (Inside(i - turn, j + 1) && M[i - turn, j + 1].state == "pion" && M[i - turn, j + 1].color != color) return true;


            ////////////              REGE                /////////////////////////
            for (int d = 0; d < 8; d++)
            {
                x = i + dx[d];
                y = j + dy[d];
                if (Inside(x,y) && M[x, y].state == "rege" && M[x, y].color != color) return true;
            }
                return false;
        }
    }
}
