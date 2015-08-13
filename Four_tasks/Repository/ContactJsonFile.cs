using System.Collections.Generic;
using Four_tasks.Repository.Interfaces;
using Four_tasks.Repository.Config;
using System.Configuration;
using System.IO;
using Four_tasks.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Four_tasks.Repository
{
    public class ContactJsonFile : IContactJsonRepository
    {
        string fileName = ((FileNameConfiguration)ConfigurationManager.GetSection("fileNameConfiguration")).FileName;

        public async Task writeAsync(Contact data)
        {
            if (null != data)
            {                
                var tempData = await readAsync();
                tempData.Add(data);
                FileStream fs = new FileStream(fileName, FileMode.Create);

                using (StreamWriter sW = new StreamWriter(fs))
                {                    
                    var serializeData = JsonConvert.SerializeObject(tempData);                    
                    sW.Write(serializeData);                    
                }
            }
        }

        public async Task<List<Contact>> readAsync()
        {
            List<Contact> dat = null;
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);

            using (StreamReader sR = new StreamReader(fs))
            {                
                var tempData = await sR.ReadToEndAsync();
                dat = JsonConvert.DeserializeObject<List<Contact>>(tempData);
            }
            return (null==dat)?new List<Contact>(0):dat;
        }
    }
}