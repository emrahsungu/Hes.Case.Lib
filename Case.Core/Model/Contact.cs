using System;

namespace CaseLib.Core.Model {
    public class Contact {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PreferredContactChannel { get; set; }
        public string ContactId { get; set; }
        public string ContactIdSource { get; set; }
        public bool IsPrimaryContact { get; set; }
        public bool IncludeInCommunication { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string id { get; set; }

    }
}