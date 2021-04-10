using System;
using System.Collections.Generic;
using System.Text;

namespace APITool
{
    public class Hello
    {
        public string name { get; set; }
        public int id { get; set; }

        public override string ToString()
        {
            return ($"[\"name\":\"{name}\",\"id\":\"{id}\"]");
        }
    }
}
