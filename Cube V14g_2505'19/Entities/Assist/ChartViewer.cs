using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube_V11.Entities.Assist
{
    public abstract class ChartViewer
    {
        public LiveCharts.WinForms.CartesianChart GetChart { get; set; }
        public LineSeries GetLineSeries { get; set; }

        virtual public void InitChart()
        {
            if (IsInitialize()) //проверка количества графиков
            {
                GetChart.AxisX.Clear(); //отчистка стандартной оси Х 
                GetChart.AxisX.Add(new Axis //создание новой оси Х
                {
                    IsEnabled = true, //видимость
                    ShowLabels = true, //отображение текста
                    Separator = new Separator //создание вертикальной сетки
                    {
                        StrokeThickness = 1, //толщина линии
                        //цвет линии
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                    },
                    Labels = new List<string>()
                });

                GetChart.AxisY.Clear(); //отчистка стандартной оси Y

                GetChart.AxisY.Add(new Axis //создание новой оси Y
                {
                    IsMerged = true, //отображение текста внутри сетки
                    Separator = new Separator //создание горизонтальной сетки
                    {
                        StrokeThickness = 1, //толщина линии
                        //цвет линии
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                    }
                });

                GetChart.Zoom = ZoomingOptions.X; //функция увеличения

                GetChart.LegendLocation = LegendLocation.Top; //положение подписей кривых
            }
        }

        public bool IsInitialize()
        {
            return GetChart.Series.Count == 0;
        }

        public void ClearSeries()
        {
            GetChart.Series.Clear();
        }

        public void ClearValues()
        {
            foreach (Series series in GetChart.Series)
            {
                series.Values.Clear();
            }
        }

        public void ClearValues(int i)
        {
            GetChart.Series[i].Values.Clear();
        }

        public void ClearSeries(int i)
        {
            GetChart.Series.Remove(GetChart.Series[i]);
        }

        public LineSeries AddSerie(string name)
        {
            LineSeries ser = new LineSeries() //создание новой кривой
            {
                Values = new ChartValues<float>(), //тип значений
                PointGeometry = null, //отображение точек на графике
                Fill = System.Windows.Media.Brushes.Transparent, //окрашивание области под кривой
                StrokeThickness = 1.5, //толщина линии
                Title = name //подпись кривой
            };

            return ser;
        }
    }
}
