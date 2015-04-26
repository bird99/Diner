namespace Diner.Core.OrderProcessing
{
    /// <summary>
    /// Responsible for taking user input and outputing the order in the correct order
    /// </summary>
    public interface IOrderProcessor
    {
        void ProcesOrder(string userInput);
    }
}