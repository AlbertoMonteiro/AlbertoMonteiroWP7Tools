using System.Windows;
using System.Windows.Controls;

namespace AlbertoMonteiroWP7Tools.Controls
{
    public class ScrollViewerUtilities
    {
        #region HorizontalOffset

        /// <summary>
        ///     HorizontalOffset Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.RegisterAttached("HorizontalOffset",
                                                typeof (double), 
                                                typeof (ScrollViewerUtilities), 
                                                new PropertyMetadata(0.0, OnHorizontalOffsetChanged));

        /// <summary>
        ///     Gets the HorizontalOffset property.  This dependency property
        ///     indicates ....
        /// </summary>
        public static double GetHorizontalOffset(DependencyObject dependencyObject)
        {
            return (double) dependencyObject.GetValue(HorizontalOffsetProperty);
        }

        /// <summary>
        ///     Sets the HorizontalOffset property.  This dependency property
        ///     indicates ....
        /// </summary>
        public static void SetHorizontalOffset(DependencyObject dependencyObject, double value)
        {
            dependencyObject.SetValue(HorizontalOffsetProperty, value);
        }

        /// <summary>
        ///     Handles changes to the HorizontalOffset property.
        /// </summary>
        private static void OnHorizontalOffsetChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer) dependencyObject;
            scrollViewer.ScrollToHorizontalOffset((double) e.NewValue);
        }

        #endregion

        #region VerticalOffset

        /// <summary>
        ///     VerticalOffset Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.RegisterAttached("VerticalOffset",
                                                typeof (double),
                                                typeof (ScrollViewerUtilities),
                                                new PropertyMetadata(0.0, OnVerticalOffsetChanged));

        /// <summary>
        ///     Gets the VerticalOffset property.  This dependency property
        ///     indicates ....
        /// </summary>
        public static double GetVerticalOffset(DependencyObject dependencyObject)
        {
            return (double) dependencyObject.GetValue(VerticalOffsetProperty);
        }

        /// <summary>
        ///     Sets the VerticalOffset property.  This dependency property
        ///     indicates ....
        /// </summary>
        public static void SetVerticalOffset(DependencyObject dependencyObject, double value)
        {
            dependencyObject.SetValue(VerticalOffsetProperty, value);
        }

        /// <summary>
        ///     Handles changes to the VerticalOffset property.
        /// </summary>
        private static void OnVerticalOffsetChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer) dependencyObject;
            scrollViewer.ScrollToVerticalOffset((double) e.NewValue);
        }

        #endregion
    }
}