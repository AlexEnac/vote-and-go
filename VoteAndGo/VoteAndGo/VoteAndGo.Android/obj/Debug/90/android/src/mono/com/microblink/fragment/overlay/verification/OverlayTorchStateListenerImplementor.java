package mono.com.microblink.fragment.overlay.verification;


public class OverlayTorchStateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microblink.fragment.overlay.verification.OverlayTorchStateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTorchStateChanged:(Z)V:GetOnTorchStateChanged_ZHandler:Com.Microblink.Fragment.Overlay.Verification.IOverlayTorchStateListenerInvoker, BlinkIDAARBinding\n" +
			"n_onTorchStateInitialised:(Z)V:GetOnTorchStateInitialised_ZHandler:Com.Microblink.Fragment.Overlay.Verification.IOverlayTorchStateListenerInvoker, BlinkIDAARBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Microblink.Fragment.Overlay.Verification.IOverlayTorchStateListenerImplementor, BlinkIDAARBinding", OverlayTorchStateListenerImplementor.class, __md_methods);
	}


	public OverlayTorchStateListenerImplementor ()
	{
		super ();
		if (getClass () == OverlayTorchStateListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microblink.Fragment.Overlay.Verification.IOverlayTorchStateListenerImplementor, BlinkIDAARBinding", "", this, new java.lang.Object[] {  });
	}


	public void onTorchStateChanged (boolean p0)
	{
		n_onTorchStateChanged (p0);
	}

	private native void n_onTorchStateChanged (boolean p0);


	public void onTorchStateInitialised (boolean p0)
	{
		n_onTorchStateInitialised (p0);
	}

	private native void n_onTorchStateInitialised (boolean p0);

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
