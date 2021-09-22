﻿using Bibliothek.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.Model.Entity
{
    public class Slider:CoreEntity
    {
        public string SliderPath { get; set; }
        public string Queue { get; set; }
        public Guid CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
