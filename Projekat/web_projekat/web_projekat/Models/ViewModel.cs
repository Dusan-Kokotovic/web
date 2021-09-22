using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_projekat.Models
{
    public class ViewModel
    {
       public List<Model_Jedinica> jedinice { get; set; }
       public Model_Smestaj smestaj { get; set; }
        public ViewModel(List<Model_Jedinica> j,Model_Smestaj s)
        {
            this.jedinice = j;
            this.smestaj = s;
           
        }
        public ViewModel() { }
    }
}