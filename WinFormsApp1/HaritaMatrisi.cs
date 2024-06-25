using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class HaritaMatrisi
    {
        private int[,] matris;

        public HaritaMatrisi(int haritaBoyutu1, int haritaBoyutu2, int kareBoyutu)
        {
            int kareSayisi1 = (int)haritaBoyutu1 / kareBoyutu;
            int kareSayisi2 = (int)haritaBoyutu2 / kareBoyutu;

            // Matrisi oluştur
            matris = new int[kareSayisi1, kareSayisi2];

            // Tüm elemanları başlangıçta 0 olarak ayarla
            for (int i = 0; i < kareSayisi1; i++)
            {
                for (int j = 0; j < kareSayisi2; j++)
                {
                    matris[i, j] = 0;
                }
            }
        }

        // Matris elemanlarına erişim için indeksleyici
        public int this[int i, int j]
        {
            get { return matris[i, j]; }
            set { matris[i, j] = value; }
        }

        // Matrisi doğrudan geri döndüren metot
        public int[,] GetMatris()
        {
            return matris;
        }

    }

}
