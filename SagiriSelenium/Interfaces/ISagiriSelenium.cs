namespace SagiriSelenium.Interfaces
{
    /// <summary>
    /// Providing Selenium functionality interfaces.
    /// </summary>
    public interface ISagiriSelenium
    {
        /// <summary>
        /// Selenium execution and post processing to Twitter.
        /// </summary>
        /// <param name="url"> selenium open to url. </param>
        /// <param name="tweet"> twitter posting text data. </param>
        /// <returns></returns>
        Task<bool> RunSeleniumAndPrePostTwitterAsync(string url, string tweet);

        /// <summary>
        /// Process for uploading album art to Twitter.
        /// </summary>
        /// <param name="targetTask"></param>
        /// <param name="tweet"></param>
        /// <returns></returns>
        Task<bool> PostTwitterAsync();

        /// <summary>
        /// Dispose
        /// </summary>
        void Dispose();
    }
}
