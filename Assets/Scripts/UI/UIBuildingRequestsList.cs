using System.Collections.Generic;

public class UIBuildingRequestsList : UIList<BuildingRequest> 
{
    public void Instantiate(List<BuildingRequest> requests)
    {
        UpdateList(requests);
    }
}