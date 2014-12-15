using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_5_8
{
    public class Kart
    {
        private int numara;
        private string resim_yolu = "";
        private Kart_Kapak kk;

        public int _Numara
        {
            get { return numara; }
            set { numara = value; }
        }

        public Kart_Kapak _KartKapak
        {
            get { return kk; }
            set { kk = value; }
        }

        public string _Resim_Yolu
        {
            get { return resim_yolu; }
            set { resim_yolu = value; }
        }
    }
}
