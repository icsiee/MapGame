using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{

    public class Lokasyon
    {
        // Özellikler
        private float X { get; set; }
        private float Y { get; set; }

        // Constructor
        public Lokasyon(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

}

