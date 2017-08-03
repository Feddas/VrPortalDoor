using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [Tooltip("Where materials for the objects will be rendered")]
    public List<Renderer> StencilWalls;

    [Tooltip("Mask materials that will be applied to StencilWalls so that the activated room can be seen")]
    public List<Material> Stencils;

    [Tooltip("1-based index into Stencils of which room this portal goes to")]
    public int RoomTo;

    [Tooltip("1-based index into Stencils of which room this portal starts in")]
    public int RoomFrom;

    [Tooltip("Layers that can cause the portal to activate")]
    public LayerMask CanTriggerPortal;

    void Start()
    {
        // convert number from 1-based index to 0-based index
        RoomTo--;
        RoomFrom--;
    }

    void Update()
    {
    }

    void OnTriggerExit(Collider other)
    {
        // make sure the colliding object is allowed to trigger the portal
        if (CanTriggerPortal != (CanTriggerPortal | (1 << other.gameObject.layer)))
            return;

        // determine which side of the portal they exited on to determine which room they're goin in
        Vector3 toOther = (other.transform.position - this.transform.position).normalized;
        bool isGoingIn = Vector3.Dot(toOther, this.transform.forward) > 0;
        Debug.Log(other.gameObject.name + " isGoingIn=" + isGoingIn);

        // change materials to render the correct room
        Material nextRoomStencil = isGoingIn ? Stencils[RoomTo] : Stencils[RoomFrom];
        foreach (var wall in StencilWalls)
        {
            wall.material = nextRoomStencil;
        }
    }
}
