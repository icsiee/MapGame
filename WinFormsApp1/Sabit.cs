using System.Drawing;

namespace WinFormsApp1
{
    internal class Sabit : Engel
    {
        private Color Renk { get; set; }
        private int ID { get; set; }

        public Sabit(string engelAdi, Point konum, int boyut, Color renk, int id)
            : base(engelAdi, konum, boyut)
        {
            Renk = renk;
            ID = id;
        }
    }
}
