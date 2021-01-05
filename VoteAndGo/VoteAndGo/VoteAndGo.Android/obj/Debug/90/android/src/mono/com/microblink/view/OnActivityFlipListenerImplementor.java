package mono.com.microblink.view;


public class OnActivityFlipListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microblink.view.OnActivityFlipListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onActivityFlip:()V:GetOnActivityFlipHandler:Com.Microblink.View.IOnActivityFlipListenerInvoker, BlinkIDAARBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Microblink.View.IOnActivityFlipListenerImplementor, BlinkIDAARBinding", OnActivityFlipListenerImplementor.class, __md_methods);
	}


	public OnActivityFlipListenerImplementor ()
	{
		super ();
		if (getClass () == OnActivityFlipListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microblink.View.IOnActivityFlipListenerImplementor, BlinkIDAARBinding", "", this, new java.lang.Object[] {  });
	}


	public void onActivityFlip ()
	{
		n_onActivityFlip ();
	}

	private native void n_onActivityFlip ();

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
