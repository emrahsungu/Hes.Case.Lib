namespace Case.Core
{
    public interface ICaseStorage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caseToAddToStorage"></param>
        /// <returns></returns>
        bool AddCase(Case caseToAddToStorage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Case GetCase(string id);
    }
}