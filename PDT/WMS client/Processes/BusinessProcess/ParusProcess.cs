using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WMS_client.Processes
    {
    public abstract class ParusProcess : BusinessProcess
        {
        public ParusProcess()
            : base(1)
            {
            }

        protected void chooseLiner(Action<CatalogItem> action)
            {
            SelectFromCatalog(new Repository().GetLinersList(), action);
            }

        protected void chooseTray(Action<CatalogItem> action)
            {
            SelectFromCatalog(new Repository().GetTraysList(), action);
            }
        }
    }
