﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft;
using Newtonsoft.Json;
using System.Net.Http;
using System.Diagnostics;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NotificationsExtensions.Tiles;
using Windows.UI.Notifications;

namespace Account
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>

        public static Models.User user = null;

        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // 当导航堆栈尚未还原时，导航到第一页，
                // 并通过将所需信息作为导航参数传入来配置
                // 参数
                rootFrame.Navigate(typeof(Login), e.Arguments);
            }
            // 确保当前窗口处于活动状态
            Window.Current.Activate();

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += onBackRequested;

            rootFrame.Navigated += (s, a) =>
            {
                if (rootFrame.CanGoBack)
                {
                    Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Visible;
                }
                else
                {
                    Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
                }
            };
        }

        private void onBackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null) return;

            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        public static async void upload()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("username", user.username);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                //发送POST请求
                HttpResponseMessage response = await httpClient.PostAsync("http://119.29.232.29:3000/update",
                                       new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
                //确保返回值为成功状态
                response.EnsureSuccessStatusCode();
                Byte[] getByte = await response.Content.ReadAsByteArrayAsync();
                string returnContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(returnContent);
            }
            catch (HttpRequestException ex1)
            {
                Debug.WriteLine(ex1.ToString());
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(ex2.ToString());
            }
        }

        public static async Task<string> download()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("username", user.username);
                //发送POST请求
                HttpResponseMessage response = await httpClient.PostAsync("http://119.29.232.29:3000/download", new StringContent(""));
                //确保返回值为成功状态
                response.EnsureSuccessStatusCode();
                Byte[] getByte = await response.Content.ReadAsByteArrayAsync();
                string returnContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(returnContent);
                if (returnContent != "failed")
                    user = JsonConvert.DeserializeObject<Models.User>(returnContent);
            }
            catch (HttpRequestException ex1)
            {
                Debug.WriteLine(ex1.ToString());
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(ex2.ToString());
            }
            return "Finished";
        }

        public static void UpdateTile(TileBindingContentAdaptive tileContent, int tileStyle)
        {
            //  tileStyle == 0  ->  中磁贴
            //  tileStyle == 1  ->  宽磁贴
            TileContent content;
            if (tileStyle == 0)
            {
                content = new TileContent()
                {
                    Visual = new TileVisual()
                    {
                        TileMedium = new TileBinding()
                        {
                            Content = tileContent
                        }
                    }
                };
            }
            else
            {
                content = new TileContent()
                {
                    Visual = new TileVisual()
                    {
                        TileWide = new TileBinding()
                        {
                            Content = tileContent
                        }
                    }
                };
            }
            TileUpdateManager.CreateTileUpdaterForApplication().Update(new TileNotification(content.GetXml()));
        }
    }
}
