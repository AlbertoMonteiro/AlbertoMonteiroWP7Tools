using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace AlbertoMonteiroWP7Tools.Navigation
{
    public class NavigationService
    {
        private Dictionary<String, String> currentQueryString;

        private PhoneApplicationFrame mainFrame;

        public event NavigatingCancelEventHandler Navigating;
        public event NavigatedEventHandler Navigated;

        public void NavigateTo(string pageUri)
        {
            if (EnsureMainFrame())
            {
                mainFrame.Navigate(new Uri(pageUri, UriKind.RelativeOrAbsolute));
                if (pageUri.Contains("?"))
                    currentQueryString = pageUri.Substring(pageUri.IndexOf('?') + 1).Split('&').Select(i =>
                    {
                        var values = i.Split('=');
                        return new KeyValuePair<String, String>(values[0], values[1]);
                    }).ToDictionary(i => i.Key, i => i.Value);
                else
                    currentQueryString = new Dictionary<string, string>();
            }
        }

        public void GoBack()
        {
            if (EnsureMainFrame()
                && mainFrame.CanGoBack)
                mainFrame.GoBack();
        }

        public string GetParameter(string key, string defaultValue = "")
        {
            var result = defaultValue;

            if (currentQueryString != null && currentQueryString.ContainsKey(key))
                result = currentQueryString[key];

            return result;
        }

        public string CurrentUri()
        {
            if (EnsureMainFrame())
                return mainFrame.CurrentSource.ToString();
            return "unknown";
        }

        private bool EnsureMainFrame()
        {
            if (mainFrame != null)
                return true;

            mainFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (mainFrame != null)
            {
                // Could be null if the app runs inside a design tool
                mainFrame.Navigating += (s, e) =>
                {
                    if (Navigating != null)
                        Navigating(s, e);
                };

                mainFrame.Navigated += (s, e) =>
                {
                    if (Navigated != null)
                        Navigated(s, e);
                };

                return true;
            }

            return false;
        }
    }
}