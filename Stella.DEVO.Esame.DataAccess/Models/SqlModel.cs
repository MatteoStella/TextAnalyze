using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stella.DEVO.Esame.DataAccess.Models
{
    public class SqlModel
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Sentiment { get; set; }
        public string Positive { get; set; }
        public string Negative { get; set; }
        public string Neutral { get; set; }
    }
}
