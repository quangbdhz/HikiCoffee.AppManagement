namespace HikiCoffee.Models.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }

        public ApiSuccessResult()
        {
            IsSuccessed = true;
            Message = "Successful";
        }

        public ApiSuccessResult(string message)
        {
            IsSuccessed = true;
            Message = message;
        }
    }
}
