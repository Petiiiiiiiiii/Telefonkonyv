using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA240129
{
    internal class Ember
    {
        public string Neve { get; set; }
        public string Telefonszama { get; set; }

        public Ember(string sor)
        {
            var atmeneti = sor.Split(';');
            this.Neve = atmeneti[0];
            this.Telefonszama = atmeneti[1];
        }
        public Ember(string Neve, string Telefonszama)
        {
            this.Neve = Neve;
            this.Telefonszama = Telefonszama;
        }

        public override string ToString()
        {
            return $"{this.Neve} | {this.Telefonszama}";
        }
    }
}
