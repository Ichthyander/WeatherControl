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

/*1. Разработать в WPF приложении класс WeatherControl, моделирующий погодную сводку – температуру (целое число в диапазоне от -50 до +50), 
 * направление ветра (строка), скорость ветра (целое число), наличие осадков (возможные значения: 0 – солнечно, 1 – облачно, 2 – дождь, 3 – снег. 
 * Можно использовать целочисленное значение, либо создать перечисление enum). Свойство «температура» преобразовать в свойство зависимости.*/

namespace WeatherControlClass
{
    enum Cloudiness
    {
        Солнечно = 0,
        Облачно,
        Дождь,
        Снег
    }
    class WeatherControl : DependencyObject
    {
        public static readonly DependencyProperty TemperatureProperty;

        public double Temperature 
        { 
            get => (double) GetValue(TemperatureProperty); 
            set => SetValue(TemperatureProperty, value); 
        }
        public string WindDirection { get; set; }
        public double WindSpeed { get; set; }
        public Cloudiness CurrentCloudiness { get; set; }

        public WeatherControl(string windDirection, double windSpeed, Cloudiness currentCloudiness)
        {
            WindDirection = windDirection;
            WindSpeed = windSpeed;
            CurrentCloudiness = currentCloudiness;
        }

        static WeatherControl()
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(double),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0.0,  
                    FrameworkPropertyMetadataOptions.Journal,
                    null,
                    new CoerceValueCallback(CoerceTemperature)),
                    new ValidateValueCallback(ValidateTemperature)
                );
        }

        private static object CoerceTemperature(DependencyObject d, object baseValue)
        {
            double temperature = (double)baseValue;

            if (temperature > 50)
            {
                return 50.0;
            }
            else if (temperature < -50)
            {
                return -50.0;
            }
            else
            {
                return temperature;
            }
        }


        private static bool ValidateTemperature(object value)
        {
            double temperature = (double)value;

            if (temperature > 50 && temperature < -50)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WeatherControl weatherControl = new WeatherControl("Север", 5, Cloudiness.Снег);

            //Отладка и проверка
            string windDirection = weatherControl.WindDirection;
            double windSpeed = weatherControl.WindSpeed;
            Cloudiness currentCloudiness = weatherControl.CurrentCloudiness;
            weatherControl.Temperature = 60;

            InitializeComponent();


            textBlock.Text = $"1. Направление ветра - {windDirection} 2. Скорость ветра - {windSpeed} 3. Облачность - {currentCloudiness} 4. Температура - {weatherControl.Temperature}";
        }
    }
}
