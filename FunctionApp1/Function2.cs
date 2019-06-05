using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Clients.ActiveDirectory.Extensibility;
using Newtonsoft.Json;

namespace FunctionApp1
{



    public class Ui : ICustomWebUi {

        public Task<Uri> AcquireAuthorizationCodeAsync(Uri authorizationUri, Uri redirectUri) {
            return Task<Uri>.FromResult(authorizationUri);
        }

    }

    public class Contact
    {

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

    public class Customer
    {

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
    public static class Function2
    {
        [FunctionName("Function2")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,ILogger log) {

            var by = Convert.FromBase64String(
                "AwAAAAEAAACOAWh0dHBzOi8vbG9naW4ud2luZG93cy5uZXQvNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3Lzo6Omh0dHBzOi8vYXBpLnN1cHBvcnQubWljcm9zb2Z0LmNvbTo6OmFkOWEzOGRjLTJmYTUtNDg2My05NTU3LTRmOWI0YTIzZTQ0Yjo6OjDrInsiUmF3Q2xpZW50SW5mbyI6ImV5SjFhV1FpT2lKa1pEVm1PV1k0TXkwd1pqVm1MVFEyWXpNdFlqYzBNUzAwWlRJNVpUVTVOVGMwTXpjaUxDSjFkR2xrSWpvaU56Sm1PVGc0WW1ZdE9EWm1NUzAwTVdGbUxUa3hZV0l0TW1RM1kyUXdNVEZrWWpRM0luMCIsIlJlZnJlc2hUb2tlbiI6IkFRQUJBQUFBQUFEQ29NcGpKWHJ4VHE5Vkc5dGUtN0ZYZ3lnbGZ5N2piLVZiLU1fX1YxVXN6VVNHaEJhMzV4NlJQeVhLTTJMaWw3bTFJMGh6akNQakVqaHRYMHpBV2ktSUdWYmtxNk83Z2k5T3BHd3l4Nl9WVzJfVi10eWxuS3UyaHlfbV9Td0k2NExVNWlwMFVFdHU0aEJlUzQzbUI4YTBYNEZlM3cxZThtV3dmclctTXdyQ1E3bTZ0eTVKLUNxQmN1S2dpMjkwZ1RHcFItOTJkRTVOVDlUN2N1LXV3djRabl9QVjB6al9JSmQ3TEl2d01kVnBWMHptcTlVZW93ck9CYjJkM2RmaC1YdzNxYVNCQ3d0TmtwbjJSek1mTTVweUV5SmxWYWVGanI3bXFTaG9CR1JjVjEzSVZWR0dhRUZJb1R1blU0SlJpZklmOThWVnM2TC1IUHBHQnFPZE8tVFRPTk1ZdGNFM0RqX3BEX2c5MUstbHpNRG1yaXN0M1U5ZnV1UVBmNktGankxTFhLS3J4VE5SWjlJQUl4Q2dwdTdQYjZEam9hWmoxM0VhNjVTR2FQU2tkSXhLZERUNWpmMHFqZElxa0ZYUUNRQ0x3bXp6ZHZfRzlDZHNEbnpvNzNjaTI0clJQNU5SbUYxWV92LXNkUlhmMnFjcXBBUmtKMXZ4STdlNHctTzJmdE5SZkp4LUtrVVI4VnlEWjltZ1FKZzNiZEZvQVc1eFdJeFI5blpmM0VXeUNpeGJfbGt5Tm5haEJ5S3VwM0tjS0dRS1k2bjUyMUNWUXBJNC0wYlJIbnJKZV9Jdk1KaF9PT0dOdnpwZFVaTV9FWkp3QTg0X29FM2hrUEQ4U3hEcnNoZDZPZ0FpY2c4UDIxc2V1V1ljMjExYXphT20xSmpZZ0xDNXFOQWcydjNUZFRGN1A5UEgzWmVUMU9Kbl9BRUgzbTFIU281TTBpMlhoY2t4UmRrWHcxZ0Mya2MxMEk0MFMybHFJWTVtZUdKX3cwajVtdFpZb1F1WHhqbGoxeURKbk9KV3FHd3RlSXlOVVUyNTBDSUhvMnBCSUZuR3RUdHpoZlZ6M09YVVFTSVFQRDZaRkFpYVVLT19qN1dhSHdiT2VMaEFnX3RRRHJUU0JobFhZdnBrX05WM1JOMmZOZ09FY2h0Y1BTSWF0cnNRdWotTm5lMUxnTzlRWkVveHZvekswNzhNQW9INElnakp2ZU93TUxmdzJCX1hsd0tZYlBSLTZ6Zmp3ZDNDRVYyX1B1bnBuUUVtdTlhUXRxbjhROHhhQTllVHE0bkJvX0paZnllR2RVNmFlQ0hxQ2ZVM1FROWozRU9wZzFOVGZhVS1VV1ppc1ZOblA3YkE0eG02Z2JhM2lMRXJ3ZEtaYkNWUjBXUGZMZWZPTmNMT1hWd0NuOGpaeHhWeTNnSUFQaU10bUZZNGg0bWhHVFROV0k1S1VSR3hyVS1GbmptUm00ZGZFUm90OFl1UmtyOXFBZHVOdXNZNGpadnlDamxpajN3blFMNk5xeEw1ZjJqR0owNmVYVTQzeDNLU0dXbnJKZ1F0eUVxZ3N1b1VWa2h0QmlYMmNoRy1JcUZ4MkR2LVBEY3kxaUFBIiwiUmVzb3VyY2VJblJlc3BvbnNlIjoiaHR0cHM6XC9cL2FwaS5zdXBwb3J0Lm1pY3Jvc29mdC5jb20iLCJSZXN1bHQiOnsiQWNjZXNzVG9rZW4iOiJleUowZVhBaU9pSktWMVFpTENKaGJHY2lPaUpTVXpJMU5pSXNJbmcxZENJNklraENlR3c1YlVGbE5tZDRZWFpEYTJOdlQxVXlWRWh6UkU1aE1DSXNJbXRwWkNJNklraENlR3c1YlVGbE5tZDRZWFpEYTJOdlQxVXlWRWh6UkU1aE1DSjkuZXlKaGRXUWlPaUpvZEhSd2N6b3ZMMkZ3YVM1emRYQndiM0owTG0xcFkzSnZjMjltZEM1amIyMGlMQ0pwYzNNaU9pSm9kSFJ3Y3pvdkwzTjBjeTUzYVc1a2IzZHpMbTVsZEM4M01tWTVPRGhpWmkwNE5tWXhMVFF4WVdZdE9URmhZaTB5WkRkalpEQXhNV1JpTkRjdklpd2lhV0YwSWpveE5UVTVOalkwTkRRNExDSnVZbVlpT2pFMU5UazJOalEwTkRnc0ltVjRjQ0k2TVRVMU9UWTJPRE0wT0N3aVlXTnlJam9pTVNJc0ltRnBieUk2SWtGV1VVRnhMemhNUVVGQlFXSnJUVVpaSzNJMUx5OUtRVFJsVFZoSGIxRnlkbFJ4YTNoTlMyMWpjRmRoUWtwUVdqWkdjVEpQY2xGU1dFeFFVbkl3U1RkbmNHWlNibVkzTVRsWVdIZFZkbGhwUTBGUVQzSnNTbmhJY1hkb1JEWmtTREozSzNSTFVpOUZVM2hsZVZKU1VFRklNVEZCUjFkVlBTSXNJbUZ0Y2lJNld5SndkMlFpTENKeWMyRWlMQ0p0Wm1FaVhTd2lZWEJ3YVdRaU9pSmhaRGxoTXpoa1l5MHlabUUxTFRRNE5qTXRPVFUxTnkwMFpqbGlOR0V5TTJVME5HSWlMQ0poY0hCcFpHRmpjaUk2SWpBaUxDSmtaWFpwWTJWcFpDSTZJamd4TjJFek5EUXhMVEkzT0RjdE5EVmxOeTA0WWpNM0xXVXhaalZtT0RBMU9HVTFPQ0lzSW1aaGJXbHNlVjl1WVcxbElqb2lVM1Z1WjNVaUxDSm5hWFpsYmw5dVlXMWxJam9pUlcxeVlXZ2lMQ0pwY0dGa1pISWlPaUl5Tnk0eE16Y3VNall1TmpNaUxDSnVZVzFsSWpvaVJXMXlZV2dnVTNWdVozVWlMQ0p2YVdRaU9pSmtaRFZtT1dZNE15MHdaalZtTFRRMll6TXRZamMwTVMwMFpUSTVaVFU1TlRjME16Y2lMQ0p2Ym5CeVpXMWZjMmxrSWpvaVV5MHhMVFV0TWpFdE1qRTBOamMzTXpBNE5TMDVNRE16TmpNeU9EVXROekU1TXpRME56QTNMVEkwTmpjMU9UY2lMQ0p6WTNBaU9pSjFjMlZ5WDJsdGNHVnljMjl1WVhScGIyNGlMQ0p6ZFdJaU9pSTJSRmt0YTNZM0xVRnFhREJTWTJRd1VuQkhOVEJRTUZaak5IbENkR3hYVjBoWWJrbDNiRmxZU1d4Rklpd2lkR2xrSWpvaU56Sm1PVGc0WW1ZdE9EWm1NUzAwTVdGbUxUa3hZV0l0TW1RM1kyUXdNVEZrWWpRM0lpd2lkVzVwY1hWbFgyNWhiV1VpT2lKb1lYTjFibWQxUUcxcFkzSnZjMjltZEM1amIyMGlMQ0oxY0c0aU9pSm9ZWE4xYm1kMVFHMXBZM0p2YzI5bWRDNWpiMjBpTENKMWRHa2lPaUpMWTBaR01tWlZhMVJGY1ZWc1MyTnVlREpqUVVGQklpd2lkbVZ5SWpvaU1TNHdJbjAud2dmV0hjaTc0OEdEeEdmejl3clBUVmoxUXp6TWd0R1dKRmRfLU5uZERmQ2Z0eFdpUzVDUFRZNDAyQ3BFSDZIOXliOG1uOENSOEVlRW4wSFQzalpJSnlEcU9XaTQzTkVTTDRZXzYxSXRSdUFIOW5Sckc1N01Lcmt5ajF2cXVsUWUxbUw4U0pMdmc0TTAwc2tKanNTckV3UUNJems4S2VOZEdsS2R6N3RwSmx5VnRZV1R4QXp0WUZKSnpPVGc5OE9SNURlYW01VC1YNzZoNThpU3dwSnBvWVdxSFpIeVFmbXNSLXl0WmRkUzhXbE85VzBRQzV6Q05sQ0FOWlF6THU4ZEtNMERsV2k0WFJxN3RqSFVzZWlRdkVBa1NPejgzUUpoSXRmNU13UXBsZGN3bG1XSmxubDlGRG9rYmM0RFQtd1VrNEpheXZLV0FZZkJBMEt3VC01Q3VnIiwiQWNjZXNzVG9rZW5UeXBlIjoiQmVhcmVyIiwiRXhwaXJlc09uIjp7IkRhdGVUaW1lIjoiXC9EYXRlKDE1NTk2NjgzNDczMjgpXC8iLCJPZmZzZXRNaW51dGVzIjowfSwiRXh0ZW5kZWRFeHBpcmVzT24iOnsiRGF0ZVRpbWUiOiJcL0RhdGUoMTU1OTY2ODM0NzMyOClcLyIsIk9mZnNldE1pbnV0ZXMiOjB9LCJFeHRlbmRlZExpZmVUaW1lVG9rZW4iOmZhbHNlLCJJZFRva2VuIjoiZXlKMGVYQWlPaUpLVjFRaUxDSmhiR2NpT2lKdWIyNWxJbjAuZXlKaGRXUWlPaUpoWkRsaE16aGtZeTB5Wm1FMUxUUTROak10T1RVMU55MDBaamxpTkdFeU0yVTBOR0lpTENKcGMzTWlPaUpvZEhSd2N6b3ZMM04wY3k1M2FXNWtiM2R6TG01bGRDODNNbVk1T0RoaVppMDRObVl4TFRReFlXWXRPVEZoWWkweVpEZGpaREF4TVdSaU5EY3ZJaXdpYVdGMElqb3hOVFU1TmpZME5EUTRMQ0p1WW1ZaU9qRTFOVGsyTmpRME5EZ3NJbVY0Y0NJNk1UVTFPVFkyT0RNME9Dd2lZVzF5SWpwYkluQjNaQ0lzSW5KellTSXNJbTFtWVNKZExDSm1ZVzFwYkhsZmJtRnRaU0k2SWxOMWJtZDFJaXdpWjJsMlpXNWZibUZ0WlNJNklrVnRjbUZvSWl3aWFYQmhaR1J5SWpvaU1qY3VNVE0zTGpJMkxqWXpJaXdpYm1GdFpTSTZJa1Z0Y21Gb0lGTjFibWQxSWl3aWIybGtJam9pWkdRMVpqbG1PRE10TUdZMVppMDBObU16TFdJM05ERXROR1V5T1dVMU9UVTNORE0zSWl3aWIyNXdjbVZ0WDNOcFpDSTZJbE10TVMwMUxUSXhMVEl4TkRZM056TXdPRFV0T1RBek16WXpNamcxTFRjeE9UTTBORGN3TnkweU5EWTNOVGszSWl3aWMzVmlJam9pZFdwb2FYbENaV2RMZEVVNFNVVkdURTlVVGpFM1kyZG1abE42ZEd0VlQxZE5RbmRvZHpGSE1UVkdXU0lzSW5ScFpDSTZJamN5WmprNE9HSm1MVGcyWmpFdE5ERmhaaTA1TVdGaUxUSmtOMk5rTURFeFpHSTBOeUlzSW5WdWFYRjFaVjl1WVcxbElqb2lhR0Z6ZFc1bmRVQnRhV055YjNOdlpuUXVZMjl0SWl3aWRYQnVJam9pYUdGemRXNW5kVUJ0YVdOeWIzTnZablF1WTI5dElpd2lkbVZ5SWpvaU1TNHdJbjAuIiwiVGVuYW50SWQiOiI3MmY5ODhiZi04NmYxLTQxYWYtOTFhYi0yZDdjZDAxMWRiNDciLCJVc2VySW5mbyI6eyJEaXNwbGF5YWJsZUlkIjoiaGFzdW5ndUBtaWNyb3NvZnQuY29tIiwiRmFtaWx5TmFtZSI6IlN1bmd1IiwiR2l2ZW5OYW1lIjoiRW1yYWgiLCJJZGVudGl0eVByb3ZpZGVyIjoiaHR0cHM6XC9cL3N0cy53aW5kb3dzLm5ldFwvNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3XC8iLCJQYXNzd29yZENoYW5nZVVybCI6bnVsbCwiUGFzc3dvcmRFeHBpcmVzT24iOm51bGwsIlVuaXF1ZUlkIjoiZGQ1ZjlmODMtMGY1Zi00NmMzLWI3NDEtNGUyOWU1OTU3NDM3In19LCJVc2VyQXNzZXJ0aW9uSGFzaCI6bnVsbH0=");
            var to = new TokenCache(by);


            var authority = "https://login.microsoftonline.com/common/";
            var authenticationContext = new AuthenticationContext(authority, to);

            var result = authenticationContext.AcquireTokenAsync(
                "https://api.support.microsoft.com",
                "ad9a38dc-2fa5-4863-9557-4f9b4a23e44b",
                new Uri("https://casebuddy.microsoft.com"),
                new PlatformParameters(PromptBehavior.Auto, new Ui()),
                new UserIdentifier("hasungu@microsoft.com", UserIdentifierType.OptionalDisplayableId)).Result;




            var hashset = new HashSet<string>();
            using (var client = new WebClient
            {
                Headers = {
                    ["Content-Type"] =  "application/json",
                    ["Authorization"] = $"Bearer {result.AccessToken}",
                    ["cache-control"] = "no-cache"
                }
            })
            {

           

                var jsondata = JsonConvert.SerializeObject(QueryMakeQuery("hasungu@microsoft.com"));
                var response = client.UploadData("https://api.support.microsoft.com/v0/queryidresult", "POST", Encoding.UTF8.GetBytes(jsondata));
                var res = JsonConvert.DeserializeObject<Welcome>(Encoding.UTF8.GetString(response));
                var tParam = res.TableParameters.Single();
                foreach (var @case in Enumerable.Range(0, tParam.RowCount).
                    Select(index => CaseMapper.Map(tParam.HeaderNames, res.TableParameters.First().TableParameterResult[index]))) {
                    hashset.Add(@case.CaseNumber);
                }
            }
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            return   new OkObjectResult(hashset.OrderBy(x => x).Last());
        }
        public partial class TableParameter
        {
            [JsonProperty("header_names")]
            public string[] HeaderNames { get; set; }

