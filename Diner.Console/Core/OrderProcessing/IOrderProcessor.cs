namespace Diner.Core.OrderProcessing
{
    /// <summary>
    /// Responsible for taking user input and outputing the order in the correct order
    /// </summary>
    public interface IOrderProcessor
    {
        /// <summary>
        /// Process an order based on user input and returns result string
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        string ProcesOrder(string userInput);
    }
}