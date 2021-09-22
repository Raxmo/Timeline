using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Timeline
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Set-up
		[DllImport("Kernel32")]
		public static extern void AllocConsole();
		[DllImport("Kernel32")]
		public static extern void FreeConsole();

		public MainWindow()
		{
			InitializeComponent();
			AllocConsole();
		}
		#endregion

		#region Properties
		double zoom = 0.0;//-> 0 - 1, from one order of magnitude to the next
		byte zlom = 0; //-> zero is a one-year span

		double pan = 0;
		int year = 0;

		Rectangle[] sepperators = new Rectangle[201];
		#endregion

		#region Methods

		double Lerp(double a, double b, double t)
		{
			return (1 - t) * a + t * b;
		}

		void InitSepperators()
		{
			for(int i = 0; i < sepperators.Length; i++)
			{
				Rectangle sep = new Rectangle();
				sep.Fill = new SolidColorBrush(Colors.White);
				TLPan.Children.Add(sep);
				sep.Width = 3;
				sep.Height = TLPan.RenderSize.Height;
				sep.VerticalAlignment = VerticalAlignment.Stretch;
				sepperators[i] = sep;
			}
		}

		void DrawSepperators()
		{
			if (sepperators[0] == null) return;
			double gap = Math.Pow(10, zoom) * (double)TLPan.RenderSize.Width / ((sepperators.Length - 1.0) / 2);

			for (int i = 0; i < sepperators.Length; i++)
			{
				Rectangle sep = sepperators[i];
				double x = gap * i;
				Canvas.SetLeft(sep, x);

				double op = 1.0;

				if(i % 100 == 0)
				{

				}
				else if(i % 50 == 0)
				{
					op = Lerp(0.5, 1.0, zoom);
				}
				else if(i % 10 == 0)
				{
					op = Lerp(0.25, 1.0, zoom);
				}
				else if(i % 5 == 0)
				{
					op = Lerp(0.0, 0.5, zoom);
				}
				else
				{
					op = Lerp(0.0, 0.25, zoom);
				}

				sep.Opacity = op;
			}
		}
		#endregion

		#region Events
		private void TLPan_Loaded(object sender, RoutedEventArgs e)
		{
			InitSepperators();
			DrawSepperators();
		}

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			DrawSepperators();
		}

		private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			if(e.Delta > 0 && zlom > 0)
			{
				zoom += 0.1;
			}
			else if(e.Delta < 0 && zlom < 255)
			{
				zoom -= 0.1;
			}

			if (zoom >= 1)
			{
				zoom -= 1.0;
				zlom--;
			}

			if(zoom < 0)
			{
				zoom += 1.0;
				zlom++;
			}

			DrawSepperators();
		}

		private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
		{

		}
	}
	#endregion
}
