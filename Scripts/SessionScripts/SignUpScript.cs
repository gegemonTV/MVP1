using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class SignUpScript : MonoBehaviour
{

    [SerializeField]
    private InputField nicknameField;
    [SerializeField]
    private InputField emailField;
    [SerializeField]
    private InputField passwordField;

    [SerializeField]
    private GameObject sessions, signUp;

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

    public void OnSignUpButtonClicked()
    {
        string nickname = nicknameField.text;
        string email = emailField.text;
        string password = passwordField.text;

        if (nicknameField.text.Equals(""))
        {
            Toast.Instance.Show("Nickname can not be empty");
        } else if (emailField.text.Equals(""))
        {
            Toast.Instance.Show("Email can not be empty");
        } else if (passwordField.text.Length <6)
        {
            Toast.Instance.Show("Password can not be <6");
        } else
        {
            
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UserCreating Cancelled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("We've got error: " + task.Exception);
                }

                FirebaseUser newUser = task.Result;
                Debug.Log("Successfuly created new user:" + newUser.DisplayName + " " + newUser.UserId);

                UserProfile profile = new UserProfile
                {
                    DisplayName = nickname
                };

                newUser.UpdateUserProfileAsync(profile).ContinueWith(task1 =>
                {
                    if (task1.IsCanceled)
                    {
                        Debug.LogError("cancelled");
                        return;
                    }
                    if (task1.IsFaulted)
                    {
                        Debug.LogError("Error: " + task1.Exception);
                        return;
                    }

                    Debug.Log("Successfully Updated: " + newUser.DisplayName);
                    signUp.SetActive(false);
                    sessions.SetActive(true);
                });
            });
        }
    }
}
