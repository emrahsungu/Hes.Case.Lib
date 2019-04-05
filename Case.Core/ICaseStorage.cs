using CaseLib.Core.Model;

namespace CaseLib.Core {
    public interface ICaseStorage {

        /// <summary>
        /// </summary>
        /// <param name="caseToAddToStorage"></param>
        /// <returns></returns>
        OResult<bool> AddCase(Case caseToAddToStorage);

        /// <summary>
        /// </summary>
        /// <param name="caseNumber"></param>
        /// <returns></returns>
        OResult<Case> GetCase(string caseNumber);

    }
}