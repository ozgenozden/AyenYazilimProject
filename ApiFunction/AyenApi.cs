using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApiFunction.AyenViewModel;

namespace ApiFunction
{
    public class AyenApi
    {
        private readonly RestClient Client;
        

        public AyenApi()
        {
            Client = new RestClient();
            
        }

        public async Task<List<AyenUrunler>> GetAyenProduct()
        {
            var request = new RestRequest();
            request.Resource = "https://cdn.ayensoftware.com/00Coding/01/products.json";
            var response = await Client.ExecuteAsync(request);

            string dosya = @"C:\Users\userThink\Downloads\null.csv";

            string[] allLines = File.ReadAllLines(dosya);

            var query = allLines.Skip(1).Select((r, index) => new
            {
                i = index,
                Data = r.Split(';')
            }).ToList();


            List<AyenResponse> rr = JsonConvert.DeserializeObject<List<AyenResponse>>(response.Content);
            var dd = rr.AsEnumerable().Where(s => s.Brand!="Adidas" && s.Brand !="adidas").GroupBy(g => new { g.ProductCode, g.Color }).
                 Select(s => new AyenUrunler()
                 {
                     Urunler= new List<Urun>() {
                       new Urun()
                        {
                            UrunKodu=s.Select(a=>a.ProductCode).FirstOrDefault(),
                            UrunAdi=s.Select(a=>a.Name).FirstOrDefault(),
                            Marka=s.Where(x=>x.Brand !="Adidas" || x.Brand !="adidas").Select(a=>a.Brand).FirstOrDefault(),

                            Stok=query.Select(x=>x.Data[1]).Select(Int32.Parse).Sum(),

                            Varyantlar=new List<Varyant>()
                            {
                                new Varyant()
                                {
                                    Sku=s.Select(a=>a.SKU).FirstOrDefault(),
                                    Stok=query.Where(x=>x.Data[0]==s.Select(a=>a.SKU).FirstOrDefault()).Select(x=>x.Data[1]).Select(Int32.Parse).FirstOrDefault(),
                                    Fiyat=query.Where(y=>y.Data[0]==s.Select(a=>a.SKU).FirstOrDefault()).Select(y=>y.Data[2]).Select(double.Parse).FirstOrDefault(),
                                    Ozellikler=new List<Ozellik>()
                                    {
                                        new Ozellik()
                                        {

                                            Renk=s.Select(a=>a.Color).FirstOrDefault(),
                                            Beden=s.Select(a=>a.Size).FirstOrDefault(),
                                        }
                                    }

                                }
                           }
                      }
                   }
                 });

            var f = Newtonsoft.Json.JsonConvert.SerializeObject(dd, Newtonsoft.Json.Formatting.Indented);
            FileStream fs = new FileStream("D:\\AyenUrunler.json", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(f);
            sw.Close();

            return dd.ToList();

        }
    }
}
