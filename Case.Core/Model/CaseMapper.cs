using System.Collections.Generic;
using Newtonsoft.Json;

namespace CaseLib.Core.Model {
    public static class CaseMapper {

        /// <summary>
        /// !!!THIS CODE IS AUTO GENERATED!!!
        /// Using reflection will yield a much more beautfil code at the expense of speed.
        /// I am considering whcih approach to use.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Case Map(string[] headers, string[] data) {
            var c = new Case();
            for (var i = 0; i < headers.Length; i++) {
                var header = headers[i];
                var d = data[i];
                switch (header) {
                    case "CaseNumber":
                        c.CaseNumber = d;
                        break;
                    case "AgentId":
                        c.AgentId = d;
                        break;
                    case "CaseType":
                        c.CaseType = d;
                        break;
                    case "State":
                        c.State = d;
                        break;
                    case "taskId":
                        c.taskId = d;
                        break;
                    case "taskStartsOn":
                        c.taskStartsOn = d;
                        break;
                    case "taskEndsOn":
                        c.taskEndsOn = d;
                        break;
                    case "StateAnnotation":
                        c.StateAnnotation = d;
                        break;
                    case "StateAnnotationLastUpdatedOn":
                        c.StateAnnotationLastUpdatedOn = d;
                        break;
                    case "Severity":
                        c.Severity = d;
                        break;
                    case "CreatedOn":
                        c.CreatedOn = d;
                        break;
                    case "UpdatedOn":
                        c.UpdatedOn = d;
                        break;
                    case "LastEmailInteractionCreatedOn":
                        c.LastEmailInteractionCreatedOn = d;
                        break;
                    case "StateLastUpdatedOn":
                        c.StateLastUpdatedOn = d;
                        break;
                    case "AgentIdAssignedCount":
                        c.AgentIdAssignedCount = d;
                        break;
                    case "CaseAge":
                        c.CaseAge = d;
                        break;
                    case "SlaExpiresOn":
                        c.SlaExpiresOn = d;
                        break;
                    case "SlaCompletedOn":
                        c.SlaCompletedOn = d;
                        break;
                    case "SlaState":
                        c.SlaState = d;
                        break;
                    case "FCRTarget":
                        c.FCRTarget = d;
                        break;
                    case "FCRState":
                        c.FCRState = d;
                        break;
                    case "FCRKpiType":
                        c.FCRKpiType = d;
                        break;
                    case "FDRTarget":
                        c.FDRTarget = d;
                        break;
                    case "FDRState":
                        c.FDRState = d;
                        break;
                    case "FDRKpiType":
                        c.FDRKpiType = d;
                        break;
                    case "Duration":
                        c.Duration = d;
                        break;
                    case "Path":
                        c.Path = d;
                        break;
                    case "servicelevel":
                        c.servicelevel = d;
                        break;
                    case "EntitlementDescription":
                        c.EntitlementDescription = d;
                        break;
                    case "ServiceName":
                        c.ServiceName = d;
                        break;
                    case "supportTimeZone":
                        c.supportTimeZone = d;
                        break;
                    case "supportLanguage":
                        c.supportLanguage = d;
                        break;
                    case "supportCountry":
                        c.supportCountry = d;
                        break;
                    case "Is24x7optedin":
                        c.Is24x7optedin = d;
                        break;
                    case "Customers":
                        c.Customers = JsonConvert.DeserializeObject<List<Customer>>(d);
                        break;
                    case "Title":
                        c.Title = d;
                        break;
                    case "IncidentId":
                        c.IncidentId = d;
                        break;
                    case "isIncidentServiceImpactingEvent":
                        c.isIncidentServiceImpactingEvent = d;
                        break;
                    case "IsCritSit":
                        c.IsCritSit = d;
                        break;
                    case "IsPublicSector":
                        c.IsPublicSector = d;
                        break;
                    case "RestrictedAccess":
                        c.RestrictedAccess = d;
                        break;
                    case "InternalTitle":
                        c.InternalTitle = d;
                        break;
                    case "AssignmentPending":
                        c.AssignmentPending = d;
                        break;
                    case "PolicyCaseType":
                        c.PolicyCaseType = d;
                        break;
                    case "UpdatedBy":
                        c.UpdatedBy = d;
                        break;
                    case "Description":
                        c.Description = d;
                        break;
                    case "BlockedBy":
                        c.BlockedBy = d;
                        break;
                }
            }
            return c;
        }

    }
}