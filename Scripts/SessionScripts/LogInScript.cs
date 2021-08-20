using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class LogInScript : MonoBehaviour
{

    [SerializeField]
    private InputField emailField;
    [SerializeField]
    private InputField passwordField;

    [SerializeField]
    private GameObject sessions, login;

    private FirebaseAuth auth;
    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSignInButton()
    {
        string email = emailField.text;
        string password = passwordField.text;

        if (emailField.text.Equals(""))
        {
            Toast.Instance.Show("Email can not be empty");
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            login.SetActive(false);
            sessions.SetActive(true);
        });
    }
}
