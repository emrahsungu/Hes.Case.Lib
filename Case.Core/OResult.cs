using System;

namespace CaseLib.Core {
    public sealed class OResult<T> {

        /// <summary>
        /// Creates an Operation Result object with provided exception.
        /// </summary>
        /// <param name="exception">Exception thrwon while trying to process the operation.</param>
        /// <param name="isSuccessful">Is operation successful?</param>
        public OResult(Exception exception, bool isSuccessful = false) {
            Exception = exception;
            IsSuccessful = isSuccessful;
        }

        /// <summary>
        /// Creates an Operation Result object with provided result.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="isSuccessful"></param>
        public OResult(T result, bool isSuccessful = true) {
            Result = result;
            IsSuccessful = isSuccessful;
        }

        /// <summary>
        /// Exception which occurred during the operation, if any.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Result of the operation, if exists.
        /// </summary>
        public T Result { get; }

        /// <summary>
        /// Is operation successful?
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static OResult<T> CreateSucces(T result) {
            return new OResult<T>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static OResult<T> CreateFailure(Exception exception) {
            return new OResult<T>(exception);
        }

    }
}