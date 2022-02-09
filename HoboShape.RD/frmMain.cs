// Company: Hobo Group
//  Auther: whanjit@gmail.com
//    Date: April 3, 2013
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

namespace HoboShape
{
  public partial class frmMain : Form
  {
    public frmMain()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.inf2DSurf2.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.inf2DSurf1_pictureBox1_MouseMove);
      CenterToScreen();
      DoubleBuffered = true;
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    private void inf2DSurf1_pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {//picturebox1 must me public
      this.toolStripStatusLabel1.Text = Convert.ToString(Math.Round(this.inf2DSurf2.logical_mouse_x, 3)) + " ; "
  + Convert.ToString(Math.Round(this.inf2DSurf2.logical_mouse_y, 3));
    }

    private DrawingFrame dwg_frame;
    private void btnPrint_Click(object sender, EventArgs e)
    {
      //printFont = new Font("Arial", 10);
      PaperSize pp = new PaperSize();//("A3",1170,1650);

      dwg_frame = new DrawingFrame(297, 210, 0, GraphicsUnit.Millimeter);
      pp.RawKind = (int)PaperKind.A4;

      //dwg_frame = new DrawingFrame(420, 297, 0, GraphicsUnit.Millimeter);
      //pp.RawKind = (int)PaperKind.A3;

      printDoc.DefaultPageSettings.PaperSize = pp;
      printDoc.DefaultPageSettings.Margins.Left = 0;
      printDoc.DefaultPageSettings.Margins.Right = 0;
      printDoc.DefaultPageSettings.Margins.Top = 0;
      printDoc.DefaultPageSettings.Margins.Bottom = 0;//*/
      printDoc.OriginAtMargins = false;
      //printDoc.DefaultPageSettings.
      printDoc.DefaultPageSettings.Landscape = true;
      //printDoc.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
      printDoc.Print();
      //printDoc.PrinterSettings
    }

    private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      Graphics g = e.Graphics;

      // If you set printDocumet.OriginAtMargins to 'false' this event 
      // will print the largest rectangle your printer is physically 
      // capable of. This is often 1/8" - 1/4" from each page edge.
      // ----------
      // If you set printDocument.OriginAtMargins to 'false' this event
      // will print the largest rectangle permitted by the currently 
      // configured page margins. By default the page margins are 
      // usually 1" from each page edge but can be configured by the end
      // user or overridden in your code.
      // (ex: printDocument.DefaultPageSettings.Margins)

      // Grab a copy of our "soft margins" (configured printer settings)
      // Defaults to 1 inch margins, but could be configured otherwise by 
      // the end user. You can also specify some default page margins in 
      // your printDocument.DefaultPageSetting properties.
      RectangleF marginBounds = e.MarginBounds;

      // Grab a copy of our "hard margins" (printer's capabilities) 
      // This varies between printer models. Software printers like 
      // CutePDF will have no "physical limitations" and so will return 
      // the full page size 850,1100 for a letter page size.
      RectangleF printableArea = e.PageSettings.PrintableArea;

      // If we are print to a print preview control, the origin won't have 
      // been automatically adjusted for the printer's physical limitations. 
      // So let's adjust the origin for preview to reflect the printer's 
      // hard margins.
      //if (printAction == PrintAction.PrintToPreview)
      //    g.TranslateTransform(printableArea.X, printableArea.Y);

      // Are we using soft margins or hard margins? Lets grab the correct 
      // width/height from either the soft/hard margin rectangles. The 
      // hard margins are usually a little wider than the soft margins.
      // ----------
      // Note: Margins are automatically applied to the rotated page size 
      // when the page is set to landscape, but physical hard margins are 
      // not (the printer is not physically rotating any mechanics inside, 
      // the paper still travels through the printer the same way. So we 
      // rotate in software for landscape)
      int availableWidth = (int)Math.Floor(printDoc.OriginAtMargins ? marginBounds.Width : (e.PageSettings.Landscape ? printableArea.Height : printableArea.Width));
      int availableHeight = (int)Math.Floor(printDoc.OriginAtMargins ? marginBounds.Height : (e.PageSettings.Landscape ? printableArea.Width : printableArea.Height));

      // Draw our rectangle which will either be the soft margin rectangle 
      // or the hard margin (printer capabilities) rectangle.
      // ----------
      // Note: we adjust the width and height minus one as it is a zero, 
      // zero based co-ordinates system. This will put the rectangle just 
      // inside the available width and height.
      //g.DrawRectangle(Pens.Red, 0, 0, availableWidth - 1, availableHeight - 1);
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

      float xmin = e.PageSettings.PrintableArea.Left;
      float ymin = e.PageSettings.PrintableArea.Top;
      float xmax = e.PageSettings.PrintableArea.Bottom;
      float ymax = e.PageSettings.PrintableArea.Right;

      //int xmid = (int)(xmin + (xmax - xmin) / 2);
      //int ymid = (int)(ymin + (ymax - ymin) / 2);


      int res_x = e.PageSettings.PrinterResolution.X;
      int res_y = e.PageSettings.PrinterResolution.Y;

      //e.Cancel = true;
      //e.Graphics.PageUnit = GraphicsUnit.Millimeter;
      RectangleF rect = e.PageSettings.PrintableArea;
      //e.Graphics.ResetTransform();

      //DrawingFrame frame = new DrawingFrame();
      //e.Graphics.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Height, rect.Width);
      Utils.FitRectangles(e.Graphics, 0, dwg_frame.Width, 0, dwg_frame.Height, 0, availableWidth, 0, availableHeight);



      dwg_frame.Draw(e.Graphics);
      // Draw a different shape for each page//*/
      /*switch (m_NextPage)
      {
          case 0:
              // Draw a triangle.
              Point[] pts = {
                  new Point(xmid, ymin),
                  new Point(xmax, ymax),
                  new Point(xmin, ymax)
              };
              using (Pen thick_pen = new Pen(Color.Blue, 10))
              {
                  thick_pen.DashStyle = DashStyle.Dot;
                  e.Graphics.DrawPolygon(thick_pen, pts);
              }
              break;
          case 1:
              // Draw an ellipse.
              using (Pen thick_pen = new Pen(Color.Red, 10))
              {
                  e.Graphics.DrawEllipse(thick_pen, e.MarginBounds);
              }
              break;
          case 2:
              // Draw a rectangle.
              using (Pen thick_pen = new Pen(Color.Green, 10))
              {
                  thick_pen.DashStyle = DashStyle.Dash;
                  e.Graphics.DrawRectangle(thick_pen, e.MarginBounds);
              }
              break;
          case 3:
              // Draw an X.
              using (Pen thick_pen = new Pen(Color.Black, 10))
              {
                  thick_pen.DashStyle = DashStyle.Custom;
                  thick_pen.DashPattern = new float[] { 10, 10 };
                  e.Graphics.DrawLine(thick_pen, xmin, ymin, xmax, ymax);
                  e.Graphics.DrawLine(thick_pen, xmin, ymax, xmax, ymin);
              }
              break;
      }//*/

      // Draw the page number.
      /*using (Font the_font = new Font("Times New Roman", 250,
              FontStyle.Bold, GraphicsUnit.Point))
      {
          using (StringFormat sf = new StringFormat())
          {
              sf.Alignment = StringAlignment.Center;
              sf.LineAlignment = StringAlignment.Center;
              e.Graphics.DrawString(m_NextPage.ToString(),
                  the_font, Brushes.Black, xmid, ymid, sf);
          }
      }//*/
      e.HasMorePages = false;
    }
  }
}