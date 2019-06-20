using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// add the UXF namespace
using UXF;

public class TaskManager : MonoBehaviour
{

    public UXF.Session session;

    public GameObject leftTarget;
    public GameObject rightTarget;
    public GameObject targetFulcrum;
    public int jon;

    public AudioClip StartExperimentSound;
    public AudioClip CountDownSound;
    public AudioClip TrialEndSound;
    public Trial trialref;

    /// <summary>
    /// generates the trials and blocks for the session
    /// </summary>
    /// <param name="experimentSession"></param>

    /// <summary>
    /// Example method presenting a stimulus to a user
    /// </summary>
    /// <param name="trial"></param>
    public void PresentStimulus(Trial trial)
    {
        // we can call this function via the event "On Trial Begin", which is called when the trial starts
        // here we can imagine presentation of some stimulus
        session = trial.session;
        Debug.Log(string.Format("Start trial {0}", trial.number));

        // we can access our settings to (e.g.) modify our scene
        // for more information about retrieving settings see the documentation

        //float size = trial.settings.GetFloat("size");
        //Debug.LogFormat("The 'size' for this trial is: {0}", size);

        // record custom values...
        //string observation = UnityEngine.Random.value.ToString();
        //Debug.Log(string.Format("We observed: {0}", observation));
        //trial.result["some_variable"] = observation;

        // end trial and prepare next trial in 1 second
        Invoke("EndAndPrepare", 15);
    }

    public void EndAndPrepare()
    {
        Debug.Log(string.Format("Ending trial {0}",session.CurrentTrial));
        session.CurrentTrial.End();
        if (session.CurrentTrial == session.LastTrial)
        {
            session.End();
        }
        else
        {
            SetupTrial();
            GetComponent<AudioSource>().PlayOneShot(TrialEndSound);
        }

    }

    public void firstTrialHandler (Session sess)
    {
        Trial curTrial = sess.CurrentTrial;
        //SetupTrial(curTrial);
    }

    public void SetupTrial() 
    {
        Trial trial = session.NextTrial;
        double angle = trial.settings.GetDouble("angle");
        double targetSize = trial.settings.GetDouble("targetSize");

        targetFulcrum.transform.rotation = Quaternion.Euler(0, (float)angle, 0);
        leftTarget.transform.localScale.Set((float)targetSize,1,(float)targetSize);

        leftTarget.GetComponent<Renderer>().enabled = false;
        rightTarget.GetComponent<Renderer>().enabled = false;

        Invoke("StartTrialWarmup",3);
        GetComponent<AudioSource>().PlayOneShot(CountDownSound);

    }

    void StartTrialWarmup()
    { 
        leftTarget.GetComponent<Renderer>().enabled = true;
        rightTarget.GetComponent<Renderer>().enabled = true;

        Invoke("StartRecording", 5);
    }

    void StartRecording()
    {
        session.BeginNextTrial();
        Invoke("EndAndPrepare",15);
    }

}

