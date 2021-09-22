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



		Rectangle[] sepperators = new Rectangle[101];
		#endregion

		#region Methods
		void InitSepperators()
		{
			double gap = (double)TLPan.ActualWidth / (sepperators.Length - 1.0);

			for(int i = 0; i < sepperators.Length; i++)
			{
				Rectangle sep = new Rectangle();
				sep.Fill = new SolidColorBrush(Colors.White);
				TLPan.Children.Add(sep);
				sep.Width = 3;
				sep.Height = TLPan.RenderSize.Height;
				double x = gap * i;
				Canvas.SetLeft(sep, x);
				sep.VerticalAlignment = VerticalAlignment.Stretch;
				sepperators[i] = sep;
			}
		}

		void DrawSepperators()
		{
			if (sepperators[0] == null) return;
			double gap = (double)TLPan.RenderSize.Width / (sepperators.Length - 1.0);

			for (int i = 0; i < sepperators.Length; i++)
			{
				Rectangle sep = sepperators[i];
				double x = gap * i;
				Canvas.SetLeft(sep, x);
			}
		}
		#endregion

		#region Events
		private void TLPan_Loaded(object sender, RoutedEventArgs e)
		{
			InitSepperators();
		}

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			DrawSepperators();
		}
	}
	#endregion
}
