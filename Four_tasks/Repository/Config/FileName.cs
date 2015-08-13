using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Four_tasks.Repository.Config
{
    public class FileNameConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("fileName", IsRequired = true)]
        public string FileName
        {
            get
            {
                return this["fileName"] as string;
            }
            set
            {
                this["fileName"] = value;
            }
        }
    }
}