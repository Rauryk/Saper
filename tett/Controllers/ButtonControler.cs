using Microsoft.AspNetCore.Mvc;

namespace tett.moje
{
    public class ButtonControler : Controller
    {
       public static List<(int, string, ButtonControler,int)> lista = new List<(int, string, ButtonControler,int)>();
        Random Random = new Random();
        public string klocek="x";

        public string IleBomb(string a, int[] bomby)
        {
            int[] pozycje = new int[] { -11, -10, -9, 1, 11, 10, 9, -1 };
            int IloscMin = 0;
            if (Int32.Parse(a)%10==0)
            {
                pozycje[pozycje.Length-1]=0;
                pozycje[pozycje.Length-2]=0;
                pozycje[0]=0;

            }
            if (Int32.Parse(a) % 10 == 9)
            {
                pozycje[4]=0;
                pozycje[3]=0;
                pozycje[2]=0;
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
                    lista.Add((Int32.Parse(i.ToString() + ii.ToString()), Convert
                        .ToBoolean(pozycje.ToList().Find(x => x == Int32.Parse(i.ToString() + ii.ToString()))) ? "B"
                        : IleBomb(i.ToString() + ii.ToString(), pozycje), new ButtonControler(),1));
                }

            }
            return View();
        }
        [HttpPost]
        public ActionResult sprawdz(string pole)
        {
            pokaz(pole);
            return View();


        }
        
        public string pokaz(string pole)
        {
            int ii = lista.FindIndex(x => x.Item1 ==Int32.Parse( pole));

            var item=lista.FirstOrDefault(x=>x.Item1 == ii);
            if (true)
                item.Item4 = 0;

            lista.Remove(lista.FirstOrDefault(x => x.Item1 == ii)) ;
            lista.Insert(ii,item);
      
            return lista[ii].Item2.ToString();
        }
        public bool Czypokazac(string a)
        {
            if (lista[lista.FindIndex(x => x.Item1 == Int32.Parse(a))].Item4 == 0)
                return true;
            return false;
            
        }


    }
}
