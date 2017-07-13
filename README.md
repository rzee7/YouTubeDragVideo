# YouTubeDragVideo
Drag video at bottom in your iOS application like YouTube does.

I have created a binding projet against iOS swift [Library](https://github.com/entotsu/DraggableFloatingViewController). I also created a sample project where you guys can see It's implementation.
This library supports following architectures:
- ARM64
- ARMV7
- i386

## Setup:
- Clone Repository.
- Add Binding Project (YouTubeDragVideo) Refrence to Your project.
- Create VideoController with `DraggableFloatingViewController`.
- Adjust video playing screens | actions.

Create [VideoViewController](https://github.com/rzee7/YouTubeDragVideo/blob/master/YouTubeDragVideo.Demo/ViewControllers/VideoViewController.cs)

I used AppDelegate to control video controller, you an use your own way if you wish to. Mine AppDelegate looks like:

    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
      // class-level declarations
      public static AppDelegate Current { get; set; }
      public VideoViewController VideoController
      {
        get;
        set;
      }
      public override UIWindow Window
      {
        get;
        set;
      }

      public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
      {
        Current = this;
        // Override point for customization after application launch.
        // If not required for your application you can safely delete this method
        this.VideoController = new VideoViewController();
        return true;
      }
    }

I have added a Play Video `Button` which is playing video from app bundle.

	void Btn_TouchUpInside(object sender, EventArgs e)
	{
		AppDelegate.Current.VideoController.Show();
	}
    
Boom!! We are good to go!! you can drag video now.

![Screening](https://raw.githubusercontent.com/rzee7/YouTubeDragVideo/master/DragVideoPanel.gif)
   
Thanks to [entotsu](https://github.com/entotsu) native lirary creator.

Cheers!!

{ rzee }
