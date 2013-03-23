using System.Collections.Generic;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace AlbertoMonteiroWP7Tools.Controls
{
    public class GlobalLoading
    {
        private static GlobalLoading _in;
        private readonly Stack<bool> loadingStack;
        private int loadingCount;
        private ProgressIndicator mangoIndicator;

        private GlobalLoading()
        {
            loadingStack = new Stack<bool>();
        }

        public static GlobalLoading Instance
        {
            get { return _in ?? (_in = new GlobalLoading()); }
        }

        public void PushLoading()
        {
            if (loadingStack.Count == 0)
            {
                ++loadingCount;
            }
            NotifyValueChanged();
            loadingStack.Push(true);
        }

        public void PopLoading()
        {
            loadingStack.Pop();
            if (loadingStack.Count == 0)
            {
                loadingCount = 0;
                NotifyValueChanged();
            }
        }

        public void Initialize(PhoneApplicationFrame frame)
        {
            mangoIndicator = new ProgressIndicator();

            frame.Navigated += OnRootFrameNavigated;
        }

        private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            var content = e.Content;
            var applicationPage = content as PhoneApplicationPage;
            if (applicationPage != null)
            {
                applicationPage.SetValue(SystemTray.ProgressIndicatorProperty, mangoIndicator);
                NotifyValueChanged();
            }
        }

        private void NotifyValueChanged()
        {
            if (mangoIndicator != null)
            {
                mangoIndicator.IsIndeterminate = loadingCount > 0;

                if (mangoIndicator.IsVisible == false)
                    mangoIndicator.IsVisible = true;

                mangoIndicator.Text = loadingCount > 0 ? "Carregando..." : "";
            }
        }
    }
}