using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Train train;
    [SerializeField] private EnemyTrain enemyTrain;
    [SerializeField] private GameObject trainGene;
    [SerializeField] private List<GameObject> genePoints = new List<GameObject>();

    private float geneTime;
    private float nextGeneTime;
    // Start is called before the first frame update
    void Start()
    {
        geneTime = 3f;
        nextGeneTime = geneTime;
    }

    // Update is called once per frame
    void Update()
    {
        trainGene.transform.position = new Vector3(trainGene.transform.position.x, trainGene.transform.position.y, train.transform.position.z + 30);
        
        geneTime -= Time.deltaTime;
        if (geneTime <= 0) {
            GeneTrain();

            //ŽŸ‚Ì¶¬ŽžŠÔ‚ð‘‚­‚·‚é
            nextGeneTime -= 0.1f;
            geneTime = nextGeneTime;
        }

        //ŽŸ‚Ì¶¬ŽžŠÔ‚ª0.1f‚ð‰º‰ñ‚ç‚È‚¢‚æ‚¤‚É
        if (nextGeneTime <= 1f) {
            nextGeneTime = 1f;
        }
    }

    private void GeneTrain() {
        int rand = Random.Range(0, genePoints.Count);
        EnemyTrain enemy = Instantiate(enemyTrain, genePoints[rand].transform.position, transform.rotation);
    }
}
