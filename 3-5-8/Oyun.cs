using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace _3_5_8
{
    class Oyun
    {
        public static int el_sayisi=-1; //Oyundaki kaçıncı elde olduğunu belirler.
        public static int oyun_sayisi = 0; 
        public static int oyun_eli = 2; //oyunun kaç el olacağını belirler.
        private int sira = 0;
        private int koz;

        public struct Yerdeki_Kart {
            public Kart kart;
            public Kisi kisi;
            public int kart_degeri;
        }

        private List<Yerdeki_Kart> yerdeki_kartlar = new List<Yerdeki_Kart>();

        public List<Yerdeki_Kart> _Yerdeki_Kartlar
        {
            get { return yerdeki_kartlar; }
            set { yerdeki_kartlar = value; }
        }

        public int _Oyun_Eli
        {
            get { return oyun_eli; }
            set { oyun_eli = value; }
        }

        public int _Oyun_Sayisi
        {
            get { return oyun_sayisi; }
            set { oyun_sayisi = value; }
        }

        public int _El_Sayisi
        {
            get { return el_sayisi; }
            set { el_sayisi = value; }
        }

        public int _Sira
        {
            get {  return sira; }
            set { sira = value; }
        }

        public int _Koz
        {
            get { return koz; }
            set { koz = value; }
        }

        // Ayni tip kontrol eden fonksiyon ve atilabilir kart var ise bunu döndüren fonksiyon. 
        // Eğer aynı tip kart var ise büyük elinde büyük yoksa küçük kağıt atılır.
        public bool ayni_atilabilir_kartlar(Kisi kisi, out List<Kart> atilabilir_kartlar)
        {
            atilabilir_kartlar = new List<Kart>();
            bool buyuk_varmi = false;
            Kart yerdeki = yerdeki_kartlar[0].kart;
            if (yerdeki_kartlar.Count > 1)
            {
                if (yerdeki_kartlar[0].kart._Numara / 100 == yerdeki_kartlar[1].kart._Numara / 100) //yerdeki kartlar aynı ise
                    if (yerdeki_kartlar[1].kart._Numara % 100 > yerdeki_kartlar[0].kart._Numara % 100) //1.kart 0.inci karttan buyuk ise
                        yerdeki = yerdeki_kartlar[1].kart;

            }
            int kart_tipi = yerdeki._Numara / 100;
            for (int i = 0; i < kisi._Kartlar.Count; i++)
            {
                if (kart_tipi == kisi._Kartlar[i]._Numara / 100)
                {
                    if (buyuk_varmi)
                    {
                        if (kisi._Kartlar[i]._Numara > yerdeki._Numara)
                            atilabilir_kartlar.Add(kisi._Kartlar[i]);
                    }
                    else
                    {
                        if (kisi._Kartlar[i]._Numara > yerdeki._Numara)
                        {
                            buyuk_varmi = true;
                            atilabilir_kartlar.Clear();
                        }
                        atilabilir_kartlar.Add(kisi._Kartlar[i]);
                    }
                }
            }

            atilabilir_kartlar = atilabilir_kartlar.OrderBy(o => o._Numara).ToList();
            if (atilabilir_kartlar.Count > 0)
                return true;
            else
                return false;
        }

        //Yerdeki kağıt sayısı bir ise atilabilecek koz olan kartlar
        public bool koz_atilabilir_kartlar(Kisi kisi, out List<Kart> atilabilir_kartlar)
        {
            atilabilir_kartlar = new List<Kart>();
            //Yerde iki kart var demektir.
            if (yerdeki_kartlar.Count > 1)
            {
                //Yerdeki 2.kart koz demektir.
                if (yerdeki_kartlar[1].kart._Numara / 100 == koz)
                {
                    for (int i = 0; i < kisi._Kartlar.Count; i++)
                    {
                        //ELindeki kart koz mu değil mi
                        if (kisi._Kartlar[i]._Numara / 100 == koz && kisi._Kartlar[i]._Numara % 100 > yerdeki_kartlar[1].kart._Numara % 100)
                        {
                            atilabilir_kartlar.Add(kisi._Kartlar[i]);
                        }
                    }
                    atilabilir_kartlar = atilabilir_kartlar.OrderBy(o => o._Numara).ToList();
                    if (atilabilir_kartlar.Count > 0)
                        return true;
                    else
                        return false;
                }
            }

            for (int i = 0; i < kisi._Kartlar.Count; i++)
            {
                //ELindeki kart koz mu değil mi
                if (kisi._Kartlar[i]._Numara / 100 == koz)
                {
                    atilabilir_kartlar.Add(kisi._Kartlar[i]);
                }
            }

            atilabilir_kartlar = atilabilir_kartlar.OrderBy(o => o._Numara).ToList();
            if (atilabilir_kartlar.Count > 0)
                return true;
            else
                return false;
        }

        // Kişinin elinde hem aynı tip hemde koz olmaması durumunda çalışacak fonksiyon.
        public List<Kart> farkli_atilabilir_kartlar(Kisi kisi)
        {
            List<Kart> atilabilir_kartlar = new List<Kart>();
            atilabilir_kartlar = atilabilir_kartlar.OrderBy(o => (o._Numara % 100)).ToList();
            for (int i = 0; i < kisi._Kartlar.Count; i++)
            {
                atilabilir_kartlar.Add(kisi._Kartlar[i]);
            }
            atilabilir_kartlar = atilabilir_kartlar.OrderBy(o => (o._Numara % 100)).ToList();
            return atilabilir_kartlar;
        }


        //Atılabilir kartlar atılabilir olan kartları döndürür. Yukarıdaki fonksiyonları kullanarak buna karar verir.
        public List<Kart> atilabilir_kartlar(Kisi kisi) 
        {
            List<Kart> atilabilir_kartlar = new List<Kart>();
            if (yerdeki_kartlar.Count == 0)
            {
                //İlk kagit atiliyorsa elindeki tüm kartlar atılabilir.
                for (int i = 0; i < kisi._Kartlar.Count; i++)
                {
                    atilabilir_kartlar.Add(kisi._Kartlar[i]);
                }
            }
            //yerdeki kart sayisi 1 veya 2 ise bu koşul çalışır.
            else 
            {
                if (ayni_atilabilir_kartlar(kisi, out atilabilir_kartlar))
                {
                    return atilabilir_kartlar;
                }
                else if (koz_atilabilir_kartlar(kisi, out atilabilir_kartlar))
                {
                    return atilabilir_kartlar;
                }
                else {
                    //Elinde koz veya aynı tip kart bulunmamasi durumu
                    atilabilir_kartlar = farkli_atilabilir_kartlar(kisi);
                }
            }

            return atilabilir_kartlar;
        }

        //Kullanıcının seçtiği kartın atımını yapan fonksiyon
        public Kisi kart_at(Kisi kisi, Kart kart){
            Yerdeki_Kart atilan_kart = new Yerdeki_Kart();
            atilan_kart.kisi = kisi;
            atilan_kart.kart = kart;
            // atilan kara deger atanir.
            atilan_kart.kart_degeri = kart_degeri_ata(kart);
            yerdeki_kartlar.Add(atilan_kart);
            kisi._Kartlar.Remove(kart);
            sira = (sira + 1) % 3;
            return kisi;
        }

        //Bilgisayarın kart atmasını sağlayan fonksiyon
        public Kisi bilgisayar_kart_at(Kisi kisi, int b) 
        {
            List<Kart> bil_atilabilir_kartlar = atilabilir_kartlar(kisi);
            Kart atilacak_kart;

            //Burada eğer bilgisayar yerdeki ilk kartı atıyorsa rastgele bir sayı atıldı.
            if (yerdeki_kartlar.Count == 0)
            {
                Random rnd = new Random();
                int sayi = rnd.Next(0, bil_atilabilir_kartlar.Count);
                atilacak_kart = bil_atilabilir_kartlar[sayi];   
            }
            else
            {
                atilacak_kart = bil_atilabilir_kartlar[0];       
            }

            Canvas.SetZIndex(atilacak_kart._KartKapak, b);
            atilacak_kart._KartKapak._Kaynak = "kartlar/" + atilacak_kart._Numara.ToString() + ".png";
            kart_animasyon(atilacak_kart._KartKapak, b);
            Yerdeki_Kart atilan_kart = new Yerdeki_Kart();
            atilan_kart.kisi = kisi;

            atilan_kart.kart = atilacak_kart;
            atilan_kart.kart_degeri = kart_degeri_ata(atilacak_kart); //Kartın değeri atanır.            

            yerdeki_kartlar.Add(atilan_kart);
            kisi._Kartlar.Remove(atilan_kart.kart);
            sira = (sira + 1) % 3;
            return kisi;
        }


        //Bilgisayar kozu belirledikten sonra elindeki en kötü 4 kağıt ile yerdeki kağıtları değiştirir.
        public List<Kart> bilgisayar_dort_kart_degis(Kisi kisi) {
            List<Kart> degisecek_kartlar = new List<Kart>();
            for (int i = 0; i < kisi._Kartlar.Count; i++)
            {
                if (kisi._Kartlar[i]._Numara / 100 != koz)
                {
                    if (degisecek_kartlar.Count != 4)
                    {
                        degisecek_kartlar.Add(kisi._Kartlar[i]);
                    }
                    else {
                        Kart enbuyuk = degisecek_kartlar[0];
                        for (int j = 0; j < degisecek_kartlar.Count; j++)
                        {
                            if (degisecek_kartlar[j]._Numara % 100 > enbuyuk._Numara % 100)
                            {
                                enbuyuk = degisecek_kartlar[j];
                            }
                        }
                        if (kisi._Kartlar[i]._Numara % 100  < enbuyuk._Numara % 100)
                        {
                            degisecek_kartlar.Remove(enbuyuk);
                            degisecek_kartlar.Add(kisi._Kartlar[i]);
                        }
                    }   
                }
            }
            return degisecek_kartlar;
        }

        //Koz belirleme sırası bilgisayarda ise bu fonksiyon ile elindeki 4 kartı yere bırakarak yerdeki 4 kartı alır.
        public void bilgisayar_koz_belirle(Kisi kisi)
        {
            int[] dizi = new int[4];
            for (int i = 0; i < kisi._Kartlar.Count; i++)
            {
                dizi[(kisi._Kartlar[i]._Numara / 100) - 1] += (kisi._Kartlar[i]._Numara % 100);
            }

            int enbuyuk = 0;
            for (int i = 0; i < dizi.Length; i++)
            {
                if (dizi[i] > dizi[enbuyuk])
                {
                    enbuyuk = i;
                }
            }
            this.koz = enbuyuk + 1;
        }

        //Atılan karta değer ataması yapan fonksiyondur. Bu değer ataması ile kartı kimin alacağı belirlenir.
        public int kart_degeri_ata(Kart kart){
            int kart_degeri = 0;
            // Atılan kart koz ise kart değerinin 40 fazlası eklenir.
            if (kart._Numara / 100 == koz)
            {
                kart_degeri = kart._Numara + 800;
            }
            else if (yerdeki_kartlar.Count == 0)
            {
                kart_degeri =  kart._Numara + 400;
            }//yere atılan ilk kart ile atılacak olan kartın tipi aynı ise 20 eklenir.
            else if(yerdeki_kartlar[0].kart._Numara / 100 == kart._Numara / 100){
                kart_degeri = kart._Numara + 400;
            }
            //Atılan kart bu ikisine uymuyorsa kart değer kart numarası olarak belirlenir.
            else
            {
                kart_degeri = kart._Numara;
            }
            return kart_degeri;
        }

        //Atılabilir olan kartlar 20 yukarı kaydırılarak kullanıcıya gösterilir.
        public void atilabilir_kartlar_konumlandir(List<Kart> atilabilir_kartlar) {
            foreach (var kart in atilabilir_kartlar)
            {
                kart._KartKapak.Margin = new Thickness(kart._KartKapak.Margin.Left, kart._KartKapak.Margin.Top-20, 0, 0);
            }
        }


        //Yerdeki kartları alan kullanıcıya gönderen ve temizlenmesini sağlayan fonksiyon.
        public void yerdeki_kartlari_al(int sira, Canvas Cerceve) {
            double sol = 0, yukari = 0;
            //Buradaki sira yerdeki kağıtları kimin aldığını belirler.
            if (sira == 2)
            {
                sol = -800;
            }
            else if (sira == 1)
            {
                sol = 800;
            }
            else
            {
                yukari = 800;
            }

            for (int i = 0; i < yerdeki_kartlar.Count; i++)
            {
                Cerceve.Children.Remove(yerdeki_kartlar[i].kart._KartKapak); //Yerdeki kartlar temizlenir.
                Kart_Kapak kart_kapak = new Kart_Kapak(); //daha sonra tekrar oluşturulur
                kart_kapak._Kaynak = yerdeki_kartlar[i].kart._Resim_Yolu;
                Canvas.SetZIndex(kart_kapak, (i+1)*100); //ayarlamaları yapılır.
                yerdeki_kartlar[i].kart._KartKapak = kart_kapak;
                kart_kapak.Margin = new Thickness((419 + i * 20), 239, 0, 0);
                Cerceve.Children.Add(kart_kapak); //Cerceveye eklenir

                Storyboard sb = new Storyboard(); //Animasyonlu bir şekilde alan kişiye kaydırılır.
                DoubleAnimation da = new DoubleAnimation(0, sol, new Duration(new TimeSpan(0, 0, 0, 0, 1000))); //left
                DoubleAnimation db = new DoubleAnimation(0, yukari, new Duration(new TimeSpan(0, 0, 0, 0, 1000))); //top
                Storyboard.SetTargetProperty(da, new PropertyPath("(Canvas.Left)"));
                Storyboard.SetTargetProperty(db, new PropertyPath("(Canvas.Top)"));
                sb.Children.Add(da);
                sb.Children.Add(db);
                yerdeki_kartlar[i].kart._KartKapak.BeginStoryboard(sb);
            }
            yerdeki_kartlar.Clear();
        }

        //Kartı anımasyonlu bir şekilde taşıyan fonksiyon
        public void kart_animasyon(Kart_Kapak kart_kapak, int b) {
            double sol = 0 , yukari = 0;
            sol = (419 + b * 20) - kart_kapak.Margin.Left;
            yukari = 239 - kart_kapak.Margin.Top ;
            DoubleAnimation da = new DoubleAnimation(0, sol, new Duration(new TimeSpan(0, 0, 0, 0, 500))); //left
            DoubleAnimation db = new DoubleAnimation(0, yukari, new Duration(new TimeSpan(0, 0, 0, 0, 500))); //top
            Storyboard.SetTargetProperty(da, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(db, new PropertyPath("(Canvas.Top)"));
            Storyboard sb = new Storyboard();
            sb.Children.Add(da);
            sb.Children.Add(db);
            kart_kapak.BeginStoryboard(sb);
        }

        //Yerdeki en büyük karta sahip olan kişiyi geri döndüren fonksiyon. Buna göre puan alır.
        public Kisi en_buyuk_kart()
        {
            Yerdeki_Kart en_buyuk = yerdeki_kartlar[0];
            for (int i = 1; i < yerdeki_kartlar.Count; i++)
            {
                if (yerdeki_kartlar[i].kart_degeri > en_buyuk.kart_degeri)
                {
                    en_buyuk = yerdeki_kartlar[i];
                }
            }
            return en_buyuk.kisi;
        }

        // Kisiler ile ilgili ihaleleri belirleme metodu
        public List<Kisi> ihale_belirle(List<Kisi> kisiler)
        {
            kisiler[sira]._Ihale = 8;
            kisiler[(sira + 1) % 3]._Ihale = 5;
            kisiler[(sira + 2) % 3]._Ihale = 3;
            return kisiler;
        }

        //Kişilerin aldığı karta göre puanını dağıtan fonksiyon.
        public List<Kisi> kullanici_puan_ver(List<Kisi> kisiler) {
            for (int i = 0; i < kisiler.Count; i++)
            {
                if (kisiler[i]._Ihale <= kisiler[i]._AldigiKart)
                {
                    kisiler[i]._Puan += kisiler[i]._AldigiKart;
                }
                else {
                    kisiler[i]._Puan -= kisiler[i]._Ihale;
                }
                kisiler[i]._AldigiKart = 0;
            }
            return kisiler;
        }

    }
}
