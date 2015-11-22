using System;
using System.Xml.Serialization;

namespace Sem_Benes.Model
{
    [Serializable()]
    [XmlType("firma")]
    public class Company
    {
        [XmlIgnore]
        public long Id { get; set; }
        [XmlAttribute(AttributeName = "ICO")]
        public int Ico { get; set; }
        [XmlAttribute(AttributeName = "DIC")]
        public string Dic { get; set; }
        [XmlElement(ElementName = "nazev")]
        public string Name { get; set; }
        [XmlElement(ElementName = "obor-cinnosti")]
        public string BusinessType { get; set; }
        [XmlElement(ElementName = "adresa")]
        public string Address { get; set; }

        public Company()
        {
        }

        public Company(int ico, string dic, string address, string name, string businessType)
        {
            Id = -1;
            Ico = ico;
            Dic = dic;
            Address = address;
            Name = name;
            BusinessType = businessType;
        }
    }
}
