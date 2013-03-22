using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using AlbertoMonteiroWP7Tools.Extensions;

namespace AlbertoMonteiroWP7Tools.Controls
{
    public class FixedListBox : ListBox
    {
        public static readonly DependencyProperty AnimateOnScrollEndProperty =
            DependencyProperty.Register("AnimateOnScrollEnd", typeof (bool), typeof (FixedListBox), new PropertyMetadata(default(bool)));

        private FrameworkElement _frameworkElement;
        private ScrollViewer _scrollViewer;
        private Storyboard _storyboard;

        public bool AnimateOnScrollEnd
        {
            get { return (bool) GetValue(AnimateOnScrollEndProperty); }
            set
            {
                SetValue(AnimateOnScrollEndProperty, value);
                if (value)
                    Loaded += OnLoaded;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _frameworkElement = this;
            _scrollViewer = _frameworkElement.FindChildOfType<ScrollViewer>();

            _storyboard = default(Storyboard);

            var frameworkElement = VisualTreeHelper.GetChild(_scrollViewer, 0) as FrameworkElement;

            var stateGroup = frameworkElement.FindVisualState("ScrollStates");
            if (stateGroup != null)
            {
                stateGroup.Transitions.Clear();
                stateGroup.CurrentStateChanged += ApplyAnimationIfNoScrolling;
            }
        }

        private void ApplyAnimationIfNoScrolling(object sender, VisualStateChangedEventArgs args)
        {
            if (args.NewState.Name == "NotScrolling")
            {
                if (_scrollViewer.VerticalOffset != Math.Round(_scrollViewer.VerticalOffset))
                {
                    args.NewState.Dispatcher.BeginInvoke(() =>
                    {
                        double deOnde = _scrollViewer.VerticalOffset;
                        double praOnde = Math.Round(_scrollViewer.VerticalOffset);

                        double fator = Math.Abs(deOnde - praOnde);

                        var animation = new DoubleAnimation
                        {
                            From = deOnde,
                            By = deOnde,
                            To = praOnde,
                            Duration = new Duration(TimeSpan.FromSeconds(fator*3)),
                            EasingFunction = new BackEase {Amplitude = 0.5, EasingMode = EasingMode.EaseOut},
                            SpeedRatio = 0.8
                        };

                        Storyboard.SetTarget(animation, _scrollViewer);
                        Storyboard.SetTargetProperty(animation, new PropertyPath(ScrollViewerUtilities.VerticalOffsetProperty));

                        // Create a storyboard to contain the animation.
                        _storyboard = new Storyboard();
                        _storyboard.Children.Add(animation);
                        _storyboard.AutoReverse = false;

                        // Animate the button width when it's clicked.
                        _storyboard.Begin();
                    });
                }
            }
        }
    }
}