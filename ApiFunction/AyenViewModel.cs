using System;
using System.Collections.Generic;
using System.Text;

namespace ApiFunction
{
    public class AyenViewModel
    {
        public class AyenResponse
        {

            public string ProductCode { get; set; }
            public string Name { get; set; }
            public string SKU { get; set; }
            public string Brand { get; set; }
            public string Color { get; set; }
            public string Size { get; set; }

        }
        public class AyenUrunler
        {
            public List<Urun> Urunler { get; set; }

        }
        public class Urun
        {
            public string UrunKodu { get; set; }
            public string UrunAdi { get; set; }
            public string Marka { get; set; }
            public int Stok { get; set; }
            public List<Varyant> Varyantlar { get; set; }

        }
        public class Varyant
        {
            public string Sku { get; set; }
            public int Stok { get; set; }
            public double Fiyat { get; set; }
            public List<Ozellik> Ozellikler { get; set; }
        }
        public class Ozellik
        {
            public string Renk { get; set; }
            public string Beden { get; set; }
        }
    }
}
