using System;
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

        public bool Empty
            {
            get { return Id <= 0; }
            }

        public CatalogItem GetCopy()
            {
            return new CatalogItem()
                {
                    Description = Description,
                    Id = Id
                };
            }

        public void Clear()
            {
            Description = string.Empty;
            Id = 0;
            }

        public void CopyFrom(CatalogItem item)
            {
            if (item == null)
                {
                Id = 0;
                Description = string.Empty;
                }
            else
                {
                Id = item.Id;
                Description = item.Description;
                }
            }
        }
    }
