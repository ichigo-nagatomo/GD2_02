using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Train train;
    [SerializeField] private EnemyTrain enemyTrain;
    [SerializeField] private Station stations;
    [SerializeField] private GameObject trainGene;
    [SerializeField] private List<GameObject> genePoints = new List<GameObject>();

    private float geneTime;
    private float nextGeneTime;

    private float stationGeneTime;

    private int nowScore;
    // Start is called before the first frame update
    void Start()
    {
        geneTime = 3f;
        nextGeneTime = geneTime;
        stationGeneTime = 10f;
        nowScore = 0;

    }

    // Update is called once per frame
    void Update()
    {
        trainGene.transform.position = new Vector3(trainGene.transform.position.x, trainGene.transform.position.y, train.transform.position.z + 30);
        
        geneTime -= Time.deltaTime;
        if (geneTime <= 0) {
            GeneTrain();
            geneTime = nextGeneTime;
        }

        if (train.GetHitStation()) {
            //ŽŸ‚Ì¶¬ŽžŠÔ‚ð‘‚­‚·‚é
            nextGeneTime -= 0.5f;
        }

        //ŽŸ‚Ì¶¬ŽžŠÔ‚ª0.1f‚ð‰º‰ñ‚ç‚È‚¢‚æ‚¤‚É
        if (nextGeneTime <= 1f) {
            nextGeneTime = 1f;
        }

        //‰w‚Ì¶¬
        stationGeneTime -= Time.deltaTime;
        if (stationGeneTime <= 0) {
            GeneStation();

            stationGeneTime = 10f;
        }

        //ƒvƒŒƒCƒ„[‚ª‰w‚É’…‚¢‚½‚ç
        if (train.GetHitStation()) {
            nowScore += train.GetPassenger();
            train.SetPassenger(0);
            train.SetHitStation(false);
        }
    }

    private void GeneTrain() {
        int rand = Random.Range(0, genePoints.Count);
        EnemyTrain enemy = Instantiate(enemyTrain, genePoints[rand].transform.position, transform.rotation);
        enemy.SetTrain(train);
    }

    private void GeneStation() {
        int rand = Random.Range(0, genePoints.Count);
        Station station = Instantiate(stations, genePoints[rand].transform.position, transform.rotation);
    }

    public int GetScore() {
        return nowScore;
    }
}
