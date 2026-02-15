namespace SAFLC_MVC.Applications.Helpers
{
    public class ResponseHelper
    {
        public static ResultResponse<T> BuildSuccess<T>(T data, string? message = null)
        {
            return new ResultResponse<T>
            {
                Success = true,
                Item = data,
                Message = message
            };
        }
        public static ResultResponse<T> BuildFailure<T>(string message)
        {
            return new ResultResponse<T>
            {
                Success = false,
                Message = message,
                Item = default,
            };
        }

        public static ResultResponse<T> BuildFailureList<T>(List<T> data, List<string> errors, string? message = null)
        {
            return new ResultResponse<T>
            {
                Success = false,
                Item = default,
                Errors = errors,
                Message = "Validation failed."
            };
        }
    }
}
