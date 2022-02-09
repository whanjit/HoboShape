// Company: Hobo Group
//  Auther: whanjit@gmail.com
//    Date: April 3, 2013
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HoboShape
{
  public partial class HBCanvas : UserControl
  {
    public HBCanvas()
    {
      InitializeComponent();
      this.MouseWheel += new MouseEventHandler(Inf2DSurf_MouseWheel);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    private double dbOrigin_x = 0;
    private double dbOrigin_y = 0;

    //x is left --> right y is top --> down
    public double logical_mouse_x = 0, logical_mouse_y = 0; // mouse x and y

    int px_mouse_down_x, px_mouse_down_y;   // mousedown x and y
    int px_mouse_move_x, px_mouse_move_y;   // Mouse Move x and y
    public double db_scale_factor = 1;      // scael factor

    private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        Image imgHand = Image.FromFile("./hand.png");
        Cursor hand = new System.Windows.Forms.Cursor(((Bitmap)imgHand).GetHicon());
        Cursor = hand;// Cursors.SizeAll;
        px_mouse_down_x = e.X;
        px_mouse_down_y = e.Y;
      }

      this.pictureBox1.Invalidate();
    }

    private void pictureBox1_MouseHover(object sender, EventArgs e)
    {
      this.Focus();
      this.pictureBox1.Invalidate();
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        dbOrigin_x -= (e.X - px_mouse_down_x) * db_scale_factor;
        dbOrigin_y -= (e.Y - px_mouse_down_y) * db_scale_factor;
        px_mouse_down_x = e.X;
        px_mouse_down_y = e.Y;
      }

      px_mouse_move_x = e.X;
      px_mouse_move_y = e.Y;
      logical_mouse_x = (e.X * db_scale_factor + dbOrigin_x);
      logical_mouse_y = (e.Y * db_scale_factor + dbOrigin_y);
      this.pictureBox1.Invalidate();
    }

    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
        Cursor = Cursors.Arrow;
      this.pictureBox1.Invalidate();
    }
    double db_page_scale = 1;
    DrawingFrame frame = new DrawingFrame(297, 210, 0, GraphicsUnit.Millimeter);
    //DrawingFrame frame = new DrawingFrame(420, 297, 0, GraphicsUnit.Millimeter);
    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
      e.Graphics.PageUnit = GraphicsUnit.Pixel;
      //e.Graphics.PageScale = 1;
      db_page_scale = e.Graphics.PageScale;
      int xmin = pictureBox1.ClientRectangle.X;
      int ymin = pictureBox1.ClientRectangle.Y;
      int xmax = pictureBox1.ClientRectangle.Width;
      int ymax = pictureBox1.ClientRectangle.Height;
      float dwid = xmax - xmin;
      float dhgt = ymax - ymin;
      //Utils.FitRectangles(e.Graphics, (float)dbOrigin_x, (float)(dbOrigin_x+dbWidth), (float)dbOrigin_y, (float)(dbOrigin_y+dbHeight), xmin, xmax, ymin, ymax);
      Utils.MapRectangles(e.Graphics, (float)dbOrigin_x, (float)(dbOrigin_x + (dwid * db_scale_factor)), (float)dbOrigin_y, (float)(dbOrigin_y + (dhgt * db_scale_factor)), xmin, xmax, ymin, ymax);

      /*Pen myPen = new Pen(Color.Green, 0);
      for (int i = 0; i < 119; i++)
      {
          for(int j=0;j<84;j++)
          e.Graphics.DrawEllipse(myPen, i * 25, j * 25, 20, 20);
          //e.Graphics.DrawLine(Pens.Green, (float)(-ox + i * 50), (float)(-oy + i * 50), (float)((-ox + i * 50) + m * 100), (float)((-oy + i * 50)));
      }//*/
      //e.Graphics.DrawString("Hello World! ที่กว้างขึ้น 私は東京に行きます Ho intenzione di tokyo Én megyek, hogy Tokió Estou indo para Tóquio", new Font("Times New Roman", (float)(15), FontStyle.Bold), Brushes.White, new PointF((float)10, (float)50));
      frame.Draw(e.Graphics);//,this.pictureBox1);
      e.Graphics.ResetTransform();
    }

    private void Inf2DSurf_MouseWheel(object sender, MouseEventArgs e)
    {
      if (e.Delta < 0 & db_scale_factor < 1280)
      {
        //Zoom In
        db_scale_factor *= Math.Sqrt(2);
        dbOrigin_x = logical_mouse_x - (px_mouse_move_x * db_scale_factor);
        dbOrigin_y = logical_mouse_y - (px_mouse_move_y * db_scale_factor);
      }

      if (e.Delta > 0 & db_scale_factor > 0.006)
      {
        //Zomm Out
        db_scale_factor /= Math.Sqrt(2);
        dbOrigin_x = logical_mouse_x - (px_mouse_move_x * db_scale_factor);
        dbOrigin_y = logical_mouse_y - (px_mouse_move_y * db_scale_factor);
      }

      this.pictureBox1.Invalidate();
    }

    private void pictureBox1_Resize(object sender, EventArgs e)
    {
      this.pictureBox1.Invalidate();
    }

  }
}