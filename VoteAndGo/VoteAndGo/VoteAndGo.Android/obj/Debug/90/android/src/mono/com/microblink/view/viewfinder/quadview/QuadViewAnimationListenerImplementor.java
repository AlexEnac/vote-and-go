package mono.com.microblink.view.viewfinder.quadview;


public class QuadViewAnimationListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microblink.view.viewfinder.quadview.QuadViewAnimationListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationEnd:()V:GetOnAnimationEndHandler:Com.Microblink.View.Viewfinder.Quadview.IQuadViewAnimationListenerInvoker, BlinkIDAARBinding\n" +
			"n_onAnimationStart:()V:GetOnAnimationStartHandler:Com.Microblink.View.Viewfinder.Quadview.IQuadViewAnimationListenerInvoker, BlinkIDAARBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Microblink.View.Viewfinder.Quadview.IQuadViewAnimationListenerImplementor, BlinkIDAARBinding", QuadViewAnimationListenerImplementor.class, __md_methods);
	}


	public QuadViewAnimationListenerImplementor ()
	{
		super ();
		if (getClass () == QuadViewAnimationListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microblink.View.Viewfinder.Quadview.IQuadViewAnimationListenerImplementor, BlinkIDAARBinding", "", this, new java.lang.Object[] {  });
	}


	public void onAnimationEnd ()
	{
		n_onAnimationEnd ();
	}

	private native void n_onAnimationEnd ();


	public void onAnimationStart ()
	{
		n_onAnimationStart ();
	}

	private native void n_onAnimationStart ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
