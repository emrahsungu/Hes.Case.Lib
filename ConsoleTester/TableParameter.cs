using Newtonsoft.Json;

namespace ConsoleTester {
    public partial class TableParameter
    {
        [JsonProperty("header_names")]
        public string[] HeaderNames { get; set; }

        [JsonProperty("table_parameter_result")]
        public string[][] TableParameterResult { get; set; }

        [JsonProperty("row_count")]
        public int RowCount { get; set; }
    }
}