using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace YouTubeDragVideo
{
	// @protocol DraggableFloatingViewControllerDelegate
	[Protocol, Model]
	interface DraggableFloatingViewControllerDelegate
	{
		// @required -(void)removeDraggableFloatingViewController;
		[Abstract]
		[Export("removeDraggableFloatingViewController")]
		void RemoveDraggableFloatingViewController();
	}

	// @interface DraggableFloatingViewController : UIViewController <UIGestureRecognizerDelegate>
	[BaseType(typeof(UIViewController))]
	interface DraggableFloatingViewController : IUIGestureRecognizerDelegate
	{
		// @property (nonatomic, strong) UIView * bodyView;
		[Export("bodyView", ArgumentSemantic.Strong)]
		UIView BodyView { get; set; }

		// @property (nonatomic, strong) UIView * controllerView;
		[Export("controllerView", ArgumentSemantic.Strong)]
		UIView ControllerView { get; set; }

		// @property (nonatomic, strong) UIView * messageView;
		[Export("messageView", ArgumentSemantic.Strong)]
		UIView MessageView { get; set; }

		// -(void)setupViewsWithVideoView:(UIView *)vView videoViewHeight:(CGFloat)videoHeight;
		[Export("setupViewsWithVideoView:videoViewHeight:")]
		void SetupViewsWithVideoView(UIView vView, nfloat videoHeight);

		// -(void)didExpand;
		[Export("didExpand")]
		void DidExpand();

		// -(void)didMinimize;
		[Export("didMinimize")]
		void DidMinimize();

		// -(void)didStartMinimizeGesture;
		[Export("didStartMinimizeGesture")]
		void DidStartMinimizeGesture();

		// -(void)didFullExpandByGesture;
		[Export("didFullExpandByGesture")]
		void DidFullExpandByGesture();

		// -(void)didDisappear;
		[Export("didDisappear")]
		void DidDisappear();

		// -(void)didReAppear;
		[Export("didReAppear")]
		void DidReAppear();

		// -(void)minimizeView;
		[Export("minimizeView")]
		void MinimizeView();

		// -(void)expandView;
		[Export("expandView")]
		void ExpandView();

		// -(void)hideControllerView;
		[Export("hideControllerView")]
		void HideControllerView();

		// -(void)showControllerView;
		[Export("showControllerView")]
		void ShowControllerView();

		// -(void)showMessageView;
		[Export("showMessageView")]
		void ShowMessageView();

		// -(void)hideMessageView;
		[Export("hideMessageView")]
		void HideMessageView();

		// -(void)show;
		[Export("show")]
		void Show();

		// -(void)bringToFront;
		[Export("bringToFront")]
		void BringToFront();
	}
}
