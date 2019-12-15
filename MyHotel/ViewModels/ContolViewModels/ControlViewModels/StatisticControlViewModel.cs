using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class StatisticControlViewModel : BaseViewModel
    {
        private readonly string _exportPathFolder = Path.Combine(Environment.CurrentDirectory, "Reports");

        private readonly int? _reservationSum = 0, _salarySum = 0, 
                            _roomsMaintance = 0, _serviceProfit = 0;

        private Mode _selectMode = Mode.Profit;

        public enum Mode { All, Profit, Lose };

        public Mode SelectMode
        {
            get => _selectMode;
            set
            {
                _selectMode = value;
                UpdateGraph();
            }
        }

        public ICommand ExportCSVCommand { get; set; }

        public IEnumerable<Mode> ModeList => Enum.GetValues(typeof(Mode)).Cast<Mode>();

        public PlotModel GraphModel { get; set; }

        public StatisticControlViewModel(IShellViewModel shell) : base(shell)
        {
            GraphModel = new PlotModel();

            _reservationSum = CoreManager.OrderManager.Payments.ToList()?.Sum(u => u.Cost) ?? 0;
            _salarySum = CoreManager.UserManager.Salarys.ToList()?.Sum(u => u.Count) ?? 0;
            _roomsMaintance = CoreManager.RoomManager.MaintenanceCosts.ToList()?.Sum(u => u.Cost) ?? 0;
            _serviceProfit = CoreManager.OrderManager.Payments.ToList()?.Where(u => !u.IsHousingOrder).Sum(u => u.Cost) ?? 0;

            ExportCSVCommand = new DelegateCommand(ExportCSVDelegate);

            UpdateGraph();
        }

        public string TotalProfit => $"{_reservationSum}$";

        public string TotalLoss => $"{_roomsMaintance + _salarySum}$";

        public string RoomsMain => $"{_roomsMaintance}$";

        public string Salarys => $"{_salarySum}$";

        public string Reservation => $"{_reservationSum}$";

        public string ServiceProfit => $"{_serviceProfit}$";

        public int CountGuests => _shell.CoreManager.UserManager.Guests.ToList()?.Count() ?? 0;

        public int CountStaffs => _shell.CoreManager.UserManager.Staffs.ToList()?.Count() ?? 0;

        public int CountOrders => _shell.CoreManager.OrderManager.HousingOrders.ToList()?.Count() ?? 0 + _shell.CoreManager.OrderManager.ServiceOrders.ToList()?.Count() ?? 0;

        private void UpdateGraph()
        {
            GraphModel.Series.Clear();
            //AxisPosition.Bottom, "Date", "dd/MM/yy HH:mm"

            if (GraphModel.Axes.Count == 0)
            {
                var dateAxis = new DateTimeAxis() 
                {
                    Position = AxisPosition.Bottom,
                    StringFormat = "dd/MM HH:mm",

                    MinorIntervalType = DateTimeIntervalType.Days,
                    IntervalType = DateTimeIntervalType.Days,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.None,
                };
                GraphModel.Axes.Add(dateAxis);
            }

            GraphModel.Title = SelectMode.ToString();

            switch (SelectMode)
            {
                case Mode.All:
                    SetLosePoints();
                    SetProfitPoints();
                    break;
                case Mode.Lose:
                    SetLosePoints();
                    break;
                case Mode.Profit:
                    SetProfitPoints();
                    break;
                default:
                    return;
            }

            NotifyPropertyChanged(() => GraphModel);
            GraphModel.InvalidatePlot(true);
        }

        private LineSeries GetLine(Mode mode)
        {
            return new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 3,
                //MarkerStroke = OxyColor,
                CanTrackerInterpolatePoints = false,
                Title = mode.ToString(),
            };
        }

        private void SetProfitPoints()
        {
            var list = new List<(DateTime, int)>();

            var basic = CoreManager.OrderManager.Payments.ToList();

            if (basic != null)
                basic.ForEach(u => list.Add((DateTime.Parse(u.CreateTime), u.Cost)));

            var line = GetLine(Mode.Profit);

            list.Sort();

            int sum = 0;

            foreach (var p in list)
            {
                sum += p.Item2;
                line.Points.Add(new DataPoint(DateTimeAxis.ToDouble(p.Item1), sum));
            }

            GraphModel.Series.Add(line);
        }

        private void SetLosePoints()
        {
            var list = new List<(DateTime, int)>();

            var basic = CoreManager.UserManager.Salarys.ToList();

            if (basic != null)
                basic.ForEach(u => list.Add((DateTime.Parse(u.PaymentDate), u.Count)));

            var basicRoom = CoreManager.RoomManager.MaintenanceCosts.ToList();

            if (basicRoom != null)
                basicRoom.ForEach(u => list.Add((DateTime.Parse(u.Date), u.Cost)));

            var line = GetLine(Mode.Lose);

            list.Sort();

            int sum = 0;

            foreach (var p in list)
            {
                sum += p.Item2;
                line.Points.Add(new DataPoint(DateTimeAxis.ToDouble(p.Item1), sum));
            }

            GraphModel.Series.Add(line);
        }

        private void ExportCSVDelegate(object o)
        {
            Directory.CreateDirectory(_exportPathFolder);

            WriteCSVFile($"Report({DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss.fff")}).csv");

            Process.Start(_exportPathFolder);
        }

        private void WriteCSVFile(string name)
        {
            using (var fs = new FileStream(Path.Combine(_exportPathFolder, name), FileMode.OpenOrCreate))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"Financial report. Created by {DateTime.Now}");

                    sw.WriteLine("\nProfit");
                    WriteReservationOrders(sw);
                    WriteServiceOrders(sw);

                    sw.WriteLine("\nLoss");
                    WriteSalaries(sw);
                    WriteMaintance(sw);

                    sw.WriteLine($"\n;;;;PROFIT: {TotalProfit}");
                    sw.WriteLine($";;;;LOSS: {TotalLoss}");
                    sw.WriteLine($";;;;NET PROFIT: {_reservationSum - _roomsMaintance - _salarySum}$");
                }
            }
        }

        private void WriteMaintance(StreamWriter sw)
        {
            var orders = CoreManager.RoomManager.MaintenanceCosts.ToList();
            var id = CoreManager.RoomManager.LivingRooms.Select(u => u.Id).ToList();
            
            if (orders == null || id == null)
                return;

            id.Sort();

            sw.WriteLine("\nMaintance");
            sw.WriteLine("Room ID; Date; Cost");

            int sum = 0;

            foreach (var i in id)
            {
                var sal = orders.Where(u => u.RoomId == i);
                var curSum = sal.Sum(u => u.Cost);

                if (curSum == 0)
                    continue;

                sw.WriteLine($"{id};{sal.LastOrDefault().Date};{curSum}");
                sum += curSum;
            }

            sw.WriteLine($";;;;Loss: {sum}$");
        }

        private void WriteSalaries(StreamWriter sw)
        {
            var orders = CoreManager.UserManager.Salarys.ToList();
            var staff = CoreManager.UserManager.Staffs.ToList();

            if (orders == null && staff == null)
                return;

            sw.WriteLine("\nSalaries");
            sw.WriteLine("User; Date; Cost");

            int sum = 0;

            foreach (var s in staff)
            {
                var sal = orders.Where(u => u.StaffId == s.Id);
                var curSum = sal.Sum(u => u.Count);

                if (curSum == 0)
                    continue;

                sw.WriteLine($"{s.Name};{sal.LastOrDefault().PaymentDate};{curSum}");
                sum += curSum;
            }

            sw.WriteLine($";;;;Loss: {sum}$");
        }

        private void WriteReservationOrders(StreamWriter sw)
        {
            var orders = CoreManager.OrderManager.HousingOrders.ToList();

            if (orders == null && orders.Count > 0)
                return;

            sw.WriteLine("\nReservation");
            sw.WriteLine("Room ID; User; Date In; Date Out; Cost");

            int sum = 0;

            foreach (var o in orders)
                if (o.IsPaid)
                {
                    var user = CoreManager.UserManager.TryGetGuests(o.UserId);

                    if (user == null)
                        return;

                    sw.WriteLine($"{o.RoomId};{user.Email};{o.InTime};{o.OutTime};{o.Cost}$");
                    sum += o.Cost;
                }

            sw.WriteLine($";;;;Profit: {sum}$");
        }

        private void WriteServiceOrders(StreamWriter sw)
        {
            var services = CoreManager.OrderManager.Services.ToList();
            var orders = CoreManager.OrderManager.ServiceOrders.ToList();

            if (services == null || orders == null)
                return;

            sw.WriteLine("\nServices");
            sw.WriteLine("Name; Count; Cost");

            int sum = 0;

            foreach (var s in services)
            {
                var curOrders = orders.Where(u => u.ServiceId == s.Id && u.IsPaid);
                int curSum = curOrders.Sum(u => u.Cost);

                if (curSum == 0)
                    continue;

                sw.WriteLine($"{s.Name};{curOrders.Count()};{curSum}$");
                sum += curSum;
            }

            sw.WriteLine($";;;;Profit: {sum}$");
        }
    }
}
