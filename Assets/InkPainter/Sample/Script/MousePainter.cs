using UnityEngine;

namespace Es.InkPainter.Sample
{
	public class MousePainter : MonoBehaviour
	{
        public GameObject Player1, Player2;
		/// <summary>
		/// Types of methods used to paint.
		/// </summary>
		[System.Serializable]
		private enum UseMethodType
		{
			RaycastHitInfo,
			WorldPoint,
			NearestSurfacePoint,
			DirectUV,
		}

		[SerializeField]
		private Brush brush1,brush2;

		[SerializeField]
		private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;

		[SerializeField]
		bool erase = false;

        bool turnP1 = true;
		private void Update()
		{
            if (turnP1)
            {
                //Direction to Paint WALL
                int directionP1 = 1;
                if (Player1.transform.eulerAngles.y == 90)
                {
                    directionP1 = -1;
                }
                if (Player1.transform.eulerAngles.y == 270)
                {
                    directionP1 = 1;
                }

                //Paint Wall
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool success = true;
                RaycastHit hitInfo;
                if (Physics.Raycast(Player1.transform.position, directionP1 * Player1.transform.right, out hitInfo))
                {

                    var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
                    if (paintObject != null)
                    {
                        switch (useMethodType)
                        {
                            case UseMethodType.RaycastHitInfo:
                                success = erase ? paintObject.Erase(brush1, hitInfo) : paintObject.Paint(brush1, hitInfo);
                                break;

                            case UseMethodType.WorldPoint:
                                success = erase ? paintObject.Erase(brush1, hitInfo.point) : paintObject.Paint(brush1, hitInfo.point);
                                break;

                            case UseMethodType.NearestSurfacePoint:
                                success = erase ? paintObject.EraseNearestTriangleSurface(brush1, hitInfo.point) : paintObject.PaintNearestTriangleSurface(brush1, hitInfo.point);
                                break;

                            case UseMethodType.DirectUV:
                                if (!(hitInfo.collider is MeshCollider))
                                    Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
                                success = erase ? paintObject.EraseUVDirect(brush1, hitInfo.textureCoord) : paintObject.PaintUVDirect(brush1, hitInfo.textureCoord);
                                break;
                        }
                    }

                    if (!success)
                        Debug.LogError("Failed to paint.");
                }

                turnP1 = false;
                return;
            }
            if (!turnP1) { 

            int directionP2 = 1;
            if (Player2.transform.eulerAngles.y == 90)
            {
                directionP2 = -1;
            }
            if (Player2.transform.eulerAngles.y == 270)
            {
                directionP2 = 1;
            }

            //Paint Wall
            var ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool success2 = true;
            RaycastHit hitInfo2;
                if (Physics.Raycast(Player2.transform.position, directionP2 * Player2.transform.right, out hitInfo2))
                {

                    var paintObject = hitInfo2.transform.GetComponent<InkCanvas>();
                    if (paintObject != null)
                    {
                        switch (useMethodType)
                        {
                            case UseMethodType.RaycastHitInfo:
                                success2 = erase ? paintObject.Erase(brush2, hitInfo2) : paintObject.Paint(brush2, hitInfo2);
                                break;

                            case UseMethodType.WorldPoint:
                                success2 = erase ? paintObject.Erase(brush2, hitInfo2.point) : paintObject.Paint(brush2, hitInfo2.point);
                                break;

                            case UseMethodType.NearestSurfacePoint:
                                success2 = erase ? paintObject.EraseNearestTriangleSurface(brush2, hitInfo2.point) : paintObject.PaintNearestTriangleSurface(brush2, hitInfo2.point);
                                break;

                            case UseMethodType.DirectUV:
                                if (!(hitInfo2.collider is MeshCollider))
                                    Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
                                success2 = erase ? paintObject.EraseUVDirect(brush2, hitInfo2.textureCoord) : paintObject.PaintUVDirect(brush2, hitInfo2.textureCoord);
                                break;
                        }
                    }
                    turnP1 = true;
                    return;
                }

                if (!success2)
                    Debug.LogError("Failed to paint.");
            }


        }

        public void OnGUI()
		{
			if(GUILayout.Button("Reset"))
			{
				foreach(var canvas in FindObjectsOfType<InkCanvas>())
					canvas.ResetPaint();
			}
		}
	}
}