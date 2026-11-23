class DziennikWyprawy
{
    private const int PojemnoscHistorii = 3;
    private const int LiczbaPokoi = 4;

    private bool[] _odwiedzone = new bool[LiczbaPokoi];
    private string[] _wpisy = { "", "", "" };

    public int LiczbaOdwiedzonych
    {
        get
        {
            int licznik = 0;
            for (int i = 0; i < _odwiedzone.Length; i++)
            {
                if (_odwiedzone[i])
                {
                    licznik++;
                }
            }
            return licznik;
        }
    }

    public int LiczbaWpisow { get; private set; }

    public DziennikWyprawy()
    {
        Reset();
    }

    public void Odwiedz(int numer)
    {
        if (numer < 1 || numer > LiczbaPokoi)
        {
            return;
        }

        _odwiedzone[numer - 1] = true;
    }

    public bool CzyOdwiedzono(int numer)
    {
        if (numer < 1 || numer > LiczbaPokoi)
        {
            return false;
        }

        return _odwiedzone[numer - 1];
    }

    public void DodajWpis(string? tresc)
    {
        if (tresc == null)
        {
            return;
        }

        string oczyszczony = tresc.Trim();
        if (oczyszczony == "")
        {
            return;
        }

        if (LiczbaWpisow < PojemnoscHistorii)
        {
            _wpisy[LiczbaWpisow] = oczyszczony;
            LiczbaWpisow++;
            return;
        }

        for (int i = 0; i < PojemnoscHistorii - 1; i++)
        {
            _wpisy[i] = _wpisy[i + 1];
        }

        _wpisy[PojemnoscHistorii - 1] = oczyszczony;
    }

    public string PobierzWpis(int indeks)
    {
        if (indeks < 0 || indeks >= LiczbaWpisow || indeks >= PojemnoscHistorii)
        {
            return "";
        }

        return _wpisy[indeks];
    }

    public void Reset()
    {
        for (int i = 0; i < _odwiedzone.Length; i++)
        {
            _odwiedzone[i] = false;
        }

        _odwiedzone[0] = true;

        for (int i = 0; i < _wpisy.Length; i++)
        {
            _wpisy[i] = "";
        }

        LiczbaWpisow = 0;
    }
}