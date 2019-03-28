using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Case.Core
{
    public class FileSystemCaseStorage : ICaseStorage
    {

        /// <summary>
        /// 
        /// </summary>
        private static readonly ILogger Logger = LoggerFactory.GetLogger(typeof(FileSystemCaseStorage));

        /// <summary>
        /// 
        /// </summary>
        private readonly string _directoryToSaveFiles;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryToSaveFiles"></param>
        public FileSystemCaseStorage(string directoryToSaveFiles)
        {
            _directoryToSaveFiles = directoryToSaveFiles;
            if (Directory.Exists(_directoryToSaveFiles) == false)
            {
                Directory.CreateDirectory(_directoryToSaveFiles);
            }
        }

        public bool AddCase(Case caseToAddToStorage) {
            var path = Path.Combine(_directoryToSaveFiles, caseToAddToStorage.Id);
            if (File.Exists(path)) {
                Logger.Debug($"{caseToAddToStorage.Id} already exists. Id must be unique.");
                return false;
            }

            try {
                File.WriteAllText(path, JsonConvert.SerializeObject(caseToAddToStorage, Formatting.Indented));
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public Case GetCase(string id)
        {
            throw new NotImplementedException();
        }
    }
}
