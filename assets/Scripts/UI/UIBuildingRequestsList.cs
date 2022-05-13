using System.Collections.Generic;

public class UIBuildingRequestsList : UIList<BuildingRequest> 
{
    public void Instantiate(Building forBuilding)
    {
        List<BuildingRequest> _buildingRequests = new List<BuildingRequest>()
        {
            new BuildingRequest(forBuilding, BuildingRequestType.ChangeConfig),
            new BuildingRequest(forBuilding, BuildingRequestType.CopyConfig),
            new BuildingRequest(forBuilding, BuildingRequestType.PasteConfig),
            new BuildingRequest(forBuilding, BuildingRequestType.RemoveBuilding)
        };

        UpdateList(_buildingRequests);
    }
}