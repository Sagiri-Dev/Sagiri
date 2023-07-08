namespace SagiriSelenium.Interop
{
    internal static class Helper
    {
        #region Chrome Options

        internal static readonly string Language = "jp";
        internal static readonly string ProfileDir = "your name";
        internal static readonly string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36";
        internal static readonly string UserDataDir = @"your path";
        internal static readonly string WindowSize = "1080,720";

        #endregion Chrome Options

        #region Selenium Scripts

        internal static readonly string ClickScript = "arguments[0].click();";
        internal static readonly string TweetTextAreaTag = "notranslate";
        internal static readonly string ImageUploadTag = "//div[@aria-label='画像や動画を追加']";
        internal static readonly string TweetButtonTag = "//div[@data-testid='tweetButtonInline']";

        /// <summary>
        /// Serenium (Chrome) Initial Wating Interval 10 sec.
        /// </summary>
        internal static readonly int InitialInterval = 10000;

        /// <summary>
        /// Serenium (Chrome) Wating Interval 1 sec.
        /// </summary>
        internal static readonly int TinyInterval = 1000;

        #endregion Selenium Scripts

        #region Others

        internal static readonly string ScreenShotFileName = "selenium-error-analysis.png";

        #endregion Ohters
    }
}
