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
using System.Windows.Media.Animation;
using System.Timers;
namespace _3_5_8
{
    /// <summary>
    /// Interaction logic for Kart_Kapak.xaml
    /// </summary>
    public partial class Kart_Kapak : UserControl
    {

        private Kart kart;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        // User kontrolun kartını tutacagiz.
        public Kart _Kart
        {
            get { return kart; }
            set { kart = value; }
        }

        public string _Kaynak 
        {
            set 
            {
                var uriKaynak = new Uri(value, UriKind.Relative);
                kart_resim.Source = new BitmapImage(uriKaynak);
            } 
        }

        public Kart_Kapak()
        {
            InitializeComponent();
        }       
    }
}