using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace C_51125_ProyClaseQ4.Services
{
    public class FirebaseServices
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly string _projectId;

        public FirebaseServices(IConfiguration configuration)
        {
            //Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\\Users\\Dafuentes\\Documents\\GitHub\\C_51125_ProyClaseQ4\\C_51125_ProyClaseQ4\\Config\\firebase-credentials.json");
            //_firestoreDb = FirestoreDb.Create("spqr-opera-base");
            _projectId = configuration["Firebase:ProjectId"]
                ?? throw new InvalidOperationException("Firebase ProjectId is not configured.");


            if (FirebaseApp.DefaultInstance == null)
            {
                var credential = GoogleCredential.GetApplicationDefault();
                FirebaseApp.Create(new AppOptions
                {
                    Credential = credential,
                    ProjectId = _projectId
                });

            }

            _firestoreDb = FirestoreDb.Create(_projectId);
        }

        public FirestoreDb GetFirestoreDb()
        {
            return _firestoreDb;
        }

        public CollectionReference GetCollection(string collectionName)
        {
            return _firestoreDb.Collection(collectionName);
        }
    


    }
}
