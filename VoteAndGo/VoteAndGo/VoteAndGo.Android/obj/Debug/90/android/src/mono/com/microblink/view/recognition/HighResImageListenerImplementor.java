package mono.com.microblink.view.recognition;


public class HighResImageListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microblink.view.recognition.HighResImageListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onHighResImageAvailable:(Lcom/microblink/image/highres/HighResImageWrapper;)V:GetOnHighResImageAvailable_Lcom_microblink_image_highres_HighResImageWrapper_Handler:Com.Microblink.View.Recognition.IHighResImageListenerInvoker, BlinkIDAARBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Microblink.View.Recognition.IHighResImageListenerImplementor, BlinkIDAARBinding", HighResImageListenerImplementor.class, __md_methods);
	}


	public HighResImageListenerImplementor ()
	{
		super ();
		if (getClass () == HighResImageListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microblink.View.Recognition.IHighResImageListenerImplementor, BlinkIDAARBinding", "", this, new java.lang.Object[] {  });
	}


	public void onHighResImageAvailable (com.microblink.image.highres.HighResImageWrapper p0)
	{
		n_onHighResImageAvailable (p0);
	}

	private native void n_onHighResImageAvailable (com.microblink.image.highres.HighResImageWrapper p0);

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
