using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //drop down
    [SerializeField] public Dropdown algorithmDropdown;
    //text
    [SerializeField] public TextMeshProUGUI nodesText;
    [SerializeField] public TextMeshProUGUI runtimeText;
    //buttons
    public Button playRestartButton;
    public TextMeshProUGUI playRestartButtonText;

    private float elapsedTime = 0f;
    private int nodesSearched = 0;
    private List<Node> path = new List<Node>();

    private enum Algorithm
    {
        //dijsktra's, A*, DFS, BFS
        Dijkstra, AStar, DFS, BFS
    }

    private bool isPlaying = false;
    private Algorithm selectedAlgorithm;
    private Search search;
    private void Start()
    {
        search = FindObjectOfType<Search>();
        PopulateAlgorithmDropdown();
        playRestartButton.onClick.AddListener(PlayRestart);
        playRestartButtonText = playRestartButton.GetComponentInChildren<TextMeshProUGUI>();
        // Remove this line
        // algorithmDropdown = GetComponent<Dropdown>();

        // Set the time scale to 0 at the start to pause the game
        Time.timeScale = 0;
    }
    private void FixedUpdate()
    {
        
        if (isPlaying) 
        {
            elapsedTime += Time.deltaTime;
            
            //update the runtime and nodes searched
            UpdateElapsedTime(elapsedTime);
            UpdateNodesSearched(nodesSearched);
        }
    }

    private void PopulateAlgorithmDropdown()
    {
        List<string> algorithmNames = new List<string> { "Dijkstra's", "A* (heuristic search)", "DFS", "BFS" };
        algorithmDropdown.AddOptions(algorithmNames);
        algorithmDropdown.onValueChanged.AddListener(delegate { UpdateSelectedAlgorithm(); });
    }

    private void UpdateSelectedAlgorithm()
    {
        selectedAlgorithm = (Algorithm)algorithmDropdown.value;
    }

    private void PlayRestart()
    {
        if (isPlaying)
        {
            playRestartButtonText.text = "Play";
            Time.timeScale = 0;
            
            
        }
        else
        {
            playRestartButtonText.text = "Pause";
            Time.timeScale = 1;
        }

        isPlaying = !isPlaying;
        search.StartSearch(); 
    }




    private void UpdateElapsedTime(float elapsedTime)
    {
        runtimeText.text = $"Elapsed Time: {elapsedTime} seconds";
    }

    private void UpdateNodesSearched(int nodesSearched)
    {
        List<Node> reachedNodes = JJAgent.reachedNodes;
        string reachedNodeString = "";
        foreach (Node node in reachedNodes)
        {
            reachedNodeString += node.name + ", ";
        }
        nodesText.text = $"Nodes Searched: {reachedNodeString}";
    }


    //wait for search to finish coroutine
    IEnumerator WaitForSearchToFinish()
    {
        Debug.Log("Waiting for search to finish");

        while (search.isSearching)
        {
            Debug.Log("Waiting");
            //wait 1 second
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
    }
}