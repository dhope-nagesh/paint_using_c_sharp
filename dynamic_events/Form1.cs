using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dynamic_events
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            label_color_indicator.BackColor = Color.Black;
            PictureBox picture_box = new PictureBox();

            picture_box.Location = new Point(100, 100);
            picture_box.BorderStyle = BorderStyle.FixedSingle;
            picture_box.Size = new Size(500, 500);
            this.Controls.Add(btn);
           
            //register events 
            btn.MouseDown += new MouseEventHandler(pictureBox_MouseDown);
            btn.MouseUp += new MouseEventHandler(pictureBox_MouseUp);
            btn.MouseMove += new MouseEventHandler(pictureBox_MouseMove);


        }

        private Point? _Previous = null;
        private Pen _Pen = new Pen(Color.Black, 10);
        
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox tmpPicBox = (PictureBox)sender;
            _Previous = new Point(e.X, e.Y);
           // pictureBox_MouseDown(sender, e);
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox tmpPicBox = (PictureBox)sender;
            if (_Previous != null)
            {
                if (tmpPicBox.Image == null)
                {
                    Bitmap bmp = new Bitmap(tmpPicBox.Width, tmpPicBox.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                    }
                    tmpPicBox.Image = bmp;
                }
                using (Graphics g = Graphics.FromImage(tmpPicBox.Image))
                {
                    g.DrawLine(_Pen, _Previous.Value.X, _Previous.Value.Y, e.X, e.Y);
                }
                tmpPicBox.Invalidate();
                _Previous = new Point(e.X, e.Y);
            }

            
        }
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            _Previous = null;
        }
        private void btn_click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                _Pen = new Pen(colorDialog1.Color, 10);
                label_color_indicator.BackColor = colorDialog1.Color;
            }
        }
    }
}
