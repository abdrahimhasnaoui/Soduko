using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Timers;
namespace Soduku
{
    public partial class Form1 : Form
    {

        Thread thread;
        System.Timers.Timer t;
        int h, m, s;

        Thread th;
        Button[] Rows1 = new Button[9];
        Button[] Rows2 = new Button[9];
        Button[] Rows3 = new Button[9];
        Button[] Rows4 = new Button[9];
        Button[] Rows5 = new Button[9];
        Button[] Rows6 = new Button[9];
        Button[] Rows7 = new Button[9];
        Button[] Rows8 = new Button[9];
        Button[] Rows9 = new Button[9];

        List<Button[]> Rows;


        //Colums

        Button[] cl1 = new Button[9];
        Button[] cl2 = new Button[9];
        Button[] cl3 = new Button[9];
        Button[] cl4 = new Button[9];
        Button[] cl5 = new Button[9];
        Button[] cl6 = new Button[9];
        Button[] cl7 = new Button[9];
        Button[] cl8 = new Button[9];
        Button[] cl9 = new Button[9];

        List<Button[]> Columns;

        //TAbs

        ArrayList tab1 = new ArrayList();
        ArrayList tab2 = new ArrayList();
        ArrayList tab3 = new ArrayList();
        ArrayList tab4 = new ArrayList();
        ArrayList tab5 = new ArrayList();
        ArrayList tab6 = new ArrayList();
        ArrayList tab7 = new ArrayList();
        ArrayList tab8 = new ArrayList();
        ArrayList tab9 = new ArrayList();

        List<ArrayList> Tabs;

        List<Button> Vide=new List<Button>();

        int ButtonIndix = 0;
        int limit;


        
        public Form1()
        {
            

            InitializeComponent();
            foreach (Control btn in panel1.Controls)
            {
                if(btn is Button)
                {
                    ((Button)btn).Click += new System.EventHandler(this.Saisir);
                }
            }

            ButtonsVide();
            limit = Vide.Count;
            panel1.Select();

            //

            Rows = new List<Button[]> {Rows1,Rows2, Rows3,Rows4, Rows5, Rows6 ,Rows7, Rows8, Rows9};
            Columns = new List<Button[]> { cl1, cl2, cl3, cl4, cl5, cl6, cl7, cl8, cl9 };
            Tabs = new List<ArrayList> { tab1, tab2, tab3, tab4, tab5, tab6, tab7, tab8, tab9 };

            foreach (Control b in panel1.Controls)
            {
                if(b is Button)
                {
                    Button b2 = (Button)b;

                    int Tag = int.Parse((string)(b2.Tag))-1;
                    int indixRows =( b2.TabIndex-1)/9;
                    int indixColums = (b2.TabIndex - 1) % 9;

                    Rows[indixRows][indixColums] = b2;
                    Columns[indixColums][indixRows] = b2;

                    Tabs[Tag].Add(b2);
                }
            }

            
        }

        private void Saisir(object sender, EventArgs e)
        {

            Button b = (Button)sender;
            Remplir r = new Remplir(b);
            r.ShowDialog();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Submit();
            Stopwatch sw = Stopwatch.StartNew();

            InsertValue();

            sw.Stop();
            TimeSpan time = sw.Elapsed;
            
            label1.Text = String.Format("{0:00}:{1:00}:{2:00}",
                time.Minutes, time.Seconds, time.Milliseconds / 10
            );

        }

        void Submit()
        {
            foreach (Control btn in panel1.Controls)
            {
                if (btn is Button)
                {
                    if (((Button)btn).Text == "")
                    {
                        ((Button)btn).ForeColor = Color.Red;
                        ((Button)btn).FlatAppearance.BorderColor = Color.Black;
                    }
                }
            }


            ButtonsVide();
            limit = Vide.Count;
        }
        //my Fonction
        //
      
        //Vide
        void ButtonsVide()
        {
            Vide.Clear();
            foreach (Control b in panel1.Controls)
            {
                if (b is Button)
                {
                    Button b2 = (Button)b;

                    if (b2.Text == "")
                        Vide.Add(b2);
                }
            }
        }

