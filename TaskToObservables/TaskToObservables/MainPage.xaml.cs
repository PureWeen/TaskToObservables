using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaskToObservables
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            Observable.FromEventPattern<EventHandler, EventArgs>(
                x => BOOM.Clicked += x,
                x => BOOM.Clicked -= x
                )
            .SelectMany(_=> App.PopEverythingAndSetALabel())
            .Subscribe();
        }

        public void SetException(string error)
        {
            Observable.Start(() =>
            {
                lblError.Text = error;
                lblError.TextColor = Color.Red;
            },RxApp.MainThreadScheduler
            ).Subscribe();
            
        }

        public void SetLabel()
        {
#if __IOS__
            someLabel.Text = $"{Guid.NewGuid()}";
#else
            someLabel.Text = $"IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread} - {Guid.NewGuid()} ";
#endif

            lblError.Text = String.Empty;

        }
    }
}
