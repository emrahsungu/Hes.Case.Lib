using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace CaseLib.Core.Model {
    public class Case {

        /// <summary>
        /// 
        /// </summary>
        public string CaseNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AgentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CaseType { get; set; }
        public string State { get; set; }
        public string taskId { get; set; }
        public string taskStartsOn { get; set; }
        public string taskEndsOn { get; set; }
        public string StateAnnotation { get; set; }
        public string StateAnnotationLastUpdatedOn { get; set; }
        public string Severity { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string LastEmailInteractionCreatedOn { get; set; }
        public string StateLastUpdatedOn { get; set; }
        public string AgentIdAssignedCount { get; set; }
        public string CaseAge { get; set; }
        public string SlaExpiresOn { get; set; }
        public string SlaCompletedOn { get; set; }
        public string SlaState { get; set; }
        public string FCRTarget { get; set; }
        public string FCRState { get; set; }
        public string FCRKpiType { get; set; }
        public string FDRTarget { get; set; }
        public string FDRState { get; set; }
        public string FDRKpiType { get; set; }
        public string Duration { get; set; }
        public string Path { get; set; }
        public string servicelevel { get; set; }
        public string EntitlementDescription { get; set; }
        public string ServiceName { get; set; }
        public string supportTimeZone { get; set; }
        public string supportLanguage { get; set; }
        public string supportCountry { get; set; }
        public string Is24x7optedin { get; set; }
        public List<Customer> Customers { get; set; }
        public string Title { get; set; }
        public string IncidentId { get; set; }
        public string isIncidentServiceImpactingEvent { get; set; }
        public string IsCritSit { get; set; }
        public string IsPublicSector { get; set; }
        public string RestrictedAccess { get; set; }
        public string InternalTitle { get; set; }
        public string AssignmentPending { get; set; }
        public string PolicyCaseType { get; set; }
        public string UpdatedBy { get; set; }
        public string Description { get; set; }
        public string BlockedBy { get; set; }

    }
}