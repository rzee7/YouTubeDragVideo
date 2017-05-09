using System;
using CoreGraphics;
using UIKit;

namespace YouTubeDragVideo.Demo
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.View.BackgroundColor = UIColor.White;

			var playVideoButton = new UIButton();
			playVideoButton.BackgroundColor = UIColor.Black;
			playVideoButton.SetTitle("Play Video", UIControlState.Normal);
			playVideoButton.Frame = new CGRect(x: 10, y: 65, width: this.View.Frame.Width - 20, height: 30);
			playVideoButton.TouchUpInside += Btn_TouchUpInside;
			this.View.AddSubview(playVideoButton);


			var dismissBtn = new UIButton();
			dismissBtn.BackgroundColor = UIColor.Black;
			dismissBtn.SetTitle("Dissmiss View", UIControlState.Normal);
			dismissBtn.Frame = new CGRect(x: 10, y: 110, width: this.View.Frame.Width - 20, height: 30);
			dismissBtn.TouchUpInside += DismissBtn_TouchUpInside;
			this.View.AddSubview(dismissBtn);
		}

		void DismissBtn_TouchUpInside(object sender, EventArgs e)
		{
			AppDelegate.Current.VideoController.BringToFront();
			var secondVC = new SecondViewController();
			this.PresentViewController(secondVC, true, null);
		}

		void Btn_TouchUpInside(object sender, EventArgs e)
		{
			//AppDelegate
			AppDelegate.Current.VideoController.Show();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
