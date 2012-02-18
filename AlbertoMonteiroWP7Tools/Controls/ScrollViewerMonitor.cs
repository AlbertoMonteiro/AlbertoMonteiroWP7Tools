using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Interactivity;

namespace AlbertoMonteiroWP7Tools.Controls
{
    public class ScrollViewerMonitor
    {
        public static DependencyProperty AtEndCommandProperty = DependencyProperty.RegisterAttached("AtEndCommand",
                                                                                                    typeof (ICommand),
                                                                                                    typeof (ScrollViewerMonitor),
                                                                                                    new PropertyMetadata(OnAtEndCommandChanged));

        public static ICommand GetAtEndCommand(DependencyObject obj)
        {
            return (ICommand) obj.GetValue(AtEndCommandProperty);
        }

        public static void SetAtEndCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(AtEndCommandProperty, value);
        }

        public static void OnAtEndCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement) d;
            if (element != null)
            {
                element.Loaded -= ElementLoaded;
                element.Loaded += ElementLoaded;
            }
        }

        private static void ElementLoaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement) sender;
            element.Loaded -= ElementLoaded;
            var scrollViewer = FindChildOfType<ScrollViewer>(element);
            if (scrollViewer == null)
                throw new InvalidOperationException("ScrollViewer not found.");

            var listener = new DependencyPropertyListener();
            listener.Changed += (o, args) =>
            {
                var atBottom = scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight;

                if (atBottom)
                {
                    var atEnd = GetAtEndCommand(element);
                    if (atEnd != null)
                        atEnd.Execute(null);
                }
            };
            var binding = new Binding("VerticalOffset") {Source = scrollViewer};
            listener.Attach(scrollViewer, binding);
        }

        private static T FindChildOfType<T>(DependencyObject root) where T : class
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                for (var i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typedChild = child as T;
                    if (typedChild != null)
                        return typedChild;
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    }
}