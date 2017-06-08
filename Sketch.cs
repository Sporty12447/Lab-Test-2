using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class Sketch : MonoBehaviour {
    public GameObject myPrefab;
    //string DataSeat = "RevenueTest2"; 
    // Put your URL here
    //public 
    string _WebsiteURL = "http://infomgmt192.azurewebsites.net/tables/RevenueTest2?zumo-api-version=2.0.0";

    void Start () {
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
//        string Dataseat = "treesurveyv3";
        string jsonResponse = Request.GET(_WebsiteURL);

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        RevenueTest2[] revenuetest = JsonReader.Deserialize<RevenueTest2[]>(jsonResponse);

        int totalCubes = revenuetest.Length;
        int totalDistance = 5;
        int i = 0;
        //We can now loop through the array of objects and access each object individually
        foreach (RevenueTest2 revenue in revenuetest)
        {
            //Example of how to use the object
            Debug.Log("This categories name is: " + revenue.ProductID);
            float perc = i / (float)totalCubes;
            i++;
            float x = perc * totalDistance;
            float y = 5.0f;
            float z = 0.0f;
            GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
            newCube.GetComponent<myCubeScript>().setSize((1.0f - perc) * 2);
            newCube.GetComponent<myCubeScript>().ratateSpeed = perc;
            newCube.GetComponentInChildren<TextMesh>().text = revenue.ProductID;
            newCube.GetComponent<Renderer>().material.color = Color.green;
            

        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
