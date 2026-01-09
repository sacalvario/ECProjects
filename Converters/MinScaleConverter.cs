using System;
using System.Globalization;
using System.Windows.Data;

namespace ProjectManager.Converters
{
    public class MinScaleConverter : IMultiValueConverter
    {
        public double BaselineWidth { get; set; } = 1280.0;
        public double BaselineHeight { get; set; } = 800.0;
        public double MinScale { get; set; } = 0.85;
        public double MaxScale { get; set; } = 1.25;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return 1.0;

            if (values[0] is double width && values[1] is double height && width > 0 && height > 0)
            {
                var scaleW = width / BaselineWidth;
                var scaleH = height / BaselineHeight;
                var scale = Math.Min(scaleW, scaleH);
                if (scale < MinScale) scale = MinScale;
                if (scale > MaxScale) scale = MaxScale;
                return scale;
            }
            return 1.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
