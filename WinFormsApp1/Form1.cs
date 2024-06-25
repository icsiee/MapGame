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

            // TextBox'tan metin de�erini al
            string haritaBoyutStr1 = textbox_boyutGir1.Text;
            string haritaBoyutStr2 = textbox_boyutGir2.Text;
           

            // Metin de�erini int t�r�ne d�n��t�r
            if (int.TryParse(haritaBoyutStr1, out int haritaBoyut1) && int.TryParse(haritaBoyutStr2, out int haritaBoyut2))
            {
                haritaBoyut1 = haritaBoyut1 *3/2;//g�r�n�m�n netli�i i�in 3/2 ile �arp�yorum alan� 
                haritaBoyut2 = haritaBoyut2 *3/2; //g�r�n�m�n netli�i i�in 3/2 ile �arp�yorum alan� 
                // D�n���m ba�ar�l�ysa yeni form olu�tur ve g�ster
                HaritaFormu yeniForm = new HaritaFormu(haritaBoyut1, haritaBoyut2, karakter);
                yeniForm.Size = new Size(haritaBoyut1, haritaBoyut2);

                yeniForm.Show();
                

            }

            else
            {
                // D�n���m ba�ar�s�zsa hata mesaj� g�ster
                MessageBox.Show("Ge�ersiz harita boyutu! L�tfen bir tam say� girin.");
            }






        }

     
    }
}
