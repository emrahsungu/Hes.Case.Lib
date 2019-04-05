using System.Collections.Generic;

namespace ConsoleTester {
    public class Query
    {
        public string queryid { get; set; }
        public List<QueryParameter> query_parameters { get; set; }
    }
}