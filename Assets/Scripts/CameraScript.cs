using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject hero;
    // Start is called before the first frame update
    void Update()
    {
        transform.position = new Vector3(hero.transform.position.x, hero.transform.position.y, -10f);
    }

}
