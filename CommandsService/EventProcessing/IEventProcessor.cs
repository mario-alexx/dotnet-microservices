namespace CommandsService.EventProcessing 
{
    /// <summary>
    /// Provides an interface for processing events.
    /// </summary>
    public interface IEventProcessor 
    {
        /// <summary>
        /// Processes an event message.
        /// </summary>
        /// <param name="message">The event message to process.</param>
        void ProcessEvent(string message);
    }
}