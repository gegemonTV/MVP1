using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;

public class FirebaseInit : MonoBehaviour
{

    FirebaseAuth auth;
    FirebaseUser user;
    public UnityEvent OnFirebaseInitialized = new UnityEvent();

    [SerializeField]
    private GameObject authChoose, sessionStart;

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError($"Failed to init Firebase with {task.Exception}");
                return;
            }

            OnFirebaseInitialized.Invoke();
        });

        auth = FirebaseAuth.DefaultInstance;

        // DEBUG
        // auth.SignOut();
        // END DEBUG
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                authChoose.SetActive(false);
                sessionStart.SetActive(true);
            }
        }
    }

}
