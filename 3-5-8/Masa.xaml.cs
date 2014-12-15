/*
 *
 *  Wpf c# 3-5-8 Oyunu
 *
 * */
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
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Threading;

namespace _3_5_8
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Masa : Window
    {

        Deste deste;
        List<Kisi> kisiler = new List<Kisi>(); //Kişileri sıfırlamak için
        List<Skor> skor_tablosu = new List<Skor>(); //Skor tablolarının tutulduğu liste
        List<Kart> degisim_kartlari = new List<Kart>(); //Kişinin seçtiği kartları tutmak için kullanilir.
        Oyun oyun; // Oyun takibinin yaiplacigi sinif ornekleme
        int b=0; //kagit konum icin
        MediaPlayer mp = new MediaPlayer(); //Sesler için
        string url;

        public Masa(List<Kisi> kisiler){
            InitializeComponent();
            this.kisiler = kisiler;
        }

        //İhalelerin skor user controllere basılması
        public void skor_ihale_goster() {
            for (int i = 0; i < skor_tablosu.Count; i++)
            {
                skor_tablosu[i]._Kisi_Ihale.Content = kisiler[i]._Ihale.ToString();
            }
        }

        //Kişinin aldığı kart skor User Controllde gösterilir.
        public void kisi_aldigi_kart_sayisini_goster(int i) {
            skor_tablosu[i]._Kisi_Skor.Content = kisiler[i]._AldigiKart.ToString();
        }

        //Kullanıcıya gösterilecek olan user kontrol nesnesinin oluşturulmasını sağlayan fonksiyon
        public void skor_olustur() {
            skor_tablosu.Clear();
            for (int i = 0; i < 3; i++)
            {
                Skor skor = new Skor();
                skor._Kisi_Isim.Content = kisiler[i]._Isım;
                skor_tablosu.Add(skor);
                skor.Height = 120;
                skor.Width = 320;
            }
        }

        //Oyun bittikten sonra kullanıcıya skor tablosu gösterilir.
        public void skor_goster() {
            for (int i = 0; i < skor_tablosu.Count; i++)
            {
                if (i == 0)
                {
                    skor_tablosu[i].Margin = new Thickness(317, 515, 0, 0);
                }
                else if (i == 2)
                {
                    RotateTransform rotate = new RotateTransform(90, -50, 50);
                    skor_tablosu[i].RenderTransform = rotate;
                    skor_tablosu[i].Margin = new Thickness(127, 9, 0, 0);
                }
                else if (i == 1)
                {
                    RotateTransform rotate = new RotateTransform(-90, -50, 50);
                    skor_tablosu[i].RenderTransform = rotate;
                    skor_tablosu[i].Margin = new Thickness(967, 429, 0, 0);
                }
                skor_tablosu[i]._Kisi_Puan.Content = kisiler[i]._Puan.ToString();
                skor_tablosu[i]._Kisi_Ihale.Content = kisiler[i]._Ihale.ToString();
                skor_tablosu[i]._Kisi_Skor.Content = kisiler[i]._AldigiKart.ToString();
                Cerceve.Children.Add(skor_tablosu[i]);
            }

            _El_Sayisi_Oynanan.Content = oyun._El_Sayisi + 1;
            _El_Sayisi_Toplam.Content = oyun._Oyun_Eli;
            _Panel_Oyun_Sayisi.Visibility = System.Windows.Visibility.Visible;
            Cerceve.Children.Add(_Panel_Oyun_Sayisi);
        }

        //Kart Yerlestir Metodu
        public void kart_yerlestir() {
            kart_temizle(); // Kartlar temizlenir.
            //Kullanici kartlarinin gosterilmesi
            for (int i = 0; i < 3; i++ )
            {
                for (int j = 0; j <kisiler[i]._Kartlar.Count; j++)
                {
                    Cerceve.Children.Add(kisiler[i]._Kartlar[j]._KartKapak);
                    if(i == 0)
                        kisiler[i]._Kartlar[j]._KartKapak.Margin = new Thickness((234 + ((16 - kisiler[i]._Kartlar.Count)*15)) + (j * 30), 409, 0, 0);
                    else if (i == 2)
                        kisiler[i]._Kartlar[j]._KartKapak.Margin = new Thickness(137, 69 + ((16 - kisiler[i]._Kartlar.Count)*10)+(j*20), 0, 0);
                    else if (i == 1)
                        kisiler[i]._Kartlar[j]._KartKapak.Margin = new Thickness(792, 69 + ((16 - kisiler[i]._Kartlar.Count) * 10) + (j * 20), 0, 0);
                }
            }
        }

        public void dort_kart_yerlestir(bool durum)
        {
            //Kalan 4 karti yerlestirilmesi
            for (int i = 0; i < 4; i++)
            {
                Kart_Kapak kart_kapak = new Kart_Kapak();
                if (durum == false)
                {
                    kart_kapak._Kaynak = "kartlar/999.png";
                }
                else
                {
                    kart_kapak._Kaynak = deste._Deste[i]._Resim_Yolu;
                }
                kart_kapak._Kart = deste._Deste[i];
                deste._Deste[i]._KartKapak = kart_kapak;
                Cerceve.Children.Add(deste._Deste[i]._KartKapak);
                deste._Deste[i]._KartKapak.Margin = new Thickness(414 + (i * 30), 95, 0, 0);
            }
        }

        //Kullanici kartlarinin ekrandan temizlenmesi
        public void kart_temizle() {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < kisiler[i]._Kartlar.Count; j++)
                {
                    Cerceve.Children.Remove(kisiler[i]._Kartlar[j]._KartKapak);
                }
            }        
        }

        //Tüm oyunu sifirlayan ve başlatan fonkisyon
        public void oyun_baslat() {
            skor_olustur(); //Oyun ilk başladığında skor nesneleri bir kere oluşturulur.
            oyun = new Oyun(); //Oyunun takibi sifirlanir       
            oyun._El_Sayisi = -1;
            for (int i = 0; i < kisiler.Count; i++)
            {
                kisiler[i]._Puan = 0;
                kisiler[i]._Ihale = 0;
                kisiler[i]._AldigiKart = 0;
            }
            string url = @"" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + @"\..\..\kartlar\sesler\baslat.wav";
            mp.Open(new Uri(url));
            mp.Play();
            el_baslat(); //Oyundan sonra el başlatılır.
            return;
        }

        //Yeni oyun butonuna tıklanma olayı..
        private void YeniOyun_Click(object sender, RoutedEventArgs e)
        {
            if (oyun != null)
            {
                if (MessageBox.Show("Yeni oyun başlatmak istediğinize emin misiniz?", "Yeni Oyun", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Cerceve.Children.Clear();
                    _Oyun_Window.Width = 1020;
                    _Oyun_Window.Height = 680;
                    oyun_baslat();
                }
            }
            else
            {
                oyun_baslat();
            }
        }

        //Bir el oynandıktan sonra el tekrardan başlatılır. Bu işlemi yapan fonksiyon
        public void el_baslat() {

            var zamanlayici_el_baslat = new DispatcherTimer();
            zamanlayici_el_baslat.Interval = TimeSpan.FromMilliseconds(500);
            zamanlayici_el_baslat.Start();
            zamanlayici_el_baslat.Tick += (sender, args) =>
            {
                zamanlayici_el_baslat.Stop();
                Cerceve.Children.Clear(); //Cerceve temizlenir 
                if (oyun._El_Sayisi + 1 >= oyun._Oyun_Eli)
                {
                    _Oyun_Window.Width = 500;
                    _Oyun_Window.Height = 530;
                    List<Kisi> kisiler_sirali = new List<Kisi>();
                    Oyun_Sonucu oyun_sonucu = new Oyun_Sonucu();
                    kisiler_sirali = kisiler.OrderByDescending(o => o._Puan).ToList();
                    oyun_sonucu._Birinci_Isim.Content = kisiler_sirali[0]._Isım;
                    oyun_sonucu._Ikinci_Isim.Content = kisiler_sirali[1]._Isım;
                    oyun_sonucu._Ucuncu_Isim.Content = kisiler_sirali[2]._Isım;

                    oyun_sonucu._Birinci_Puan.Content = kisiler_sirali[0]._Puan;
                    oyun_sonucu._Ikinci_Puan.Content = kisiler_sirali[1]._Puan;
                    oyun_sonucu._Ucuncu_Puan.Content = kisiler_sirali[2]._Puan;

                    if (kisiler[0] == kisiler_sirali[0])
                    {
                        oyun_sonucu._Sonuc_Bildirim.Content = "Tebrikler. Kazandınız.";
                    }
                    else
                    {
                        oyun_sonucu._Sonuc_Bildirim.Content = "Üzgünüz. Kaybettiniz.";
                    }

                    oyun_sonucu._Kazanan_Kupa.Content = kisiler_sirali[0]._Isım;
                    oyun_sonucu._Yeni_Oyun.Click += YeniOyun_Click;
                    oyun_sonucu.Width = 500; oyun_sonucu.Height = 500;
                    oyun_sonucu.Margin = new Thickness(0, 0, 0, 0);
                    Cerceve.Children.Add(oyun_sonucu);
                }
                else
                {
                    b = 0;
                    oyun._Oyun_Sayisi = 0;
                    deste = new Deste(); //desteyi sifirlamak icin
                    kisiler = deste.kart_dagit(kisiler, deste); //Kişilere tekrar kağıt dağıtılır.
                    kisiler= deste.kisiler_kart_sirala(kisiler); //kisi 0 siralanir
                    degisim_kartlari.Clear();
                    kart_yerlestir(); //kartlar ekrana bastirilir 
                    dort_kart_yerlestir(false); //kalan 4 kart ekrana yerlestirilir.

                    //Kişinin değerleri temizlenir.
                    for (int i = 0; i < kisiler.Count; i++)
                    {
                        kisiler[i]._Ihale = 0;
                        kisiler[i]._AldigiKart = 0;
                    }

                    oyun._El_Sayisi++;
                    oyun._Sira = oyun._El_Sayisi % 3;
                    //Ihale sırası kullanıcıda

                    //Kişilerin ihaleleri belirlenerek atama yapılır.
                    kisiler = oyun.ihale_belirle(kisiler);

                    //Kişiye ait olan double click olayları silinir.
                    for (int i = 0; i < kisiler[0]._Kartlar.Count; i++)
                    {
                        kisiler[0]._Kartlar[i]._KartKapak.MouseDoubleClick -= _KartKapak_MouseDoubleClick;

                    }

                    skor_goster(); //Skor gösterilir.

                    if (oyun._Sira == 0)
                    {
                        //El kullanıcıda olduğu için koz seç fonksiyonu çağırılır.
                        koz_sec();
                    }
                    else
                    {
                        //İhale olan bilgisayarda kagitlari otomatik degis ve eli baslat.
                        oyun.bilgisayar_koz_belirle(kisiler[oyun._Sira]);
                        degisim_kartlari = oyun.bilgisayar_dort_kart_degis(kisiler[oyun._Sira]); //Değişim kartlarına bilgisayarın belirlediği dört kart atılır.
                        dort_kart_degis(kisiler[oyun._Sira]);
                        dort_kart_yerlestir(true); //Kullanıcının verdigi 4 kart yerlestirilir.
                        koz_header_belirle(); // Koz ekrana basılır
                        skor_ihale_goster(); // Kullanıcıların ihaleleri ekrana basılır.
                        el_oyna();
                        return;
                    }
                }
            };
        }

        //Elin oynanmasını takip eden fonksiyon.
        void el_oyna()
        {
            //Kart atılırken kart sesi oynatılmıştır.
            url = @"" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + @"\..\..\kartlar\sesler\oynat.wav";
            mp.Open(new Uri(url));
            mp.Play(); 
            if (oyun._Yerdeki_Kartlar.Count == 3)
            {
                //3 kagit atildiktan sonra topla sesi çağırılmıştır.
                var zamanlayici2 = new DispatcherTimer();
                zamanlayici2.Interval = TimeSpan.FromMilliseconds(300);
                zamanlayici2.Start();
                zamanlayici2.Tick += (sender, args) =>
                {
                    zamanlayici2.Stop();
                    //Kart atılırken kart sesi oynatılmıştır.
                    url = @"" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + @"\..\..\kartlar\sesler\topla.wav";
                    mp.Open(new Uri(url));
                    mp.Play();
                };
            }
            

            //kagit atildiktan sonra programin beklemesi için kagidin yere gitmesi beklenir.
            var zamanlayici = new DispatcherTimer();
            zamanlayici.Interval = TimeSpan.FromMilliseconds(500);
            zamanlayici.Start();
            zamanlayici.Tick += (sender, args) =>
            {
                zamanlayici.Stop();
                kart_yerlestir(); //kartlar ekrana bastirilir     

                //Kişiye ait olan double click olayları silinir.
                for (int i = 0; i < kisiler[0]._Kartlar.Count; i++)
                {
                    kisiler[0]._Kartlar[i]._KartKapak.MouseDoubleClick -= _KartKapak_MouseDoubleClick;

                }

                //Yerdeki kart sayisi üç ise kartı alan kullanıcı belirlenir.
                if (oyun._Yerdeki_Kartlar.Count == 3)
                {
                    b = 0;
                    Kisi en_buyuk = oyun.en_buyuk_kart();
                    en_buyuk._AldigiKart++; //Kartı alan kullanıcının kartı verilir.
                    oyun.yerdeki_kartlari_al(kisiler.IndexOf(en_buyuk), Cerceve); //Kartı alan kullanıcı için işlem yapılır.
                    kisi_aldigi_kart_sayisini_goster(kisiler.IndexOf(en_buyuk));
                    oyun._Sira = kisiler.IndexOf(en_buyuk);
                    oyun._Oyun_Sayisi++;
                }
                
                if (oyun._Oyun_Sayisi == 16)
                {
                    kisiler = oyun.kullanici_puan_ver(kisiler); //Kişilere alması gereken puanlar atanır.
                    el_baslat();
                    return;
                }

                if (oyun._Sira == 0)
                {
                    List<Kart> atilabilir_kartlar = oyun.atilabilir_kartlar(kisiler[0]);
                    oyun.atilabilir_kartlar_konumlandir(atilabilir_kartlar);
                    foreach (var kart in atilabilir_kartlar)
                    {
                        kart._KartKapak.MouseDoubleClick += _KartKapak_MouseDoubleClick;
                    }
                }
                else
                {
                    kisiler[oyun._Sira] = oyun.bilgisayar_kart_at(kisiler[oyun._Sira], b);
                    b++;
                    el_oyna(); // El oyna fonksiyonu tekrar çağırılır.
                    return;
                }
            };
        }


        //Kullanıcının koz seçimini sağlayan fonksiyon.
        void koz_sec() {
            Koz_Sec koz_sec = new Koz_Sec();
            Cerceve.Children.Add(koz_sec);
            koz_sec.Margin = new Thickness(247, 229, 0, 0);
            koz_sec.Height = 114;
            koz_sec.Width = 500;
            koz_sec.Karo.Click += (sender2, e2) => _Koz_Secim_Yap(sender2, e2, koz_sec);
            koz_sec.Maca.Click += (sender2, e2) => _Koz_Secim_Yap(sender2, e2, koz_sec);
            koz_sec.Sinek.Click += (sender2, e2) => _Koz_Secim_Yap(sender2, e2, koz_sec);
            koz_sec.Kupa.Click += (sender2, e2) => _Koz_Secim_Yap(sender2, e2, koz_sec);    
        }

        //Doubleclick olayında kart yere atılır.
        void _KartKapak_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Kart_Kapak kart_kapak = (Kart_Kapak)sender;
            Canvas.SetZIndex(kart_kapak, b);
            oyun.kart_animasyon(kart_kapak, b);
            kisiler[0] = oyun.kart_at(kisiler[0],kart_kapak._Kart);
            b++;
            el_oyna();
            return;
        }

        //Kullanıcının kagit seçmesi işlemini yapan event. Kağıda tıklandığında değişim kartlarına atanır.
        void _KartKapak_KagitSec(object sender, MouseButtonEventArgs e)
        {
            //Gelen sender'in Kart_Kapat türüne dönüştürülmüştür.
            Kart_Kapak tiklandi = (Kart_Kapak)sender;
            //Eğer ekle ise eklenmemiş demektir
            if (tiklandi.Tag == (Object)"Ekle")
            {
                //Degişim kartları kullanıcının seçtiği kartları tutmaktadır. En fazla 4 kart seçilebilir.
                if (degisim_kartlari.Count == 4)
                {
                    MessageBox.Show("Değişim için 4 kart seçtiniz. Kart seçebilmek için seçtiklerinizden birini silmelisiniz.");
                }
                else {
                    degisim_kartlari.Add(tiklandi._Kart);
                    tiklandi.Margin = new Thickness(tiklandi.Margin.Left, tiklandi.Margin.Top - 30, tiklandi.Margin.Right, tiklandi.Margin.Bottom);
                    tiklandi.Tag = "Sil";
                } 
            }
            else 
            {
                if (degisim_kartlari.Count == 0)
                {
                    MessageBox.Show("Henüz hiç kart seçmediniz.");
                }
                else {
                    degisim_kartlari.Remove(tiklandi._Kart);
                    tiklandi.Margin = new Thickness(tiklandi.Margin.Left, tiklandi.Margin.Top + 30, tiklandi.Margin.Right, tiklandi.Margin.Bottom);
                    tiklandi.Tag = "Ekle";
                }
            }
        }

        //Verilen kişi ile yerdeki dört kağıdın yerini değiştiren fonksiyon
        public void dort_kart_degis(Kisi kisi){
                for (int i = 0; i < 16; i++)
                {
                    Cerceve.Children.Remove(kisi._Kartlar[i]._KartKapak);
                }
                for (int i = 0; i <4 ; i++)
                {
                    Cerceve.Children.Remove(deste._Deste[i]._KartKapak);
                }

                kisi = deste.kart_degis(kisi, degisim_kartlari);
                degisim_kartlari.Clear();
                kisi = deste.kart_sirala(kisi); //kisi siralanir 
                

                for (int i = 0; i < 16; i++)
                {
                    kisi._Kartlar[i]._Resim_Yolu = "kartlar/" + kisi._Kartlar[i]._Numara.ToString() + ".png";
                    //Eğer kullanıcı oyuncu ise kartları düz değilse arka yüzü gösterilir.
                    if (kisi == kisiler[0])
                    {
                        kisi._Kartlar[i]._KartKapak._Kaynak = "kartlar/" + kisi._Kartlar[i]._Numara.ToString() + ".png";
                    }
                    else {
                        kisi._Kartlar[i]._KartKapak._Kaynak = "kartlar/999.png";
                    }
                    kart_yerlestir(); // Kullanıcı kartları ekrana yerleştirilir.
                    kisi._Kartlar[i]._KartKapak.MouseDoubleClick -= _KartKapak_KagitSec;
                }                       
        }

        //Kullanıcının 4 kart seçtikten sonra kağıt değiş butonuna tıklaması. Bu durumda yerdeki 4 kart ile seçilen 4 kart değiştirilir.
        private void _Kagit_Degis_Click_1(object sender, RoutedEventArgs e)
        {
            if (degisim_kartlari.Count != 4)
            {
                MessageBox.Show("Lütfen 4 Kagit Seçtikten Sonra Bu İşlemi Yapınız.");
            }
            else
            {
                _Kart_Degis.Visibility = System.Windows.Visibility.Hidden;
                dort_kart_degis(kisiler[0]);
                dort_kart_yerlestir(true); //Kullanıcının verdigi 4 kart yerlestirilir.
                el_oyna();
                return;
            }
        }

        //Kullanıcının koz seçmesi olayı. Eğer koz onaylanırsa koz seçimi tamamlanmıştır.
        private void _Koz_Secim_Yap(object sender, RoutedEventArgs e, Koz_Sec koz_sec)
        {
            //Kişiye ait olan double click olayları silinir.
            for (int i = 0; i < kisiler[0]._Kartlar.Count; i++)
            {
                kisiler[0]._Kartlar[i]._KartKapak.MouseDoubleClick -= _KartKapak_MouseDoubleClick;

            }

            if (MessageBox.Show("Seçtiğiniz Kozu Onaylıyor musunuz?", "Koz Seçimi", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Koz seçimi onaylanır
                Button btn = (Button)sender;
                oyun._Koz = Convert.ToInt16(btn.Tag);
                Cerceve.Children.Remove(koz_sec);
                koz_header_belirle(); //Koz erkana basılır
                skor_ihale_goster(); // Kullanıcıların ihaleleri basılır.
                //kagit degis  butonunu goster
                for (int i = 0; i < 16; i++)
                {
                    //Sıra kullanıcıda olduğu için ilk kagit seçme eventi atanır.
                    kisiler[0]._Kartlar[i]._KartKapak.MouseDoubleClick += _KartKapak_KagitSec;
                    kisiler[0]._Kartlar[i]._KartKapak.Tag = "Ekle";
                }
                Cerceve.Children.Add(_Kart_Degis);
                _Kart_Degis.Visibility = System.Windows.Visibility.Visible;
            }
        }


        //Ekrana seçilen koza göre kozun resmi bastırılır.
        public void koz_header_belirle(){
            Image _Koz = new Image();
            _Koz.Height = 54;
            _Koz.Width = 50;
            _Koz.Margin = new Thickness(467,33,0,0);
            string _Koz_Source = "";
            switch (oyun._Koz)
            {
                case 1:
                    _Koz_Source = "kartlar/kart_tipleri/sinek.png";
                break;

                case 2:
                    _Koz_Source = "kartlar/kart_tipleri/karo.png";
                break;

                case 3:
                    _Koz_Source = "kartlar/kart_tipleri/maca.png";
                break;

                case 4:
                    _Koz_Source = "kartlar/kart_tipleri/kupa.png";
                break;
            }
            var uriKaynak = new Uri(_Koz_Source, UriKind.Relative);
            _Koz.Source = new BitmapImage(uriKaynak);
            Cerceve.Children.Add(_Koz);
        }
    }
}
