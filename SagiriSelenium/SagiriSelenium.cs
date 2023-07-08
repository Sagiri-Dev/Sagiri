using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using SagiriSelenium.Interfaces;
using SagiriSelenium.Interop;

namespace SagiriSelenium
{
    /// <summary>
    /// Providing Selenium functionality in order to Sagiri.
    /// </summary>
    public class SagiriSelenium : ISagiriSelenium
    {
        private static ChromeDriver? _ChromeDriver { get; set; } = default!;

        public SagiriSelenium() { }

        /// <summary>
        /// Main Entry.
        /// </summary>
        /// <remarks>
        /// As a class library, it is an empty method because there is no implementation process in the entry.
        /// </remarks>>
        /// <returns></returns>
        public static async Task Main() { }

        /// <summary>
        /// Selenium execution and post processing to Twitter.
        /// </summary>
        /// <param name="url"> selenium open to url. </param>
        /// <param name="tweet"> twitter posting text data. </param>
        /// <returns></returns>
        async Task<bool> ISagiriSelenium.RunSeleniumAndPrePostTwitterAsync(string url, string tweet)
        {
            var seleniumTask = Task.Run(() => {
                if (_ChromeDriver is null)
                {
                    var driverService = ChromeDriverService.CreateDefaultService();
                    driverService.HideCommandPromptWindow = true;

                    ChromeOptions options = new();
                    //options.AddArgument($"--window-size={Helper.WindowSize}");
                    //options.AddArgument("--headless"); // twitter headless mode is dead............
                    options.AddArgument("--no-sandbox");
                    options.AddArgument($"--user-agent={Helper.UserAgent}");
                    options.AddArgument($"--lang={Helper.Language}");
                    options.AddArgument($"user-data-dir={Helper.UserDataDir}");
                    options.AddArgument($"--profile-directory={Helper.ProfileDir}");

                    _ChromeDriver = new ChromeDriver(driverService, options);
                    _ChromeDriver.Navigate().GoToUrl(url);
                }
            });

            var result = await this._UploadAlbumArtProcessAsync(seleniumTask, tweet);
            return result;
        }

        /// <summary>
        /// Process for uploading album art to Twitter.
        /// </summary>
        /// <param name="isCompletedPreTask"></param>
        /// <param name="tweet"></param>
        /// <returns></returns>
        private async Task<bool> _UploadAlbumArtProcessAsync(Task seleniumTask, string tweet)
        {
            try
            {
                // Here ChromeDriver instance is not created yet :-(
                var waitInterval = (_ChromeDriver is null) ? Helper.InitialInterval : Helper.TinyInterval;
                await Task.Delay(waitInterval);

                if (!seleniumTask.IsCompletedSuccessfully)
                    return false;

                var textAreaElement = _ChromeDriver?.FindElement(By.ClassName(Helper.TweetTextAreaTag));
                textAreaElement?.Click();
                textAreaElement?.SendKeys(tweet);

                var imageUploadInterval = Helper.TinyInterval;
                await Task.Delay(imageUploadInterval);

                var imageUploadElement = _ChromeDriver?.FindElement(By.XPath(Helper.ImageUploadTag));
                _ChromeDriver?.ExecuteScript(Helper.ClickScript, imageUploadElement);

                return true;
            }
            catch (Exception ex) when
                ( ex is NotImplementedException   ||
                  ex is NoSuchElementException    ||
                  ex is NoSuchFrameException      ||
                  ex is NoSuchShadowRootException ||
                  ex is ElementClickInterceptedException
                )
            {
                this._GetScreenShot();
            }
            finally { }

            return false;
        }

        /// <summary>
        /// Posting for Twitter.
        /// </summary>
        /// <returns></returns>
        async Task<bool> ISagiriSelenium.PostTwitterAsync() => await Task.Run(async () =>
        {
            try
            {
                var postInterval = Helper.TinyInterval;
                await Task.Delay(postInterval);

                var tweetButtonElement = _ChromeDriver?.FindElement(By.XPath(Helper.TweetButtonTag));
                _ChromeDriver?.ExecuteScript(Helper.ClickScript, tweetButtonElement);
                return true;
            }
            catch (Exception ex) when
                ( ex is NotImplementedException   ||
                  ex is NoSuchElementException    ||
                  ex is NoSuchFrameException      ||
                  ex is NoSuchShadowRootException ||
                  ex is ElementClickInterceptedException
                )
            {
                this._GetScreenShot();
            }
            finally { }

            return false;
        });

        /// <summary>
        /// Take screenshot for error analysis.
        /// </summary>
        private void _GetScreenShot()
        {
            _ChromeDriver?.Manage().Window.FullScreen();

            Screenshot screenshot = (_ChromeDriver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile(Helper.ScreenShotFileName, ScreenshotImageFormat.Png);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        void ISagiriSelenium.Dispose()
        {
            _ChromeDriver?.Quit();
            _ChromeDriver?.Dispose();
            _ChromeDriver = null;
        }
    }
}
