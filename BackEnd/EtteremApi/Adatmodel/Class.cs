namespace EtteremApi.Adatmodel
{
    public enum Szerepkor
    {
        Vendeg,
        Alkalmazott
    }

    public enum FoglalasStatusz
    {
        Fuggoben,
        Jovahagyva,
        Lemondva,
        Tavozott
    }

    public class Felhasznalo
    {
        public int Id { get; set; }
        public string Nev { get; set; } = "";
        public string Email { get; set; } = "";
        public string JelszoHash { get; set; } = "";
        public string? Telefon { get; set; }
        public Szerepkor Szerepkor { get; set; } = Szerepkor.Vendeg;
        public DateTime RegisztracioDatuma { get; set; } = DateTime.UtcNow;

        public Alkalmazott? Alkalmazott { get; set; }
        public List<Foglalas> Foglalasok { get; set; } = new();
    }

    public class Alkalmazott
    {
        public int Id { get; set; }
        public int FelhasznaloId { get; set; }
        public Felhasznalo Felhasznalo { get; set; } = null!;
        public string Munkakor { get; set; } = "";
        public decimal Ber { get; set; }
        public DateTime BelepesDatum { get; set; }

        public List<Kiadas> RogzitettKiadasok { get; set; } = new();
        public List<Foglalas> KezeltFoglalasok { get; set; } = new();
    }

    public class Asztal
    {
        public int Id { get; set; }
        public int Szam { get; set; }
        public int Ferohely { get; set; }

        public List<FoglalasAsztal> Foglalasok { get; set; } = new();
    }

    public class Foglalas
    {
        public int Id { get; set; }
        public int FelhasznaloId { get; set; }
        public Felhasznalo Felhasznalo { get; set; } = null!;

        public DateTime Idopont { get; set; }
        public int Letszam { get; set; }
        public string? Megjegyzes { get; set; }
        public FoglalasStatusz Statusz { get; set; } = FoglalasStatusz.Fuggoben;
        public DateTime LetrehozasIdopont { get; set; } = DateTime.UtcNow;
        public DateTime? TavozasIdopont { get; set; }

        public int? KezeloAlkalmazottId { get; set; }
        public Alkalmazott? KezeloAlkalmazott { get; set; }

        public List<FoglalasAsztal> Asztalok { get; set; } = new();
    }

    public class FoglalasAsztal
    {
        public int FoglalasId { get; set; }
        public Foglalas Foglalas { get; set; } = null!;
        public int AsztalId { get; set; }
        public Asztal Asztal { get; set; } = null!;
    }

    public class KiadasKategoria
    {
        public int Id { get; set; }
        public string Nev { get; set; } = "";
        public List<Kiadas> Kiadasok { get; set; } = new();
    }

    public class Kiadas
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public decimal Osszeg { get; set; }
        public string? Leiras { get; set; }

        public int KategoriaId { get; set; }
        public KiadasKategoria Kategoria { get; set; } = null!;

        public int RogzitetteAlkalmazottId { get; set; }
        public Alkalmazott RogzitetteAlkalmazott { get; set; } = null!;
    }
}