// Company: Hobo Group
//  Auther: whanjit@gmail.com
//    Date: April 3, 2013
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HoboShape
{
  class DrawingFrame
  {
    public DrawingFrame()
    {
      Margin = 5;
      Width = 420;
      Height = 297;
      PageUnit = GraphicsUnit.Millimeter;
    }
    public DrawingFrame(float width, float height, float margin, GraphicsUnit pageUnit)
    {
      Margin = margin;
      Width = width;
      Height = height;
      PageUnit = pageUnit;
    }
    public float Margin { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public GraphicsUnit PageUnit { get; set; }

    public void Draw(System.Drawing.Printing.PrintPageEventArgs e)
    {
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

      int xmin = e.MarginBounds.Left;
      int ymin = e.MarginBounds.Top;
      int xmax = e.MarginBounds.Right;
      int ymax = e.MarginBounds.Bottom;
      int xmid = (int)(xmin + (xmax - xmin) / 2);
      int ymid = (int)(ymin + (ymax - ymin) / 2);
      Utils.FitRectangles(e.Graphics, 0, Width, 0, Height, xmin, xmax, ymin, ymax);
      Draw(e.Graphics);
      e.Graphics.ResetTransform();
    }
    public void Draw(Graphics graphic, Control canvas)
    {
      graphic.SmoothingMode = SmoothingMode.AntiAlias;

      int xmin = canvas.ClientRectangle.X + 10;// e.MarginBounds.Left;
      int ymin = canvas.ClientRectangle.Y + 10;// e.MarginBounds.Top;
      int xmax = canvas.ClientRectangle.Width - 10;// e.MarginBounds.Right;
      int ymax = canvas.ClientRectangle.Height - 10;// e.MarginBounds.Bottom;
      int xmid = (int)(xmin + (xmax - xmin) / 2);
      int ymid = (int)(ymin + (ymax - ymin) / 2);
      Utils.FitRectangles(graphic, 0, Width, 0, Height, xmin, xmax, ymin, ymax);
      Draw(graphic);
      graphic.ResetTransform();
    }
    public void Draw(Graphics graphic)
    {
      //Utils.FitRectangles(graphic,
      Pen pen = new Pen(Color.Black, 0.25f);
      graphic.DrawLine(pen, 0, 0, Width, 0);
      graphic.DrawLine(pen, 0, Height, Width, Height);
      graphic.DrawLine(pen, 0, 0, 0, Height);
      graphic.DrawLine(pen, Width, 0, Width, Height);

      float width_div_3 = Width / 3;
      graphic.DrawLine(pen, width_div_3, 0, width_div_3, Height);
      graphic.DrawLine(pen, width_div_3 * 2, 0, width_div_3 * 2, Height);
      graphic.DrawLine(pen, width_div_3, Height / 3, width_div_3 * 2, Height / 3);
      graphic.DrawLine(pen, width_div_3, Height / 3 * 2, Width, Height / 3 * 2);

      //FontFamily oneFontFamily =FontFamily.Families.
      //String msg= "Hello World! TRULY 私は東京に行きます सही मायने में 진심으로 本当に Ho intenzione di tokyo Én megyek, hogy Tokió Estou indo para Tóquio";
      //Font font = new Font("Times New Roman", 1.5f, FontStyle.Bold, GraphicsUnit.Pixel);
      //font = FindBestFitFont(graphic, msg, font, new Size((int)Width, (int)Height));
      //graphic.DrawString(msg, font, Brushes.Black, new PointF(width_div_3 * 2, 25));

      Font font = new Font("Times New Roman", 16f, FontStyle.Bold, GraphicsUnit.Pixel);
      String msg = "Hobo Group";
      graphic.DrawString(msg, font, Brushes.Black, new PointF(width_div_3 * 2, 10));

      font = new Font("Times New Roman", 10f, FontStyle.Bold, GraphicsUnit.Pixel);
      msg = "TRULY";
      graphic.DrawString(msg, font, Brushes.Black, new PointF(width_div_3 * 2, 30));
      msg = "私は東京に行きます";
      graphic.DrawString(msg, font, Brushes.Black, new PointF(width_div_3 * 2, 40));
      msg = "सही मायने में";
      graphic.DrawString(msg, font, Brushes.Black, new PointF(width_div_3 * 2, 50));
      msg = "진심으로";
      graphic.DrawString(msg, font, Brushes.Black, new PointF(width_div_3 * 2, 60));
      msg = "本当に";
      graphic.DrawString(msg, font, Brushes.Black, new PointF(width_div_3 * 2, 70));
      msg = "真正";
      graphic.DrawString(msg, font, Brushes.Black, new PointF(width_div_3 * 2, 80));
      msg = "VERAMENTE";
      graphic.DrawString(msg, font, Brushes.Black, new PointF(width_div_3 * 2, 90));
      msg = "ЧЫНДЫК";
      graphic.DrawString(msg, font, Brushes.Black, new PointF(width_div_3 * 2, 90)); 
    }

    public SizeF GetTextDrawingArea(Graphics graphic, string text, Font font)
    {
      // Contract.Requires(!string.IsNullOrEmpty(text));
      // Contract.Requires(font != null);

      //using (Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb))
      //{
      Graphics g = graphic;//Graphics.FromImage(bitmap);

      StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);

      stringFormat.SetMeasurableCharacterRanges(new[] { new CharacterRange(0, text.Length) });

      Region[] regions = g.MeasureCharacterRanges(text, font, RectangleF.Empty, stringFormat);

      SizeF size = SizeF.Empty;

      if (regions != null && regions.Length > 0)
      {
        Region region = regions[0];

        // Assert.NotNull(region);

        size = region.GetBounds(g).Size;
      }

      // Assert.False(size.IsEmpty);

      return size;
      //}
    }

    private Font FindBestFitFont(Graphics g, String text, Font font, Size proposedSize)
    {
      // Compute actual size, shrink if needed
      while (true)
      {
        SizeF size = g.MeasureString(text, font);

        // It fits, back out
        if (size.Height <= proposedSize.Height &&
             size.Width <= proposedSize.Width) { return font; }

        // Try a smaller font (90% of old size)
        Font oldFont = font;
        font = new Font(font.Name, (float)(font.Size * .9), font.Style);
        oldFont.Dispose();
      }
    }
  }
}
