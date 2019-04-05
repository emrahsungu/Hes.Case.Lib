using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CaseLib.Core;
using CaseLib.Core.JsonCaseStorage;
using CaseLib.Core.Model;
using Newtonsoft.Json;

namespace ConsoleTester {
    internal class Program {

        private static readonly ICaseStorage CaseStorage = new FileSystemCaseStorage("dbDirectory");

        private static void Main(string[] args) {

            // Queries all the cases for the given user
            var query = new Query {
                queryid = "CASE_QUERY",
                query_parameters = new List<QueryParameter> {
                    new QueryParameter {
                        name = "caseType",
                        values = new List<string> {"Case"}
                    },
                    new QueryParameter {
                        name = "state",
                        values = new List<string> {"Open"}
                    },
                    new QueryParameter {
                        name = "selectedAgentId",
                        values = new List<string> {"MYALIAS@m*****.com"}
                    },
                    new QueryParameter {
                        name = "agentId",
                        values = new List<string> { "MYALIAS@m*****.com" }
                    }
                }
            };

            using (var client = new WebClient {
                Headers = {
                    ["Content-Type"] =  "application/json",
                    ["Authorization"] = "Bearer MY JWT TOKEN",
                    ["cache-control"] = "no-cache"
                }
            }) {
                var data = JsonConvert.SerializeObject(query);
                var response = client.UploadData("", "POST", Encoding.UTF8.GetBytes(data));
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