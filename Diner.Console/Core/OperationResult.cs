using Diner.Enums;

namespace Diner.Core
{
    /// <summary>
    /// Wraps success and failure operations with a message and object
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class OperationResult<TResult>
    {
        public TResult Item;

        public string Message;

        public bool IsSuccessful
        {
            get { return Status == OperationStatus.Success; }
        }

        public bool IsFailure
        {
            get { return Status == OperationStatus.Failure; }
        }

        public OperationStatus Status { get; private set; }

        public OperationResult()
        {
            Status = OperationStatus.Unknown;;
        }

        public void SetAsFail(string message)
        {
            Message = message;
            Status = OperationStatus.Failure;;
        }        
        
        public void SetAsSuccess(TResult item, string message = null)
        {
            Item = item;
            Message = message;
            Status = OperationStatus.Success;
        }

    }
}