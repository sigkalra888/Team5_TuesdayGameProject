using UnityEngine;

namespace taoye
{
    public class Player : MonoBehaviour
    {
        public float speed;//speed

        private void Update()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * speed);

            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    switch (hitInfo.collider.tag)
                    {
                        case "Cube1":
                            UIManager.Instance.ShowDesPanel("Cube1");
                            break;
                        case "Cube2":
                            UIManager.Instance.ShowDesPanel("Cube2");
                            break;
                        case "Cube3":
                            UIManager.Instance.ShowDesPanel("Cube3");
                            break;
                        case "Cube4":
                            UIManager.Instance.ShowDesPanel("Cube4");
                            break;
                        case "Cube5":
                            UIManager.Instance.ShowDesPanel("Cube5");
                            break;
                    }
                }
            }
        }
    }

}

