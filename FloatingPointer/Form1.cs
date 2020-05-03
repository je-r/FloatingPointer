using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FloatingPointer
{
    public partial class FloatingPointer : Form
    {
        public FloatingPointer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 140, 0);
            //Color.Red;
            this.TopMost = true;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            GraphicsPath path = new GraphicsPath();
            //path.AddClosedCurve() bad
            //good: path.AddEllipse(0, 0, this.Width, this.Height);
            GraphicsPath myPath = new GraphicsPath();
            myPath.StartFigure();
            //myPath.SetMarkers();
            if (arrowIsVertical)
            {
                if (arrowIsUp)
                { // up
                    myPath.AddLine(0, this.Height / 2, this.Width / 2, 0);
                    myPath.AddLine(this.Width / 2, 0, this.Width, this.Height / 2);
                    myPath.AddLine(this.Width, this.Height / 2, this.Width, this.Height);
                    myPath.AddLine(this.Width, this.Height, 0, this.Height);
                    myPath.AddLine(0, this.Height, 0, this.Height / 2);
                }
                else
               { // down
                    myPath.AddLine(0, 0, this.Width , 0);
                    myPath.AddLine( this.Width, 0, this.Width, this.Height/2);
                    myPath.AddLine( this.Width, this.Height / 2, this.Width/2, this.Height);
                    myPath.AddLine( this.Width / 2, this.Height, 0, this.Height/2);
                    myPath.AddLine( 0, this.Height / 2, 0,0);
                }
            } else 
            {

                if (arrowIsLeft)
                {
                    myPath.AddLine(new Point(10, this.Height / 2), new Point(this.Width / 3, 0));
                    myPath.AddLine(new Point(this.Width / 3, this.Height / 3), new Point(this.Width, this.Height / 3));
                    myPath.AddLine(new Point(this.Width, this.Height / 3), new Point(this.Width, 2 * (this.Height / 3)));
                    myPath.AddLine(new Point(this.Width, 2 * (this.Height / 3)), new Point(this.Width / 3, 2 * (this.Height / 3)));
                    myPath.AddLine(new Point(this.Width / 3, 2 * (this.Height / 3)), new Point(this.Width / 3, this.Height));
                }
                else
                {
                    //button1.Location.X = button1.Location.X
                    myPath.AddLine(new Point(10, this.Height / 3), new Point(2 * (this.Width / 3), this.Height / 3));
                    myPath.AddLine(new Point(2 * (this.Width / 3), this.Height / 3), new Point(2 * (this.Width / 3), 0));
                    myPath.AddLine(new Point(2 * (this.Width / 3), 0), new Point(this.Width, this.Height / 2));
                    myPath.AddLine(new Point(this.Width, this.Height / 2), new Point(2 * (this.Width / 3), this.Height));

                    myPath.AddLine(new Point(2 * (this.Width / 3), this.Height), new Point(2 * (this.Width / 3), 2 * (this.Height / 3)));

                    myPath.AddLine(new Point(2 * (this.Width / 3), 2 * (this.Height / 3)), new Point(10, 2 * (this.Height / 3)));
                }
            }
            
            myPath.CloseFigure();
            Region region = new Region(myPath);
   
            this.Region = region;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void FloatingPointer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private bool arrowIsLeft = true;
        private bool arrowIsUp = true;
        private bool arrowIsVertical = false;

        private void button2_Click(object sender, EventArgs e)
        {
            arrowIsVertical = false;
            arrowIsLeft = ! arrowIsLeft;
            this.Form1_Load(sender, e);
        }

        private Random rnd = new Random();

        private void button3_Click(object sender, EventArgs e)
        {
            //Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            //this.BackColor = randomColor;
                 // Show the color dialog.
            ColorDialog colorDialog1 = new ColorDialog();
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                this.BackColor = colorDialog1.Color;
            }
        }

       // private System.Drawing.Color oldButtonColor = Color.Red;

        private void button1_MouseHover(object sender, EventArgs e)
        {
           // oldButtonColor = button1.ForeColor;
            button1.BackColor = this.BackColor;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
           // button1.ForeColor = oldButtonColor;
            button1.UseVisualStyleBackColor = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            arrowIsVertical = true;
            arrowIsUp = !arrowIsUp;
            this.Form1_Load(sender, e);
        }
    }
}

/*
 
    TODO https://www.arclab.com/en/kb/csharp/save-and-restore-position-size-windows-forms-application.html

 */
