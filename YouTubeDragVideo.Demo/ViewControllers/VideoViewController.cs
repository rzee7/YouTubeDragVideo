using System;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using MediaPlayer;
using UIKit;

namespace YouTubeDragVideo.Demo
{
	public partial class VideoViewController : DraggableFloatingViewController
	{
		#region Private Fields

		MPMoviePlayerController mediaPlayer;
		UIActivityIndicatorView loadingSpinner;
		private const int NSEC_PER_SEC = 1000000000;
		#endregion

		#region Constructor

		public VideoViewController()
		{

		}

		#endregion

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			mediaPlayer = new MPMoviePlayerController();
			loadingSpinner = new UIActivityIndicatorView();
			this.SetupViewsWithVideoView(mediaPlayer.View, 160); //Height: It could be dynamic as well.
			SetupMoviePlayer();
			//This will notify orientation changed
			NSNotificationCenter.DefaultCenter.AddObserver(UIDevice.OrientationDidChangeNotification, OnOrientationChanged);

			// design controller view
			var minimizeButton = new UIButton();
			minimizeButton.Frame = new CGRect(x: 0, y: 0, width: 44, height: 44);
			minimizeButton.SetImage(UIImage.FromFile("DownArrow"), UIControlState.Normal);
			minimizeButton.TouchUpInside += MinimizeButton_TouchUpInside;
			//AddTarget(this, new ObjCRuntime.Selector("OnTapMinimizeButton"), UIControlEvent.TouchUpInside);
			this.ControllerView.AddSubview(minimizeButton);

			var testControl = new UILabel();
			testControl.Frame = new CGRect(x: 100, y: 5, width: 150, height: 40);
			testControl.Text = "Controller View";
			testControl.TextColor = UIColor.White;
			this.ControllerView.AddSubview(testControl);

			// design body view
			this.BodyView.BackgroundColor = UIColor.White;
			this.BodyView.Layer.BorderColor = UIColor.Black.CGColor;
			this.BodyView.Layer.BorderWidth = 10;

			var testView = new UILabel();
			testView.Frame = new CGRect(x: 20, y: 10, width: this.View.Frame.Width - 40, height: 40);
			testView.Text = "Octopus Target Or Body View";
			testView.TextColor = UIColor.Gray;
			this.BodyView.AddSubview(testView);

			// design message view
			this.MessageView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.8F);
			loadingSpinner.Frame = new CGRect(0, 0, 50, 50);
			loadingSpinner.Center = this.MessageView.Center;
			loadingSpinner.HidesWhenStopped = false;
			loadingSpinner.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;
			this.MessageView.AddSubview(loadingSpinner);
		}
		public override void DidReAppear()
		{
			base.DidReAppear();
			SetupMoviePlayer();
		}
		public override void DidDisappear()
		{
			base.DidDisappear();
			mediaPlayer.Pause();
		}

		public override void ShowMessageView()
		{
			base.ShowMessageView();
			loadingSpinner.StartAnimating();

		}
		public override void HideMessageView()
		{
			base.HideMessageView();
			loadingSpinner.StopAnimating();
		}
		public override void DidFullExpandByGesture()
		{
			base.DidFullExpandByGesture();
			UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.None);
			ShowVideoControl();
		}
		public override void DidExpand()
		{
			base.DidExpand();
			UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.None);
			ShowVideoControl();
		}
		public override void DidMinimize()
		{
			base.DidMinimize();
			HideVideoControl();
		}
		public override void DidStartMinimizeGesture()
		{
			base.DidStartMinimizeGesture();
			UIApplication.SharedApplication.SetStatusBarHidden(false, UIStatusBarAnimation.None);
		}

		void MinimizeButton_TouchUpInside(object sender, EventArgs e)
		{
			UIApplication.SharedApplication.SetStatusBarHidden(false, UIStatusBarAnimation.None);
			this.MinimizeView();
		}

		void SetupMoviePlayer()
		{
			//Setup Movie
			var url = NSUrl.FromFilename(NSBundle.MainBundle.PathForResource("SampleVideo/test", ofType: "mp4"));
			mediaPlayer.ContentUrl = url;
			mediaPlayer.Fullscreen = false;
			mediaPlayer.ControlStyle = MPMovieControlStyle.None;
			mediaPlayer.RepeatMode = MPMovieRepeatMode.None;
			mediaPlayer.PrepareToPlay();

			// play
			double seconds = 1.0f;
			double delay = seconds * NSEC_PER_SEC;// nanoseconds per seconds
			var dispatchTime = new DispatchTime(DispatchTime.Now, (long)delay);
			DispatchQueue.MainQueue.DispatchAfter(dispatchTime, () => { mediaPlayer.Play(); });

			// for movie loop
			NSNotificationCenter.DefaultCenter.AddObserver(this, new ObjCRuntime.Selector("moviePlayBackDidFinish:"), MPMoviePlayerController.PlaybackDidFinishNotification,
			mediaPlayer);
		}

		[Export("moviePlayBackDidFinish:")]
		void MoviePlayBackDidFinish(NSNotification notification)
		{
			mediaPlayer.Play();
			NSNotificationCenter.DefaultCenter.RemoveObserver(MPMoviePlayerController.PlaybackDidFinishNotification);
		}
		void OnOrientationChanged(NSNotification obj)
		{
			var orientation = UIApplication.SharedApplication.StatusBarOrientation;
			switch (orientation)
			{
				case UIInterfaceOrientation.Portrait:
				case UIInterfaceOrientation.PortraitUpsideDown:
					ExitFullScreen();
					break;
				case UIInterfaceOrientation.LandscapeLeft:
				case UIInterfaceOrientation.LandscapeRight:
					GoFullScreen();
					break;
				default:
					break;
			}
		}

		void SetOrientation(UIInterfaceOrientation orientation)
		{
			var orientationNum = new NSNumber((int)orientation);
			UIDevice.CurrentDevice.SetValueForKey(NSObject.FromObject(orientation), new NSString("orientation"));
		}

		public bool IsFullScreen
		{
			get { return mediaPlayer.Fullscreen; }
		}
		void GoFullScreen()
		{
			if (!IsFullScreen)
			{
				mediaPlayer.ControlStyle = MPMovieControlStyle.Fullscreen;
				mediaPlayer.Fullscreen = true;
				NSNotificationCenter.DefaultCenter.AddObserver(MPMoviePlayerController.WillExitFullscreenNotification, WillExitFullScreen);
			}
		}

		void WillExitFullScreen(NSNotification obj)
		{
			if (IsLandScape)
			{
				SetOrientation(UIInterfaceOrientation.Portrait);
			}
			NSNotificationCenter.DefaultCenter.RemoveObserver(MPMoviePlayerController.WillExitFullscreenNotification);
		}

		void ExitFullScreen()
		{
			if (IsFullScreen)
			{
				mediaPlayer.Fullscreen = false;
			}
		}
		void ShowVideoControl()
		{
			mediaPlayer.ControlStyle = MPMovieControlStyle.None;
		}
		void HideVideoControl()
		{
			mediaPlayer.ControlStyle = MPMovieControlStyle.None;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public bool IsLandScape
		{
			get
			{
				if (UIApplication.SharedApplication.StatusBarOrientation.IsLandscape())
				{
					return true;

				}
				else {
					return false;

				}
			}
		}
	}
}

