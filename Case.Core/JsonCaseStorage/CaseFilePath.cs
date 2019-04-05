using System;
using System.IO;
using CaseLib.Core.Logging;
using CaseLib.Core.Model;
using Newtonsoft.Json;

namespace CaseLib.Core.JsonCaseStorage {
    public class CaseFilePath {

        /// <summary>
        /// Logger for this class.
        /// </summary>
        private static readonly ILogger Logger = LoggerFactory.GetLogger(typeof(CaseFilePath));

        /// <summary>
        /// Creates an object which represents a Case, serialized in JSON format.
        /// </summary>
        /// <param name="caseNumber">Case number</param>
        /// <param name="directory">Directory to save files</param>
        public CaseFilePath(string caseNumber, string directory) {
            CaseNumber = caseNumber;
            Directory = directory;
            FilePath = Path.Combine(directory, $"{caseNumber}.json");
        }

        /// <summary>
        /// Case number
        /// </summary>
        public string CaseNumber { get; }

        /// <summary>
        /// Directory to save files
        /// </summary>
        public string Directory { get; }

        /// <summary>
        /// File path.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Checks if the file exists.
        /// </summary>
        /// <returns>true if file exists, false otherwise</returns>
        public bool Exists() {
            return File.Exists(FilePath);
        }

        /// <summary>
        /// Serializes the contents of Case file to the path represented by this object.
        /// </summary>
        /// <param name="case">Case file to serialze to path</param>
        public OResult<bool> SerializeToPath(Case @case) {
            try {
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(@case,Formatting.Indented));
                return OResult<bool>.CreateSucces(true);
            }
            catch (Exception ex) {
                Logger.Debug(ex.Message);
                return OResult<bool>.CreateFailure(ex);
            }
        }

        public OResult<Case> DeserializeFromPath() {
            try {
                var @case = JsonConvert.DeserializeObject<Case>(File.ReadAllText(FilePath));
                return OResult<Case>.CreateSucces(@case);
            }
            catch(Exception ex){
                Logger.Debug(ex.Message);
                return OResult<Case>.CreateFailure(ex);
            }
        }

    }
}