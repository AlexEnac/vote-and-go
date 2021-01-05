package mono.com.microblink.hardware.orientation;


public class OrientationChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microblink.hardware.orientation.OrientationChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onOrientationChange:(Lcom/microblink/hardware/orientation/Orientation;)V:GetOnOrientationChange_Lcom_microblink_hardware_orientation_Orientation_Handler:Com.Microblink.Hardware.Orientation.IOrientationChangeListenerInvoker, BlinkIDAARBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Microblink.Hardware.Orientation.IOrientationChangeListenerImplementor, BlinkIDAARBinding", OrientationChangeListenerImplementor.class, __md_methods);
	}


	public OrientationChangeListenerImplementor ()
	{
		super ();
		if (getClass () == OrientationChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microblink.Hardware.Orientation.IOrientationChangeListenerImplementor, BlinkIDAARBinding", "", this, new java.lang.Object[] {  });
	}


	public void onOrientationChange (com.microblink.hardware.orientation.Orientation p0)
	{
		n_onOrientationChange (p0);
	}

	private native void n_onOrientationChange (com.microblink.hardware.orientation.Orientation p0);

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
