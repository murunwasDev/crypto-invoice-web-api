﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Entities;

namespace WebApi.Models.InvoiceAjaxModel
{
    public class InvoiceInitDataAjaxModel
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public List<object> InvoiceList {get; set; }
    }
    public class InvoiceInitModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int Status { get; set; }
    }
}
