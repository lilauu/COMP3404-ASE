using Android.App;
using Android.Content.PM;
using Content = global::Android.Content;

namespace COMP3404_Client.Platforms.Android;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(new[] { Content.Intent.ActionView },
          Categories = new[] { Content.Intent.CategoryDefault, Content.Intent.CategoryBrowsable },
          DataScheme = CALLBACK_SCHEME)]
public class WebAuthenticationCallbackActivity : Microsoft.Maui.Authentication.WebAuthenticatorCallbackActivity
{
    const string CALLBACK_SCHEME = "comp3404";
}
