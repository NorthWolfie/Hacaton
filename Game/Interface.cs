using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    //public class MyIm
    //{
    //    public PictureBox pic = new PictureBox();
    //    public int value;
    //    public MyIm(PictureBox pic,int value)
    //    {
    //        this.value = value;
    //        this.pic = pic;
    //    }
    //}

    public partial class Interface : Form
    {
        PictureBox pers1 = new PictureBox();
        PictureBox pers2 = new PictureBox();
        public static bool turn = true;
        public static int y = 460;
        Label count_1 = new Label();
        Label count_2 = new Label();
        Bitmap bubble = new Bitmap("bubble.png");
        public Interface()
        {
            InitializeComponent();
            #region создание поля-фона
            Width = 416;
            Height = 840;
            MaximumSize = new Size(416,840);
            MinimumSize = MaximumSize;
            BackgroundImage = Image.FromFile("поле.png");
            Interface_Load();
            #endregion
        }

        private void Interface_Load()
        {
            #region счётчик для первого игрока
            Label player1 = new Label();
            player1.Text = "I игрок:";
            player1.Width = 100;
            player1.Height = 50;
            player1.BackColor = Color.Transparent;
            player1.Font = new Font("Times New Roman", 18, FontStyle.Bold);
            player1.ForeColor = Color.Blue;
            Controls.Add(player1);
            count_1.Location = new Point(120, 0);
            count_1.Text = "0";
            count_1.Width = 70;
            count_1.Height = 50;
            count_1.BackColor = Color.Transparent;
            count_1.Font = new Font("Times New Roman", 30, FontStyle.Bold);
            count_1.ForeColor = Color.Blue;
            Controls.Add(count_1);
            #endregion

            #region счётчик для второго игрока
            Label player2 = new Label();
            player2.Text = "II игрок:";
            player2.Location = new Point(210, 0);
            player2.Width = 110;
            player2.Height = 50;
            player2.BackColor = Color.Transparent;
            player2.Font = new Font("Times New Roman", 18, FontStyle.Bold);
            player2.ForeColor = Color.Red;
            Controls.Add(player2);
            count_2.Location = new Point(330, 0);
            count_2.Text = "0";
            count_2.Width = 70;
            count_2.Height = 50;
            count_2.BackColor = Color.Transparent;
            count_2.Font = new Font("Times New Roman", 30, FontStyle.Bold);
            count_2.ForeColor = Color.Red;
            Controls.Add(count_2);
            #endregion

            Random rnd = new Random();
            int bubble_count = 3;
            #region создание поля
            int PosY = 140;
            for (int i = 0; i < 6; i++)
            {
                int PosX = 0;
                if (i > 0)
                    PosY += 80;

                for(int j = 0; j < 5; j++)
                {
                    int choose = rnd.Next(0, 3);
                    PictureBox pic = new PictureBox();
                    pic.Name = (i.ToString() + j.ToString());
                    pic.Width = 80;
                    pic.Height = 80;
                    pic.BackColor = Color.Transparent;
                    pic.Location = new Point(PosX, PosY);
                    PosX += 80;
                    pic.Click += Action_Click;
                    if (i != 5)
                    {
                        pic.Enabled = false;
                    }

                    if (choose == 0 && bubble_count != 0)
                    {
                        pic.Image = Image.FromFile("bubble.png");
                        bubble_count--;
                    }
                    else
                    {
                        choose = rnd.Next(1, 3);
                    }
                    if (choose == 1)
                    {
                        pic.Image = Image.FromFile("+1.png");
                    }
                    if(choose == 2)
                    {
                        pic.Image = Image.FromFile("+2.png");
                    }
                    //MyIm myIm = new MyIm(pic,value);
                    Controls.Add(pic);
                }
            }
            #endregion

            #region создание игроков
            pers1.Name = "pers1";
            pers1.Width = 80;
            pers1.Height = 80;
            pers1.Image = Image.FromFile("player1.png");
            pers1.Location = new Point(80, 700);
            pers1.BackColor = Color.Transparent;
            Controls.Add(pers1);

            pers2.Name = "pers2";
            pers2.Width = pers1.Width;
            pers2.Height = pers1.Height;
            pers2.Image = Image.FromFile("player2.png");
            pers2.Location = new Point(240, 700);
            pers2.BackColor = Color.Transparent;
            Controls.Add(pers2);
            #endregion
        }

        private void Action_Click(object sender, EventArgs e)
        {
            if(sender is PictureBox &&(sender != pers1 || sender != pers2))
            {
                Bitmap image = new Bitmap((sender as PictureBox).Image);
                Bitmap Plus1 = new Bitmap("+1.png");
                Bitmap Plus2 = new Bitmap("+2.png");
                if (turn)
                {
                    #region движение первого игрока
                    if (((sender as PictureBox).Location.X == pers1.Location.X + 80 || (sender as PictureBox).Location.X == pers1.Location.X - 80 || (sender as PictureBox).Location.X == pers1.Location.X) && (sender as PictureBox).Location.Y < pers1.Location.Y)
                    {
                        if(image.GetPixel(50,50) == Plus1.GetPixel(50,50))
                        {
                            count_1.Text = (int.Parse(count_1.Text) + 1).ToString();
                        }
                        if(image.GetPixel(50, 50) == Plus2.GetPixel(50, 50))
                        {
                            count_1.Text = (int.Parse(count_1.Text) + 2).ToString();
                        }
                        pers1.Location = (sender as PictureBox).Location;
                        Controls.Remove((sender as Control));
                        turn = false;
                    }
                    #endregion
                }
                else
                {
                    #region движение второго игрока
                    if (((sender as PictureBox).Location.X == pers2.Location.X + 80 || (sender as PictureBox).Location.X == pers2.Location.X - 80 || (sender as PictureBox).Location.X == pers2.Location.X) && (sender as PictureBox).Location.Y < pers2.Location.Y) 
                    {
                        if (image.GetPixel(50, 50) == Plus1.GetPixel(50, 50))
                        {
                            count_2.Text = (int.Parse(count_2.Text) + 1).ToString();
                        }
                        if (image.GetPixel(50, 50) == Plus2.GetPixel(50, 50))
                        {
                            count_2.Text = (int.Parse(count_2.Text) + 2).ToString();
                        }
                        pers2.Location = (sender as PictureBox).Location;
                        Controls.Remove((sender as Control));
                        turn = true;
                        Open(y);
                        y -= 80;
                        if (y == -20)
                            End();
                    }
                    #endregion
                }
            }
        }

        private void Open(int y)
        {
            int x = 0;
            foreach(Control item in Controls)
            {
                if(item is PictureBox && item.Location.X==x && item.Location.Y == y)
                {
                    Bitmap rav = new Bitmap((item as PictureBox).Image);
                    if (rav.GetPixel(50, 50) != bubble.GetPixel(50, 50))
                    {
                        item.Enabled = true;
                        x += 80;
                    }
                    else
                    {
                        x += 80;
                    }
                }
            }
        }

        private void End()
        {
            int result1 = int.Parse(count_1.Text);
            int result2 = int.Parse(count_2.Text);
            if (result1 > result2)
            {
                MessageBox.Show("Сongratulations!", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (result2 > result1)
            {
                MessageBox.Show("You Lose!", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if(result1==result2)
            {
                MessageBox.Show("Draw!", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DialogResult result = MessageBox.Show("Заново?", "Новая игра?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if( result == DialogResult.Yes)
            {
                Controls.Clear();
                Interface_Load();
                y = 460;
            }
            if(result == DialogResult.No)
            {
                MessageBox.Show("Пока!", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }
    }
}
