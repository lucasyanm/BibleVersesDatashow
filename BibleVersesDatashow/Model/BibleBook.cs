using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BibleVersesDatashow.Model
{
    public class BibleBook
    {
        public required string abbrev { get; set; }
        public required List<List<string>> chapters { get; set; }
        public required string name { get; set; }
    }
}
