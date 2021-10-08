using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class Service
    {
        public int Id { get; set; }
        public int ROOMNO { get; set; }
        public string SDESC { get; set; }
        public decimal AMOUNT { get; set; }
        public string ServiceDate { get; set; }
    }
}
