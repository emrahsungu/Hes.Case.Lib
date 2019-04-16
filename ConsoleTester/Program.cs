using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using CaseLib.Core;
using CaseLib.Core.JsonCaseStorage;
using CaseLib.Core.Model;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace ConsoleTester {
    internal class Program {

        private static readonly ICaseStorage CaseStorage = new FileSystemCaseStorage("dbDirectory");


        private static Query QueryMakeQuery(string alias, bool onlyOpenCases = true) {
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

        private static IEnumerable<string> CaseStatusmaker(bool onlyOpenCase) {
            yield return "Open";
            if (onlyOpenCase) {
                yield break;
            }
            yield return "Closed";
        }
        private static void Main(string[] args) {
            var authority = "https://login.microsoftonline.com/common/";
            var authenticationContext = new AuthenticationContext(authority, new TokenCache());
            var result = authenticationContext.AcquireTokenAsync(
                "",
                "",
                new Uri("https://casebuddy.microsoft.com"),
                new PlatformParameters(PromptBehavior.Auto),
                new UserIdentifier("",UserIdentifierType.OptionalDisplayableId)).Result;

           

            using (var client = new WebClient {
                Headers = {
                    ["Content-Type"] =  "application/json",
                    ["Authorization"] = $"Bearer {result.AccessToken}",
                    ["cache-control"] = "no-cache"
                }
            }) {
                var data = JsonConvert.SerializeObject(QueryMakeQuery(""));
                var response = client.UploadData("https://api.support.microsoft.com/v0/queryidresult", "POST", Encoding.UTF8.GetBytes(data));
                var res = JsonConvert.DeserializeObject<Welcome>(Encoding.UTF8.GetString(response));
                var tParam = res.TableParameters.Single();
                foreach (var @case in Enumerable.Range(0, tParam.RowCount).
                    Select(index => CaseMapper.Map(tParam.HeaderNames, res.TableParameters.First().TableParameterResult[index]))) {
                    CaseStorage.AddCase(@case);
                }
            }
        }

    }
}