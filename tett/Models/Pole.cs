using System;
using System.ComponentModel.DataAnnotations;

namespace tett.Models
{
    public class Pole
    {
        public string Name
        {
            get;
            set;
        }
        public Pole()
        {tett.moje.ButtonControler buttonControler = new tett.moje.ButtonControler();
            
            Name = buttonControler.klocek; 
        }
    }
}
