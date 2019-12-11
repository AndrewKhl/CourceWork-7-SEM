using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class StatisticControlViewModel : BaseViewModel
    {
        public enum Mode { All, Profit };

        public Mode SelectMode { get; set; } = Mode.Profit;

        public IEnumerable<Mode> ModeList => Enum.GetValues(typeof(Mode)).Cast<Mode>();

        public PlotModel GraphModel { get; set; }

        public StatisticControlViewModel(IShellViewModel shell) : base(shell)
        {
            GraphModel = new PlotModel();

            //AxisPosition.Bottom, "Date", "dd/MM/yy HH:mm"

            //var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            //GraphModel.Axes.Add(dateAxis);

            GraphModel.Title = "Zdarova";

            var lineSerie = new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 3,
                //MarkerStroke = OxyColor,
                CanTrackerInterpolatePoints = false,
                Title = "Dadadad",
            };

            var pairs = new List<(int, int)>() { (5, 3), (1, 2), (1, 3), (2, 4), (6, 7) };
            //DateTimeAxis.ToDouble(d.DateTime)
            pairs.Sort();
            pairs.ForEach(d => lineSerie.Points.Add(new DataPoint(d.Item1, d.Item2)));

            GraphModel.Series.Add(lineSerie);

            NotifyPropertyChanged(() => GraphModel);
        }
    }
}
