using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace KurlarServis
{
    // NOT: "Service1" sınıf adını kodda, svc'de ve yapılandırma dosyasında birlikte değiştirmek için "Yeniden Düzenle" menüsündeki "Yeniden Adlandır" komutunu kullanabilirsiniz.
    // NOT: Bu hizmeti test etmek üzere WCF Test İstemcisi'ni başlatmak için lütfen Çözüm Gezgini'nde Service1.svc'yi veya Service1.svc.cs'yi seçin ve hata ayıklamaya başlayın.
    public class KurServis : IKurServis
    {
        
        public double KurlariGetir(string kurTipi)
        {
            // Bugün (en son iş gününe) e ait döviz kurları için
            string today = "http://www.tcmb.gov.tr/kurlar/today.xml";

            // 14 Şubat 2013 e ait döviz kurları için
            string anyDays = "http://www.tcmb.gov.tr/kurlar/201302/14022013.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(today);

            // Xml içinden tarihi alma - gerekli olabilir
            DateTime exchangeDate = Convert.ToDateTime(xmlDoc.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);

            string sonuc = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='"+kurTipi+ "']/BanknoteSelling").InnerXml;
            return Convert.ToDouble(sonuc);
        }

        public List<string> ParaBirimleriGetir()
        {
            List<string> paraBirimleri = new List<string>();
            paraBirimleri.Add("USD");
            paraBirimleri.Add("EUR");
            paraBirimleri.Add("GBP");
            paraBirimleri.Add("CHF");
            paraBirimleri.Add("SEK");
            paraBirimleri.Add("CAD");

            return paraBirimleri;
        }

        public List<Tur> TumunuGetir()
        {
            // Bugün (en son iş gününe) e ait döviz kurları için
            string today = "http://www.tcmb.gov.tr/kurlar/today.xml";

            // 14 Şubat 2013 e ait döviz kurları için
            string anyDays = "http://www.tcmb.gov.tr/kurlar/201302/14022013.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(today);

            // Xml içinden tarihi alma - gerekli olabilir
            DateTime exchangeDate = Convert.ToDateTime(xmlDoc.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);
            List<Tur> sonuc = new List<Tur>();

            foreach (var item in ParaBirimleriGetir())
            {
                // BİZİM LİSTELEDİĞİMİZ KURLARIN, KUR TİPLERİNİN TÜMÜNÜ ALIYORUZ

                sonuc.Add(new Tur() {
                    KurTipi=item,
                    Tutar = Convert.ToDouble(xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='" + item + "']/BanknoteSelling").InnerXml)
                });
            }
           
            return sonuc;
        }
    }
}
