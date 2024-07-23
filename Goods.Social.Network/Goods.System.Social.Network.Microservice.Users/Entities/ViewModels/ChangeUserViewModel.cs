using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities.ViewModels
{
    public class ChangeUserViewModel
    {
        public int id { get; set; }
        public string email { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public DateTime date_birthday { get; set;} = DateTime.MinValue;
        public string status { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string avatar { get; set;} = string.Empty;
        public string background { get; set;} = string.Empty;
    }
}
