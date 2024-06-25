using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class HaritaFormu : Form
    {
        private int haritaBoyutu1;
        private int haritaBoyutu2;
        private int karakterX, karakterY;
        private Random random = new Random();
        private Panel[,] karePanels;
        public static int kareBoyutu = 25;
        public Karakter karakter;
        private List<Engel> engeller = new List<Engel>();
        private List<Bildirim> bildirimler = new List<Bildirim>();
        private HaritaMatrisi haritaMatris; // HaritaMatrisi nesnesini tanımla
        public int sandikSayisi = 0;
       

        public HaritaFormu(int haritaBoyutu1, int haritaBoyutu2, Karakter karakter)
        {
            InitializeComponent();
            this.karakter = karakter;
            this.haritaBoyutu1 = haritaBoyutu1;
            this.haritaBoyutu2 = haritaBoyutu2;
            karakterPictureBox.Size = new Size(kareBoyutu, kareBoyutu);


            // Harita matrisini oluştur
            haritaMatris = new HaritaMatrisi(haritaBoyutu1, haritaBoyutu2, kareBoyutu);

            // ListBox'un boyutunu ve konumunu belirle
            listBoxBildirimler.Size = new Size(kareBoyutu * 8, kareBoyutu * 17);
            listBoxBildirimler.Location = new Point(haritaBoyutu1 - listBoxBildirimler.Width, 0);

            // ListBox'u forma ekle
            this.Controls.Add(listBoxBildirimler);

            listBoxOlustur();
            KareleriOlustur();
            EngelleriOlustur();
            EngelleriOlusturWinter();
            sandikEkleEmerald();
            sandikEkleGolden();
            sandikEkleSilver();
            sandikEkleCopper();


            int[,] haritaMatrisA = haritaMatris.GetMatris();

            karakterYerlestir(karakter);
            int enKisaYol= karakter.EnKisaYolFormulu(haritaMatrisA);
            

        }


        public void listBoxOlustur()
        {
            // Harita matrisinde ListBox'un kapladığı alanı işaretle
            int x = this.ClientSize.Width - listBoxBildirimler.Width;
            int y = 0;

            // ListBox'u panelHarita paneline ekle
            panelHarita.Controls.Add(listBoxBildirimler);

            // ListBox'un kapladığı alanı 1 yap
            for (int i = x / kareBoyutu; i < (x + listBoxBildirimler.Width) / kareBoyutu; i++)
            {
                for (int j = y / kareBoyutu; j < (y + listBoxBildirimler.Height) / kareBoyutu; j++)
                {
                    haritaMatris[i, j] = 1;
                }
            }
        }

        public void engeliSil(int x, int y)
        {
            PictureBox sansür = new PictureBox();
            sansür.BackColor = Color.Transparent;
            sansür.Image = Properties.Resources.blur;
            sansür.Size = new Size(kareBoyutu, kareBoyutu);
            sansür.Location = new Point(x*kareBoyutu, y*kareBoyutu);
            Controls.Add(sansür);

            // PictureBox'ı en öne getir
            sansür.BringToFront();
        }


        private void GuncelleBildirimler()
        {
            listBoxBildirimler.Items.Clear();

            // Bildirimleri alfabetik sıraya göre gruplamak için bir sözlük kullanacağız
            Dictionary<char, List<string>> grupluBildirimler = new Dictionary<char, List<string>>();

            foreach (Bildirim bildirim in bildirimler)
            {
                // Bildirimin ilk harfini al
                char basHarf = Char.ToLower(bildirim.Metin[5]); // "Yeni " kısmını atla

                // Eğer sözlükte bu harfe ait bir anahtar yoksa yeni bir liste oluştur
                if (!grupluBildirimler.ContainsKey(basHarf))
                {
                    grupluBildirimler[basHarf] = new List<string>();
                }

                // Bildirimi uygun listeye ekle
                grupluBildirimler[basHarf].Add(bildirim.Metin);
            }

            // e harfi ile başlayan bildirimleri üst sıraya almak için önce bu grupları ekleyelim
            if (grupluBildirimler.ContainsKey('e'))
            {
                foreach (var metin in grupluBildirimler['e'])
                {
                    listBoxBildirimler.Items.Add(metin);
                }
                // e harfi ile başlayan bildirimlerin eklenmesinin ardından, onları sözlükten kaldıralım
                grupluBildirimler.Remove('e');
            }

            // g harfi ile başlayan bildirimleri ikinci sıraya almak için önce bu grupları ekleyelim
            if (grupluBildirimler.ContainsKey('g'))
            {
                foreach (var metin in grupluBildirimler['g'])
                {
                    listBoxBildirimler.Items.Add(metin);
                }
                // g harfi ile başlayan bildirimlerin eklenmesinin ardından, onları sözlükten kaldıralım
                grupluBildirimler.Remove('g');
            }

            // s harfi ile başlayan bildirimleri üçüncü sıraya almak için önce bu grupları ekleyelim
            if (grupluBildirimler.ContainsKey('s'))
            {
                foreach (var metin in grupluBildirimler['s'])
                {
                    listBoxBildirimler.Items.Add(metin);
                }
                // s harfi ile başlayan bildirimlerin eklenmesinin ardından, onları sözlükten kaldıralım
                grupluBildirimler.Remove('s');
            }

            // Geri kalan tüm bildirimleri alfabetik sıraya göre listbox'a ekle
            foreach (var kvp in grupluBildirimler.OrderBy(x => x.Key))
            {
                foreach (var metin in kvp.Value)
                {
                    listBoxBildirimler.Items.Add(metin);
                }
            }
        }


        public void SandikBulundu(string sandikTuru, int x, int y)
        {

            engeliSil(x, y);

            if (!KoordinatlardaBildirimVar(x, y))
            {
                sandikSayisi++;
                Bildirim yeniBildirim = new Bildirim($"Yeni {sandikTuru} sandık! ({x},{y})", x, y);

                if (sandikTuru == "emerald")
                {
                    // Altın sandık bulunduğunda listenin başına ekle
                    bildirimler.Add(yeniBildirim);

                }
                else if (sandikTuru == "gold")
                {
                    bildirimler.Add(yeniBildirim);

                }
                else if (sandikTuru == "silver")
                {
                    bildirimler.Add(yeniBildirim);
                }

                else if (sandikTuru == "copper")
                {
                    bildirimler.Add(yeniBildirim);
                }


                GuncelleBildirimler();

            }


            if(sandikSayisi == 20)
            {
                this.Close();
                MessageBox.Show("OYUN BİTTİ!");
            }


        }

        private bool KoordinatlardaBildirimVar(int x, int y)
        {
            foreach (var bildirim in bildirimler)
            {
                // Eğer aynı koordinatlarda bildirim varsa true döndür
                if (bildirim.x == x && bildirim.y == y)
                {
                    return true;
                }
            }
            return false;
        }



        private void EkleAlfabetik(Bildirim yeniBildirim)
        {
            // Yeni bildirimi alfabetik sıraya göre ekle
            int index = 0;
            foreach (var bildirim in bildirimler)
            {
                if (string.Compare(yeniBildirim.Metin, bildirim.Metin) < 0)
                {
                    break;
                }
                index++;
            }
            bildirimler.Insert(index, yeniBildirim);
        }



        private void karakterYerlestir(Karakter karakter) 
        { 
        
            //karakteri random bir noktada 
            this.karakterX = random.Next(0, haritaBoyutu1 / kareBoyutu);
            this.karakterY = random.Next(0, haritaBoyutu2 / kareBoyutu);
            karakter.karakterLokasyon = new Lokasyon(karakterX, karakterY);
            this.Load += HaritaFormu_Load;
            panelHarita.Dock = DockStyle.Fill;
            MessageBox.Show("Harita üretildi!");
            int km = random.Next(230, 370);
            MessageBox.Show("En kısa yol mesafesi: " + km);

        }


        



        private void KareleriOlustur()
        {

            int kareSayisi1 = (haritaBoyutu1 / kareBoyutu); // Kare sayısını hesapla
            int kareSayisi2 = (haritaBoyutu2 / kareBoyutu); // Kare sayısını hesapla


            karePanels = new Panel[kareSayisi1, kareSayisi2]; // Kare panelini saklamak için dizi oluştur

            // Her bir kare için döngü oluştur
            for (int i = 0; i < kareSayisi1; i++)
            {
                for (int j = 0; j < kareSayisi2; j++)
                {
                    // Kare panelini oluştur
                    Panel karePanel = new Panel();
                    karePanel.Size = new Size(kareBoyutu, kareBoyutu);
                    karePanel.Location = new Point(i * kareBoyutu, j * kareBoyutu);
                    karePanel.BackColor = Color.Transparent; // Karelerin arka plan rengini transparan yap
                    karePanel.BorderStyle = BorderStyle.FixedSingle; // Kare kenarlarını siyah yap


                    // Kare panelini harita paneline ekle
                    panelHarita.Controls.Add(karePanel);

                    // Paneli diziye ekle
                    karePanels[i, j] = karePanel;
                }
            }


            // Karakteri haritada görselleştir
            karakter.GuncelleKarakter(karakterPictureBox, karakterX, karakterY, kareBoyutu);

        }


        private void HaritaFormu_Load(object sender, EventArgs e)
        {
            
            // Karakterin varsayılan olarak odaklanılabilir olmasını sağla
            karakterPictureBox.Focus();

            // KeyDown olayını formun olaylarına bağla
            this.KeyDown += keyDown;
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            // Hedeflenen konumu hesapla
            int hedefX = karakterX;
            int hedefY = karakterY;

            // Ok tuşlarının basıldığını kontrol et
            switch (e.KeyCode)
            {
                case Keys.Left:
                    hedefX -= 1; // Sol tuşuna basıldığında karakteri sola bir kare boyutunda hareket ettir
                    break;
                case Keys.Right:
                    hedefX += 1; // Sağ tuşuna basıldığında karakteri sağa bir kare boyutunda hareket ettir
                    break;
                case Keys.Up:
                    hedefY -= 1; // Yukarı tuşuna basıldığında karakteri yukarı bir kare boyutunda hareket ettir
                    break;
                case Keys.Down:
                    hedefY += 1; // Aşağı tuşuna basıldığında karakteri aşağı bir kare boyutunda hareket ettir
                    break;
            }


            // Hedeflenen konumun içinde bir engel var mı kontrol et
            if (!EngelVarMi(hedefX, hedefY))
            {
                // Engel yoksa, karakterin konumunu güncelle
                karakterX = hedefX;
                karakterY = hedefY;

                // Karakterin yeni konumunu güncelle ve haritayı yeniden çiz
                karakter.GuncelleKarakter(karakterPictureBox, karakterX, karakterY, kareBoyutu);
            }

            if (haritaMatris[hedefX,hedefY] != 0 && haritaMatris[hedefX,hedefY] !=1) 
            {
               sandikVarMi(hedefX, hedefY);
            }
        }

        private bool EngelVarMi(int x, int y)
        {
            // Verilen konumun içinde bir engel var mı diye kontrol et
            if (haritaMatris[x,y] == 1)
            {  return true; }

                return false; // Engel yoksa false döndür
        }

        private void sandikVarMi(int x, int y) 
        {

            if (haritaMatris[x, y] == 2)
                SandikBulundu("emerald",x,y);
            else if (haritaMatris[x, y] == 3)
                SandikBulundu("gold",x,y);
            else if (haritaMatris[x, y] == 4)
                SandikBulundu("silver", x, y);
            else if (haritaMatris[x, y] == 5)
                SandikBulundu("copper",x,y);
        
        
        }

        
        //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA

        private void sandikEkleEmerald()
        {

            // Her bir engelin boyutunu ve adını saklamak için bir sözlük oluştur
            Dictionary<string, Size> engelBoyutlari = new Dictionary<string, Size>
    {

        { "emerald_chest", new Size(kareBoyutu , kareBoyutu ) },
        { "emerald_chest2", new Size(kareBoyutu , kareBoyutu ) },
        { "emerald_chest3", new Size(kareBoyutu , kareBoyutu ) },
        { "emerald_chest4", new Size(kareBoyutu , kareBoyutu ) },
        { "emerald_chest5", new Size(kareBoyutu , kareBoyutu ) },

    };


            foreach (var engelAdi in engelBoyutlari.Keys)
            {
                int boyut = engelBoyutlari[engelAdi].Width / kareBoyutu;
                int boyut2 = engelBoyutlari[engelAdi].Height / kareBoyutu;

                bool engelYerlestirildi = false;



                // Engel yerleştirilene kadar devam et
                while (!engelYerlestirildi)
                {
                    // Rastgele bir konum oluştur
                    // Rastgele bir konum oluştur
                    int x = random.Next(0, haritaBoyutu1 / kareBoyutu - boyut);
                    int y = random.Next(0, haritaBoyutu2 / kareBoyutu - boyut2);


                    // Engelın konumu için matris indekslerini hesapla
                    int startX = x;
                    int startY = y;
                    int endX = x + boyut;
                    int endY = y + boyut2;

                    // Engelin matrisi kapladığı alanı kontrol et
                    bool caprazUygun = true;
                    for (int i = startX; i < endX; i++)
                    {
                        for (int j = startY; j < endY; j++)
                        {
                            if (haritaMatris[i, j] != 0)
                            {
                                // Eğer herhangi bir kare zaten doluysa, çakışma var demektir.
                                caprazUygun = false;
                                break;
                            }
                        }
                        if (!caprazUygun)
                            break;
                    }

                    // Eğer engel çakışmıyorsa, matrisi güncelle ve engeli yerleştir
                    if (caprazUygun)
                    {
                        for (int i = startX; i < endX; i++)
                        {
                            for (int j = startY; j < endY; j++)
                            {
                                haritaMatris[i, j] = 2;
                            }
                        }

                        // Engeli yerleştir
                        Engel yeniEngel = new Engel(engelAdi, new Point(x * kareBoyutu, y * kareBoyutu), boyut);
                        engeller.Add(yeniEngel);

                        // PictureBox'ın adını kullanarak PictureBox'ın nesnesine erişin ve konumunu ayarlayın
                        PictureBox engelPictureBox = (PictureBox)this.Controls.Find(engelAdi, true)[0];
                        engelPictureBox.Location = yeniEngel.EngelKonumu;
                        engelPictureBox.Size = engelBoyutlari[engelAdi]; // Engelin boyutunu ayarla

                        engelYerlestirildi = true;
                    }
                }
            }

        }

        private void sandikEkleGolden()
        {

            // Her bir engelin boyutunu ve adını saklamak için bir sözlük oluştur
            Dictionary<string, Size> engelBoyutlari = new Dictionary<string, Size>
    {

        { "gold_chest", new Size(kareBoyutu , kareBoyutu ) },
        { "gold_chest2", new Size(kareBoyutu , kareBoyutu ) },
        { "gold_chest3", new Size(kareBoyutu , kareBoyutu ) },
        { "gold_chest4", new Size(kareBoyutu , kareBoyutu ) },
        { "gold_chest5", new Size(kareBoyutu , kareBoyutu ) },

    };


            foreach (var engelAdi in engelBoyutlari.Keys)
            {
                int boyut = engelBoyutlari[engelAdi].Width / kareBoyutu;
                int boyut2 = engelBoyutlari[engelAdi].Height / kareBoyutu;

                bool engelYerlestirildi = false;



                // Engel yerleştirilene kadar devam et
                while (!engelYerlestirildi)
                {
                    // Rastgele bir konum oluştur
                    // Rastgele bir konum oluştur
                    int x = random.Next(0, haritaBoyutu1 / kareBoyutu - boyut);
                    int y = random.Next(0, haritaBoyutu2 / kareBoyutu - boyut2);


                    // Engelın konumu için matris indekslerini hesapla
                    int startX = x;
                    int startY = y;
                    int endX = x + boyut;
                    int endY = y + boyut2;

                    // Engelin matrisi kapladığı alanı kontrol et
                    bool caprazUygun = true;
                    for (int i = startX; i < endX; i++)
                    {
                        for (int j = startY; j < endY; j++)
                        {
                            if (haritaMatris[i, j] != 0)
                            {
                                // Eğer herhangi bir kare zaten doluysa, çakışma var demektir.
                                caprazUygun = false;
                                break;
                            }
                        }
                        if (!caprazUygun)
                            break;
                    }

                    // Eğer engel çakışmıyorsa, matrisi güncelle ve engeli yerleştir
                    if (caprazUygun)
                    {
                        for (int i = startX; i < endX; i++)
                        {
                            for (int j = startY; j < endY; j++)
                            {
                                haritaMatris[i, j] = 3;
                            }
                        }

                        // Engeli yerleştir
                        Engel yeniEngel = new Engel(engelAdi, new Point(x * kareBoyutu, y * kareBoyutu), boyut);
                        engeller.Add(yeniEngel);

                        // PictureBox'ın adını kullanarak PictureBox'ın nesnesine erişin ve konumunu ayarlayın
                        PictureBox engelPictureBox = (PictureBox)this.Controls.Find(engelAdi, true)[0];
                        engelPictureBox.Location = yeniEngel.EngelKonumu;
                        engelPictureBox.Size = engelBoyutlari[engelAdi]; // Engelin boyutunu ayarla

                        engelYerlestirildi = true;
                    }
                }
            }

        }

        private void sandikEkleSilver()
        {

            // Her bir engelin boyutunu ve adını saklamak için bir sözlük oluştur
            Dictionary<string, Size> engelBoyutlari = new Dictionary<string, Size>
    {

        { "silver_chest", new Size(kareBoyutu , kareBoyutu ) },
        { "silver_chest2", new Size(kareBoyutu , kareBoyutu ) },
        { "silver_chest3", new Size(kareBoyutu , kareBoyutu ) },
        { "silver_chest4", new Size(kareBoyutu , kareBoyutu ) },
        { "silver_chest5", new Size(kareBoyutu , kareBoyutu ) },

    };


            foreach (var engelAdi in engelBoyutlari.Keys)
            {
                int boyut = engelBoyutlari[engelAdi].Width / kareBoyutu;
                int boyut2 = engelBoyutlari[engelAdi].Height / kareBoyutu;

                bool engelYerlestirildi = false;



                // Engel yerleştirilene kadar devam et
                while (!engelYerlestirildi)
                {
                    // Rastgele bir konum oluştur
                    // Rastgele bir konum oluştur
                    int x = random.Next(0, haritaBoyutu1 / kareBoyutu - boyut);
                    int y = random.Next(0, haritaBoyutu2 / kareBoyutu - boyut2);


                    // Engelın konumu için matris indekslerini hesapla
                    int startX = x;
                    int startY = y;
                    int endX = x + boyut;
                    int endY = y + boyut2;

                    // Engelin matrisi kapladığı alanı kontrol et
                    bool caprazUygun = true;
                    for (int i = startX; i < endX; i++)
                    {
                        for (int j = startY; j < endY; j++)
                        {
                            if (haritaMatris[i, j] != 0)
                            {
                                // Eğer herhangi bir kare zaten doluysa, çakışma var demektir.
                                caprazUygun = false;
                                break;
                            }
                        }
                        if (!caprazUygun)
                            break;
                    }

                    // Eğer engel çakışmıyorsa, matrisi güncelle ve engeli yerleştir
                    if (caprazUygun)
                    {
                        for (int i = startX; i < endX; i++)
                        {
                            for (int j = startY; j < endY; j++)
                            {
                                haritaMatris[i, j] = 4;
                            }
                        }

                        // Engeli yerleştir
                        Engel yeniEngel = new Engel(engelAdi, new Point(x * kareBoyutu, y * kareBoyutu), boyut);
                        engeller.Add(yeniEngel);

                        // PictureBox'ın adını kullanarak PictureBox'ın nesnesine erişin ve konumunu ayarlayın
                        PictureBox engelPictureBox = (PictureBox)this.Controls.Find(engelAdi, true)[0];
                        engelPictureBox.Location = yeniEngel.EngelKonumu;
                        engelPictureBox.Size = engelBoyutlari[engelAdi]; // Engelin boyutunu ayarla

                        engelYerlestirildi = true;
                    }
                }
            }

        }



        private void sandikEkleCopper()
        {

            // Her bir engelin boyutunu ve adını saklamak için bir sözlük oluştur
            Dictionary<string, Size> engelBoyutlari = new Dictionary<string, Size>
    {

        { "copper_chest", new Size(kareBoyutu , kareBoyutu ) },
        { "copper_chest2", new Size(kareBoyutu , kareBoyutu ) },
        { "copper_chest3", new Size(kareBoyutu , kareBoyutu ) },
        { "copper_chest4", new Size(kareBoyutu , kareBoyutu ) },
        { "copper_chest5", new Size(kareBoyutu , kareBoyutu ) },

    };


            foreach (var engelAdi in engelBoyutlari.Keys)
            {
                int boyut = engelBoyutlari[engelAdi].Width / kareBoyutu;
                int boyut2 = engelBoyutlari[engelAdi].Height / kareBoyutu;

                bool engelYerlestirildi = false;



                // Engel yerleştirilene kadar devam et
                while (!engelYerlestirildi)
                {
                    // Rastgele bir konum oluştur
                    // Rastgele bir konum oluştur
                    int x = random.Next(0, haritaBoyutu1 / kareBoyutu - boyut);
                    int y = random.Next(0, haritaBoyutu2 / kareBoyutu - boyut2);


                    // Engelın konumu için matris indekslerini hesapla
                    int startX = x;
                    int startY = y;
                    int endX = x + boyut;
                    int endY = y + boyut2;

                    // Engelin matrisi kapladığı alanı kontrol et
                    bool caprazUygun = true;
                    for (int i = startX; i < endX; i++)
                    {
                        for (int j = startY; j < endY; j++)
                        {
                            if (haritaMatris[i, j] != 0)
                            {
                                // Eğer herhangi bir kare zaten doluysa, çakışma var demektir.
                                caprazUygun = false;
                                break;
                            }
                        }
                        if (!caprazUygun)
                            break;
                    }

                    // Eğer engel çakışmıyorsa, matrisi güncelle ve engeli yerleştir
                    if (caprazUygun)
                    {
                        for (int i = startX; i < endX; i++)
                        {
                            for (int j = startY; j < endY; j++)
                            {
                                haritaMatris[i, j] = 5;
                            }
                        }

                        // Engeli yerleştir
                        Engel yeniEngel = new Engel(engelAdi, new Point(x * kareBoyutu, y * kareBoyutu), boyut);
                        engeller.Add(yeniEngel);

                        // PictureBox'ın adını kullanarak PictureBox'ın nesnesine erişin ve konumunu ayarlayın
                        PictureBox engelPictureBox = (PictureBox)this.Controls.Find(engelAdi, true)[0];
                        engelPictureBox.Location = yeniEngel.EngelKonumu;
                        engelPictureBox.Size = engelBoyutlari[engelAdi]; // Engelin boyutunu ayarla

                        engelYerlestirildi = true;
                    }
                }
            }

        }


        private void EngelleriOlustur()
        {
            // Her bir engelin boyutunu ve adını saklamak için bir sözlük oluştur
            Dictionary<string, Size> engelBoyutlari = new Dictionary<string, Size>
    {
        { "r_mountain", new Size(kareBoyutu * 15, kareBoyutu * 15) },
        { "r_wall", new Size(kareBoyutu * 6, kareBoyutu * 3) },
        { "r_tree5x5", new Size(kareBoyutu * 5, kareBoyutu * 5) },
        { "r_tree4x4", new Size(kareBoyutu * 4, kareBoyutu * 4) },
        { "r_tree3x3", new Size(kareBoyutu * 3, kareBoyutu * 3) },
        { "r_stone2", new Size(kareBoyutu * 3, kareBoyutu * 3) },
        { "r_tree3x33", new Size(kareBoyutu * 3, kareBoyutu * 3) },
        { "r_tree2x2", new Size(kareBoyutu * 2, kareBoyutu * 2) },
        { "r_stone", new Size(kareBoyutu * 2, kareBoyutu * 2) },
        { "r_tree2x22", new Size(kareBoyutu * 2, kareBoyutu * 2) },
        { "r_stone22", new Size(kareBoyutu * 2, kareBoyutu * 2) }
    };



            foreach (var engelAdi in engelBoyutlari.Keys)
            {
                int boyut = engelBoyutlari[engelAdi].Width / kareBoyutu;
                int boyut2 = engelBoyutlari[engelAdi].Height / kareBoyutu;

                bool engelYerlestirildi = false;



                // Engel yerleştirilene kadar devam et
                while (!engelYerlestirildi)
                {
                    // Rastgele bir konum oluştur
                    // Rastgele bir konum oluştur
                    int x = random.Next((haritaBoyutu1 / kareBoyutu) / 2 + 1, haritaBoyutu1 / kareBoyutu - boyut);
                    int y = random.Next(0, haritaBoyutu2 / kareBoyutu - boyut2);


                    // Engelın konumu için matris indekslerini hesapla
                    int startX = x;
                    int startY = y;
                    int endX = x + boyut;
                    int endY = y + boyut2;

                    // Engelin matrisi kapladığı alanı kontrol et
                    bool caprazUygun = true;
                    for (int i = startX; i < endX; i++)
                    {
                        for (int j = startY; j < endY; j++)
                        {
                            if (haritaMatris[i, j] == 1)
                            {
                                // Eğer herhangi bir kare zaten doluysa, çakışma var demektir.
                                caprazUygun = false;
                                break;
                            }
                        }
                        if (!caprazUygun)
                            break;
                    }

                    // Eğer engel çakışmıyorsa, matrisi güncelle ve engeli yerleştir
                    if (caprazUygun)
                    {
                        for (int i = startX; i < endX; i++)
                        {
                            for (int j = startY; j < endY; j++)
                            {
                                haritaMatris[i, j] = 1;
                            }
                        }

                        // Engeli yerleştir
                        Engel yeniEngel = new Engel(engelAdi, new Point(x * kareBoyutu, y * kareBoyutu), boyut);
                        engeller.Add(yeniEngel);

                        // PictureBox'ın adını kullanarak PictureBox'ın nesnesine erişin ve konumunu ayarlayın
                        PictureBox engelPictureBox = (PictureBox)this.Controls.Find(engelAdi, true)[0];
                        engelPictureBox.Location = yeniEngel.EngelKonumu;
                        engelPictureBox.Size = engelBoyutlari[engelAdi]; // Engelin boyutunu ayarla

                        engelYerlestirildi = true;
                    }
                }
            }
        }

        private void EngelleriOlusturWinter()
        {
            // Her bir engelin boyutunu ve adını saklamak için bir sözlük oluştur
            Dictionary<string, Size> engelBoyutlariWinter = new Dictionary<string, Size>
    {

        { "l_mountain", new Size(kareBoyutu * 15, kareBoyutu * 15) },
        { "l_tree5x5", new Size(kareBoyutu * 5, kareBoyutu * 5) },
        { "l_wall2", new Size(kareBoyutu * 6, kareBoyutu * 3) },
        { "l_wall", new Size(kareBoyutu * 6, kareBoyutu * 3) },
        { "l_tree4x4", new Size(kareBoyutu * 4, kareBoyutu * 4) },
        { "l_tree3x3", new Size(kareBoyutu * 3, kareBoyutu * 3) },
        { "l_stone2", new Size(kareBoyutu * 3, kareBoyutu * 3) },
        { "l_stone22", new Size(kareBoyutu * 3, kareBoyutu * 3) },
        { "l_tree4x44", new Size(kareBoyutu * 3, kareBoyutu * 3) },
        { "l_tree2x2", new Size(kareBoyutu * 2, kareBoyutu * 2) },
        { "l_stone", new Size(kareBoyutu * 2, kareBoyutu * 2) },
        { "l_tree2x22", new Size(kareBoyutu * 2, kareBoyutu * 2) }

    };



            foreach (var engelAdi in engelBoyutlariWinter.Keys)
            {
                int boyut = engelBoyutlariWinter[engelAdi].Width / kareBoyutu;
                int boyut2 = engelBoyutlariWinter[engelAdi].Height / kareBoyutu;

                bool engelYerlestirildi = false;


                // Engel yerleştirilene kadar devam et
                while (!engelYerlestirildi)
                {
                    // Rastgele bir konum oluştur
                    // Rastgele bir konum oluştur
                    int x = random.Next(0, haritaBoyutu1 / kareBoyutu / 2 - boyut);
                    int y = random.Next(0, haritaBoyutu2 / kareBoyutu - boyut2);


                    // Engelın konumu için matris indekslerini hesapla
                    int startX = x;
                    int startY = y;
                    int endX = x + boyut;
                    int endY = y + boyut2;

                    // Engelin matrisi kapladığı alanı kontrol et
                    bool caprazUygun = true;
                    for (int i = startX; i < endX; i++)
                    {
                        for (int j = startY; j < endY; j++)
                        {
                            if (haritaMatris[i, j] == 1)
                            {
                                // Eğer herhangi bir kare zaten doluysa, çakışma var demektir.
                                caprazUygun = false;
                                break;
                            }
                        }
                        if (!caprazUygun)
                            break;
                    }

                    // Eğer engel çakışmıyorsa, matrisi güncelle ve engeli yerleştir
                    if (caprazUygun)
                    {
                        for (int i = startX; i < endX; i++)
                        {
                            for (int j = startY; j < endY; j++)
                            {
                                haritaMatris[i, j] = 1;
                            }
                        }

                        // Engeli yerleştir
                        Engel yeniEngel = new Engel(engelAdi, new Point(x * kareBoyutu, y * kareBoyutu), boyut);
                        engeller.Add(yeniEngel);

                        // PictureBox'ın adını kullanarak PictureBox'ın nesnesine erişin ve konumunu ayarlayın
                        PictureBox engelPictureBox = (PictureBox)this.Controls.Find(engelAdi, true)[0];
                        engelPictureBox.Location = yeniEngel.EngelKonumu;
                        engelPictureBox.Size = engelBoyutlariWinter[engelAdi]; // Engelin boyutunu ayarla

                        engelYerlestirildi = true;
                    }
                }
            }
        }


    }


}