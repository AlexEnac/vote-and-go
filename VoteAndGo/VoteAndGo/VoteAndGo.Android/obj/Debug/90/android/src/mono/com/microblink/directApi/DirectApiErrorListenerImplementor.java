package mono.com.microblink.directApi;


public class DirectApiErrorListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microblink.directApi.DirectApiErrorListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onRecognizerError:(Ljava/lang/Throwable;)V:GetOnRecognizerError_Ljava_lang_Throwable_Handler:Com.Microblink.DirectApi.IDirectApiErrorListenerInvoker, BlinkIDAARBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Microblink.DirectApi.IDirectApiErrorListenerImplementor, BlinkIDAARBinding", DirectApiErrorListenerImplementor.class, __md_methods);
	}


	public DirectApiErrorListenerImplementor ()
	{
		super ();
		if (getClass () == DirectApiErrorListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microblink.DirectApi.IDirectApiErrorListenerImplementor, BlinkIDAARBinding", "", this, new java.lang.Object[] {  });
	}


	public void onRecognizerError (java.lang.Throwable p0)
	{
		n_onRecognizerError (p0);
	}

	private native void n_onRecognizerError (java.lang.Throwable p0);

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
