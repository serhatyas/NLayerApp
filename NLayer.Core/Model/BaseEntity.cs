﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Model
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
