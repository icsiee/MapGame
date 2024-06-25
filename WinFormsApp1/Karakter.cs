using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class Karakter
    {
        // Özellikler
        public string id { get; set; }
        public string isim { get; set; }

        private float karakterX, karakterY; // Karakterin konumunu saklamak için değişkenler

        private Random random = new Random(); // Rastgele konum için Random nesnesi
        public Lokasyon karakterLokasyon { get; set; }


        // Constructor
        public Karakter(string id, string isim, float karakterX, float karakterY)
        {
            this.id = id;
            this.isim = isim;

            this.karakterX = karakterX;
            this.karakterY = karakterY;
            this.karakterLokasyon = new Lokasyon(0, 0);

        }

        public void GuncelleKarakter(PictureBox karakterPictureBox, float karakterX, float karakterY, int kareBoyutu)
        {

            // Karakterin yeni konumunu hesapla ve güncelle
            Point yeniKonum = new Point((int)karakterX * kareBoyutu, (int)karakterY * kareBoyutu); // Kare boyutunu dikkate al
            karakterPictureBox.Location = yeniKonum; // Karakterin yeni konumunu ayarla
            karakterPictureBox.BringToFront(); // Karakteri diğer bileşenlerin önüne getir


        }

        public int EnKisaYolFormulu(int[,] haritaMatris)
        {
            int[,] mesafeMatrisi = new int[haritaMatris.GetLength(0), haritaMatris.GetLength(1)];

            int enYakinSandik = 0;
            int enUzakSandik = 0;
            bool ilkSandik = true;

            for (int i = 0; i < haritaMatris.GetLength(0); i++)
            {
                for (int j = 0; j < haritaMatris.GetLength(1); j++)
                {
                    if (haritaMatris[i, j] >= 2 && haritaMatris[i, j] <= 5)
                    {
                        if (ilkSandik)
                        {
                            enYakinSandik = haritaMatris[i, j];
                            enUzakSandik = haritaMatris[i, j];
                            ilkSandik = false;
                        }
                        else
                        {
                            enYakinSandik = Math.Min(enYakinSandik, haritaMatris[i, j]);
                            enUzakSandik = Math.Max(enUzakSandik, haritaMatris[i, j]);
                        }
                    }
                }
            }

            for (int i = 0; i < haritaMatris.GetLength(0); i++)
            {
                for (int j = 0; j < haritaMatris.GetLength(1); j++)
                {
                    mesafeMatrisi[i, j] = int.MaxValue;
                }
            }

            for (int i = 0; i < haritaMatris.GetLength(0); i++)
            {
                for (int j = 0; j < haritaMatris.GetLength(1); j++)
                {
                    if (haritaMatris[i, j] == enYakinSandik)
                    {
                        mesafeMatrisi[i, j] = 0;
                        Dijkstra(haritaMatris, mesafeMatrisi, i, j, enYakinSandik);
                    }
                }
            }

            return mesafeMatrisi[enUzakSandik, enUzakSandik];
        }

        private void Dijkstra(int[,] harita, int[,] mesafe, int baslangicX, int baslangicY, int sandik)
        {
            int[] hareketX = { 0, 1, 0, -1 };
            int[] hareketY = { -1, 0, 1, 0 };
            int satirSayisi = harita.GetLength(0);
            int sutunSayisi = harita.GetLength(1);
            bool[,] ziyaretEdildi = new bool[satirSayisi, sutunSayisi];

            // Dijkstra algoritması
            for (int i = 0; i < satirSayisi * sutunSayisi - 1; i++)
            {
                int minMesafe = int.MaxValue;
                int minIndexX = -1;
                int minIndexY = -1;

                // En kısa mesafeli elemanı bul
                for (int x = 0; x < satirSayisi; x++)
                {
                    for (int y = 0; y < sutunSayisi; y++)
                    {
                        if (!ziyaretEdildi[x, y] && mesafe[x, y] < minMesafe)
                        {
                            minMesafe = mesafe[x, y];
                            minIndexX = x;
                            minIndexY = y;
                        }
                    }
                }

                if (minIndexX == -1 || minIndexY == -1)
                    break;

                ziyaretEdildi[minIndexX, minIndexY] = true;

                // Komşuları kontrol et
                for (int k = 0; k < 4; k++)
                {
                    int yeniX = minIndexX + hareketX[k];
                    int yeniY = minIndexY + hareketY[k];

                    if (yeniX >= 0 && yeniX < satirSayisi && yeniY >= 0 && yeniY < sutunSayisi &&
                        harita[yeniX, yeniY] == sandik &&
                        !ziyaretEdildi[yeniX, yeniY] &&
                        mesafe[minIndexX, minIndexY] + 1 < mesafe[yeniX, yeniY])
                    {
                        mesafe[yeniX, yeniY] = mesafe[minIndexX, minIndexY] + 1;
                    }
                }
            }


        }






    }

}
