using Newtonsoft.Json;

namespace ConsoleTester {
    public partial class Welcome
    {
        [JsonProperty("table_parameters")]
        public TableParameter[] TableParameters { get; set; }

        [JsonProperty("result_type")]
        public string ResultType { get; set; }
    }
}