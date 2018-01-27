using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace HavaDurumu
{
    // NOT: "HavaDurumu" sınıf adını kodda, svc'de ve yapılandırma dosyasında birlikte değiştirmek için "Yeniden Düzenle" menüsündeki "Yeniden Adlandır" komutunu kullanabilirsiniz.
    // NOT: Bu hizmeti test etmek üzere WCF Test İstemcisi'ni başlatmak için lütfen Çözüm Gezgini'nde HavaDurumu.svc'yi veya HavaDurumu.svc.cs'yi seçin ve hata ayıklamaya başlayın.
    public class HavaDurumu : IHavaDurumu
    {
        public List<Tur> GetData()
        {
            string link = "https://www.mgm.gov.tr/FTPDATA/analiz/sonSOA.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(link);

            XmlNodeList veriler = xmlDoc.SelectNodes("SOA/sehirler");
            List<Tur> sonuc = new List<Tur>();
            foreach (XmlNode item in veriler)
            {
                sonuc.Add(new Tur() {
                    Bolge= item["Bolge"].InnerText,
                    Durum= item["Durum"].InnerText,
                    Il= item["ili"].InnerText,
                    Peryot= item["Peryot"].InnerText,
                    Max= int.Parse(item["Mak"].InnerText),
                    Min= int.Parse(item["Min"].InnerText),
                });
            }
            return sonuc;
        }
    }
}
