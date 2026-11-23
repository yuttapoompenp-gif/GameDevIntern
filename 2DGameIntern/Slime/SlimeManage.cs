using UnityEngine;

public class SlimeManage : MonoBehaviour
{

    [SerializeField] public GameObject bigSlime;
    [SerializeField] public GameObject mediumSlime;
    [SerializeField] public GameObject mediumSlime1;
    [SerializeField] public GameObject smallSlime;
    [SerializeField] public GameObject smallSlime1;
    [SerializeField] public GameObject smallSlime2;
    [SerializeField] public GameObject smallSlime3;

    bool checkActiveMediumSlime = false;
    bool checkActiveSmallSlime = false;
    bool checkActiveSmallSlime1 = false;


    private void Awake()
    {
        bigSlime.SetActive(true);
        mediumSlime.SetActive(false);
        mediumSlime1.SetActive(false);
        smallSlime.SetActive(false);
        smallSlime1.SetActive(false);
        smallSlime2.SetActive(false);
        smallSlime3.SetActive(false);
    }   

    private void Update()
    {
        split();

    }

    private void split()
    {

        if (!bigSlime.activeSelf &&  !checkActiveMediumSlime)
        {
            Vector3 spawnPos = bigSlime.transform.position;

            mediumSlime.SetActive(true);
            mediumSlime.transform.position = spawnPos + new Vector3(-1.0f, -1.3f, 0);

            mediumSlime1.SetActive(true);
            mediumSlime1.transform.position = spawnPos + new Vector3(1.0f, -1.3f, 0);

            checkActiveMediumSlime = true;
        }


        if (!bigSlime.activeSelf &&!mediumSlime.activeSelf && !checkActiveSmallSlime)
        {
            Vector3 spawnPos = mediumSlime.transform.position;

            smallSlime.SetActive(true);
            smallSlime1.SetActive(true);
            smallSlime.transform.position = spawnPos + new Vector3(-0.8f, -0.5f, 0);
            smallSlime1.transform.position = spawnPos + new Vector3(-0.85f, -0.5f, 0);

            checkActiveSmallSlime = true;

        }

        if (!bigSlime.activeSelf && !mediumSlime1.activeSelf && !checkActiveSmallSlime1)
        {
            Vector3 spawnPos = mediumSlime1.transform.position;

            smallSlime2.SetActive(true);
            smallSlime3.SetActive(true);
            smallSlime2.transform.position = spawnPos + new Vector3(0.8f, -0.5f, 0);
            smallSlime3.transform.position = spawnPos + new Vector3(0.85f, -0.5f, 0);

            checkActiveSmallSlime1 = true;
        }

    }

}
