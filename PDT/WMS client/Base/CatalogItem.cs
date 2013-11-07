﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WMS_client
    {
    public class CatalogItem
        {
        public String Description { get; set; }

        public long Id { get; set; }

        public override string ToString()
            {
            return string.Format("{0} (Id = {1})", Description, Id);
            }
        }
    }