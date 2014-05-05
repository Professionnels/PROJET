using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Map_editor
{
    public enum element
    {
        blanc = 0,
        mur=1,
        assassin=2,
        gardien=3,
        prisonnier=4,
        heros=5,
        spawn=6,
        munitions=7
    }

    public partial class Form1 : Form
    {
        Elements elements;
        Point TAILLE_CARREAU = new Point (20,20);
        Bitmap tile;
        Bitmap Display;
        element type;
        Bitmap blanc, mur, assassin, spawn, gardien, munitions, heros, prisonnier;
        public Form1()
        {
            InitializeComponent();
            blanc = new Bitmap(@"E:\Visual Studio 2010\Projects\Map editor\Map editor\Resources\Blanc.bmp");
            mur = new Bitmap(@"E:\Visual Studio 2010\Projects\Map editor\Map editor\Resources\mur_prison.bmp");
            assassin = new Bitmap(@"E:\Visual Studio 2010\Projects\Map editor\Map editor\Resources\assassin.bmp");
            gardien = new Bitmap(@"E:\Visual Studio 2010\Projects\Map editor\Map editor\Resources\gardien.bmp");
            munitions = new Bitmap(@"E:\Visual Studio 2010\Projects\Map editor\Map editor\Resources\Munitions.bmp");
            spawn = new Bitmap(@"E:\Visual Studio 2010\Projects\Map editor\Map editor\Resources\Spawn.bmp");
            heros = new Bitmap(@"E:\Visual Studio 2010\Projects\Map editor\Map editor\Resources\heros.bmp");
            prisonnier = new Bitmap(@"E:\Visual Studio 2010\Projects\Map editor\Map editor\Resources\prisonnier.bmp");
            tile = blanc;
            elements = new Elements();
            Display = new Bitmap(Screen.FromControl(this).Bounds.Size.Width, Screen.FromControl(this).Bounds.Size.Height);
            pictureBox1.Image = Display;
            pictureBox2.Image = tile;
        }
        private void PLaceObject(object sender, MouseEventArgs e)
        {
            int x = e.Location.X / TAILLE_CARREAU.X;
            int y = e.Location.Y / TAILLE_CARREAU.Y; 
            Point Location = new Point(x*TAILLE_CARREAU.X,y*TAILLE_CARREAU.Y);
            pictureBox1.CreateGraphics().DrawImage(tile, Location);
            for (int j = 1; j < tile.Width-1; j++)
                for (int i = 1; i < tile.Height-1; i++)
                    Display.SetPixel(Location.X + i, Location.Y + j, tile.GetPixel(i, j));
            elements.Add(x,y, type);
        }

        private void Choose_Click(object sender, EventArgs e)
        {
            File.InitialDirectory = @"E:\Visual Studio 2010\Projects\Map editor\Map editor\Resources\";
            if (File.ShowDialog() == DialogResult.OK)
            {
                tile = new Bitmap(File.FileName);
                pictureBox2.Image = tile;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string name = saveFileDialog1.FileName + ".txt";
                elements.Save(name);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.InitialDirectory = @"E:\Visual Studio 2010\Projects\Map editor\Map editor\Resources\";
            if (File.ShowDialog() == DialogResult.OK)
            {
                elements.load(File.FileName);
                for (int i = 0; i < 40; i++)
                {
                    for (int j = 0; j < 40; j++)
                    {
                        switch (elements.getElements()[j, i])
                        {
                            case element.mur:
                                tile = mur;
                                break;
                            case element.munitions:
                                tile = munitions;
                                break;
                            case element.assassin:
                                tile = assassin;
                                break;
                            case element.gardien:
                                tile = gardien;
                                break;
                            case element.spawn:
                                tile = spawn;
                                break;
                            case element.blanc:
                                tile = blanc;
                                break;
                            case element.heros:
                                tile = heros;
                                break;
                            case element.prisonnier:
                                tile = prisonnier;
                                break;
                        }
                        pictureBox1.CreateGraphics().DrawImage(tile, Location);
                        for (int l = 1; l < tile.Width - 1; l++)
                            for (int k = 1; k < tile.Height - 1; k++)
                                Display.SetPixel(i * TAILLE_CARREAU.X + k, j * TAILLE_CARREAU.Y + l, tile.GetPixel(k, l));
                    }
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size((int)numericUpDown1.Value * TAILLE_CARREAU.X, (int)numericUpDown2.Value * TAILLE_CARREAU.Y);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size((int)numericUpDown1.Value * TAILLE_CARREAU.X, (int)numericUpDown2.Value * TAILLE_CARREAU.Y);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            tile = blanc;
            pictureBox2.Image = tile;
            type = element.blanc;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            tile = spawn;
            pictureBox2.Image = tile;
            type = element.spawn;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            tile = mur;
            pictureBox2.Image = tile;
            type = element.mur;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            tile = gardien;
            pictureBox2.Image = tile;
            type = element.gardien;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            tile = prisonnier;
            pictureBox2.Image = tile;
            type = element.prisonnier;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            tile = assassin;
            pictureBox2.Image = tile;
            type = element.assassin;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            tile = heros;
            pictureBox2.Image = tile;
            type = element.munitions;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            tile = munitions;
            pictureBox2.Image = tile;
            type = element.munitions;
        }
    }
}