            [JsonProperty("table_parameter_result")]
            public string[][] TableParameterResult { get; set; }

            [JsonProperty("row_count")]
            public int RowCount { get; set; }
        }
        public partial class Welcome
        {
            [JsonProperty("table_parameters")]
            public TableParameter[] TableParameters { get; set; }

            [JsonProperty("result_type")]
            public string ResultType { get; set; }
        }

        public class Query
        {
            public string queryid { get; set; }
            public List<QueryParameter> query_parameters { get; set; }
        }

        public class QueryParameter
        {
            public string name { get; set; }
            public List<string> values { get; set; }
        }

        public class Case
        {

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

        public static class CaseMapper
        {

            /// <summary>
            /// !!!THIS CODE IS AUTO GENERATED!!!
            /// Using reflection will yield a much more beautfil code at the expense of speed.
            /// I am considering whcih approach to use.
            /// </summary>
            /// <param name="headers"></param>
            /// <param name="data"></param>
            /// <returns></returns>
            public static Case Map(string[] headers, string[] data)
            {
                var c = new Case();
                for (var i = 0; i < headers.Length; i++)
                {
                    var header = headers[i];
                    var d = data[i];
                    switch (header)
                    {
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

        private static IEnumerable<string> CaseStatusmaker(bool onlyOpenCase)
        {
            yield return "Open";
            if (onlyOpenCase)
            {
                yield break;
            }
            yield return "Closed";
        }


        private static Query QueryMakeQuery(string alias, bool onlyOpenCases = true)
        {
            // Queries all the cases for the given user
            return new Query
            {
                queryid = "CASE_QUERY",
                query_parameters = new List<QueryParameter> {
                    new QueryParameter {
                        name = "caseType",
                        values = new List<string> {"Case"}
                    },
                    new QueryParameter {
                        name = "state",
                        values = new List<string> ( CaseStatusmaker(onlyOpenCases))
                    },
                    new QueryParameter {
                        name = "selectedAgentId",
                        values = new List<string> { alias}
                    },
                    new QueryParameter {
                        name = "agentId",
                        values = new List<string> { alias }
                    }
                }
            };
        }
    }
}
