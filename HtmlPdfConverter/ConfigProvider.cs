using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace HtmlPdfConverter
{
    internal class ConfigProvider
    {
        public int Id;
        public ConfigProvider()
        {

        }

        public bool GetConfigParams()
        {
          var configParser = new FileIniDataParser();
            try
            {
                IniData data = configParser.ReadFile(".config");
                Id = int.Parse(data["General"]["ID_Converter"]);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
