using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperSearcherWPF
{
    /// <summary>
    /// Interaction logic for ConfirmedButton.xaml
    /// </summary>
    public partial class ConfirmedButton : Button
    {
        /// <summary>
        /// The message in the confirmation MessageBox.
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ConfirmedButton), new PropertyMetadata(""));
        /// <summary>
        /// The caption in the confirmation MessageBox.
        /// </summary>
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(ConfirmedButton), new PropertyMetadata(""));

        /// <summary>
        /// The message in the confirmation MessageBox.
        /// </summary>
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        /// <summary>
        /// The caption in the confirmation MessageBox.
        /// </summary>
        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public ConfirmedButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows a MessageBox asking to confirm the click.
        /// </summary>
        protected override void OnClick()
        {
            MessageBoxResult result = MessageBox.Show(Message, Caption,
                MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                base.OnClick();
            }
        }
    }
}
