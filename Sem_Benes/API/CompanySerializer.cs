using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Sem_Benes.Model
{
    class CompanySerializer
    {
        public void SerializeCompainesToXml(List<Company> companies, string filePath)
        {
            foreach (var company in companies)
            {
                if (company.Dic.Equals(string.Empty))
                    company.Dic = null;
            }

            var serializerObj = new XmlSerializer(typeof(List<Company>), new XmlRootAttribute("firmy"));
            TextWriter writeFileStream = new StreamWriter(filePath);
            serializerObj.Serialize(writeFileStream, companies);

            writeFileStream.Close();
        }
    }
}
