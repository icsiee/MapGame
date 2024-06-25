using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class Sandık
    {

        private string sandikAdi { get; set; }
        private Point sandikKonumu { get; set; }
        private int Boyut { get; set; }

        public Sandık(string engelAdi, Point konum, int boyut)
        {
            sandikAdi = engelAdi;
            sandikKonumu = konum;
            Boyut = boyut;
        }


    }
}
