using System;
using System.IO;
using CaseLib.Core.Logging;
using CaseLib.Core.Model;

namespace CaseLib.Core.JsonCaseStorage {
    public class FileSystemCaseStorage : ICaseStorage {

        /// <summary>
        /// Logger for this class.
        /// </summary>
        private static readonly ILogger Logger = LoggerFactory.GetLogger(typeof(FileSystemCaseStorage));

        /// <summary>
        /// Directory to save Json files.
        /// </summary>
        private readonly string _directoryToSaveFiles;

        /// <summary>
        /// Creates an ICaseStorage which saves cases to JSON formated files.
        /// </summary>
        /// <param name="directoryToSaveFiles">Directory to save files.</param>
        public FileSystemCaseStorage(string directoryToSaveFiles) {
            _directoryToSaveFiles = directoryToSaveFiles;
            if (Directory.Exists(_directoryToSaveFiles) == false) {
                Logger.Debug($"{directoryToSaveFiles} does not exist, so creating...");
                Directory.CreateDirectory(_directoryToSaveFiles);
            }
        }

        /// <summary>
        /// Adds a case to CaseStorage.
        /// </summary>
        /// <param name="caseToAddToStorage">Case to add to CaseStorage</param>
        /// <returns>Operation Result</returns>
        public OResult<bool> AddCase(Case caseToAddToStorage) {
            var caseFile = new CaseFilePath(caseToAddToStorage.CaseNumber, _directoryToSaveFiles);
            if (caseFile.Exists()) {
                var errMsg = $"{caseFile.FilePath} already exists. {caseFile.CaseNumber} must be unique.";
                Logger.Debug(errMsg);
                return OResult<bool>.CreateFailure(new Exception(errMsg));
            }
            return caseFile.SerializeToPath(caseToAddToStorage);
        }

        /// <summary>
        /// Gets the case with the given case number.
        /// </summary>
        /// <param name="caseNumber">Case Number</param>
        /// <returns></returns>
        public OResult<Case> GetCase(string caseNumber) {
            var caseFile = GetFilePath(caseNumber);
            if (caseFile.Exists() == false) {
                var exception = new FileNotFoundException(caseFile.FilePath);
                Logger.Debug(exception.Message);
                return OResult<Case>.CreateFailure(exception);
            }
            return caseFile.DeserializeFromPath();
                    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caseNumber"></param>
        /// <returns></returns>
        private CaseFilePath GetFilePath(string caseNumber) {
            return new CaseFilePath(caseNumber,_directoryToSaveFiles);
        }
    }
}