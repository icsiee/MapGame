internal class Engel
{
    public string EngelAdi { get; set; }
    public Point EngelKonumu { get; set; }
    public int Boyut { get; set; }

    public Engel(string engelAdi, Point konum, int boyut)
    {
        EngelAdi = engelAdi;
        EngelKonumu = konum;
        Boyut = boyut;
    }

    
}