        //
        //Get vAlues
        List<int> GetVAluesTabs(int indix)
        {
            List<int> numbres = new List<int>();
            List<int> numbres2 = new List<int>();

            foreach (Button b in Tabs[indix])
            {
                if(b.Text!="")
                {
                    numbres.Add(int.Parse(b.Text));
                }
            }
            for (int i = 1; i<=9; i++)
            {
                if (!numbres.Contains(i))
                    numbres2.Add(i);
            }

            return numbres2;
        }

        //get VAlues Rows

        List<int> GetValuesRows(int indix)
        {
            List<int> numbres = new List<int>();
            List<int> numbres2 = new List<int>();

            foreach (Button b in Rows[indix])
            {
                if (b.Text != "")
                {
                    numbres.Add(int.Parse(b.Text));
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                if (!numbres.Contains(i))
                    numbres2.Add(i);
            }

            
            return numbres2;
        }


        //get VAlues Rows

        List<int> GetValuesColumns(int indix)
        {
            List<int> numbres = new List<int>();
            List<int> numbres2 = new List<int>();

            foreach (Button b in Columns[indix])
            {
                if (b.Text != "")
                {
                    numbres.Add(int.Parse(b.Text));
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                if (!numbres.Contains(i))
                    numbres2.Add(i);
            }


            return numbres2;
        }

        //get Values Total
        List<int> GetValuesFinal(int indixTab,int indixRows,int indixColumns)
        {
            List<int> numbres = new List<int>();

            for (int i = 1; i <=9; i++)
            {
                if (GetVAluesTabs(indixTab).Contains(i) && GetValuesRows(indixRows).Contains(i) && GetValuesColumns(indixColumns).Contains(i))
                {
                    numbres.Add(i);
                }
            }

            return numbres;
        }

        //Stop

        bool stop()
        {
            foreach (Control c in panel1.Controls)
            {
                if(c is Button)
                {
                    if (((Button)c).Text == "")
                        return false;
                }
            }
            return true;
        }
        //Insert Values
        void InsertValue()
        {
            if (ButtonIndix<limit)
            {
                Button b = Vide[ButtonIndix];
                int Tag1 = int.Parse((string)(b.Tag)) - 1;
                int indixRows1 = (b.TabIndex - 1) / 9;
                int indixColums1 = (b.TabIndex - 1) % 9;
                List<int> numbres = GetValuesFinal(Tag1, indixRows1, indixColums1);

                Vide[ButtonIndix].BackColor = Color.Yellow;

                if (numbres.Count == 0)
                {
                    Vide[--ButtonIndix].Text = "";


                    return;
                }
                foreach (int item in numbres)
                {

                    b.Text = item.ToString();

                    ButtonIndix++;
                    InsertValue();

                    if (stop())
                    {

                        break;
                    }

                }
                if (!stop())
                {
                    if (ButtonIndix >= 0 && ButtonIndix < limit)
                    {
                        Vide[ButtonIndix].Text = "";
                        ButtonIndix--;
                    }
                    else
                    {
                        MessageBox.Show("limit:" + limit + "   ButtonIn:" + ButtonIndix);
                    }
                }


            }
           

        }

        private void button82_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls)
            {
                if(item is Button)
                {
                    ((Button)item).Text = "";
                    ((Button)item).BackColor = Color.White;
                    ((Button)item).ForeColor = Color.Black;
                    ((Button)item).FlatAppearance.BorderColor = Color.Black;
                }
            }
            ButtonIndix = 0;
        }


        private void button84_Click(object sender, EventArgs e)
        {
            Vide[ButtonIndix++].BackColor = Color.Green;
        }


        private void button84_Click_1(object sender, EventArgs e)
        {
           
            foreach (Control item in panel1.Controls)
            {
                if (item is Button)
                {
                    if(((Button)item).BackColor == Color.Yellow)
                    {
                        ((Button)item).Text = "";
                        ((Button)item).BackColor = Color.White;
                    }
                }
            }
            ButtonIndix = 0;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        
    }
}
