using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysProgAnket3.ViewModels
{
    public class DersGrupViewModel
    {
        public int ders_kodu { get; set; }
        public int grup_no { get; set; }
        public string dersin_adi { get; set; }
        public int hoca_kodu { get; set; }
    }
}