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

namespace _3_5_8
{
    /// <summary>
    /// Interaction logic for Baslangic.xaml
    /// </summary>
    public partial class Balangic : Window
    {
        private List<Kisi> kisiler = new List<Kisi>();
        public Balangic()
        {
            InitializeComponent();
        }

        private void btn_kullaniciEkle_Click(object sender, RoutedEventArgs e)
        {
            //Kullanıcının verdiği isimler alınarak Masa sınıfından bir nesne üretilir ve bu değerler o nesneye gönderilir.
            // Aynı şekilde gönderilen kişiler karşı tarafta alınır.
            if (txb_kullanici.Text == "" || txb_bil1.Text == "" || txb_bil2.Text == "")
            {
                MessageBox.Show("Lütfen tüm kullanıcıları doldurunuz.");
            }
            else {
                kisiler.Add(new Kisi(txb_kullanici.Text));
                kisiler.Add(new Kisi(txb_bil1.Text));
                kisiler.Add(new Kisi(txb_bil2.Text));
            }
            Masa masa = new Masa(kisiler);
            this.Close(); // Bu pencere kapatıldı
            masa.Show(); // Oyun penceresi açıldı
        }
    }
}
