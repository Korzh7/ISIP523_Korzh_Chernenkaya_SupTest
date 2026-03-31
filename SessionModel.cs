using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8
{
    public class SessionModel
    {
        public int Id { get; set; } 
        public DateTime DateTime { get; set; }
        public string HallNumber { get; set; }
        public string RatingName { get; set; }
        public decimal? ChairPrice { get; set; }
        public string Description { get; set; }
    }
}