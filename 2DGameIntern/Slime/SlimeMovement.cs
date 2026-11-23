using UnityEngine;

public class SlimeMovement : MonoBehaviour
{

    [SerializeField] public GameObject bigSlime;
    [SerializeField] public GameObject mediumSlime;
    [SerializeField] public GameObject mediumSlime1;
    [SerializeField] public GameObject smallSlime;
    [SerializeField] public GameObject smallSlime1;
    [SerializeField] public GameObject smallSlime2;
    [SerializeField] public GameObject smallSlime3;
    [SerializeField] Transform player;





    private void Update()
    {
        SlimeMove();

    }


    private void SlimeMove()
    {


        if (Vector3.Distance(player.position, bigSlime.transform.position) <= 10f || Vector3.Distance(player.position, mediumSlime.transform.position) <= 10f || Vector3.Distance(player.position, mediumSlime1.transform.position) <= 10f || Vector3.Distance(player.position, smallSlime.transform.position) <= 10f || Vector3.Distance(player.position, smallSlime1.transform.position) <= 10f)
        {

            if (bigSlime != null && bigSlime.activeSelf) bigSlime.transform.position = Vector3.MoveTowards(bigSlime.transform.position, new Vector2(player.position.x, bigSlime.transform.position.y), 2f * Time.deltaTime);
            if (mediumSlime != null && mediumSlime.activeSelf) mediumSlime.transform.position = Vector3.MoveTowards(mediumSlime.transform.position, new Vector2(player.position.x, mediumSlime.transform.position.y), 2f * Time.deltaTime);
            if (mediumSlime1 != null && mediumSlime1.activeSelf) mediumSlime1.transform.position = Vector3.MoveTowards(mediumSlime1.transform.position, new Vector2(player.position.x, mediumSlime1.transform.position.y), 1f * Time.deltaTime);
            if (smallSlime != null && smallSlime.activeSelf) smallSlime.transform.position = Vector3.MoveTowards(smallSlime.transform.position, new Vector2(player.position.x, smallSlime.transform.position.y), 3f * Time.deltaTime);
            if (smallSlime1 != null && smallSlime1.activeSelf) smallSlime1.transform.position = Vector3.MoveTowards(smallSlime1.transform.position, new Vector2(player.position.x, smallSlime1.transform.position.y), 2.5f * Time.deltaTime);
            if (smallSlime2 != null && smallSlime2.activeSelf) smallSlime2.transform.position = Vector3.MoveTowards(smallSlime2.transform.position, new Vector2(player.position.x, smallSlime2.transform.position.y), 3.5f * Time.deltaTime);
            if (smallSlime3 != null && smallSlime3.activeSelf) smallSlime3.transform.position = Vector3.MoveTowards(smallSlime3.transform.position, new Vector2(player.position.x, smallSlime3.transform.position.y), 2f * Time.deltaTime);

            if (bigSlime != null) FacePlayer(bigSlime);
            if (mediumSlime != null && mediumSlime.activeSelf) FacePlayer(mediumSlime);
            if (mediumSlime1 != null && mediumSlime1.activeSelf) FacePlayer(mediumSlime1);
            if (smallSlime != null && smallSlime.activeSelf) FacePlayer(smallSlime);
            if (smallSlime1 != null && smallSlime1.activeSelf) FacePlayer(smallSlime1);
            if (smallSlime2 != null && smallSlime2.activeSelf) FacePlayer(smallSlime2);
            if (smallSlime3 != null && smallSlime3.activeSelf) FacePlayer(smallSlime3);

            
        }


    }

    private void FacePlayer(GameObject slimeObj)
    {
        Vector3 scale = slimeObj.transform.localScale;

        if (player.position.x > slimeObj.transform.position.x)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        else if (player.position.x < slimeObj.transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        slimeObj.transform.localScale = scale;
    }

    
}