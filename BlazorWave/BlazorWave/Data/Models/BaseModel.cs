using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWave.Data.Models
{
    public class BaseModel
    {
        public string Id { get; set; }
        public DateTime Added { get; set; }
        public DateTime Updated { get; set; }
    }
}
