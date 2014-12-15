using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_5_8
{
    public class Kisi
    {
        private List<Kart> kartlar = new List<Kart>();
        private string isim = "";
        private int ihale;
        private int puan = 0;
        private int aldigi_kart = 0;

        public int _AldigiKart {
            get { return aldigi_kart; }
            set { aldigi_kart = value; }
        }

        public Kisi(string isim) 
        {
            this.isim = isim;
        }

        //Puan prop
        public int _Puan 
        { 
            get { return puan; }
            set { puan = value; } 
        }

        //Kullanıcı kart prop
        public List<Kart> _Kartlar
        {
            get { return kartlar; }
            set { kartlar = value; }
        }

        public string _Isım {
            get { return isim; }
            set { isim = value; }
        }

        public int _Ihale
        {
            get { return ihale; }
            set { ihale = value; }
        }

        //Kişinin verdiği 4 kağit ile yerdeki 4 kağıdı değişen fonksiyon
        public Deste kagit_degis(int[] secili_kartlar, Deste deste){
            for (int i = 0; i < 4; i++)
            {
                Kart tmp = this._Kartlar[secili_kartlar[i]];
                this._Kartlar[secili_kartlar[i]] = deste._Deste[i];
                deste._Deste[i] = tmp;
            }
            return deste;
        }


    }
}
