﻿using NotificationsExtensions.Tiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Account
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Details : Page
    {
        Models.IncomesList incomesList { set; get; }

        public class FinancialStuff
        {
            public string Name { get; set; }
            public int Amount { get; set; }
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChartContents();
        }

        public Details()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            this.incomesList = App.user.incomesList;
            NavigationCacheMode = NavigationCacheMode.Enabled;
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.LightGray;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.LightGray;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 改回顶栏的颜色
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Color.FromArgb(0, 136, 214, 255);
            viewTitleBar.ButtonBackgroundColor = Color.FromArgb(0, 136, 214, 255);
            UpdateTiles();
        }

        private void LoadChartContents()
        {
            List<FinancialStuff> financialStuffList = new List<FinancialStuff>();
            Dictionary<Models.kind, double> dic = new Dictionary<Models.kind, double>();
            Models.kind[] myKind = { Models.kind.food, Models.kind.traffic, Models.kind.shopping, Models.kind.medical, Models.kind.travel,
                                     Models.kind.entertainment, Models.kind.contact, Models.kind.investment, Models.kind.education, Models.kind.other,
                                     Models.kind.bonus, Models.kind.salary, Models.kind.financial, Models.kind.welfare, Models.kind.otherincome};
            for (int i = 0; i < 15; i++)
                dic.Add(myKind[i], 0);
            for (int i = 0; i < incomesList.AllIncomes.Count; i++)
                if (incomesList.AllIncomes.ToArray()[i].inOrOut == "支出")
                    dic[incomesList.AllIncomes.ToArray()[i].kind] += incomesList.AllIncomes.ToArray()[i].amount;

            for (int t = 0; t < 4; t++)
            {
                int max = 0;
                for (int i = 0; i < 15 - t; i++)
                {
                    if (dic[myKind[max]] < dic[myKind[i]]) max = i;
                }
                switch (myKind[max])
                {
                    case Models.kind.food:
                        financialStuffList.Add(new FinancialStuff() { Name = "餐饮", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.traffic:
                        financialStuffList.Add(new FinancialStuff() { Name = "交通", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.shopping:
                        financialStuffList.Add(new FinancialStuff() { Name = "购物", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.medical:
                        financialStuffList.Add(new FinancialStuff() { Name = "医疗", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.travel:
                        financialStuffList.Add(new FinancialStuff() { Name = "旅游", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.entertainment:
                        financialStuffList.Add(new FinancialStuff() { Name = "娱乐", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.contact:
                        financialStuffList.Add(new FinancialStuff() { Name = "社交", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.education:
                        financialStuffList.Add(new FinancialStuff() { Name = "教育", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.investment:
                        financialStuffList.Add(new FinancialStuff() { Name = "投资", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.other:
                        financialStuffList.Add(new FinancialStuff() { Name = "其他", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.bonus:
                        financialStuffList.Add(new FinancialStuff() { Name = "奖金", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.salary:
                        financialStuffList.Add(new FinancialStuff() { Name = "工资", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.financial:
                        financialStuffList.Add(new FinancialStuff() { Name = "理财", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.welfare:
                        financialStuffList.Add(new FinancialStuff() { Name = "福利", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                    case Models.kind.otherincome:
                        financialStuffList.Add(new FinancialStuff() { Name = "其他收入", Amount = Convert.ToInt32(dic[myKind[max]]) });
                        break;
                }
                dic.Remove(myKind[max]);
                if (max != 15 - t - 1)
                {
                    myKind[max] = myKind[15 - t - 1];
                }
            }
            (PieChart.Series[0] as PieSeries).ItemsSource = financialStuffList;
            financialStuffList.Clear();
            int mouth = DateTime.Now.Month, year = DateTime.Now.Year;

            double[] thisyear = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] lastyear = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < incomesList.AllIncomes.Count; i++)
            {
                if (incomesList.AllIncomes.ToArray()[i].date.Year == year && incomesList.AllIncomes.ToArray()[i].inOrOut == "支出")
                    thisyear[incomesList.AllIncomes.ToArray()[i].date.Month - 1] += incomesList.AllIncomes.ToArray()[i].amount;
                if (incomesList.AllIncomes.ToArray()[i].date.Year == year - 1 && incomesList.AllIncomes.ToArray()[i].inOrOut == "支出")
                    lastyear[incomesList.AllIncomes.ToArray()[i].date.Month - 1] += incomesList.AllIncomes.ToArray()[i].amount;
            }

            financialStuffList.Add(new FinancialStuff() { Name = "本月", Amount = Convert.ToInt32(thisyear[mouth - 1]) });

            for (int i = 0; i < 3; i++)
            {
                string name;
                mouth--;
                if (mouth <= 0)
                {
                    int temp = mouth + 12;
                    name = temp + "月";
                    financialStuffList.Add(new FinancialStuff() { Name = name, Amount = Convert.ToInt32(lastyear[temp - 1]) });
                }
                else
                {
                    name = mouth + "月";
                    financialStuffList.Add(new FinancialStuff() { Name = name, Amount = Convert.ToInt32(thisyear[mouth - 1]) });
                }
            }
            financialStuffList.Reverse();
            (ColumnChart.Series[0] as ColumnSeries).ItemsSource = financialStuffList;
        }

        private void addNewItemButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddNewItemPage));
        }

        private void goalsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GoalsPage));
        }


        private void splitViewButton_Click(object sender, RoutedEventArgs e)
        {
            mySplit.IsPaneOpen = !mySplit.IsPaneOpen;
        }

        private void detailsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Details));
        }

        private void addGoalButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddGoal));
        }

        private void navigation_ItemClick(object sender, ItemClickEventArgs e)
        {
            Grid e_ = (Grid)e.ClickedItem;
            string pageName = ((TextBlock)VisualTreeHelper.GetChild(e_, 1)).Text;
            if (pageName == "收支详情")
                Frame.Navigate(typeof(Details));
            else if (pageName == "添加收支")
                Frame.Navigate(typeof(AddNewItemPage));
            else if (pageName == "目标详情")
                Frame.Navigate(typeof(GoalsPage));
            else if (pageName == "添加目标")
                Frame.Navigate(typeof(AddGoal));
            else
                Frame.Navigate(typeof(MainPage));
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        // 磁贴更新----收支
        private void UpdateTiles()
        {
            double incomes = 0, outcomes = 0;
            for (int i = 0; i < incomesList.AllIncomes.Count; i++)
            {
                if (incomesList.AllIncomes.ToArray()[i].date.Month == DateTimeOffset.Now.Month)
                {
                    if (incomesList.AllIncomes.ToArray()[i].inOrOut == "支出")
                        outcomes += incomesList.AllIncomes.ToArray()[i].amount;
                    else
                        incomes += incomesList.AllIncomes.ToArray()[i].amount;
                }
            }
            App.UpdateTile(new TileBindingContentAdaptive()
            {
                Children =
                    {
                        new TileText()
                        {
                            Text = "本月支出",
                            Style = TileTextStyle.CaptionSubtle
                        },
                        new TileText()
                        {
                            Text = outcomes.ToString() + "元",
                            Style = TileTextStyle.Caption
                        },
                        new TileText()
                        {
                            Text = "本月收入",
                            Style = TileTextStyle.CaptionSubtle
                        },
                        new TileText()
                        {
                            Text =  incomes.ToString() + "元",
                            Style = TileTextStyle.Caption
                        }
                    }
            }, 0);
        }
    }
}
