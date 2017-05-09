using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace YouTubeDragVideo.Demo
{
	public class SecondViewController : UIViewController
	{
		public SecondViewController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.View.BackgroundColor = UIColor.White;

			var btn = new UIButton();
			btn.Frame = new CGRect(x: 10, y: 10, width: 100, height: 100);
			btn.BackgroundColor = UIColor.Red;
			btn.TouchUpInside += Btn_TouchUpInside;
			this.View.AddSubview(btn);


			var dismissBtn = new UIButton();
			dismissBtn.Frame = new CGRect(x: 150, y: 150, width: 100, height: 100);
			dismissBtn.BackgroundColor = UIColor.Green;
			dismissBtn.TouchUpInside += DismissBtn_TouchUpInside;
			this.View.AddSubview(dismissBtn);
		}

		void DismissBtn_TouchUpInside(object sender, EventArgs e)
		{
			this.PresentViewController(this, true, null);
			//        NSTimer.schedule(delay: 0.2) { timer in
			//            AppDelegate.videoController().changeParentVC(parentVC)//👈
			//        }
			NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(0.2), (obj) => { });
		}

		void Btn_TouchUpInside(object sender, EventArgs e)
		{
			AppDelegate.Current.VideoController.Show();
		}
	}
}
