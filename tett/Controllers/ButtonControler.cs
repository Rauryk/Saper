using Microsoft.AspNetCore.Mvc;

namespace tett.moje
{
    public class ButtonControler : Controller
    {
        //private const string V = "0";
        public static List<(int, string, ButtonControler, int)> lista = new List<(int, string, ButtonControler, int)>();
        Random Random = new Random();
        public string klocek = "X",stan_gry=string.Empty;
        

        public string IleBomb(string a, int[] bomby)
        {
            int[] pozycje = new int[] { -11, -10, -9, 1, 11, 10, 9, -1 };
            int IloscMin = 0;
            if (Int32.Parse(a) % 10 == 0)
            {
                pozycje[pozycje.Length - 1] = 0;
                pozycje[pozycje.Length - 2] = 0;
                pozycje[0] = 0;

            }
            if (Int32.Parse(a) % 10 == 9)
            {
                pozycje[4] = 0;
                pozycje[3] = 0;
                pozycje[2] = 0;
            }
            foreach (int item in pozycje)
            {
                if (bomby.ToList().Exists(x => x == (Int32.Parse(a) + item)))
                {
                    IloscMin++;
                }
            }

            return IloscMin.ToString();
        }

        public ActionResult Index()
        {
            int[] pozycje = Enumerable.Range(0, 99).OrderBy(x => Random.Next()).Take(10).ToArray();
            for (int i = 0; i < 10; i++)
            {
                for (int ii = 0; ii < 10; ii++)
                {
                    lista.Add((Int32.Parse(i.ToString() + ii.ToString()),
                        Convert.ToBoolean(pozycje.ToList().Find(x => x == Int32.Parse(i.ToString() + ii.ToString()))) ? "B"
                        : IleBomb(i.ToString() + ii.ToString(), pozycje)
                        , new ButtonControler(), 1));
                }

                
            }
            return View();
        }
        [HttpPost]
        public ActionResult sprawdz(string pole)
        {
            pokaz(pole);
            int ii = lista.FindIndex(x => x.Item1 == Int32.Parse(pole));
            
            var item = lista.FirstOrDefault(x => x.Item1 == ii);
            if (item.Item2 == "B")
            {
                Console.Write("sad");
                stan_gry = "Przegrana";
            }
                    if (item.Item2 == "0" && item.Item4 >= 0)
            {
                if (item.Item2 == string.Empty)
                    Console.WriteLine("pusty item2" + item.Item2);

                int[] pozycje = new int[] { -11, -10, -9, 1, 11, 10, 9, -1 };
                if (Int32.Parse(pole) % 10 == 0)
                {
                    pozycje[pozycje.Length - 1] = 0;
                    pozycje[pozycje.Length - 2] = 0;
                    pozycje[0] = 0;

                }
                if (Int32.Parse(pole) % 10 == 9)
                {
                    pozycje[4] = 0;
                    pozycje[3] = 0;
                    pozycje[2] = 0;
                }
                if (Int32.Parse(pole) > 89)
                {
                    pozycje[4] = 0;
                    pozycje[5] = 0;
                    pozycje[6] = 0;

                }

                (string, string, string) buff = (string.Empty, string.Empty, string.Empty);

                foreach (var pozycja in pozycje)
                {
                    if (pozycja != 0)
                        buff = pokaz((Int32.Parse(pole) + pozycja).ToString());
                    // Console.WriteLine(buff);

                    if (buff.Item2 == "0" && buff.Item1 == "0")
                    {
                        foreach (var pozycja2 in pozycje)
                        {
                            if(pozycja2!=0)
                                sprawdz((Int32.Parse(buff.Item3) + pozycja2).ToString());
                        }

                    }
                }
            }

            return View();


        }
       
        public (string wartosc, string status, string pozycja) pokaz(string pole)
        {
            int index = lista.FindIndex(x => x.Item1 == Int32.Parse(pole));
            var list_row = lista.FirstOrDefault(x => x.Item1 == index);
            if (index >= 0)
            {
               
                int[] pozycje = new int[] { -11, -10, -9, 1, 11, 10, 9, -1 };
                if (Int32.Parse(pole) % 10 == 0)
                {
                    pozycje[pozycje.Length - 1] = 0;
                    pozycje[pozycje.Length - 2] = 0;
                    pozycje[0] = 0;

                }
                if (Int32.Parse(pole) % 10 == 9)
                {
                    pozycje[4] = 0;
                    pozycje[3] = 0;
                    pozycje[2] = 0;
                }
                if (Int32.Parse(pole) > 89)
                {
                    pozycje[4] = 0;
                    pozycje[5] = 0;
                    pozycje[6] = 0;

                }

                list_row.Item4--;
                lista.Remove(lista.FirstOrDefault(x => x.Item1 == index));
                lista.Insert(index, list_row);
                return (lista[index].Item2.ToString(), lista[index].Item4.ToString(), lista[index].Item1.ToString());

            }
            Console.WriteLine("index ujemny");
            return (string.Empty, string.Empty, string.Empty);

        }
        public bool Czypokazac(string a)
        {
            if (lista[lista.FindIndex(x => x.Item1 == Int32.Parse(a))].Item4 != 1)
                return true;
            return false;

        }
        public string Fstan_gry()
        {

            return stan_gry;
        }


    }
}
