using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _3_5_8
{
    public class Deste
    {

        private List<Kart> deste;
        private Random rastgele = new Random();
        
        //deste prop
        public List<Kart> _Deste
        {
            get { return deste; }
        }
        
        public Deste()
        {
            deste = new List<Kart>();
            deste_kar();
        }
        
        // Deste oluşturulmasını sağlayan fonksiyon.
        private void deste_olustur() {
            for (int i = 1; i < 5; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    Kart kart = new Kart();
                    kart._Numara = (i * 100) + j;
                    kart._Resim_Yolu = "kartlar/"+kart._Numara.ToString() + ".png";
                    Kart_Kapak kart_kapak = new Kart_Kapak();
                    kart_kapak._Kaynak = kart._Resim_Yolu;
                    kart._KartKapak = kart_kapak;
                    deste.Add(kart);
                }
            }
        }


        //Oluşturulan destenin karılmasını sağlayan fonksiyon
        public void deste_kar() {
            deste_olustur(); // deste olustur cagirildi.
            for (int i = 0; i < 40; i++)
            {
                int rastgele_sayi = rastgele.Next(52);
                Kart gecici = deste[rastgele_sayi];
                deste[rastgele_sayi] = deste[i];
                deste[i] = gecici;
            }
        }

        //Kullanıcılara kart dağıtımını yapan fonksiyon
        public List<Kisi> kart_dagit(List<Kisi> kisi, Deste deste) {
           
            for (int j = 0; j < kisi.Count; j++)
            {
                kisi[j]._Kartlar = new List<Kart>();  //her seferinde kisinin kartlarini sifirliyoruz
                for(int i=0; i<16;i++){
                    kisi[j]._Kartlar.Add(deste._Deste[0]); // kisiye 0ın eleman verildi
                    deste._Deste.RemoveAt(0); // desteden sıfırıncı eleman silindi.

                    if (j != 0)
                    {
                        //Bilgisayar kartlarını kullanıcıya gostermiyoruz.
                        kisi[j]._Kartlar[i]._KartKapak._Kaynak = "kartlar/999.png";
                    }

                      
                    kisi[j]._Kartlar[i]._KartKapak._Kart = kisi[j]._Kartlar[i];
                }
            }
                return kisi;
        }

        //Tüm oyuncuların kartlarının sıralanmasını sağlayan fonksiyon
        public List<Kisi> kisiler_kart_sirala(List<Kisi> kisiler)
        {
            for (int k = 0; k < kisiler.Count; k++)
            {
                for (int i = 0; i < 16; i++)
                {
                    for (int j = i; j < 16; j++)
                    {
                        if (kisiler[k]._Kartlar[i]._Numara > kisiler[k]._Kartlar[j]._Numara)
                        {
                            Kart tmp = kisiler[k]._Kartlar[i];
                            kisiler[k]._Kartlar[i] = kisiler[k]._Kartlar[j];
                            kisiler[k]._Kartlar[j] = tmp;
                        }
                    }
                }
            }
            return kisiler;
        }


        //Oyunucunun elindeki kartların sıralanmasını sağlayan fonksiyon
        public Kisi kart_sirala(Kisi kisi) {
            for (int i = 0; i < 16; i++)
            {
                for (int j = i; j < 16; j++)
                {
                    if (kisi._Kartlar[i]._Numara > kisi._Kartlar[j]._Numara)
                    {
                        Kart tmp = kisi._Kartlar[i];
                        kisi._Kartlar[i] = kisi._Kartlar[j];
                        kisi._Kartlar[j] = tmp;
                    }
                }
            }
            return kisi;
        }

        //Verilen değişim kartları ile destede kalan son 4 kartın değişimini sağlayan fonksiyon
        public Kisi kart_degis(Kisi kisi, List<Kart> degisim_kartlari)
        {
            for (int i = 0; i < 4; i++)
            {
                kisi._Kartlar.Remove(degisim_kartlari[i]);
                kisi._Kartlar.Add(deste[i]);
                deste[i] = degisim_kartlari[i];
            }
            return kisi;
        }
    }
}
