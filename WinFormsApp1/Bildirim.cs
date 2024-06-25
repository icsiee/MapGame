using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class Bildirim
    {

        public string Metin { get; set; }
        public int x, y;

        public Bildirim(string metin,int x,int y)
        {
            Metin = metin;

            this.x = x;
            this.y = y;
        }


    }
}
