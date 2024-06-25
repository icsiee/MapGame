using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        
        public Form1()
        {
            InitializeComponent();
        }

        private void button_go_Click(object sender, EventArgs e)
        {
            
            string isim = textBox_isim.Text;
            string id = textBox_id.Text;

            Karakter karakter = new Karakter(isim, id, 0, 0);

            // TextBox'tan metin deðerini al
            string haritaBoyutStr1 = textbox_boyutGir1.Text;
            string haritaBoyutStr2 = textbox_boyutGir2.Text;
           

            // Metin deðerini int türüne dönüþtür
            if (int.TryParse(haritaBoyutStr1, out int haritaBoyut1) && int.TryParse(haritaBoyutStr2, out int haritaBoyut2))
            {
                haritaBoyut1 = haritaBoyut1 *3/2;//görünümün netliði için 3/2 ile çarpýyorum alaný 
                haritaBoyut2 = haritaBoyut2 *3/2; //görünümün netliði için 3/2 ile çarpýyorum alaný 
                // Dönüþüm baþarýlýysa yeni form oluþtur ve göster
                HaritaFormu yeniForm = new HaritaFormu(haritaBoyut1, haritaBoyut2, karakter);
                yeniForm.Size = new Size(haritaBoyut1, haritaBoyut2);

                yeniForm.Show();
                

            }

            else
            {
                // Dönüþüm baþarýsýzsa hata mesajý göster
                MessageBox.Show("Geçersiz harita boyutu! Lütfen bir tam sayý girin.");
            }






        }

     
    }
}
