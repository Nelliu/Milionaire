using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Milionare
{
    /// <summary>
    /// Interakční logika pro StartingMenu.xaml
    /// </summary>
    public partial class StartingMenu : Page
    {

        // this.NavigationService.Navigate(new StartingMenu());
        // buttons SVG
        public StartingMenu()
        {
            InitializeComponent();
        }
        public StartingMenu(Frame frame) : this()
        {
            
        }
        private void hexagon_Loaded(object sender, RoutedEventArgs e)
        {
            Path hexagon = sender as Path;
            CreateDataPath(hexagon.Width, hexagon.Height);
        }

        PathFigure figure;
        private void CreateDataPath(double width, double height)
        {
            height -= hexagon.StrokeThickness;
            width -= hexagon.StrokeThickness;

            PathGeometry geometry = new PathGeometry();
            figure = new PathFigure();

            //See for figure info http://etc.usf.edu/clipart/50200/50219/50219_area_hexagon_lg.gif
            figure.StartPoint = new Point(0.25 * width, 0);
            AddPoint(0.75 * width, 0);
            AddPoint(width, 0.5 * height);
            AddPoint(0.75 * width, height);
            AddPoint(0.25 * width, height);
            AddPoint(0, 0.5 * height);
            figure.IsClosed = true;
            geometry.Figures.Add(figure);
            hexagon.Data = geometry;
        }

        private void AddPoint(double x, double y)
        {
            LineSegment segment = new LineSegment();
            segment.Point = new Point(x + 0.5 * hexagon.StrokeThickness,
                y + 0.5 * hexagon.StrokeThickness);
            figure.Segments.Add(segment);
        }
    }
}
