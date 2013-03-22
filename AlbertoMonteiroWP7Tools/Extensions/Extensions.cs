using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AlbertoMonteiroWP7Tools.Extensions
{
    public static class Extensions
    {
        public static VisualStateGroup FindVisualState(this FrameworkElement element, string name)
        {
            if (element == null)
                return null;

            var groups = VisualStateManager.GetVisualStateGroups(element);
            
            return groups.Cast<VisualStateGroup>().FirstOrDefault(stateGroup => stateGroup.Name == name);
        }

        public static T FindChildOfType<T>(this DependencyObject root) where T : class
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
