using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapesDesktopApplication
{
    public partial class MainForm : Form
    {
        private List<Shapes.IDisplayable> shapes;

        public MainForm()
        {
            InitializeComponent();

            shapes = new List<Shapes.IDisplayable>();
            shapes.Add(new Shapes.Rectangle(212.2, 123.156, new Shapes.Point(120, 80)));
            shapes.Add(new Shapes.Ellipse(150, 75, new Shapes.Point(250, 250), Color.LightBlue));
            shapes.Add(new Shapes.Square(50, new Shapes.Point(300, 33), Color.Chocolate));
            shapes.Add(new Shapes.Circle(25, new Shapes.Point(456.78,245.67), Color.OrangeRed));
            shapes.Add(new Shapes.Triangle(new Shapes.Point(400, 100), new Shapes.Point(477, 67), new Shapes.Point(412, 156), Color.SeaGreen));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
            }
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            foreach (var shape in shapes)
            {
                var shapeToMove = shape as Shapes.IMovable;
                if (shape != null)
                {
                    shapeToMove.Move(0, -25);
                }
            }

            Invalidate();
        }

        private void MoveLeftButton_Click(object sender, EventArgs e)
        {
            foreach (var shape in shapes)
            {
                var shapeToMove = shape as Shapes.IMovable;
                if (shape != null)
                {
                    shapeToMove.Move(-25, 0);
                }
            }

            Invalidate();
        }

        private void MoveRightButton_Click(object sender, EventArgs e)
        {
            foreach (var shape in shapes)
            {
                var shapeToMove = shape as Shapes.IMovable;
                if (shape != null)
                {
                    shapeToMove.Move(25, 0);
                }
            }

            Invalidate();
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            foreach (var shape in shapes)
            {
                var shapeToMove = shape as Shapes.IMovable;
                if (shape != null)
                {
                    shapeToMove.Move(0, 25);
                }
            }

            Invalidate();
        }
    }
}
