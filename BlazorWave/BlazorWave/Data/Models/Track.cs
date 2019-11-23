using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWave.Data.Models
{
    public class Track : BaseModel
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public TimeSpan Length { get; set; }
        public string Location { get; set; }
    }
}
