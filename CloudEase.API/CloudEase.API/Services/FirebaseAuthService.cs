using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

namespace CloudEase.API.Services
{
    public class FirebaseAuthService
    {
        public FirebaseAuthService()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile("cloud-ease-auth-firebase-adminsdk-fbsvc-a2736ea3ca.json")
                });
            }
        }

        public async Task<string?> VerifyToken(string token)
        {
            try
            {
                var decoded = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                return decoded.Uid;
            }
            catch
            {
                return null;
            }
        }
    }
}
