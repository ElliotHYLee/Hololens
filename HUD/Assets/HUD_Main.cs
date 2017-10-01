using UnityEngine;
using System;
using UnityEngine.UI;

public class HUD_Main : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Text txtText;

    public Color newColor;
    // Use this for initialization
    void Start()
    {
        // Grab the mesh renderer that's on the same object as this script.
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;
        var gazeDirection2 = Camera.main.transform.right;
        var gazeDirection3 = Camera.main.transform.up;

        Quaternion q1 = Quaternion.LookRotation(gazeDirection);
        Quaternion q2 = Quaternion.LookRotation(gazeDirection2);
        Quaternion q3 = Quaternion.LookRotation(gazeDirection3);

        //double[] myEuler = Quat2Euler (q1);
        //double[] myEuler2 = Quat2Euler(q2);
        double roll = (q2.eulerAngles.x);
        if (roll < 0) roll = roll - 360;
        //txtText.text = "blue axis quat: " + quatForPrint(q1) + "\n"
        //    + "blue axis Euler: " + q1.eulerAngles.ToString() + "\n"
        //    + "red ais quat: " + quatForPrint(q2) + "\n"
        //    + "red axis Euler: " + q2.eulerAngles.ToString() + "\n"
        //    + "gree ais quat: " + quatForPrint(q3) + "\n"
        //    + "green axis Euler: " + q3.eulerAngles.ToString() + "\n"
        //    + "So AAE conv. Euler: \n" +
        //    "pitch: " + (360 - q1.eulerAngles.x).ToString() + " \n" +
        //    "yaw: " + (q1.eulerAngles.y).ToString() + " \n" +
        //    "roll: " + roll.ToString();

        txtText.text = "pitch: " + (360 - q1.eulerAngles.x).ToString() + " \n" +
            "yaw: " + (q1.eulerAngles.y).ToString() + " \n" +
            "roll: " + roll.ToString();



        RaycastHit hitInfo;


        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram...
            // Display the cursor mesh.
            meshRenderer.enabled = true;

            // Move thecursor to the point where the raycast hit.
            this.transform.position = hitInfo.point;

            // Rotate the cursor to hug the surface of the hologram.
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            // If the raycast did not hit a hologram, hide the cursor mesh.
            meshRenderer.enabled = false;
        }
    }

    double[] Quat2Euler(Quaternion q)
    {

        double x = q.x;
        double y = q.y;
        double z = q.z;
        double w = q.w;

        double t0 = 2 * (w * x + y * z);
        double t1 = 1 - 2 * (x * x + y * y);
        double ex = Math.Atan2(t0, t1) * 180 / Math.PI;

        double t2 = 2 * (w * y - z * x);
        if (t2 > 1)
        {
            t2 = 1;
        }
        double ey = Math.Asin(t2) * 180 / Math.PI;

        double t3 = 2 * (x * w + y * z);
        double t4 = 1 - 2 * (z * z + w * w);
        double ez = Math.Atan2(t3, t4) * 180 / Math.PI;

        double[] result = { ex, ey, ez };
        return result;
    }

    String quatForPrint(Quaternion q)
    {
        String result = "";
        String w = Math.Round(q.w, 4).ToString();
        String x = Math.Round(q.x, 4).ToString();
        String y = Math.Round(q.y, 4).ToString();
        String z = Math.Round(q.z, 4).ToString();

        result = "(" + w + ", " + x + ", " + y + ", " + z + ")";
        return result;
    }
}