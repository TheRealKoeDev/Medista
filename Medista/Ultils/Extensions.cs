using System.ComponentModel;
using System.Windows;

namespace Medista.Ultils
{
    public static class Extensions
    {
        public static void TryInitDesignInjector(this FrameworkElement frameworkElement) {
            if (DesignerProperties.GetIsInDesignMode(frameworkElement))
            {
                App.InitializeInjector();
            }
        }

        public static System.Drawing.Point ToDrawPoint(this Window window)
        {
            return new((int)window.Left, (int)window.Top);
        }

        public static System.Drawing.Point ToDrawPoint(this Point point)
        {
            return new ((int)point.X, (int)point.Y);
        }

        public static System.Drawing.Size ToDrawSize(this Window window)
        {
            return new ((int)window.Width, (int)window.Height);
        }

        public static System.Drawing.Size ToDrawSize(this Size size)
        {
            return new ((int)size.Width, (int)size.Height);
        }
    }
}
