using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;

using Xamarin.Forms;

namespace TaskToObservables
{
	public partial class App : Application
	{

        static NavigationPage page = null;

        public static IObservable<Unit> PopEverythingAndSetALabel()
        {
            return page.PopToRootAsync().ToObservable()
                .Do(_ =>
                {
                    try
                    {
                        (page.CurrentPage as MainPage).SetLabel();
                    }
                    catch(Exception exc)
                    {
                        (page.CurrentPage as MainPage).SetException(exc.ToString());
                    }
                    
                })
                .Select(_=> Unit.Default);
        }

		public App ()
		{
			InitializeComponent();


            page = new NavigationPage(new TaskToObservables.MainPage());

            page.PushAsync(new TaskToObservables.MainPage()).Wait();
            page.PushAsync(new TaskToObservables.MainPage()).Wait();
            page.PushAsync(new TaskToObservables.MainPage()).Wait();
            page.PushAsync(new TaskToObservables.MainPage()).Wait();
            page.PushAsync(new TaskToObservables.MainPage()).Wait();

            MainPage = page;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
