using System;
using System.Collections.Generic;

namespace CaseLib.Core.Model {
    public class Customer {

        public List<Contact> Contacts { get; set; }
        public string CustomerId { get; set; }
        public string CustomerIdSource { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string id { get; set; }

    }
}