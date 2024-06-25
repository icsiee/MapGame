using System.Drawing;

namespace WinFormsApp1
{
    internal class Dinamik : Engel
    {
        private Color Renk { get; set; }
        private int Hiz { get; set; }
        private int ID { get; set; }
        private string Tur { get; set; }

        public Dinamik(string engelAdi, Point konum, int boyut, Color renk, int hiz, int id, string tur)
            : base(engelAdi, konum, boyut)
        {
            Renk = renk;
            Hiz = hiz;
            ID = id;
            Tur = tur;
        }
    }
}
